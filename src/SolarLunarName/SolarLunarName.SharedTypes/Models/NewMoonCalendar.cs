using System;
using System.Collections.Immutable;
using SolarLunarName.SharedTypes.Validation;

namespace SolarLunarName.SharedTypes.Models
{
    class NewMoonCalendar
    {   
        
        private readonly ImmutableList<DateTime> _newMoonList;
        private readonly int _year;

        NewMoonCalendar(int Year, ImmutableList<DateTime> NewMoonList){
            _newMoonList = NewMoonList;

            Helpers.ValidateYear(Year);
            _year = Year;

        }
        
        public int Year => _year;
        public ImmutableList<DateTime> NewMoonList => _newMoonList;

    }
}
