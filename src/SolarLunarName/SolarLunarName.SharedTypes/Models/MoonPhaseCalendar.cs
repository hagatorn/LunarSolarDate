using System.Collections.Immutable;

namespace SolarLunarName.SharedTypes.Models
{
    public class MoonPhaseCalendar
    {
        MoonPhaseCalendar(int Year, ImmutableList<Moon.MoonPhase> MoonPhaseList){
            _year = Year;
            _moonPhaseList = MoonPhaseList;
        }

        public int Year => _year;
        public ImmutableList<Moon.MoonPhase> MoonPhaseList => _moonPhaseList;   
        
        private readonly int _year;
        private readonly ImmutableList<Moon.MoonPhase> _moonPhaseList;

    }
}
