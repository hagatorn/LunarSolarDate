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
        protected abstract bool ExpectedExceptionPredicate(Exception e); 

        public IList<ILunarSolarCalendarMonth> GetYearData(ValidYear year)
        {
            return StreamDeligate<IList<ILunarSolarCalendarMonth>>(
                year, 
                DeserializeList<LunarSolarCalendarMonth, ILunarSolarCalendarMonth>
            );
        }

        public ILunarSolarCalendarMonth GetMonthData(ValidYear year, ValidLunarMonth month)
        {

            return GetMonthData<LunarSolarCalendarMonth, ILunarSolarCalendarMonth>(year, month);

        }

        public IList<DateTime> GetYear(ValidYear year){

                return GetYearData(year)
                    .Select(x=> x.FirstDay)
                    .ToList();

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

            return GetMonthData<LunarSolarCalendarMonthDetailed, ILunarSolarCalendarMonthDetailed>(year, month);

        }
        
        // TODO Delete method in version 1.0.0
        [Obsolete("This Overload is being deprecated in version 1.0.0 cast string to ValidYear")]
        public IList<DateTime> GetYear(string year){
                return GetYear(year);
        }

        // TODO Delete method in version 1.0.0
        [Obsolete("This Overload is being deprecated in version 1.0.0 cast string to ValidYear")]
        public IList<ILunarSolarCalendarMonth> GetYearData(string year){
            return GetYearData((ValidYear)year);
        }
        
        // TODO Delete method in version 1.0.0
        [Obsolete("This Overload is being deprecated in version 1.0.0 cast ints to ValidYear and ValidLunarMonth")]
        public ILunarSolarCalendarMonth GetMonthData(int year, int month){
            return GetMonthData((ValidYear)year, (ValidLunarMonth)month);
        }
        
        // TODO Delete method in version 1.0.0
        [Obsolete("This Overload is being deprecated in version 1.0.0 cast ints to ValidYear and ValidLunarMonth")]
        public ILunarSolarCalendarMonthDetailed GetMonthDataDetailed(int year, int month) =>          
            GetMonthDataDetailed((ValidYear)year, (ValidLunarMonth)month);

        // TODO Delete method in version 1.0.0
        [Obsolete("This Overload is being deprecated in version 1.0.0 cast string to ValidYear")]
        public IList<ILunarSolarCalendarMonthDetailed> GetYearDataDetailed(string year) =>
            GetYearDataDetailed((ValidYear)year);

        
        private I GetMonthData<T, I>(ValidYear year, ValidLunarMonth month) where T : I 
        {
            try
            {   
                return StreamDeligate<T>(
                                year,
                                month,
                                Deserialize<T>
                            );
            }
            catch (Exception e) when (ExpectedExceptionPredicate(e))
            {

                var monthsInYear = GetYear(year).Count();
                throw new SolarLunarName.SharedTypes.Exceptions.MonthDoesNotExistException(year, month, monthsInYear);
            }

        }

    }

}
