using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.Entities
{
    public partial class Adres : BaseEntity, ICanSoftDelete, IContactable
    {

        //[Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        //[Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string FullAddress { get; set; }
        public string ZipCode { get; set; }
        //[Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureCountryId { get; set; }
        //[Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureCityId { get; set; }
        //[Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureDistrictId { get; set; }
        //[Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.InvalidValueError)]
        public IsLogic IsSending { get; set; }
        //[Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.InvalidValueError)]
        public IsLogic IsInvoice { get; set; }
        public int FeatureTableId { get; set; }
        public bool IsDeleted { get; set; }

        // for querying dont never remove 
        [NotMapped]
        public FeatureAdres AdresType { get; set; }
    }
}