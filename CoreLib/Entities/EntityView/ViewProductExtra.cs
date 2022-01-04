using ErkerCore.Entities;

namespace ErkerCore.Message.Request
{
    public class ViewProductExtra : BaseEntity
    {
        public double SellingPrice { get; set; }
        public double BuyingPrice { get; set; }
        public double DiscountPercent { get; set; }

        public double CalculatedPrice { get; set; }

        public double Quantity { get; set; }
    }
}