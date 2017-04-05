using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentDemo.Models
{
    public class SentimentOutputDoc
    {
        public List<SentimentOutput> documents { get; set; }

        public string[] errors { get; set; }
    }
}
