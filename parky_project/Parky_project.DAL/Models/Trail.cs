using parky_project.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.DAL.Models
{
    public class Trail
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Distance { get; set; }

        public enum DifficulityType { Easy, Moderate , Dificult, Expert }

        public DifficulityType difficulityType { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; } = new NationalPark();


    }
}
