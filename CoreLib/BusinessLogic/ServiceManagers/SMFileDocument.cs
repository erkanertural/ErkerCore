using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMFileDocument : AbstractServiceManager<FileDocument, BaseView, BaseModel, ExtendNothing>
    {
        private readonly IWebHostEnvironment env;
        public SMFileDocument(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public SMFileDocument()
        {

        }
        public override Response<long> Create(BaseRequestT<FileDocument, BaseView, BaseModel, ExtendNothing> request)
        {

            try
            {
                foreach (KeyValuePair<string, object> file in request.UploadedFiles)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        (file.Value as IFormFile).CopyTo(ms);
                        ms.Position = 0;
                        Guid guid = Guid.NewGuid();
                        request.Entity.FileGuid = guid.ToString();
                        string folder = "";
                        if (BusinessUtility.enviroment == ProjectEnviroment.Local)
                            folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.Parent +  @"\Client\Web\wwwroot\Upload";
                        else
                            folder = AppDomain.CurrentDomain.BaseDirectory + "upload";
                        // web client yolu hesapla yeterli .. yazarken web clienta yaz sadece.
                        Directory.CreateDirectory(folder);


                        string fileName = (file.Value as IFormFile).FileName;
                        string ext = Path.GetExtension(fileName);
                        string absolutePath = Path.Combine(folder, guid.ToString() + ext);

                        request.Entity.Path = "upload\\" + guid.ToString() + ext;
                        request.Entity.Name = fileName;
                        request.Entity.FileSize = ms.Length;
                        request.Entity.Key = request.FeatureId.NameFromValue<FeatureFileKey>();
                        System.IO.File.WriteAllBytes(absolutePath, ms.ToArray());
                        long fileId = FileContextDataManager.Add(request.Entity).Id;
                        if (request.UploadedFiles.Count == 1 && request.Table == FeatureTable.Product && request.FeatureId == FeatureFileKey.ProductThumbnail.ToInt64())
                        {
                            ContextDataManager.Update(new Product { Id = request.Entity.PrimaryId, ProductImageFileDocumentId = fileId });
                        }
                        else if (request.UploadedFiles.Count == 1 && request.Table == FeatureTable.Contact && request.FeatureId == FeatureFileKey.ContactProfile.ToInt64())
                        {
                            ContextDataManager.Update(new Contact { Id = request.Entity.PrimaryId, ProfileFileDocumentId = fileId });
                        }
                        return Response<long>.Successful(request.Entity.Id);
                    }

                }
            }
            catch (Exception ex)
            {

                return ex.Throw<long>();
            }

            return Response<long>.Fail(ValidationResult.GetInstance(request));
        }

        public override ValidationResult ValidationGetList(BaseRequestT<FileDocument> request)
        {
            ValidationResult vlr = base.CheckAuthorizeAndAttributeValidation(request);
            if (vlr.Fail)
                return vlr;
            if (request.Id < 1)
                return vlr.Failure(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, "Request.Id");
            if (request.ValueNumber < 1)
                return vlr.Failure(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, "Request.ValueNumber");
            return vlr.Successful();
        }
        public override Response<ListPagination<FileDocument>> GetList(BaseRequestT<FileDocument> request)
        {
            ValidationResult vlr = this.ValidationGetList(request);
            if (vlr.Success)
            {
                Expression<Func<FileDocument, bool>> exp = (x => x.PrimaryId == request.Id && x.TableId == request.ValueNumber);
                ListPagination<FileDocument> result = FileContextDataManager.GetList<FileDocument>(exp, request);
                return Response<ListPagination<FileDocument>>.Successful(result);
            }
            else
                return Response<ListPagination<FileDocument>>.Fail(vlr);

        }
    }
}
