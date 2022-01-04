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
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMContactRelation : AbstractServiceManager<FeatureValue, ViewContactRelationType,ModelContactRelation,ExtendNothing>
    {
        // create : ModelContactRelation[ConnectedContactId,ConnectedContactFVRelationId] ,ownerId
        //Edit : ModelContactRelation[ConnectedContactId,ConnectedContactFVRelationId] ,ownerId
        // GetListModel  : Bağlantılar listesi.
        public SMContactRelation() { Target = Features.ContactRelationData; }


        public override Response<long> Create(BaseRequestT<FeatureValue, ViewContactRelationType, ModelContactRelation,ExtendNothing> request)
        {
            ValidationResult vlr = base.ValidationCreate(request);
            if (vlr.Success)
            {
                FeatureValue fv = new FeatureValue();
                fv.PrimaryId = request.Model.ConnectedContactId;
                fv.ContactId = request.OwnerId;
                fv.RelatedPrimaryId = request.Model.ConnectedContactFVRelationId;
                request.SaveExtend = false; // veritabanında model için json  tutma
                return base.Create(request);
            }
            else
                return Response<long>.Fail(vlr);
        }

        // fv id si modelde geçilmesi lazım 
        public override Response<bool> Edit(BaseRequestT<FeatureValue, ViewContactRelationType, ModelContactRelation,ExtendNothing> request)
        {
            ValidationResult vlr = base.ValidationEdit(request);
            if (vlr.Success)
            {
                FeatureValue fv = ContextDataManager.Get<FeatureValue>(o => o.Id == request.Id);
                fv.PrimaryId = request.Model.ConnectedContactId;
                fv.RelatedPrimaryId = request.Model.ConnectedContactFVRelationId;
                fv.Value = request.Model.Serialize();
                return base.Edit(request);
            }
            else
                return Response<bool>.Fail(vlr);
        }


        public override Response<ListPagination<ModelContactRelation>> GetListModel(BaseRequestT<FeatureValue> request)
        {

            try
            {
                ValidationResult vlr = ValidationGetModel(request);
                if (vlr.Success)
                {
                    List<FeatureValue> relatedFv = ContextDataManager.GetList<FeatureValue>(o => o.FeatureId == Features.ContactRelationData.ToInt64(), request);
                    ListPagination<ModelContactRelation> result = new ListPagination<ModelContactRelation>();
                    foreach (FeatureValue item in relatedFv)
                    {
                        Contact connectedContact = ContextDataManager.Get<Contact>(item.PrimaryId);
                        Contact currentContact = ContextDataManager.Get<Contact>(item.ContactId);
                        Feature relationConnected = ContextDataManager.Get<Feature>(o => o.Id == item.RelatedPrimaryId);
                        Feature relationCurrentContact = null;
                        if (relationConnected != null)
                            relationCurrentContact = ContextDataManager.Get<Feature>(o => o.Id == relationConnected.ParentId);
                        ModelContactRelation r = new ModelContactRelation { Id = item.Id, CurrentContactId = currentContact.Id, ConnectedContactName = connectedContact.Name, ConnectedContactId = connectedContact.Id, ConnectedContactFVRelationId = relationConnected.Id, CurrentContactFVRelationId = relationCurrentContact == null ? -1 : relationCurrentContact.Id, ConnectedContactRelationName = relationCurrentContact == null ? "" : relationCurrentContact.Name, CurrentContactRelationName = currentContact.Name };
                        result.Add(r);
                    }
                    return Response<ListPagination<ModelContactRelation>>.Successful(result);
                }
                else
                    return Response<ListPagination<ModelContactRelation>>.Fail(vlr);
            }
            catch (Exception ex)
            {

                return ex.Throw<ListPagination<ModelContactRelation>>();
            }
        }

    }
}
