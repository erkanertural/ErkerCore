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
    /// Vergi daireleri için CRUDS işlemlerini gerçekleyen Controller sınıfı.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Route("[controller]")]
    public class TaxOfficeController : BaseController<SMTaxOffice, FeatureValue, ViewTaxOffice, BaseModel, ExtendNothing> { }

}
