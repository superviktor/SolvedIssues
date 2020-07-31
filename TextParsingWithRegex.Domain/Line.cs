using System.Collections.Generic;

namespace TextParsingWithRegex.Domain
{
    public class Line
    {
        public decimal Amount;
        public IEnumerable<Sequence> Sequences { get; set; }
    }
}
