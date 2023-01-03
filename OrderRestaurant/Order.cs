namespace OrderRestaurant
{
    public class Order
    {
        
        public string Item { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
