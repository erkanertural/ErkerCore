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
    public class SMProductMultiCategory : AbstractServiceManager<FeatureValue, BaseView, BaseModel, ExtendNothing>
    {
        /// request.Id : Ürüne ait id değeri bu parametre ile verilir.
        /// request.RelatedPrimaryId : Ürün kategori bilgisine ait Id ( ViewFeature içinden geliyor bu Id) 
        public SMProductMultiCategory() {
            Target = Features.ProductMultiCategory; 
        
            AddCustomValidation(o => CreateRequest.Entity.RelatedPrimaryId < 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => CreateRequest.Entity.RelatedPrimaryId, Message.Helper.ValidationOperationType.Create);

            // todo: entity null geldiğinde ,  patlaması ve dil desteği ile ilgili olan kısmı idealize şekilde optimize edilecek.

        }
    }
}
