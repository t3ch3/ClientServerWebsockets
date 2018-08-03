using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Server
{
    public class Data
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("on")]
        public bool On { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
