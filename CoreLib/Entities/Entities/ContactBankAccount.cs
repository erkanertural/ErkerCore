using RitmaFlexPro.Entities;
using RitmaFlexPro.Message.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitmaFlexPro.Entities
{
    public class ContactBankAccount : BaseEntity,IContactable
    {

        
        [Validate(ValidationTypeEnum.IdCheck, Library.FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, Library.FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureBankId { get; set; }


        [NotMapped]
        public override string Name { get; set; }
   

    }
}