using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class ContactAddress : BaseEntity,IContactable
    {
    //    [NotMapped]
        public Adres Address { get; set; }
        
        public string AddressName { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long AddressId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }

        [NotMapped]
        public override string Name { get; set; }
    }
}