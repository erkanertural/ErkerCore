using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Model;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Response;
using RitmaFlexPro.Message.Result;
using RitmaFlexPro.View;
using System;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _ContactManager : BaseManager<Contact, RequestContact, BaseRequest, RequestContact, RequestBaseSearch>
    {


        public static ValidationResult GetContactDetailByIdValidate()
        {
            return null;
        }
        public TaskResult GetContactDetailById(BaseRequest request)
        {
            try
            {
                ContactModel contactInfo = new ContactModel();
                contactInfo.BankAccounts =new BankAccountManager().GetBankAccountListByContactId(new BaseRequest { Id = request.Id }).Data;
                contactInfo.ContactPerson = DataManager.GetList<ContactPerson>(o => o.ContactId == request.Id);
                contactInfo.Addresses = DataManager.GetList<ViewContactAddressDetail>(o => o.ContactId == request.Id);
                contactInfo.Points = 3.5;
                contactInfo.IsTransactionValid = true;
                contactInfo.EmployeeCountInfo = "1-10 Çalışan";
                contactInfo.SymbolName = "RT";
                contactInfo.SectorName = "Yazılım";
                contactInfo.Contact = DataManager.Get<Contact>(request.Id);
                contactInfo.DefaultAddress = DataManager.Get<ViewContactAddressDetail>(contactInfo.Contact.DefaultAddressId);
                contactInfo.ContactPerson = DataManager.GetList<ContactPerson>(o => o.ContactId == request.Id);

                return TaskResult.Successful(contactInfo);

            }
            catch (Exception ex)
            {
                return ex.Throw();
            }

            // ikinci adresi set etme yöntemi 

            //List<ViewContactAddressDetail> addressDetails = new List<ViewContactAddressDetail>();
            //List<ContactAddress> detailList = DataManager.GetList<ContactAddress>(o => o.ContactId == request.Id);
            //foreach (ContactAddress item in detailList)
            //{
            //    ViewContactAddressDetail adresDetail = new ViewContactAddressDetail();
            //    Address a = DataManager.Get<Address>(item.AddressId);

            //    adresDetail.District = DataManager.Get<FeatureValue>(o => o.Id == a.FeatureCityId).Value;
            //    adresDetail.Country = DataManager.Get<FeatureValue>(o => o.Id == a.FeatureCityId).Value;
            //    adresDetail.City = DataManager.Get<FeatureValue>(o => o.Id == a.FeatureCityId).Value;
            //    addressDetails.Add(adresDetail);
            //}
            //contactInfo.Addresses = addressDetails;

        }


        public ValidationResult AddContactValidate(RequestContact contact)
        {
            ValidationResult vlr = new ValidationResult();
            if (contact == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, contact));
            else if (contact.Contact.Id < 1)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, contact));
            else if (contact.Address.Id < 1)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, contact));
            else
                return ValidationResult.Successful();

       //     return AddressManager.CheckValCreateAddress(new RequestAddress { Address = contact.Address }); // "bunu anlamadım" ~Akadir
            //if (contact.Address.)
        }
        public TaskResult AddContact(RequestContact request)
        {
            try
            {

                request.Contact.ShortName = request.Contact.ShortName.IsNullOrEmpty() ? request.Contact.Name : request.Contact.ShortName;
                DataManager.Add(request.Contact, request.UserId);
                request.Address.ContactId = request.Contact.Id;
                DataManager.Add(request.Address, request.UserId);

                request.Contact.DefaultAddressId = request.Address.Id;
                DataManager.Update(request.Contact);

                //todo:ContactManager.AddContact methodune Extra fieldlar eklenecek.
                return TaskResult.Successful(new BaseResponse { Id = request.Contact.Id });

                //   return TaskResult.Successful(new ResponseContact() { Contact = DataManager.Get<Contact>(request.Id)  });
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public static ValidationResult GetDefaultContactPersonByIdValidate(BaseRequest request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            //else if (request.Id<1)
            //    return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult GetDefaultContactPersonById(BaseRequest request)
        {
            try
            {
                ValidationResult valres = GetDefaultContactPersonByIdValidate(request);
                if (valres.Success == true)
                {
                    ContactPerson personInfo = new ContactPerson();
                    personInfo = DataManager.Get<ContactPerson>(o => o.Id == request.Id);
                    return TaskResult.Successful(personInfo);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public ValidationResult SetDefaultContactPersonByIdValidate(BaseRequest request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Id < 1)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else if (request.NumericValue < 1)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult SetDefaultContactPersonById(BaseRequest request)
        {
            try
            {
                ValidationResult valres = SetDefaultContactPersonByIdValidate(request);
                if (valres.Success == true)
                {
                    Contact contactInfo = DataManager.Get<Contact>(request.Id);
                    contactInfo.Id = request.Id;
                    contactInfo.DefaultAuthorityPersonId = request.NumericValue;
                    DataManager.Update(contactInfo);
                    return TaskResult.Successful();
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public ValidationResult SetDefaultAddressByIdValidate(BaseRequest request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Id < 1)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else if (request.NumericValue < 1)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult SetDefaultAddressById(BaseRequest request)
        {
            try
            {
                ValidationResult valres = SetDefaultAddressByIdValidate(request);
                if (valres.Success == true)
                {
                    Contact contactInfo = DataManager.Get<Contact>(request.Id);
                    contactInfo.Id = request.Id;
                    contactInfo.DefaultAddressId = request.NumericValue;
                    DataManager.Update(contactInfo);
                    return TaskResult.Successful();
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
    }
}
