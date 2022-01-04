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
using System.Linq;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductBrand : AbstractServiceManager<FeatureValue, ViewFeatureValue, BaseModel, ExtendNothing>
    {
        public SMProductBrand() { Target = Features.ProductBrand; }
 
        public override Response<List<FeatureValue>> GetTreeview(BaseRequest request)
        {
            List<FeatureValue> brand = ContextDataManager.GetList<FeatureValue>(o => o.FeatureId == Features.ProductBrand.ToInt64(), request);
            List<FeatureValue> model = ContextDataManager.GetList<FeatureValue>(o => o.FeatureId == Features.ProductModel.ToInt64(), request);
            List<FeatureValue> version = ContextDataManager.GetList<FeatureValue>(o => o.FeatureId == Features.ProductVersion.ToInt64(), request);
            brand.AddRange(model);
            brand.AddRange(version);
            request.ValueNumber = 5;
            var x = brand.Traverse(TraverseResult.ComboTreeview, request.ValueNumber.ToInt32());
            return Response<List<FeatureValue>>.Successful(x);

        }
        public override Response<ListPagination<BaseModel>> GetListModel(BaseRequestT<FeatureValue> request)
        {
            ValidationResult vlr = this.ValidationGetListModel(request);
            if (vlr.Success)
            {
                List<BaseModel> list = ContextDataManager.GetList<FeatureValue>(o => o.FeatureId == request.FeatureId && o.ParentId == request.Id, request).Select(o => new BaseModel { Id = o.Id, Name = o.Name }).ToList();
                return Response<ListPagination<BaseModel>>.Successful(list.ToPaginationList());
            }
            else
                return Response<ListPagination<BaseModel>>.Fail(vlr);


        }

        public override Response<ListPagination<ViewFeatureValue>> GetListView(BaseRequestT<ViewFeatureValue> request)
        {
            base.GetListView(request);
            ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request);
            if (vlr.Success)
            {
                if (request.FeatureId == -1)
                {
                    request.FeatureId = Features.ProductBrand.ToInt64();
                }
                List<ViewFeatureValue> list = ContextDataManager.GetList<ViewFeatureValue>(o => o.FeatureId == request.FeatureId && o.ParentId == request.Id, request).Select(o => new ViewFeatureValue { Id = o.Id, Name = o.Name }).ToList();
                return Response<ListPagination<ViewFeatureValue>>.Successful(list.ToPaginationList());
            }
            else
                return Response<ListPagination<ViewFeatureValue>>.Fail(vlr);
        }

        public override ValidationResult ValidationGetListModel(BaseRequestT<FeatureValue> request)
        {
            ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request);

            if (!(request.FeatureId == Features.ProductBrand.ToInt64() || request.FeatureId == Features.ProductModel.ToInt64() || request.FeatureId == Features.ProductVersion.ToInt64()))
            {
                return vlr.Failure(FeatureValidationErrorEnum.InvalidValueError, "Request.FeatureId");
            }

            return vlr.Successful();
        }
    }
}
