using System;
using System.Globalization;
using SolarLunarName.SharedTypes.Models;

namespace SolarLunarName.Standard.Types
{
    public class SolarLunarName : IComparable, IComparable<DateTime>, IComparable<SolarLunarName>, IEquatable<DateTime>, IEquatable<SolarLunarName>
    {
        public DateTime SolarDateTime { get; }
        public int Year { get; }
        public int LunarMonth { get; }
        public int LunarDay { get; }

        internal SolarLunarName(DateTime solarDateTime, int year, int lunarMonth, int lunarDay)
        {   
            // TODO this constructor does not guarantee a valid month 
            // Would Factory Pattern help?
            SolarDateTime = solarDateTime;
            Year = year;
            LunarMonth = lunarMonth;
            LunarDay = lunarDay;
        }

        public static explicit operator SolarLunarName(SolarLunarNameModel sln) => 
            new SolarLunarName(sln.SolarDateTime, sln.SolarDateTime.Year, sln.LunarMonth, sln.LunarDay);

        public int Subtract(SolarLunarName other){
            
            return this.SolarDateTime.Subtract(other.SolarDateTime).Days;
            
        }

        public static int Compare (SolarLunarName t1, SolarLunarName t2){

            return t1.CompareTo(t2);
        }

        public int CompareTo(object obj)
        {   
            if (obj == null) return 1;
            SolarLunarName other = obj as SolarLunarName;
            return this.CompareTo(other);
        }

        public int CompareTo(DateTime other)
        {
            if (other == null) return 1;
            return this.SolarDateTime.CompareTo(other);
        }

        public int CompareTo(SolarLunarName other)
        {   
            if (other == null) return 1;
            return this.SolarDateTime.CompareTo(other.SolarDateTime);
        }

        public bool Equals(DateTime other)
        {
            return other != null || this.SolarDateTime == other;
        }

        public bool Equals(SolarLunarName other)
        {
            return other != null || this.SolarDateTime == other.SolarDateTime;
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return SolarDateTime;
        }

        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }
        
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }
   
        public string ToString(string format, IFormatProvider provider) 
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider == null) provider = CultureInfo.CurrentCulture;
            
            switch (format.ToUpperInvariant())
            {   
                case "G":
                    return String.Format("{0}-{1}-{2}", Year.ToString(provider), LunarMonth.ToString(provider), LunarDay.ToString(provider));
                case "Y":
                    return Year.ToString(provider); 
                case "M":
                    return LunarMonth.ToString(provider);
                case "D":
                    return LunarDay.ToString(provider);
                default:
                    throw new FormatException(String.Format("The {0} format string is not supported.", format));
            }
        }
    }
}
