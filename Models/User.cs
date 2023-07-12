using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PassportNumber { get; set; }
        public decimal Balance { get; set; }
        
        public ICollection<Order> ClientOrders { get; set; }
    }
}
