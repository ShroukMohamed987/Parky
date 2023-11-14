using System.ComponentModel.DataAnnotations;

namespace parky_project.API.Models
{
    public class NationalPark
    {
        [Key]
        public int id{ get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string State{ get; set; }= string.Empty;

        public DateTime Created { get; set; }=DateTime.Now;
        public DateTime Established { get; set; }= DateTime.Now;
    }
}
