namespace DurableMonitor.Models
{
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{City} of {Country}";
        }
    }
}