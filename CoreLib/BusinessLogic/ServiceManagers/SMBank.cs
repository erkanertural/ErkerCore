using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using ErkerCore.View;
using System;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMBank : AbstractServiceManager<FeatureValue, ViewFeatureBank,BaseModel, ExtendBank>
    {
        public SMBank()
        {
            Target = Library.Enums.Features.Bank;
        }
    }
}
