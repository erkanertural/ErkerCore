using ErkerCore.Library.Enums;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;

using ErkerCore.Message.Result;
using ErkerCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using ErkerCore.Message.Response;
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMContact : AbstractServiceManager<Contact, ViewContactDetail, ModelContact,ExtendContactSettings>
    {
        public override Response<ModelContact> GetModel(BaseRequestT<Contact> request)
        {
            try
            {
                ValidationResult vlr = this.ValidationGetModel(request);
                if (vlr.Success)
                {

                    // ürün takibi listesi

                    ModelContact contactInfo = new ModelContact();
                    contactInfo.BankAccount = new SMBankAccount().GetListView(new BaseRequestT<ViewFeatureBank>{ OwnerId = request.Id  }).Data.Select(o=> o.Extended as ExtendContactBank).ToList();
                    contactInfo.AdresView = ContextDataManager.GetList<ViewContactAddressDetail>(o => o.ContactId == request.Id, request);
                    contactInfo.Points = 3.5;
                    contactInfo.IsTransactionValid = true;
                    contactInfo.Contact = ContextDataManager.Get<Contact>(request.Id);
                    contactInfo.SectorName = ContextDataManager.Get<ViewFeatureSector>(o => o.Id == contactInfo.Contact.FeatureSectorId)?.Name;
                    contactInfo.DefaultAddress = ContextDataManager.Get<ViewContactAddressDetail>(contactInfo.Contact.DefaultAddressId);
                    contactInfo.ContactPerson = ContextDataManager.GetList<ViewContactPersonUser>(o => o.ContactId == request.Id, request);
                    contactInfo.ContactLoginUser = ContextDataManager.GetList<ViewContactPersonUser>(o => o.ContactId == request.OwnerId, request);
                    contactInfo.EmployeeCountInfo = ContextDataManager.Get<ViewFeatureContactEmployeeCount>(contactInfo.Contact.FeatureEmployeeCountId)?.Name;
                    contactInfo.TaxOfficeName = ContextDataManager.Get<ViewTaxOffice>(contactInfo.Contact.FeatureTaxOfficeId)?.Name;


                    return Response<ModelContact>.Successful(contactInfo);
                }
                else
                    return Response<ModelContact>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<ModelContact>();
            }
        }
        
        public override Response<long> Create(BaseRequestT<Contact, ViewContactDetail, ModelContact, ExtendContactSettings> request)
        {
            try
            {
       
                {
                  

                    ValidationResult vlr = this.ValidationCreate(request);
                    if (vlr.Success)
                    {
                        request.Entity.ShortName = request.Entity.ShortName.IsNullOrEmpty() ? request.Entity.Name : request.Entity.ShortName;
                        request.Entity.ParentContactId = request.OwnerId;
                        ContextDataManager.Add(request.Entity, request.UserId);
                        request.Model.Adres.ContactId = request.Entity.Id;
                        request.Model.Adres.FeatureTableId = FeatureTable.Contact.ToInt32();
                        ContextDataManager.Add(request.Model.Adres, request.UserId);
                        request.Entity.DefaultAddressId = request.Model.Adres.Id;
                        request.Entity.DefaultInvoiceAddressId = request.Model.Adres.Id;
                        ContextDataManager.Update(request.Entity);
                        return Response<long>.Successful(request.Entity.Id);
                    }
                    else
                        return Response<long>.Fail(vlr);
                }
            }
            catch (Exception ex)
            {
                return ex.Throw<long>();
            }
        }
        
        public override Response<ListPagination<Contact>> GetList(BaseRequestT<Contact> request)
        {
            Expression<Func<Contact, bool>> exp = (o => o.SubscriberId == request.OwnerId);
            request.ExprFunc = exp;
            return base.GetList(request);
        }
        
        public Response<ListPagination<ViewContactDetail>> GetContactSupplier(BaseRequestT<ViewContactDetail> request)
        {
            Expression<Func<ViewContactDetail, bool>> exp = o => o.FeatureContactTypeId == Library.Enums.FeatureContactType.Supplier && o.SubscriberId == request.Entity.SubscriberId;
            return this.GetListView(request);
        }
    }
}
