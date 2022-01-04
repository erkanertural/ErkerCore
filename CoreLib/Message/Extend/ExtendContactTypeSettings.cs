using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{


    public class ExtendContactTypeSettings : BaseModel
    {
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string IconName { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string ColorCode { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public override string Description { get; set; }
        public override string Name { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public override object Parent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public override object Value { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore] 
        public override long Id { get; set; }
    }
}



