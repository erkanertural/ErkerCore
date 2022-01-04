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

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductCategory : AbstractServiceManager<FeatureValue, ViewFeatureProductCategory, BaseModel, ExtendNothing>
    {
        public SMProductCategory() { Target = Features.ProductCategory; }
      
        public override Response<List<FeatureValue>> GetTreeview(BaseRequest request)
        {
            request.ValueNumber = 5;
            return base.GetTreeview(request);
        }
    }
}
