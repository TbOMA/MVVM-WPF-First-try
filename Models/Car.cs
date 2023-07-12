using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public decimal RentPrice { get; set; }
        public bool IsDamaged { get; set; }
        public string? DamageDescription { get; set; }
        public string? CarName { get; set; }
        public string? Description { get; set; }
        public string? CarImagePath { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
