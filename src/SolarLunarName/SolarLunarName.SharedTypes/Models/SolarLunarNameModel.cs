using System;

namespace SolarLunarName.SharedTypes.Models
{
    public class SolarLunarNameModel: SolarLunarNameSimple
    {   
        
        public SolarLunarNameModel(DateTime SolarDateTime, int Year, int LunarMonth, int LunarDay)
            :base(Year, LunarMonth, LunarDay)
        {
            Validation.Helpers.ValidateYear(SolarDateTime.Year);
            _solarDateTime = SolarDateTime;
        }

        private readonly DateTime _solarDateTime;
        
        public DateTime SolarDateTime {

            get {
                return _solarDateTime ;
                }
        }
    }


}