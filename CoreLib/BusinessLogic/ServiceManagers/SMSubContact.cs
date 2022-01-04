using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Request;

using RitmaFlexPro.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class SMSubContact : AbstractServiceManager<BaseEntity, RequestSubContactRelation, BaseRequestRemove, RequestSubContactRelation, BaseRequestSearch>
    {
        public override ValidationResult CreateValidation(RequestSubContactRelation request)
        {

            ValidationResult vlr = base.CreateValidation(request);
            if (!vlr.Success)
                return vlr;

            Contact contact = ContextDataManager.Get<Contact>(request.CurrentContactId);
            if (contact == null)
                return vlr.Invalid(FeatureValidationErrorEnum.NotFoundRecord);
            else
                vlr.Cache.Add("contact", contact);
            long id = (request.CurrentContactId.ToString() + request.ConnectedContactId.ToString()).ToInt64();
            if (contact.Extended.ContactRelation != null && contact.Extended.ContactRelation.FirstOrDefault(o => o.Id == id) != null)
                return vlr.Invalid(FeatureValidationErrorEnum.AlreadExistRecord);

            return vlr.Successful();

        }
        public override TaskResult Create(RequestSubContactRelation request)
        {
            ValidationResult vlr = CreateValidation(request);
            if (vlr.Success)
            {
                Contact c = vlr.Cache["contact"] as Contact;

                string id = (request.CurrentContactId).ToString() + (request.ConnectedContactId).ToString();
                if (c.Extended.ContactRelation == null)
                    c.Extended.ContactRelation = new List<ExtendContactRelation>();
                c.Extended.ContactRelation.Add(new ExtendContactRelation
                {
                    Id = id.ToInt64(),
                    CurrentContactId = request.CurrentContactId,
                    CurrentContactFeatureValueRelationId = request.CurrentContactFeatureValueRelationId,
                    ConnectedContactId = request.ConnectedContactId,
                    ConnectedContactFeatureValueRelationId = request.ConnectedContactFeatureValueRelationId,
                    ConnectedContactName = request.ConnectedContactName,
                    ConnectedContactRelationName = request.ConnectedContactRelationName,
                    CurrentContactRelationName = request.CurrentContactRelationName,
                });
                ContextDataManager.Update(c);
                return TaskResult<long>.Successful(id.ToInt64());
            }
            return TaskResult.Fail(vlr);
        }

        public override ValidationResult EditValidation(RequestSubContactRelation request)
        {
            ValidationResult vlr = CreateValidation(request);
            if (!vlr.Success)
                return vlr;
            if (!vlr.IsInvalid(o => request.Id <= 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => request.Id).Success)
                return vlr;
            return vlr.Successful();
        }
        public override TaskResult Edit(RequestSubContactRelation request)
        {
            ValidationResult vlr = EditValidation(request);
            if (vlr.Success)
            {
                Contact c = vlr.Cache["contact"] as Contact;
                ExtendContactRelation sub = c.Extended.ContactRelation.FirstOrDefault(o => o.Id == request.Id);
                sub.CurrentContactFeatureValueRelationId = request.CurrentContactFeatureValueRelationId;
                sub.ConnectedContactId = request.ConnectedContactId;
                sub.ConnectedContactFeatureValueRelationId = request.ConnectedContactFeatureValueRelationId;
                sub.ConnectedContactRelationName = request.ConnectedContactRelationName;
                sub.CurrentContactRelationName = request.CurrentContactRelationName;
                sub.ConnectedContactName = request.ConnectedContactName;
                ContextDataManager.Update(c);
                return TaskResult.Successful();
            }
            return TaskResult.Fail(vlr);
        }

        public override ValidationResult RemoveValidation(BaseRequestRemove request)
        {
            ValidationResult vlr = base.RemoveValidation(request);
            if (!vlr.Success)
                return vlr;
            if (!vlr.IsInvalid(o => request.Id <= 0, FeatureValidationErrorEnum.PriceOrAmountValueMustGreaterThanZero, p => request.Id).Success)
                return vlr;

            Contact contact = ContextDataManager.Get<Contact>(request.Id);
            if (contact == null)
                return vlr.Invalid(FeatureValidationErrorEnum.NotFoundRecord);
            else
                vlr.Cache.Add("contact", contact);
            return vlr.Successful();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="request.Id">CurrentContactId bilgisini tutuyor</param>
        /// <param name="request.NumericValue">SubContactRelationId bilgisini tutuyor</param>
        /// <returns></returns>
        public override TaskResult Remove(BaseRequestRemove request)
        {

            ValidationResult vlr = RemoveValidation(request);
            if (vlr.Success)
            {
                Contact c = vlr.Cache["contact"] as Contact;
                ExtendContactRelation sub = c.Extended.ContactRelation.FirstOrDefault(o => o.Id == request.NumericValue);
                if (sub != null)
                    c.Extended.ContactRelation.Remove(sub);
                ContextDataManager.Update(c);
                return TaskResult.Successful();
            }
            return TaskResult.Fail(vlr);
        }

        public override ValidationResult GetValidation(BaseRequest request)
        {
            ValidationResult vlr = base.GetValidation(request);
            if (!vlr.Success)
                return vlr;
            if (!vlr.IsInvalid(o => request.Id <= 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => request.Id).Success)
                return vlr;
            if (!vlr.IsInvalid(o => request.NumericValue <= 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => request.NumericValue).Success)
                return vlr;

            Contact contact = ContextDataManager.Get<Contact>(request.Id);
            if (contact == null)
                return vlr.Invalid(FeatureValidationErrorEnum.NotFoundRecord);
            else
                vlr.Cache.Add("contact", contact);

            return vlr.Successful();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="request.Id">CurrentContactId bilgisini tutuyor</param>
        /// <param name="request.NumericValue">SubContactRelationId bilgisini tutuyor</param>
        /// <returns></returns>
        public override TaskResult Get(BaseRequestSearch request)
        {
            ValidationResult vlr = GetValidation(request);
            if (vlr.Success)
            {
                Contact c = vlr.Cache["contact"] as Contact;
                ExtendContactRelation sub = c.Extended.ContactRelation.FirstOrDefault(o => o.Id == request.NumericValue);
                if (sub != null)
                    return TaskResult.Successful(sub);
            }
            return TaskResult.Fail(vlr);
        }

        public override ValidationResult GetSearchListValidation(BaseRequestSearch request)
        {
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(new BaseRequest { Id = request.Id });
            if (!vlr.Success)
                return vlr;
            if (!vlr.IsInvalid(o => request.Id <= 0, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => request.Id).Success)
                return vlr;

            Contact contact = ContextDataManager.Get<Contact>(request.Id);
            if (contact == null)
                return vlr.Invalid(FeatureValidationErrorEnum.NotFoundRecord);
            else
                vlr.Cache.Add("contact", contact);
            return vlr.Successful();
        }
        public override TaskResult GetSearchList(BaseRequestSearch request)
        {
            ValidationResult vlr = GetSearchListValidation(request);
            if (vlr.Success)
            {
                Contact c = vlr.Cache["contact"] as Contact;
                if (c.Extended != null && c.Extended.ContactRelation != null)
                {
                    List<long> ids = c.Extended.ContactRelation.Select(o => o.ConnectedContactId).ToList();
                    List<Contact> contacts = ContextDataManager.GetList<Contact>(o => ids.Contains(o.Id));
                    foreach (Contact connected in contacts)
                    {
                        string id = (request.Id).ToString() + (connected.Id).ToString();
                        ExtendContactRelation relation = c.Extended.ContactRelation.FirstOrDefault(O => O.Id == id.ToInt64());
                        if (relation != null)
                            relation.ConnectedContactName = connected.Name;
                    }
                    return TaskResult.Successful(c.Extended.ContactRelation);
                }
                else
                    return TaskResult.Successful(null);

            }
            else
                return TaskResult.Fail(vlr);
        }
    }
}
