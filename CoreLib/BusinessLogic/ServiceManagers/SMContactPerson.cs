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
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    [ExtendWith(typeof(ExtendContactPerson))]
    public class SMContactPerson : AbstractServiceManager<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson>
    {
        public override Response<long> Create(BaseRequestT<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson> request)
        {
            ValidationResult vlr = base.ValidationCreate(request);
            if (vlr.Success)
            {
                ContextDataManager.Add(request.Entity, request.UserId);
                if (request.SaveExtend && request.Entity != null && request.Extend != null && request.Entity is IFormatableValue)
                    (request.Entity as IFormatableValue).Value = request.Extend.Serialize();
                request.Model.Adres.FeatureTableId = FeatureTable.ContactPerson.ToInt32();
                ContextDataManager.Add(request.Model.Adres, request.UserId);
                request.Entity.AdresId = request.Model.Adres.Id;
                ContextDataManager.Update(request.Entity);
                return Response<long>.Successful(request.Entity.Id);
            }
            else
                return Response<long>.Fail(vlr);
        }

        public override Response<bool> Edit(BaseRequestT<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson> request)
        {
            ValidationResult vlr = base.ValidationCreate(request);
            if (vlr.Success)
            {
                request.Model.Adres.FeatureTableId = FeatureTable.ContactPerson.ToInt32();
                ContextDataManager.Update<Adres>(request.Model.Adres);
                request.Entity.AdresId = request.Model.Adres.Id;
                if (request.SaveExtend && request.Entity != null && request.Extend != null && request.Entity is IFormatableValue)
                    (request.Entity as IFormatableValue).Value = request.Extend.Serialize();
                ContextDataManager.Update(request.Entity);
                return Response<bool>.Successful(true);
            }
            else
                return Response<bool>.Fail(vlr);
        }

        public override Response<ModelContactPerson> GetModel(BaseRequestT<ContactPerson> request)
        {
            ValidationResult vlr = base.ValidationGetModel(request);
            if (vlr.Success)
            {
                ModelContactPerson modelCP = new ModelContactPerson();
                modelCP.ContactPerson = ContextDataManager.Get<ContactPerson>(request.Id);
                modelCP.Adres = ContextDataManager.Get<Adres>(modelCP.ContactPerson.AdresId);
               return Response<ModelContactPerson>.Successful(modelCP);


            }
            else
                return Response<ModelContactPerson>.Fail(vlr);
        }
    }
}
