namespace SweetDebt.Models
{
    public class MyTransaction
    {
        public int Id { get; set; }
        public string? Description { get; set; }
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
