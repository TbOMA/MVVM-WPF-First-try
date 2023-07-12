using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Models
{
    public enum OrderStatus
    {
        Confirm,
        Reject,
        IsProcessed
    }
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CarID { get; set; }
        public Car? Car { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? RejectionReason { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan RentLength => EndTime.Subtract(StartTime);
    }
}
