using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ErkerCore.API;
using ErkerCore.API.Helpers;
using ErkerCore.Message.Request;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Result;
using ErkerCore.Entities;
using ErkerCore.Message.Response;
using System.Collections.Generic;
using System.Linq.Expressions;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Message.Helper;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ErkerCore.API.Controllers
{


    [Route("[controller]")]
    public class BaseController<ServiceManager, Entity, View, Model, Extend> : Controller where ServiceManager : IManagerRequest<Entity, View, Model, Extend>, new() where Entity : BaseEntity, IUnique, new() where Model : class, IUnique, new() where View : class, IUnique, new()
    {
        List<BaseDetail> flexEnums;
        public BaseController()
        {
            Manager = new ServiceManager();

        }
        public ServiceManager Manager { get; }

        [HttpPost("Create")]
        public virtual Response<long> Create([FromBody] BaseRequestT<Entity, View, Model, Extend> request)
        {

            request.FlexAction = CalculateAction(HttpContext);

            return Manager.Create(request);
        }


        private FeatureFlexAction CalculateAction(HttpContext httpContext)
        {
            string controllerName = httpContext.Request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries)[0];
            string actionName = httpContext.Request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries)[1];
            BaseDetail currEnum = FeatureFlexAction.Uncertain.GetEnumModuleAttribute().FirstOrDefault(o => o.Name == controllerName + actionName);
            return currEnum != null ? (FeatureFlexAction)currEnum.Id : FeatureFlexAction.Uncertain;

        }

        [HttpPost("Edit")]
        public virtual Response<bool> Edit([FromBody] BaseRequestT<Entity, View, Model, Extend> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.Edit(request);
        }



        [HttpPost("Get")]
        public virtual Response<Entity> Get([FromBody] BaseRequestT<Entity> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.Get(request);
        }

        [HttpPost("GetList")]
        public virtual Response<ListPagination<Entity>> GetList([FromBody] BaseRequestT<Entity> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.GetList(request);
        }
        [HttpPost("RemoveSoft")]
        public virtual Response<bool> RemoveSoft([FromBody] BaseRequestT<Entity> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.RemoveSoft(request);
        }
        [HttpPost("Remove")]
        public virtual Response<bool> Remove([FromBody] BaseRequestT<Entity> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.Remove(request);
        }


        [HttpPost("GetView")]
        public virtual Response<View> GetView([FromBody] BaseRequestT<View> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.GetView(request);
        }
        [HttpPost("GetModel")]
        public virtual Response<Model> GetModel([FromBody] BaseRequestT<Entity> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.GetModel(request);
        }

        [HttpPost("GetListView")]
        public virtual Response<ListPagination<View>> GetListView([FromBody] BaseRequestT<View> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.GetListView(request);
        }

        [HttpPost("GetListModel")]
        public virtual Response<ListPagination<Model>> GetListModel([FromBody] BaseRequestT<Entity> request)
        {
            request.FlexAction = CalculateAction(HttpContext);
            return Manager.GetListModel(request);
        }


        [HttpGet("xxx")]
        public int GetUserId()
        {
            try
            {
                string token = Request.Headers["Bearer"];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var jwtToken = jsonToken as JwtSecurityToken;

                string userId = jwtToken.Claims.First(claim => claim.Type == "userId").Value ?? "0";
                return Convert.ToInt32(userId);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var b = HttpContext;
            bool IsIntegration = context.ActionArguments.ContainsKey("request");
            //ownerId= 
            //if (IsIntegration)
            //{
            //}
            //else if (!context.ActionDescriptor.DisplayName.Contains("Login"))
            //{
            BaseRequest request = context.ActionArguments.ContainsKey("request") ? context.ActionArguments["request"] as BaseRequest : null;
            if (request != null && HttpContext.Request.Headers.ContainsKey("Authorize") && !string.IsNullOrEmpty(HttpContext.Request.Headers["Authorize"]))
            {
                string token = HttpContext.Request.Headers["Authorize"];
                if (!string.IsNullOrEmpty(token))
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    SecurityToken jsonToken = handler.ReadToken(token);
                    JwtSecurityToken jwtToken = jsonToken as JwtSecurityToken;

                    long userId = (jwtToken.Claims.First(claim => claim.Type == "userId").Value ?? "-2").ToInt64();
                    request.UserId = userId;
                }



                //long contactId = (jwtToken.Claims.First(claim => claim.Type == "ownerId").Value ?? "-2").ToInt64();
                //request.OwnerId = contactId;
            }



            //request.OwnerId = 99;
            //    request = request == null ? context.ActionArguments.ContainsKey("req") ? context.ActionArguments["req"] as BaseRequest : null : request;
            //request.UserId = 0; // jwtden token bul yada , ui tarafta userid sakla.

            //    if (request == null)
            //    {
            //        //string url = Program.deploy? "https://app.api.com.tr/app/" : "http://localhost.:5051/AuthLogin/NotAuthorize";
            //        context.Result = new RedirectToActionResult("NotAuthorize", "AuthLogin", null);
            //        Console.WriteLine("Yetki hatası...");
            //        //  AuthLoginController.NotAuthorize(null);
            //        return;
            //    }
            //    else
            //    {
            //        if (false) // permission control
            //        {

            //            context.Result = new RedirectToActionResult("NotAuthorize", "AuthLogin", null);
            //            Console.WriteLine("Yetki hatası...");
            //            return;
            //        }
            //    }
            //}
            base.OnActionExecuting(context);
        }

    }
    public class BaseTreewiewController<ServiceManager, Entity, View, Model, Extend> : BaseController<ServiceManager, Entity, View, Model, Extend> where ServiceManager : IFeatureValueTreeView, IManagerRequest<Entity, View, Model, Extend>, new() where Entity : BaseEntity, IUnique, new() where Model : class, IUnique, new() where View : class, IUnique, new()
    {
        [HttpPost("GetTreeview")]
        public virtual Response<List<FeatureValue>> GetTreeView(BaseRequest request)
        {
            return Manager.GetTreeview(request);
        }

    }
}