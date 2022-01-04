using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using ErkerCore.View;
using System.Collections.Generic;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// ProductCategory CRUD işlemleri için Controller sınıfı.
    /// </summary>
    /// <returns></returns>
    [Route("[controller]")]
    public class ProductCategoryController : BaseTreewiewController<SMProductCategory, FeatureValue, ViewFeatureProductCategory, BaseModel, ExtendNothing>
    { }
}
