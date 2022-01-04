using ErkerCore.Library.Enums;
using ErkerCore.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ErkerCore.Message.Helper;

namespace ErkerCore.Message.Request
{

    public class BaseRequestT<EntitY, VieW, ModeL, ExtenD> : BaseRequest
    {
        /// <summary>
        /// Model , View , Entity nesnesi controller'a gönderilebilir.
        /// </summary>
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.ObjectCannotBeNull, null)]
        public EntitY Entity { get; set; }
        public VieW View { get; set; }
        public ModeL Model { get; set; }
        public ExtenD Extend { get; set; }

        public BaseRequestT(BaseRequest request)
        {
            this.FeatureId = request.FeatureId;
            this.Id = request.Id;
            this.UserId = request.UserId;
            this.OwnerId = request.OwnerId;
            this.LangCode = request.LangCode;
            this.Table = request.Table;

        }
        public BaseRequestT()
        {

        }
    }
    public class BaseRequestT<T> : BaseRequest
    {
        public BaseRequestT(BaseRequest request)
        {
            this.FeatureId = request.FeatureId;
            this.Id = request.Id;
            this.UserId = request.UserId;
            this.OwnerId = request.OwnerId;
            this.LangCode = request.LangCode;
            this.Table = request.Table;
        }
        public BaseRequestT()
        {

        }
        /// <summary>
        /// Model , View , Entity nesnesi controller'a gönderilebilir.
        /// </summary>
     //   [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.InvalidValueError, null)]
        public T Entity { get; set; }

    }
}