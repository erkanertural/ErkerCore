using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// > PrimaryId = ContactId
    /// > RelatedPrimaryId = ProductId
    /// </summary>
    [Route("[controller]")]
    public class ContactProductTrackController : BaseController<SMContactProductTrack, FeatureValue, BaseView,BaseModel, ExtendContactProductTrack> { }
}