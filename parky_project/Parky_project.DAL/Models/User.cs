using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.DAL.Models
{
    public class User:IdentityUser
    {
        public string address { get; set; } = string.Empty;
    }
}
