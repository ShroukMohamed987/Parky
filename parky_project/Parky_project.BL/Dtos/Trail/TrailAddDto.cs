using parky_project.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Parky_project.DAL.Models.Trail;
using Parky_project.DAL.Dtos;

namespace Parky_project.BL.Dtos.Trail
{
    public class TrailAddDto
    {

        

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Distance { get; set; }


        public DifficulityType difficulityType { get; set; }

        [Required]
        public int NationalParkId { get; set; }


       // public NationalParkDto? NationalParkDto { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
