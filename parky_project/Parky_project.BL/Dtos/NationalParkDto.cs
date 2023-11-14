using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.DAL.Dtos
{
    public class NationalParkDto
    {
        
        
        public string Name { get; set; } = string.Empty;
       
        public string State { get; set; } = string.Empty;

        public DateTime Created { get; set; }=DateTime.Now;
        public DateTime Established { get; set; }= DateTime.Now;
    }
}
