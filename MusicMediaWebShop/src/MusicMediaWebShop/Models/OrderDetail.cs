namespace MusicMediaWebShop.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public Product Product { get; set; }
        public int Quanlity { get; set; }
        //public Order Order { get; set; }

        public decimal Cost
        {
            get
            {
                return Product.Price;
            }
        }
        public decimal Subtotal
        {
            get
            {
                return Quanlity * Product.Price;
            }
        }
    }
}