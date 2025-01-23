using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetDebt.Models
{
    public class MyTransaction
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0.00, 999999)]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TypeOfTransaction TypeOfTransaction { get; set; }
        
       
        
    }
    public enum TypeOfTransaction {Positive, Negative}
}
