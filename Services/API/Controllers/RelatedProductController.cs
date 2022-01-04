using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Result;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class RelatedProductController : BaseController<SMProductRelated, FeatureValue,BaseView, BaseModel, ExtendNothing> { }
}
