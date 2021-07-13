using System;

namespace DalPerformance.Api
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreationTime { get; set; }
        public int Rating { get; set; }
    }
}
