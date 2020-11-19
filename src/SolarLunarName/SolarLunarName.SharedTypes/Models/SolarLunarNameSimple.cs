using SolarLunarName.SharedTypes.Validation;

namespace SolarLunarName.SharedTypes.Models
{
    public class SolarLunarNameSimple
    {
        public SolarLunarNameSimple(int Year, int LunarMonth, int LunarDay)
        {   
            Helpers.ValidateYear(Year);
            _year = Year;

            Helpers.ValidateLunarMonth(LunarMonth);
            _lunarMonth = LunarMonth;

            Helpers.ValidateLunarDay(LunarDay);
            _lunarDay = LunarDay;
        
        }
        
        private readonly int _year;
        
        public int Year { 
            get {
                return _year ;
                }
        }
        private readonly int _lunarMonth;
        public int LunarMonth {    
            get {
                return _lunarMonth ;
                }
        }
        private readonly int _lunarDay;
        public int LunarDay {    
            get {
                return _lunarDay ;
                }
        }
        

    }



}