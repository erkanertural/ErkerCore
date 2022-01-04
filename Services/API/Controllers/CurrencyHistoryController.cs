using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class CurrencyHistoryController : BaseController<SMCurrencyHistory, FeatureValue, ViewFeatureCurrencyHistory, BaseModel, ExtendCurrencyHistory> { }
}
