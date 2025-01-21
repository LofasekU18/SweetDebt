namespace SweetDebt.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get;}
        public State State { get; set; }
        
        public Transaction ()
        {
            Date = DateTime.Now;
        }
        
    }
    public enum State {positive, negative}
}
