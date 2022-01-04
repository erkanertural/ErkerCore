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
 
    public class SMEventStatus : AbstractServiceManager<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings>
    {
        public SMEventStatus()
        {
            Target = Features.EventStatus;
        }

        //public override Response<long> Create(BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings> request)
        //{

        //    Response<long> resp = base.Create(new BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings>() { Entity = request.Entity, SaveExtend = false });
        //    if (resp.Success)
        //    {
        //        Response<long> respSettings = new SMEventStatusSettings().Create(new BaseRequestT<FeatureValue, ViewFeatureValue, BaseModel, ExtendEventStatusSettings> { Entity = new FeatureValue { PrimaryId = resp.Data }, Model = request.Model, OwnerId = request.OwnerId, FeatureId = request.FeatureId });
        //        return respSettings;
        //    }
        //    else
        //        return Response<long>.Fail(resp.ValidationResult);

        //}

        //public override Response<bool> Edit(BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings> request)
        //{
        //    // ***** Eğer kullanıcının mevcut duruma yaptığı değişikliğin idsi ui ya gönderilmişse direkt ID kullanılabilir.

        //    //    FeatureValue featureValue= ContextDataManager.Get<FeatureValue>(request.Id); // yeni kullanıcının eklediğini biliyorsa bunu kullanacağız 
        //    FeatureValue featureValue = ContextDataManager.Get<FeatureValue>(o => o.RelatedPrimaryId == request.Id && o.FeatureId == Features.EventStatusSettings.ToInt64());

        //    request.Entity = new FeatureValue();
        //    return base.Edit(request);
        //}
    }
}
