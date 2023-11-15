using System.ComponentModel.DataAnnotations;

namespace ParkyWeb.Models
{
    public class Trail
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Distance { get; set; }


        public enum DifficulityType { Easy, Moderate, Dificult, Expert }

        public DifficulityType difficulityType { get; set; }

        [Required]
        public int NationalParkId { get; set; }


        public NationalPark? NationalParkDto { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
