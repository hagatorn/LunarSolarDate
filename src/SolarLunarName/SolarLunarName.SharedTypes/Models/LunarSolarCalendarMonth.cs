using System;
using SolarLunarName.SharedTypes.Interfaces;

namespace SolarLunarName.SharedTypes.Models
{
    public class LunarSolarCalendarMonth : ILunarSolarCalendarMonth
    {
        public LunarSolarCalendarMonth(int Days, DateTime FirstDay){
            _days = Days;
            _firstDay = FirstDay;
        }
        
        public int Days => _days;
        public DateTime FirstDay => _firstDay;

        private readonly int _days;
        private readonly DateTime _firstDay;

    }

}