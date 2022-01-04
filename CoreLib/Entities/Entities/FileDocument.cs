using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ErkerCore.Library.Enums;
using ErkerCore.Library;
using ErkerCore.Entities;
using System.Text.Json.Serialization;
using ErkerCore.Message.Helper;
using System;

namespace ErkerCore.Entities
{

    public class FileDocument:BaseEntity, ICanSoftDelete,IContactable ,IUnique
    { 
        [Validate( ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ContactId { get; set; }
        public string Path { get; set; }
        [JsonIgnore]
        public string Key { get; set; }
        [JsonIgnore]
        public string FileGuid { get; set; }

        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long TableId { get; set; }
        public long FileSize { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long PrimaryId { get; set; }
        public bool IsDeleted { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.InvalidValueError)]
        public FeatureFileType FileType { get; set; }
        public DateTime CreateDateTime { get; set; }

    }
}