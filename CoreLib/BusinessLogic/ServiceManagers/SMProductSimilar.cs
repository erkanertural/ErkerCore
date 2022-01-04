using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Result;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductSimilar : AbstractServiceManager<FeatureValue, BaseView, BaseModel, ExtendNothing>
    {
        public SMProductSimilar()
        {
            Target = Features.ProductSimilar;

            //AddCustomValidation(o => CreateRequest.Entity.NumericValue < 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => CreateRequest.Entity.NumericValue, Message.Helper.ValidationOperationType.Create);
            //AddCustomValidation(o => GetRequest.FeatureId < 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => CreateRequest.Entity.NumericValue, Message.Helper.ValidationOperationType.Get);
        }
    }
}
