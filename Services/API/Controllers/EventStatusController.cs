using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// Name = "Durum adı", 
    /// NumericValue = "Açık kapalı belirten 0 veya 1", 
    /// ModuleId = "Hangi modül için olduğu", 
    /// ParentId = "Alt durum ise bağlı olduğunun Id değeri", 
    /// Value = "Modelden gelenler"
    /// </summary>
    [Route("[controller]")]
    public class EventStatusController : BaseController<SMEventStatus, FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings> { }
}
