using ErkerCore.Library.Enums;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;

using ErkerCore.Message.Result;
using ErkerCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ErkerCore.Message.Response;
using ErkerCore.Library.Helpers;
using ErkerCore.Message.Helper;

namespace ErkerCore.BusinessLogic.Managers
{
    //todo: silerken , güncellerken  target olarak doğrumu kontrol edilmesi gerekiyor
    public class SMFeatureValue : AbstractServiceManager<FeatureValue, ViewFeatureValue, BaseModel,ExtendNothing> { }
}
