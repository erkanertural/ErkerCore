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
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProduct : AbstractServiceManager<Product, ViewProduct, ModelContactProduct, ExtendNothing>
    {

        //Expression<Func<FeatureValueTranslate, bool>> expr = (o=> o.PrimaryId== request.Id && o.FeatureTableId == FeatureTable.Product.ToInt64() && o.FeatureId == Features.TranslateKey.ToInt64() && o.FeatureLanguageId == request.LangCode);
        //    m.ProductTranslate = new SMProductTranslate().GetList(new BaseRequestT<FeatureValueTranslate>() { ExprFunc = expr } ).Data;
        public override Response<ModelContactProduct> GetModel(BaseRequestT<Product> request)
        {
            try
            {

                ValidationResult vlr = this.ValidationGetModel(request);
                if (vlr.Success)
                {
                    SMFeatureValue smf = new SMFeatureValue();
                    ModelContactProduct m = new ModelContactProduct();
                    m.CurrentProduct = ContextDataManager.Get<ViewProduct>(o => o.Id == request.Id);
                    m.ProductCategoryList = ContextDataManager.GetList<FeatureValue>(o => o.PrimaryId == request.Id && o.FeatureId == Features.ProductMultiCategory.ToInt64(), request);
                    //List<FeatureValue> similarList2 = smf.GetList(new BaseRequestT<FeatureValue>(request) { FeatureId = Features.ProductSimilar.ToInt64(), Id=-1 }).Data;

                    List<FeatureValue> similarList = ContextDataManager.GetList<FeatureValue>(x => x.PrimaryId == request.Id && x.FeatureId == Features.ProductSimilar.ToInt64(), request);
                    m.SimilarProduct = ContextDataManager.GetList<ViewProduct>(o => similarList.Select(p => p.PrimaryId).Contains(o.Id), request);

                    List<FeatureValue> relatedList = smf.GetList(new BaseRequestT<FeatureValue>(request) { FeatureId = Features.ProductRelated.ToInt64(), Id = -1 }).Data;
                    m.RelatedProduct = ContextDataManager.GetList<ViewProduct>(o => relatedList.Select(p => p.RelatedPrimaryId).Contains(o.Id), request);
                    Expression<Func<ViewFeatureProductSupplier, bool>> exp = (x => x.PrimaryId == request.Id && x.FeatureId == Features.ProductSupplierData.ToInt64());
                    Response<ListPagination<ViewFeatureProductSupplier>> sp = new SMProductSupplier().GetListView(new BaseRequestT<ViewFeatureProductSupplier>() { Id = request.Id, OwnerId = request.OwnerId, ExprFunc = exp });
                    if (sp.Success)
                    {
                        Expression<Func<ViewContactDetail, bool>> expc = (x => sp.Data.Select(o => o.RelatedPrimaryId).ToList().Contains(x.Id));
                        Response<ListPagination<ViewContactDetail>> vcd = new SMContact().GetListView(new BaseRequestT<ViewContactDetail>(request) { ExprFunc = expc });
                        if (vcd.Success)
                        {
                            m.ProductSupplier = vcd.Data;
                        }
                        else
                            return Response<ModelContactProduct>.Fail(vcd.ValidationResult);
                    }
                    else
                        return Response<ModelContactProduct>.Fail(sp.ValidationResult);
                    return Response<ModelContactProduct>.Successful(m);
                }
                else
                    return Response<ModelContactProduct>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<ModelContactProduct>();
            }
        }

        public override Response<ListPagination<ViewProduct>> GetListView(BaseRequestT<ViewProduct> request)
        {
            try
            {
             
                ValidationResult vlr = base.ValidationGetListView(request);
                if (vlr.Success)
                {
                    ListPagination<ViewProduct> result = new ListPagination<ViewProduct>();
                    Expression<Func<ViewProduct, bool>> exp = (x => false);
                    if (request.Entity != null && request.Entity.FeatureModelId > -1 && request.Entity.FeatureBrandId > -1)
                    {
                        exp = (x => x.FeatureBrandId == request.Entity.FeatureBrandId);
                        result = ContextDataManager.GetList<ViewProduct>(exp, request);
                        return Response<ListPagination<ViewProduct>>.Successful(result);
                    }
                    else if (request.Entity != null && request.Entity.FeatureBrandId > -1)
                    {
                        exp = (x => x.FeatureBrandId == request.Entity.FeatureBrandId);
                        result = ContextDataManager.GetList<ViewProduct>(exp, request);
                        return Response<ListPagination<ViewProduct>>.Successful(result);
                    }
                    else if (request.Entity != null && request.Entity.FeatureModelId > -1)
                    {
                        exp = (x => x.FeatureBrandId == request.Entity.FeatureBrandId);
                        result = ContextDataManager.GetList<ViewProduct>(exp, request);
                        return Response<ListPagination<ViewProduct>>.Successful(result);
                    }
                    else
                    {
                        return base.GetListView(request);
                    }
                }
                else
                    return Response<ListPagination<ViewProduct>>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<ListPagination<ViewProduct>>();
            }
        }

        public override Response<long> Create(BaseRequestT<Product, ViewProduct, ModelContactProduct, ExtendNothing> request)
        {
            ValidationResult vlr = base.ValidationCreate(request);
            if (vlr.Success)
            {

                if (request.Model.ProductCategoryList.Count >= 1)
                {
                    request.Entity.FeatureDefaultCategoryId = request.Model.ProductCategoryList.First().RelatedPrimaryId;
                }
                ContextDataManager.Add(request.Entity, request.UserId);

                foreach (FeatureValue item in request.Model.ProductCategoryList)
                {
                    item.PrimaryId = request.Entity.Id;
                    item.Key = Features.ProductMultiCategory.ToString() + "->";
                    ContextDataManager.Add(item);
                }
                return Response<long>.Successful(request.Entity.Id);
            }
            else
                return Response<long>.Fail(vlr);
        }
    }
}
