
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Result;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ErkerCore.Message.Response
{

    public class Response<T>
    {

        public T Data { get; set; }

        public static Response<T> Successful(T data, string message = "")
        {
            
            if (data is IList)
            {
                IPaginationList plist = data as IPaginationList;
                if (plist!=null)
                return new Response<T> { PaginationInfo = new PaginationInfo { TotalCount = plist.TotalCount, PageSize = plist.PageSize,HasMoreData= plist.HasMoreData, TotalPage= plist.TotalPage, PageNo = plist.PageNo-1, Count = (data as IList).Count }, Data = data, Status = TaskStatus.Success, Message = message };
             else
                    return new Response<T> {  Data = data, Status = TaskStatus.Success, Message = message };
            }
            else
                return new Response<T> { Data = data, Status = TaskStatus.Success, Message = message };
        }
        string message = "";
        public TaskStatus Status { get; set; }

        //   public dynamic Data { get; set; }
        public string Message
        {
            get
            {
                if (ValidationResult != null)
                    return ValidationResult.Field + "->" + ValidationResult.TranslateMethod.Invoke(this.ValidationResult.ValidationEnum, this.ValidationResult.Request);
                else
                    return message;
            }
            set
            {
                message = value;
            }

        }
        public string GenerateTestDebugMessage()
        {
            string exmessage = "";
            if (Failure)
            {
                return exmessage = "\r\n XXX - Test Sonucu :: " + Message + (ValidationResult != null ? ValidationResult.Field + " -> " + ValidationResult.ValidationEnum.ToString() : "");
            }
            return "Test Sonucu :: ✓✓✓✓✓";
        }
        public bool Success
        {
            get
            {
                return Status == TaskStatus.Success;
            }

        }
        public bool Failure
        {
            get
            {
                return !Success;
            }

        }
        public FeatureValidationErrorEnum ValidationEnum { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="ValidationNullMessage">Validasyon </param>
        /// <returns></returns>
        public static Response<T> Fail(ValidationResult validation)
        {
            if (validation != null && validation.Status == ValidationResult.ValidationStatus.NotAuthorize)
                return Response<T>.NotAuthorize(validation);

            return new Response<T> { ValidationEnum = validation.ValidationEnum, Status = TaskStatus.Fail, ValidationResult = validation };

        }
        public static Response<T> FailWithMessage(string message)
        {
            return new Response<T> { Status = TaskStatus.Fail, Message = message };
        }
        public PaginationInfo PaginationInfo { get; set; }
        public static Response<T> NotFound()
        {
            return new Response<T> { Status = TaskStatus.NotFound, ValidationEnum = FeatureValidationErrorEnum.NotFoundRecord };
        }
        public static Response<T> NotAuthorize(ValidationResult validation)
        {
            return new Response<T> { ValidationEnum = validation.ValidationEnum, Status = TaskStatus.NotAuthorize, ValidationResult = validation };
        }
        public static Response<T> InternalServerError(Exception exception)
        {
            return new Response<T> { Status = TaskStatus.ServerInternalError, Message = exception?.ToString() };
        }
        public static Response<T> BusinessError(string message)
        {
            return new Response<T> { Status = TaskStatus.Failed, Message = message }; // it needs to translate
        }

    }
}
