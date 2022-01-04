using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class Feature : BaseEntity,IContactable, IFeature
    {
        // T Model
        // 
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Key { get; set; }
        public long PrimaryId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        public long FeatureModuleId { get; set; }
        public long ParentId { get; set; }
        public long RelatedPrimaryId { get; set; }
        public long FeatureId { get; set; }
        public FeatureTable FeatureTableId { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Value { get; set; }
        public long NumericValue { get; set; }
        public string Description { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public override string Name { get; set; }
        public IsLogic IsExtendable { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        [JsonIgnore]
        public List<Feature> Childs { get; set; }

        public void SetValue()
        {
          
        }
    }

}