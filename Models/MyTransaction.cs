using System.ComponentModel.DataAnnotations;

namespace SweetDebt.Models
{
    public class MyTransaction
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0.00, 999999.99)]
        public decimal Amount { get; set; }
        public DateTime Date { get;}
        public TypeOfTransaction TypeOfTransaction { get; set; }
        
        public MyTransaction ()
        {
            Date = DateTime.Now;
        }
        
    }
    public enum TypeOfTransaction {Positive, Negative}
}
