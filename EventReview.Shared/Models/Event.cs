using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReview.Shared.Models
{
    public class Event
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageBase64String { get; set; }

        public Review[] Reviews { get; set; }
    }
}
