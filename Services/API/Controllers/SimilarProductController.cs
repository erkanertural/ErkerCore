using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// Create ederken [request.NumericValue] = Benzer Ürün Id'si
    /// </summary>
    [Route("[controller]")]
    public class SimilarProductController : BaseController<SMProductSimilar, FeatureValue,BaseView, BaseModel, ExtendNothing> { }
}
