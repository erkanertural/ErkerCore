using Library.Enums;
using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Model;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Response;
using RitmaFlexPro.Message.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _AddressManager : BaseManager<Address, RequestAddress, BaseRequest, RequestAddress, RequestBaseSearch>
    {

        public override void SetDefaultDataManagers()
        {
            base.SetDefaultDataManagers();
        }

        public override TaskResult Create(RequestAddress request)
        {



            return this.RunWithTransactionScope(new Task<TaskResult>(() =>
           {
               try
               {
                   ValidationResult valres = CreateValidation(request);
                   if (valres.Success)
                   {
                       this.DataManager.Add<Address>(request.Address);
                       return TaskResult.Successful(new BaseResponse { Id = request.Address.Id });
                   }
                   else
                       return TaskResult.BadRequest(valres.Message);
               }
               catch (Exception ex)
               {
                   return ex.Throw();
               }


           }));
        }
        public override ValidationResult CreateValidation(RequestAddress request)
        {
            ValidationResult vlr = new ValidationResult();

            return ValidationResult.Successful();
        }


        public ValidationResult GetAllPlaceNameValidate(BaseRequest request)
        {

            // todo: Eğer bir feature enum adı değişirse bunun update edilmesi mutlaka gerekebilir.

            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            //else if (request.Id<1)
            //    return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult GetAllPlaceName(BaseRequest request, FeatureAddress featureAddressType)
        {
            try
            {
                ValidationResult valres = GetAllPlaceNameValidate(request);
                if (valres.Success)
                {
                    List<FeatureValue> placeFVList = DataManager.GetList<FeatureValue>(x => x.FeatureId == featureAddressType.ToInt64() && x.ParentId == request.Id);
                    List<BaseModel> featureValues = new List<BaseModel>();
                    foreach (FeatureValue item in placeFVList)
                    {
                        BaseModel fVModel = new BaseModel();
                        fVModel.Id = item.Id;
                        fVModel.Name = item.Value;
                        featureValues.Add(fVModel);

                    }
                    return TaskResult.Successful(featureValues);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public ValidationResult GetAddressByIdValidate(BaseRequest request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Id <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult GetAddressById(BaseRequest request)
        {
            try
            {
                ValidationResult valres = GetAddressByIdValidate(request);
                if (valres.Success)
                {
                    

                    AddressModel addressModel = new AddressModel();
                    Address address = DataManager.Get<Address>(x => x.Id == request.Id && x.IsDeleted == false);
                    addressModel.Name = address.Name;
                    addressModel.FullAddress = address.FullAddress;
                    addressModel.CountryName = GetAddressNameByType(new RequestAddress { FeatureAddressType = FeatureAddress.Country, Id = address.FeatureCountryId }).Data;
                    addressModel.CityName = GetAddressNameByType(new RequestAddress { FeatureAddressType = FeatureAddress.City, Id = address.FeatureCityId }).Data;
                    addressModel.DistrictName = GetAddressNameByType(new RequestAddress { FeatureAddressType = FeatureAddress.District, Id = address.FeatureDistrictId }).Data;
                    //Address address = DataManager.Get<Address>(x => x.Id == request.Id);
                    return TaskResult.Successful(addressModel);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public ValidationResult UpdateAddressValidate(RequestAddress request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Address.FeatureCityId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.CityCanNotBeNull, request));
            else if (request.Address.FeatureCountryId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.CountryCanNotBeNull, request));
            else if (request.Address.FeatureDistrictId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.DistrictCanNotBeNull, request));
            else if (request.Address.ContactId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult UpdateAddress(RequestAddress request)
        {
            try
            {
                ValidationResult valres = UpdateAddressValidate(request);
                if (valres.Success)
                {
                    BaseEntity result = DataManager.Update(request.Address);
                    return TaskResult.Successful(result);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public ValidationResult DeleteAddressValidate(RequestAddress request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Address.FeatureCityId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.CityCanNotBeNull, request));
            else if (request.Address.FeatureCountryId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.CountryCanNotBeNull, request));
            else if (request.Address.FeatureDistrictId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.DistrictCanNotBeNull, request));
            else if (request.Address.ContactId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult DeleteAddress(RequestAddress request)
        {
            try
            {
                ValidationResult valres = DeleteAddressValidate(request);
                if (valres.Success)
                {
                    Address result = DataManager.DeleteSoft<Address>(request.Address);
                    return TaskResult.Successful(result);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public TaskResult GetPlaceName(BaseRequest request, FeatureAddress featureAddressType)
        {
            try
            {
                string adddressName = DataManager.Get<FeatureValue>(x => x.FeatureId == featureAddressType.ToInt64() && x.Id == request.Id).Value;
                return TaskResult.Successful(adddressName);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult GetAddressNameByType(RequestAddress request)
        {
            try
            {
                string adddressName = DataManager.Get<FeatureValue>(x => x.FeatureId == request.FeatureAddressType.ToInt64() && x.Id == request.Id).Value;
                return TaskResult.Successful(adddressName);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult GetAllAddressSubFeatureOrById(RequestAddress request)
        {
            try
            {
                List<FeatureValue> list = new List<FeatureValue>();
                if (request.Id > 0)
                {
                    list = DataManager.GetList<FeatureValue>(x => x.FeatureId == request.FeatureAddressType.ToInt64() && x.Id == request.Id);
                }
                else // country
                {
                    list = DataManager.GetList<FeatureValue>(x => x.FeatureId == request.FeatureAddressType.ToInt64());
                }
                return TaskResult.Successful(list);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

    }
}
