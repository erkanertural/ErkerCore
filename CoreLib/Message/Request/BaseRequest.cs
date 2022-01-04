using ErkerCore.Library.Enums;
using ErkerCore.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ErkerCore.Library;
using System.Linq.Expressions;

namespace ErkerCore.Message.Request
{
    public class BaseRequest
    {
        public BaseRequest()
        {
            DateTimeStart = new DateTime().DefaultDateTime();
            DateTimeEnd = new DateTime().DefaultDateTime();
            UserId = -1;
            Id = -1;

            FlexAction = FeatureFlexAction.Uncertain;
            ValueNumber = -1;
            LangCode = FeatureLanguage.Uncertain;
            IsLogic = IsLogic.Uncertain;
            OwnerId = -1;
            FeatureId = -1;
            KeyValue = new Dictionary<object, object>();
            UploadedFiles = new Dictionary<string, object>();
            Module = FeatureModule.Uncertain;
            SaveExtend = true;

        }
        public bool IncludeDeletedRecord { get; set; }
        public bool AllRecord { get; set; }
        public Dictionary<string, object> UploadedFiles { get; set; }
        public FeatureTable Table { get; set; }
        public FeatureModule Module { get; set; }
        public string FieldName { get; set; }

        public virtual long Id { get; set; }
        public long FeatureId { get; set; }
        public long OwnerId { get; set; }

        public long UserId { get; set; }
        public FeatureFlexAction FlexAction { get; set; }
        public string UserName { get; set; }
        public string Value { get; set; }
        public long ValueNumber { get; set; }

        public FeatureLanguage LangCode { get; set; }

        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public IsLogic IsLogic { get; set; }


        public Dictionary<object, object> KeyValue { get; set; }
        public string Description { get; set; }

        //  public Expression<Func<object, bool>> Expression { get; set; }
        public PaginationInfo Pagination { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public bool SaveExtend { get; set; }
        public bool IncludeModel { get; set; }

        public object ExprFunc { get; set; }
        public object ExprModel { get; set; }
        public BaseRequestT<T> ToGeneric<T>()
        {

            return this.CloneGeneric<BaseRequestT<T>>();
        }
    }
}