using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.View
{
    public partial class ViewProduct : BaseView, ICanSoftDelete
    {
        [TestableEntity]
        public long ContactId { get; set; }
        public long SubscriberId { get; set; }
        public string Code { get; set; }
        public long FeatureProductTypeId { get; set; }
        public string ProductType { get; set; }
        public long FeatureDefaultCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public long FeatureBrandId { get; set; }
        public string ProductBrand { get; set; }
        public long FeatureModelId { get; set; }
        public string ProductModel { get; set; }
        public long FeatureVersionId { get; set; }
        public string ProductVersion { get; set; }
        public double BuyingPrice { get; set; }
        public double SellingPrice { get; set; }
        public double ExtValueTax { get; set; }
        public double ExtraPrice1 { get; set; }
        public double ExtraPrice2 { get; set; }
        public string Description { get; set; }
        public long FeatureMeasureUnitId { get; set; }
        public string FeatureMeasureUnit { get; set; }
        public long ProductImageFileDocumentId { get; set; }
        public int MaxStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public int MaxSellingQuantity { get; set; }
        public int MinSellingQuantity { get; set; }
        public int StockCount { get; set; }
        public int OrderCount { get; set; }
        public int InComingOrderCount { get; set; }
        public int OutGoingOrderCount { get; set; }
        public int ActiveServiceCount { get; set; }

        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }

        public bool IsDeleted { get; set; }
        public IsLogic IsSelling { get; set; }
        [NotMapped]
        public ProductStatus Status
        {

            get
            {
                return ProductStatus.Normal;
                if (StockCount < 1)
                    return ProductStatus.EmptyStock;
                else if (StockCount < MinStockQuantity)
                    return ProductStatus.CriticalStock;
                else if (IsSelling == IsLogic.No)
                    return ProductStatus.ClosedSelling;

            }
        }
        public string ImagePath { get; set; }
    }
}
