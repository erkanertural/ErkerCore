using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// > PrimaryId = ProductId
    /// > RelatedPrimaryId = ContactId
    /// </summary>
    [Route("[controller]")]
    public class ProductContactTrackController : BaseController<SMProductContactTrack, FeatureValue, BaseView, BaseModel, ExtendContactProductTrack> { }
}