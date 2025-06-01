namespace Domain.Models.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem product, int quentity, decimal price)
        {
            Product = product;
            Quentity = quentity;
            Price = price;
        }

        public ProductInOrderItem Product { get; set; }
        public int Quentity { get; set; }
        public decimal Price { get; set; }
    }
}