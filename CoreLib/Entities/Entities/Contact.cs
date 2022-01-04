
using ErkerCore.Entities;
using ErkerCore;
using ErkerCore.Library;
using System.ComponentModel.DataAnnotations.Schema;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public partial class Contact : BaseEntity, ICanSoftDelete, IFormatableValue
    {
        public Contact()
        {

        }
        public string ShortName { get; set; }
        //[Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string CardCode { get; set; } // it should be unique
        public string FixedPhone { get; set; }
        public string MobilePhone1 { get; set; }
        public string MobilePhone2 { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string GeoLocation { get; set; }
        public long FeatureTaxOfficeId { get; set; }
        public string TaxNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string AccountCode { get; set; }
        public double DiscountRate { get; set; }
        public byte PayExpirationDay { get; set; }
        public long DebtLimit { get; set; }
        public long ProfileFileDocumentId { get; set; }
        public string CommercialRegisterNumber { get; set; }

        //[Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long SubscriberId { get; set; }
        public long DefaultAddressId { get; set; }
        public long DefaultBankAccountId { get; set; }
        public long DefaultSendingAddressId { get; set; }
        public long DefaultInvoiceAddressId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public FeatureContactType FeatureContactTypeId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureContactGroupId { get; set; }
        public long FeatureSectorId { get; set; }
        public long DefaultContactPersonId { get; set; }
        public long ParentContactId { get; set; }
        public long DefaultAuthorityPersonId { get; set; }

        public bool IsDeleted { get; set; }
        public long FeatureEmployeeCountId { get; set; }
        public string Value { get ; set ; }
    }


}
