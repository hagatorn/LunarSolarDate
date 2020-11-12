using System;
using System.Collections.Generic;
using System.Linq;
using SolarLunarName.SharedTypes.Models;
using SolarLunarName.SharedTypes.Interfaces;
using System.IO;
using SolarLunarName.SharedTypes.Primitives;

namespace SolarLunarName.Standard.RestServices.Json
{

    public abstract class LunarCalendarClient : Client, ISolarLunarCalendarClient, ISolarLunarCalendarClientDetailed
    {
        protected abstract T StreamDeligate<T>(ValidYear year, ValidLunarMonth month, Func<Stream, T> method);
        protected override abstract T StreamDeligate<T>(ValidYear year, Func<Stream, T> method);

        public IList<ILunarSolarCalendarMonth> GetYearData(ValidYear year)
        {
            return StreamDeligate<IList<ILunarSolarCalendarMonth>>(
                year, 
                DeserializeList<LunarSolarCalendarMonth, ILunarSolarCalendarMonth>
            );
        }

        public ILunarSolarCalendarMonth GetMonthData(ValidYear year, ValidLunarMonth month)
        {

            try
            {   
                return StreamDeligate<LunarSolarCalendarMonth>(
                    year,
                    month,
                    Deserialize<LunarSolarCalendarMonth>
                );
            }
            catch (System.IO.DirectoryNotFoundException)
            {

                var monthsInYear = GetYear(year).Count();
                throw new SolarLunarName.SharedTypes.Exceptions.MonthDoesNotExistException(year, month, monthsInYear);
            }

        }

        public IList<DateTime> GetYear(ValidYear year){
                return GetYearData(year)
                    .Select(x=> x.FirstDay)
                    .ToList();
        }
        public IList<DateTime> GetYear(string year){
                return GetYear(year);
        }

        public IList<ILunarSolarCalendarMonthDetailed> GetYearDataDetailed(ValidYear year)
        {
            return StreamDeligate<IList<ILunarSolarCalendarMonthDetailed>>(
                    year,
                    DeserializeList<LunarSolarCalendarMonthDetailed, ILunarSolarCalendarMonthDetailed>
            );
        
        }

        public ILunarSolarCalendarMonthDetailed GetMonthDataDetailed(ValidYear year, ValidLunarMonth month)
        {
            return StreamDeligate<ILunarSolarCalendarMonthDetailed>(
                    year,
                    month,
                    Deserialize<LunarSolarCalendarMonthDetailed>
            );


        }

        
        public IList<ILunarSolarCalendarMonth> GetYearData(string year){
            return GetYearData((ValidYear)year);
        }
        public ILunarSolarCalendarMonth GetMonthData(int year, int month){
            return GetMonthData((ValidYear)year, (ValidLunarMonth)month);
        }

        public ILunarSolarCalendarMonthDetailed GetMonthDataDetailed(int year, int month) =>          
            GetMonthDataDetailed((ValidYear)year, (ValidLunarMonth)month);

        public IList<ILunarSolarCalendarMonthDetailed> GetYearDataDetailed(string year) =>
            GetYearDataDetailed((ValidYear)year);

    }

}