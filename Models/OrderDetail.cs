namespace ReportPDF.Models
{
    public class OrderDetail
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
