using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestSharpDemo.Model
{
    public class Location
    {
        [JsonProperty("post code")]
        public long PostCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("places")]
        public List<Places.Place> Places { get; set; }
    }
}