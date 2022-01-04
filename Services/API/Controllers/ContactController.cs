using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using ErkerCore.View;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ErkerCore.API.Controllers
{
    //[Helpers.Authorize]
    [Route("[controller]")]
    public class ContactController : BaseController<SMContact, Contact, ViewContactDetail, ModelContact, ExtendContactSettings>
    {

        [HttpPost("GetContactSupplier")]
        public Response<ListPagination<ViewContactDetail>> GetContactSupplier(BaseRequestT<ViewContactDetail> request)
        {

            return Manager.GetContactSupplier(request);
        }












































    }
}