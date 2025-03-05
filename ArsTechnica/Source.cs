using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArsTechnica
{
    internal class Source
    {
        public int Id { get; set; }
        public String Url { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public DateTime LastCrawl { get; set; }
    }
}
