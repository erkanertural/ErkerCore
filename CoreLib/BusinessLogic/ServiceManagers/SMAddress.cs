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
using System.Threading.Tasks;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMAddress : AbstractServiceManager<Adres, ViewContactAddressDetail, BaseModel,ExtendNothing>
    {

        public ValidationResult GetRelatedCityDistrictCountryValidate(BaseRequestT<Adres> request)
        {
            request.FlexAction = FeatureFlexAction.GetRelatedCityDistrictCountry;
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request, Message.Helper.ValidationOperationType.Custom);
            if (vlr.Fail)
                return vlr;
            if (vlr.IsInvalid(o => request.Entity == null, FeatureValidationErrorEnum.ObjectCannotBeNull, p => "request.Entity").Fail)
                return vlr;
            if (vlr.IsInvalid(o => request.Entity.AdresType.ToInt64() < 1, FeatureValidationErrorEnum.ObjectCannotBeNull, P => request.Entity.AdresType).Fail)
                return vlr;
            return vlr.Successful();
        }

        public  Response<ViewContactAddressDetail> GetAddressDEMO(BaseRequestT<ViewContactAddressDetail> request)
        {

            var data= base.GetView(request);
            Adres adres = data.Data.Extended;
            ViewContactAddressDetail detay = new ViewContactAddressDetail();
            detay.Country = Cache.GetCountry(adres.FeatureCountryId);
             detay.City = ContextDataManager.Get<Feature>(adres.FeatureCityId)?.Name;
            return null;

        }

        public Response<List<Feature>> GetRelatedCityDistrictCountry(BaseRequestT<Adres> request)
        {
            try
            {
                ValidationResult vlr = GetRelatedCityDistrictCountryValidate(request);
                if (vlr.Success)
                {
                    if (request.Entity.AdresType == FeatureAdres.Country)
                    { 
                    return Response<List<Feature>>.Successful(ContextDataManager.GetList<Feature>(x => x.FeatureId == request.Entity.AdresType.ToInt64(), request));
                    }
                    else
                    return Response<List<Feature>>.Successful(ContextDataManager.GetList<Feature>(x => x.FeatureId == request.Entity.AdresType.ToInt64() && x.ParentId == request.Id, request));
                }
                else
                    return Response<List<Feature>>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<List<Feature>>();
            }
        }
    }
}
