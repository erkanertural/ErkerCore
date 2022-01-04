using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using ErkerCore.View;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductType : AbstractServiceManager<FeatureValue, ViewFeatureProductType,BaseModel, ExtendProductType>
    {
        public SMProductType()
        {
            Target = Features.ProductType;
        }

    }


}

