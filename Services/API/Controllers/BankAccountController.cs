using ErkerCore.Entities;
using ErkerCore.Message.Request;
using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Result;
using ErkerCore.Message.Response;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class BankAccountController : BaseController<SMBankAccount, FeatureValue, ViewFeatureBank,BaseModel, ExtendContactBank>    { }
}
