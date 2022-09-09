namespace CSVHandler
{
    public class Book
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("satauri")]
        public string Title { get; set; }
        [DisplayName("fasi")]
        public decimal Price { get; set; }
        [DisplayName("raodenoba")]
        public int Quantity { get; set; }
    }
}
