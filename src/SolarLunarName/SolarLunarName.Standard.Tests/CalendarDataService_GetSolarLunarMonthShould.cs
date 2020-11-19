using System;
using Xunit;
using SolarLunarName.Standard.ApplicationServices;
using SolarLunarName.SharedTypes.Models;

namespace SolarLunarName.Standard.Tests
{
    public class CalendarDataService_GetSolarLunarMonthShould
    {   
        private readonly DetailedCalendarDataService _calendarDataService;

        public CalendarDataService_GetSolarLunarMonthShould(){
            var client = new Standard.RestServices.LocalJson.LunarCalendarClient(Paths.Local.DetailedcalendarApi);
            _calendarDataService = new DetailedCalendarDataService(client);
        }

        [Fact]
        public void GetSolarLunarMonthShould_InputIs1700_ReturnCorrectMonthData()
        {
            var month = _calendarDataService.GetSolarLunarMonth(1700,0);

            var monthLiteral = new LunarSolarCalendarMonth(19, DateTime.Parse("1700-01-01T00:00:00"));

            var result =  month.FirstDay == monthLiteral.FirstDay && month.Days == monthLiteral.Days;

            Assert.True(result, $"FirstDay {month.FirstDay} == {monthLiteral.FirstDay}");
        }
    }
    
}