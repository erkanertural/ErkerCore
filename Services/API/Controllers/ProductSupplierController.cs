using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    /// <summary>
    /// > PrimaryId = ProductId
    /// > RelatedPrimaryId = SupplierContactId
    /// </summary>
    [Route("[controller]")]
    public class ProductSupplierController : BaseController<SMProductSupplier, FeatureValue, ViewFeatureProductSupplier, BaseModel, ExtendNothing> { }
}
