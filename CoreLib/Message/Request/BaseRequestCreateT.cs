using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using RitmaFlexPro.Library;

namespace RitmaFlexPro.Message.Request
{
    public class BaseRequestCreateT<T> : BaseRequestT<T> where T : IUnique
    {


        public BaseRequestEditT<T> ForEdit( bool ignoreValidation)
        {
       
            BaseRequestEditT<T> req= this.CloneGeneric<BaseRequestEditT<T>>();
            
            req.IgnoreValidation = ignoreValidation;
            return req;
        }
        public BaseRequestEditT<H> ForEditConvert<H>(H entityVM, bool ignoreValidation) where H: BaseEntity,IUnique
        {
           BaseRequestEditT<H> req= this.CloneGeneric<BaseRequestEditT<H>>();
            req.EntityVM = entityVM;
            req.IgnoreValidation = ignoreValidation;
            return req;
        } 
        public BaseRequestCreateT<H> ForCreateConvert<H>(H entityVM, bool ignoreValidation) where H: BaseEntity,IUnique
        {
           BaseRequestCreateT<H> req= this.CloneGeneric<BaseRequestCreateT<H>>();
            req.EntityVM = entityVM;
            req.IgnoreValidation = ignoreValidation;
            return req;
        }
    }

}