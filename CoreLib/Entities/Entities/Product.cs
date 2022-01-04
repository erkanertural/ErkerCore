using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class Product : BaseEntity, ICanSoftDelete,IContactable
    {
   
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.InvalidValueError, "deger")]
        public string Code { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public FeatureProductType FeatureProductTypeId { get; set; } //71


        public long FeatureBrandId { get; set; }//-1
        public long FeatureModelId { get; set; }
        public long FeatureVersionId { get; set; }
        public long FeatureDefaultCategoryId { get; set; }
    
        public long FeatureMeasureUnitId { get; set; }
        [Validate(ValidationTypeEnum.MinValue, FeatureValidationErrorEnum.InvalidValueError,min:1)]
        public double BuyingPrice { get; set; }
        public double SellingPrice { get; set; }
        public double ExtValueTax { get; set; }
        public double ExtraPrice1 { get; set; }
        public double ExtraPrice2 { get; set; }
        public int MaxStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public int MaxSellingQuantity { get; set; }
        [Validate(ValidationTypeEnum.MaxValue, FeatureValidationErrorEnum.InvalidValueError, min:0)]
        public int MinSellingQuantity { get; set; }
        public long ProductImageFileDocumentId { get; set; }
        public string Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }

        public bool IsDeleted { get; set; }

        public IsLogic IsSelling { get; set; }
    }
}
