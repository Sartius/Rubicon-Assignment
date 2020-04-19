using System;
using System.Collections.Generic;

namespace BlogModelsDTO
{
    public class BlogPost
    {
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public List<string> taglist { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}
