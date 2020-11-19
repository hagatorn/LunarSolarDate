using System;

namespace SolarLunarName.SharedTypes.Interfaces
{
    public interface ILunarSolarCalendarMonth
    {
        int Days { get; }

        DateTime FirstDay { get; }
    }
}