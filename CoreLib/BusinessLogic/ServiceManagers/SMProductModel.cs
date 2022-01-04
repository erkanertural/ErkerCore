using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Result;
using System;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductModel : AbstractServiceManager<FeatureValue, BaseView, BaseModel, ExtendNothing>
    {
        public SMProductModel()
        {
            Target = Features.ProductModel;
        }
    }
}
