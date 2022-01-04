using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class ContactPerson : BaseEntity, ICanSoftDelete,IContactable,IFormatableValue 
    {
        public ContactPerson()
        {
           
        }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string MobilePhone { get; set; }

        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        public long ImageFileDocumentId { get; set; }
        public long AdresId { get; set; }
        public bool IsDeleted { get; set; }
        public IsLogic IsOutSource { get; set; }
        public string Value { get ; set ; }

    }
}
