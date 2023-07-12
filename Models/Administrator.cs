using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
