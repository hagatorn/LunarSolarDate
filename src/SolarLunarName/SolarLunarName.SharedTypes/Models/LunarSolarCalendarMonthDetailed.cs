using System;
using System.Collections.Generic;
using SolarLunarName.SharedTypes.Interfaces;

namespace SolarLunarName.SharedTypes.Models
{
    public class LunarSolarCalendarMonthDetailed : ILunarSolarCalendarMonth, ILunarSolarCalendarMonthDetailed
    {
        public LunarSolarCalendarMonthDetailed(int Days, int Month, DateTime FirstDay, IList<MoonPhaseEntity> Phases)
        {
            _days = Days;
            _month = Month;
            _firstDay = FirstDay;
            _phases = Phases; 
        }

        public int Days => _days;
        public int Month => _month;
        public DateTime FirstDay => _firstDay;
        public IList<MoonPhaseEntity> Phases => _phases;

        private readonly int _days;
        private readonly int _month;
        private readonly DateTime _firstDay;
        private readonly IList<MoonPhaseEntity> _phases;
    }
}
