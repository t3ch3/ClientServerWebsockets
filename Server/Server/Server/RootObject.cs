using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Server
{
    public class RootObject
    {
        [JsonProperty("data")]
        public Data Data = new Data();
    }
}
