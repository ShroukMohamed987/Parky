namespace ParkyWeb.Models
{
    public class NationalPark
    {
        public int id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Established { get; set; } = DateTime.Now;
    }
}
