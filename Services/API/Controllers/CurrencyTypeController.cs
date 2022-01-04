using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Result;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class CurrencyTypeController : BaseController<SMCurrencyType, FeatureValue, ViewFeatureCurrencyType,BaseModel, ExtendNothing> { }
}
