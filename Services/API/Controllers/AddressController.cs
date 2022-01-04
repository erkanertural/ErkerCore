using ErkerCore.Entities;
using ErkerCore.Message;
using ErkerCore.Message.Request;
using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Result;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Response;
using ErkerCore.Message.Model;
using System.Collections.Generic;
using ErkerCore.Library;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class AddressController : BaseController<SMAddress, Adres, ViewContactAddressDetail, BaseModel, ExtendNothing>
    {
        [HttpPost("GetRelatedCityDistrictCountry")]
        public Response<List<Feature>> GetRelatedCityDistrictCountry([FromBody] BaseRequestT<Adres> request)
        {
            return Manager.GetRelatedCityDistrictCountry(request);
        }
    
    }
}