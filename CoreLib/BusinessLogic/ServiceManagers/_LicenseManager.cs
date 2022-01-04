using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Result;
using System;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _LicenseManager : BaseManager<License,RequestLicense,BaseRequest,RequestLicense,RequestBaseSearch>
    {


        public ValidationResult CheckValGetLicenseById(BaseRequest request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Id <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult GetLicenseById(BaseRequest request)
        {
            try
            {
                ValidationResult valres = CheckValGetLicenseById(request);
                if (valres.Success)
                {
                    License license = DataManager.Get<License>(x => x.Id == request.Id && x.IsDeleted == false);
                    return TaskResult.Successful(license);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public ValidationResult CheckValGetLicenseBySubscriberId(BaseRequest request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.Id <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult GetLicenseBySubscriberId(BaseRequest request)
        {
            try
            {
                ValidationResult valres = CheckValGetLicenseBySubscriberId(request);
                if (valres.Success)
                {
                    License license = DataManager.Get<License>(x => x.SubscriberId == request.Id && x.IsDeleted == false);
                    return TaskResult.Successful(license);
                }
                else
                    return TaskResult.BadRequest(valres.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }


        public ValidationResult CheckValCreateLicense(RequestLicense request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else if (request.License.SubscriberId <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else if (request.License.UserCount <= 0)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult CreateLicense(RequestLicense request)
        {
            try
            {
                ValidationResult valres = CheckValCreateLicense(request);
                if (valres.Success)
                {
                    License result = DataManager.Add<License>(request.License);
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

        public ValidationResult CheckValUpdateLicense(RequestLicense request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            //else if (request.License.SubscriberId <= 0)
            //    return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            //else if (request.License.UserCount <= 0)
            //    return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.NumberMustGreaterThanZero, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult UpdateLicense(RequestLicense request)
        {
            try
            {
                ValidationResult valres = CheckValUpdateLicense(request);
                if (valres.Success)
                {
                    BaseEntity result = DataManager.Update(request.License);
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

        public ValidationResult CheckValDeleteLicense(RequestLicense request)
        {
            ValidationResult vlr = new ValidationResult();
            if (request == null)
                return ValidationResult.BadRequest(BusinessUtility.GetErrorTranslatedText(ErrorEnums.RequestMustNotNull, request));
            else
                return ValidationResult.Successful();
        }
        public TaskResult DeleteLicense(RequestLicense request)
        {
            try
            {
                ValidationResult valres = CheckValDeleteLicense(request);
                if (valres.Success)
                {
                    License result = DataManager.DeleteSoft<License>(request.License);
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

    }
}
