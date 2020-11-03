﻿using System.Management.Automation;
using SolarLunarName.Standard.ApplicationServices;

namespace SolarLunarName.PoSH
{
    [Cmdlet(VerbsCommon.Get, "SolarLunarCalendar", DefaultParameterSetName="year")]
    public class GetSolarLunarCalendar : PSCmdlet
    {   
        [Parameter(
            Position = 0, 
            Mandatory=true, 
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateYear()]
        public int Year { get; set; }

        [Parameter(
            ParameterSetName="month", 
            Position = 1, 
            Mandatory=true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateMonth()]
        public int Month  { get; set; }

        private CalendarDataService cds;

        protected override void BeginProcessing(){
            var client = new Standard.RestServices.RemoteJson.LunarCalendarClient();
            cds = new CalendarDataService(client);

        }

        protected override void ProcessRecord()
        {   

            if(this.ParameterSetName == "year")
            {   
                var data = cds.GetSolarLunarYear(Year);
                this.WriteObject(data);
            }
            else{

                var data = cds.GetSolarLunarMonth(Year, Month);
                this.WriteObject(data);
            }
            
            base.EndProcessing();
        }

    }
}
    
