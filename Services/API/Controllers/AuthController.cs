using ErkerCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.API;
using ErkerCore.Message.Result;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.View;
using System.Collections.Generic;
using ErkerCore.Message.Model;

namespace ErkerCore.API.Controllers
{
    [Helpers.Authorize]
    [Route("[controller]")]
    public class AuthController : BaseController<SMContactPersonAccount, ContactPersonAccount, ViewContactLoginUser, BaseModel, ExtendNothing>
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        public Response<ViewContactLoginUser> Login([FromBody] BaseRequestT<ViewContactLoginUser> request)
        {
            return new SMContactPersonAccount().GetView(request);
        }

    }
}