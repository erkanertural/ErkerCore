using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using RitmaFlexPro.Library;

namespace RitmaFlexPro.Message.Request
{
    public class BaseRequestEditT<T> : BaseRequestT<T> where T : IUnique
    {
       

      

        public BaseRequestCreateT<T> ForCreate(bool ignoreValidation)
        {

            BaseRequestCreateT<T> req = this.CloneGeneric<BaseRequestCreateT<T>>();

            req.IgnoreValidation = ignoreValidation;
            return req;
        }
        public BaseRequestCreateT<H> ForCreateConvert<H>(H entityVM, bool ignoreValidation) where H : BaseEntity, IUnique
        {
            BaseRequestCreateT<H> req = this.CloneGeneric<BaseRequestCreateT<H>>();
            req.EntityVM = entityVM;
            req.IgnoreValidation = ignoreValidation;
            return req;
        }
        public BaseRequestEditT<H> ForEditConvert<H>(H entityVM, bool ignoreValidation) where H : BaseEntity, IUnique
        {
            BaseRequestEditT<H> req = this.CloneGeneric<BaseRequestEditT<H>>();
            req.EntityVM = entityVM;
            req.IgnoreValidation = ignoreValidation;
            return req;
        }
       
    }
}