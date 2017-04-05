using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentDemo.Models
{
    public class SentimentOutput
    {
        public string score { get; set; }
        public string[] keyPhrases { get; set; }
        public string id { get; set; }
    }
}
