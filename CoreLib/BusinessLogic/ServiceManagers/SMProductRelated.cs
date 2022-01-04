using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System;
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductRelated : AbstractServiceManager<FeatureValue, BaseView, BaseModel, ExtendNothing>
    {
        public SMProductRelated()
        {
            Target = Features.ProductRelated;

            AddCustomValidation(o => CreateRequest.Entity.NumericValue < 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => CreateRequest.Entity.NumericValue, Message.Helper.ValidationOperationType.Create);
            AddCustomValidation(o => RequestGet.FeatureId < 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => CreateRequest.Entity.NumericValue, Message.Helper.ValidationOperationType.Get);
        }
    }
}
