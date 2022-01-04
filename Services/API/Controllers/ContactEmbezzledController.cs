using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Result;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    public class ContactEmbezzledController : BaseController<SMContactEmbezzled, FeatureValue, BaseView,BaseModel, ExtendEmbezzled> { }
}
