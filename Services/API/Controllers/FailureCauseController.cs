using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    public class FailureCauseController : BaseController<SMFailureCause, FeatureValue, ViewFeatureFailureCause, BaseModel, ExtendNothing> { }
}
