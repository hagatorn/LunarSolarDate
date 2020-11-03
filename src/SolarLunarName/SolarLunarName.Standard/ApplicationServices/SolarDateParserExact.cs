﻿using SolarLunarName.SharedTypes.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using SolarLunarName.SharedTypes.Exceptions;

namespace SolarLunarName.Standard.ApplicationServices
{
    public partial class  SolarDateParser
    {

        public DateTime ConvertSolarLunarNameExact(string date){

            var SolarLunarName = ParseSolarLunarName(date);

            return ConvertSolarLunarNameExact(SolarLunarName.Year, SolarLunarName.LunarMonth, SolarLunarName.LunarDay);
        }

        public DateTime ConvertSolarLunarNameExact(int year, int month, int day)
        {
       
            try{
                int daysInMonth = db.GetMonthData(year,month).Days;
                if( daysInMonth < day){
                    throw new DayDoesNotExistException(year, month, day, daysInMonth);
                };
    
                var startOfYear = new DateTime(year, 1, 1);

                DateTime newMoon = db.GetYearData(year.ToString())
                                .OrderBy(x => x.FirstDay)
                                .Where((_, index) => index == month )
                                .Select( x => x.FirstDay)
                                .First();

                var solarDateTime = newMoon.AddDays(day - 1);

                return new DateTime(solarDateTime.Year, solarDateTime.Month, solarDateTime.Day);
            }
            catch (System.IO.DirectoryNotFoundException) {
                
                // FIXME
                // Year or Month not Found in local json set.  
                // Would like to improve handling of this error to deeper into the codebase - may need contracts.
                // Will be a different error for each client.  Consider 
                throw new DateDoesNotExistException();

            }
            catch (DayDoesNotExistException e ){
                
                throw e;

            }
            
        }
        public SolarLunarNameSimple ParseSolarLunarName(string date){

            var rx = new Regex(@"(?<year>\d{4})-(?<month>\d{1,2})-(?<day>\d{1,2})", RegexOptions.Compiled);
            
            var match = rx.Match(date);
            if( match.Success == false ){
                throw new FormatException("Should be of format '{{Year}}-{{Month}}-{{Day}}' for example '1750-10-6'.  Year must be four digits and Month & Day one or two digits.");
            }
            var year = Int16.Parse(match.Groups["year"].Value);
            var month = Int16.Parse(match.Groups["month"].Value);
            var day = Int16.Parse(match.Groups["day"].Value);
            return new SolarLunarNameSimple(year, month, day);
        }
    }
}
