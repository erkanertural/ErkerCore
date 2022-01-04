using Microsoft.AspNetCore.Hosting;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMFeatureValueTranslate : AbstractServiceManager<FeatureValueTranslate, BaseView, BaseModel , ExtendNothing>
    {
        public override Response<long> Create(BaseRequestT<FeatureValueTranslate, BaseView, BaseModel, ExtendNothing> request)
        {
            try
            {
                ValidationResult vlr = ValidationCreate(request);
                if (vlr.Success)
                {
                    // bir ürünün veya ürün benzeri yapıdaki bir bilginin alt özelliklerine çoklu dil desteği sağlıyor
                    if (request.KeyValue.Count > 0 && request.Id > 0 && request.FeatureId > 0)
                    {
                        foreach (KeyValuePair<object, object> item in request.KeyValue)
                        {
                            request.Entity = new FeatureValueTranslate();
                            request.Entity.TranslatedText = item.Value?.ToString();
                            request.Entity.FeatureTranslateKeyId = item.Key.ToInt64();
                            request.Entity.PrimaryId = request.Id;
                            request.Entity.FeatureId = request.FeatureId;
                            request.Entity.ContactId = request.OwnerId;
                            request.Entity.FeatureLanguageId = request.LangCode;
                            request.Entity.FeatureTableId = request.Table.ToInt64();
                            request.Entity.Key = Features.TranslateKey.ToString()+ "->" + item.Key.ToInt64().NameFromValue<FeatureTranslateKey>().ToString();
                            ContextDataManager.Add(request.Entity);
                        }
                    }
                    else if (request.KeyValue.Count > 0 && request.FeatureId < 0 && request.Id > 0 && request.Table > 0)
                    {
                        // request.Value da değiştirmek istediği kolonun adını vermek zorundadır.
                        request.Entity.Key = request.Entity.FeatureTableId.CastTo<FeatureTable>().ToString() + "->" + request.KeyValue.Values.ToList()[0];
                        ContextDataManager.Add(request.Entity);
                        // örneğin modül adını çevirisini kaydetmek çalışıyor.
                        // COMPONENT->NAME
                    }
                    else if (request.Entity != null && request.Entity.FeatureTableId == FeatureTable.FeatureValue.ToInt64())
                    {
                        FeatureValue fv = ContextDataManager.Get<FeatureValue>(request.Id);
                        FeatureValue fp = null;
                        string parentFormat = "";
                        fp = fv.ParentId > 0 ? ContextDataManager.Get<FeatureValue>(fv.ParentId) : null;
                        parentFormat = fp == null ? fp.Name : fp.Key + "->" + fv.Name;
                        request.Entity.FeatureId = (fp == null ? fv.FeatureId : fp.FeatureId);
                        request.Entity.Key = parentFormat;
                        FeatureValueTranslate fvt = new FeatureValueTranslate();
                        request.Entity.Clone(fvt);
                        ContextDataManager.Add(fvt);
                        // örneğin featurevaluedaki bir değeri çevirisini kaydetmek istiyor.
                        // Takım->Voleybol    VolleyBall
                    }
                    else if (request.Entity != null && request.Entity.FeatureTableId == FeatureTable.Feature.ToInt64())
                    {
                        Feature fv = ContextDataManager.Get<Feature>(request.Id);
                        Feature fp = null;
                        string parentFormat = "";
                        if (fv.ParentId > 0)
                        {
                            fp = ContextDataManager.Get<Feature>(fv.ParentId);
                            request.Entity.FeatureId = fv.ParentId;
                        }
                        parentFormat = fp == null ? fp.Name : fp.Key + "->" + fv.Name;
                        request.Entity.Key = parentFormat;
                        ContextDataManager.Add(request.Entity);
                        // örneğin featuredaki bir değeri çevirisini kaydetmek istiyor.
                        // Days->Monday    PAZARTESİ
                    }
                    else
                    {
                        request.Entity.FeatureId = Features.TranslateKey.ToInt64();
                        request.Entity.Key = Features.TranslateKey.ToString() + "->" + Target.ToString();
                        Target.ToInt64();
                        FeatureValueTranslate added = ContextDataManager.Add(request.Entity);
                        return Response<long>.Successful(added.Id);
                    }
                    return Response<long>.Successful(-1);
                }
                else
                    return Response<long>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<long>();
            }

        }

    }
}
