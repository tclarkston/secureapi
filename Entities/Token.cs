using System;
using Newtonsoft.Json;

namespace WebApi.Entities
{
    public class Token
    {
        [JsonProperty(PropertyName = "token")]
        public string Value { get; set; }

        [JsonProperty]
        public DateTime Exp { get; set; }

        [JsonProperty]
        public DateTime Nbf { get; set; }
    }
}
