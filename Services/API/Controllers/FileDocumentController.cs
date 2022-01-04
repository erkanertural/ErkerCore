using ErkerCore.Entities;
using ErkerCore.Message;
using ErkerCore.Message.Request;
using Microsoft.AspNetCore.Mvc;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Result;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Response;
using ErkerCore.Message.Model;
using System.Collections.Generic;
using ErkerCore.Library;
using ErkerCore.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace ErkerCore.API.Controllers
{
    [Route("[controller]")]
    public class FileDocumentController : BaseController<SMFileDocument, FileDocument, BaseView, BaseModel, ExtendNothing>
    {

        //[HttpPost("CreateFile")]
        //public ActionResult CreateFile([FromForm] )
        //{

        //    return null; // base.Create(null);
        //}
        public override Response<long> Create([FromForm] BaseRequestT<FileDocument, BaseView, BaseModel, ExtendNothing> request)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                foreach (IFormFile item in HttpContext.Request.Form.Files)
                    if (!request.UploadedFiles.ContainsKey(item.FileName))
                        request.UploadedFiles.Add(item.FileName, item);
                return Manager.Create(request);
            }
            else
                return Response<long>.InternalServerError(new System.Exception("File cannot be empty"));

        }
    }
}