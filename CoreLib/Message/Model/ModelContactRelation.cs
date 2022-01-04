using ErkerCore.Library.Enums;
using ErkerCore.Entities;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Text.Json.Serialization;
using ErkerCore.Library;

namespace ErkerCore.Message.Model
{
    public class ModelContactRelation : BaseModel
    {
        public ModelContactRelation()
        {
      
        }
        [Validate( ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long CurrentContactId { get; set; }
        public long CurrentContactFVRelationId { get; set; }
        public string CurrentContactRelationName { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ConnectedContactId { get; set; }
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long ConnectedContactFVRelationId { get; set; }
        public string ConnectedContactName { get; set; }
        public string ConnectedContactRelationName { get; set; }


    }
}