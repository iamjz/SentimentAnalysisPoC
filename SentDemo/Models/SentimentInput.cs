using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SentDemo.Models
{
    public class SentimentInput
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }
}