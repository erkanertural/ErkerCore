using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Entities;
using ErkerCore.Message.Model;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class ContactPersonController : BaseController<SMContactPerson, ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson> { }
}
