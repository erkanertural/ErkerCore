
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class ContactPersonAccount : BaseEntity,ICanSoftDelete
    {
        [NotMapped]
        public override string Name { get; set; }
        public string NameSurname { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string UserName { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Password { get; set; }
        public string MobilePhone1 { get; set; }
        public string MobilePhone2 { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactPersonId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long SubscriberId { get; set; }
        public string IpAddress { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public IsLogic IsMultiSession { get; set; }
        public string Days { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long AccountId { get; set; }
        public string Hours { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}