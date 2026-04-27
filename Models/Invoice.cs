namespace InvoiceAppBackend.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string CustomerName { get; set; }

        public ICollection<Item> InvoiceItems { get; set; }
    }
}
