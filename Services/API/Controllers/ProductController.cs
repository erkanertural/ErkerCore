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
using ErkerCore.View;
using ErkerCore.Library;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// Product CRUDS işlemlerini gerçekleştiren Controller sınıfı
    /// </summary>
    [Route("[controller]")]
    [Module( FeatureModule.Product)]
    public class ProductController : BaseController<SMProduct, Product,ViewProduct, ModelContactProduct, ExtendNothing>
    {
     

    }
}
