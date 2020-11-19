using System;
using Xunit;
using SolarLunarName.Standard.ApplicationServices;
using SolarLunarName.Standard.RestServices.LocalJson;
using SolarLunarName.SharedTypes.Exceptions;

namespace SolarLunarName.Standard.Tests.SolarDateParserShould
{
    public class ConvertSolarLunarName
    {
        private SolarDateParser dp;

        public ConvertSolarLunarName()
        {

            dp = new SolarDateParser(new LunarCalendarClient(Paths.Local.CalendarApi));

        }

        private void TestTemplate(int year, int month, int day, int lunarMonth, int lunarDay)
        {
            var UtcDateTime = new DateTime(year, month, day);
            var remoteName = dp.ConvertSolarLunarName(year, lunarMonth, lunarDay);

            var result = remoteName == UtcDateTime;

            Assert.True(result, $"remoteName {remoteName} == {UtcDateTime}");

        }

        [Fact]
        public void ConvertSolarLunarNameShould_InputIs201954_Return08052019()
        {
            TestTemplate(2019, 5, 8, 5, 5);
        }

        [Fact]
        public void ConvertSolarLunarNameShould_InputIs175026_Return11021750()
        {
            TestTemplate(1750, 2, 11, 2, 6);
        }

        private void TestStringParserTemplate(string lunarDate, int year, int month, int day)
        {


            var UtcDateTime = new DateTime(year, month, day);

            var remoteName = dp.ConvertRemoteSolarLunarName(lunarDate);

            var result = remoteName == UtcDateTime;

            Assert.True(result, $"remoteName {remoteName} == {UtcDateTime}");

        }

        [Fact]
        public void ConvertRemoteSolarLunarNameShould_InputIs201955asString_Return08052019()
        {
            TestStringParserTemplate("2019-5-5", 2019, 5, 8);
        }

        [Fact]
        public void ConvertRemoteSolarLunarNameShould_InputIs175026asString_Return11021750()
        {
            TestStringParserTemplate("1750-2-6", 1750, 2, 11);
        }
        [Fact]
        public void ConvertSolarLunarNameExact_YearOutOfRangeException()
        {

            Assert.Throws<YearOutOfRangeException>(() => dp.ConvertSolarLunarNameExact(2084, 1, 1));
        }
        [Fact]
        public void ConvertSolarLunarNameExact_MonthOutOfRangeException()
        {

            Assert.Throws<MonthOutOfRangeException>(() => dp.ConvertSolarLunarNameExact(2019, 14, 31));
        }
        [Fact]
        public void ConvertSolarLunarNameExact_MonthDoesNotExistException()
        {

            Assert.Throws<MonthDoesNotExistException>(() => dp.ConvertSolarLunarNameExact(2020, 13, 31));
        }
        [Fact]
        public void ConvertSolarLunarNameExact_DayToBig_ThrowFormatException()
        {

            Assert.Throws<DayDoesNotExistException>(() => dp.ConvertSolarLunarNameExact(2019, 5, 31));
        }
        [Fact]
        public void ConvertSolarLunarName_NotThrowFormatException()
        {
            try
            {
                dp.ConvertSolarLunarName(2019, 5, 31);
            }
            catch (Exception ex)
            {
                throw new Exception("Expected no exception, but got: " + ex.Message);
            }

        }
    }

}