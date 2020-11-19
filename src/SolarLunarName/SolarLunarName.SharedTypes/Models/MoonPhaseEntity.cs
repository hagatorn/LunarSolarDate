using System;
using System.Text.Json.Serialization;

namespace SolarLunarName.SharedTypes.Models
{
    public class MoonPhaseEntity
    {
        public MoonPhaseEntity(DateTime Date, DateTime FirstDay, DateTime LastDay, Moon.MoonPhase Phase, int Days)
        {
            _date = Date;
            _firstDay = FirstDay;
            _lastDay = LastDay;
            _phase = Phase;
            _days = Days;
        }

            [JsonPropertyName("DateTime")]
            public DateTime Date => _date;
            public DateTime FirstDay => _firstDay;
            public DateTime LastDay => _lastDay;
            public Moon.MoonPhase Phase => _phase;
            public int Days => _days;

            private readonly DateTime _date;
            private readonly DateTime _firstDay;
            private readonly DateTime _lastDay;
            private readonly Moon.MoonPhase _phase;
            private readonly int _days;

    }
}