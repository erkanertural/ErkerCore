using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class FeatureValueTranslate : BaseEntity,IContactable
    {
        [NotMapped]
        public override string Name { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Key { get; set; }
        public long FeatureTableId { get; set; }
        public long PrimaryId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        public long FeatureValueId { get; set; }
        public long FeatureId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureTranslateKeyId { get; set; }
        public FeatureLanguage FeatureLanguageId { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        //public FeatureType FeatureType { get; set; }
        public string TranslatedText { get; set; }
    }
}