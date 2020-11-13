using System.Collections.Generic;
using SolarLunarName.SharedTypes.Interfaces;
using SolarLunarName.SharedTypes.Primitives;
using System.Net.Http;
using System;
using System.IO;

namespace SolarLunarName.Standard.RestServices.RemoteJson
{

    public class LunarCalendarClient : Json.LunarCalendarClient
    {   
        // TODO URI should not be in the compiled code.  
        // Need in a json file
        public LunarCalendarClient(HttpClient client, string baseUrl = "https://craigchamberlain.github.io/moon-data/api/lunar-solar-calendar-detailed/")
        {   
            _client = client;
            
            Helpers.TestUrl(_client, baseUrl);
            _baseUrl = baseUrl;
            
        }

        protected HttpClient _client;
        protected string _baseUrl;
        protected override bool ExpectedExceptionPredicate(Exception e) => 
            e.InnerException.GetType() == typeof(System.Net.Http.HttpRequestException); 


        protected override T StreamDeligate<T>(ValidYear year, ValidLunarMonth month, Func<Stream, T> method){
            
            Uri uri = Helpers.CombinePath(_baseUrl, year, month);
            return StreamDeligate<T>(uri, method);

        }
        protected override T StreamDeligate<T>(ValidYear year, Func<Stream, T> method){
            
            Uri uri = Helpers.CombinePath(_baseUrl, year);
            return StreamDeligate<T>(uri, method);

        }
        private T StreamDeligate<T>(Uri uri, Func<Stream, T> method){
            using (Stream s = _client.GetStreamAsync(uri).Result){
               return method(s);
            };

        }


    }

}
