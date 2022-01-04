using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductSupplier : AbstractServiceManager<FeatureValue, ViewFeatureProductSupplier, BaseModel, ExtendNothing>
    {
        public SMProductSupplier()
        {
           Target= Features.ProductSupplierData;
        
            AddCustomValidation(o => CreateRequest.Entity.PrimaryId < 0, FeatureValidationErrorEnum.InvalidValueError, o => CreateRequest.Entity.PrimaryId, Message.Helper.ValidationOperationType.Create);
            AddCustomValidation(o => CreateRequest.Entity.RelatedPrimaryId < 0, FeatureValidationErrorEnum.InvalidValueError, o => CreateRequest.Entity.RelatedPrimaryId, Message.Helper.ValidationOperationType.Create);
        }
    }
}
