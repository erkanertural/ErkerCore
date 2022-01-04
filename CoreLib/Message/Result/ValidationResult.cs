
using ErkerCore.Library.Enums;
using ErkerCore.Library;

using ErkerCore.Message.Helper;
using ErkerCore.Message.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ErkerCore.Message.Result
{
    public class ValidationResult
    {
        /// You don't never public. We make that explicitly
        /// If you want new instance then you shoul call 'AbstractServiceManager->InstanceValidation'
        private ValidationResult()

        {

        }
        public static ValidationResult GetInstance(BaseRequest request)
        {
            return new ValidationResult { Cache = new Dictionary<string, object>(), Request = request, Status =ValidationStatus.Success };

        }

        public enum ValidationStatus
        {

            Invalid,
            Success,
            Error,
            NotAuthorize
        }

        //public ValidationResult(BaseRequest request)
        //{
        //    Request = request;
        //    Cache = new Dictionary<string, object>();
        //}
        public delegate string TranslateMethodDelegate(FeatureValidationErrorEnum errorEnums, BaseRequest request);
        public TranslateMethodDelegate TranslateMethod { get; set; }
        public Dictionary<string, object> Cache { get; private set; }
        public ValidationStatus Status { get; set; }
        public AttributeInfo<Validate> ValidationRulesInfo { get; set; }
        public bool Success
        {
            get
            {
                return Status == ValidationResult.ValidationStatus.Success;
            }
        }
        public bool Fail
        {
            get
            {
                return Status != ValidationResult.ValidationStatus.Success;
            }
        }
        public ValidationResult Failure(FeatureValidationErrorEnum vem, string field)
        {
            this.Status = ValidationStatus.Invalid;
            this.ValidationEnum = vem;
            this.Field = field;
            return this;
        }
        public string Field { get; set; }
        public FeatureValidationErrorEnum ValidationEnum { get; set; }
        public BaseRequest Request { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func">Gerçekleşmesi istenen Kural</param>
        /// <param name="valEnum">Kural sağlanırsa , dönmesi istenen validasyon enumu </param>
        /// <param name="func2">Field olarak hangi alna döneceğini belirler </param>
        /// <returns></returns>
        public ValidationResult IsInvalid(Expression<Func<object, bool>> func, FeatureValidationErrorEnum valEnum, Expression<Func<object, object>> func2 = null)
        {
            try
            {
                Func<object, bool> f = func.Compile();
                if (f.Invoke(null))
                {
                    ValidationEnum = valEnum;
                    Status = ValidationStatus.Invalid;
                    Field = GetProp(func2.Body);
                    return this;
                }
                else
                {
                    this.ValidationEnum = FeatureValidationErrorEnum.Uncertain;
                    this.Status = ValidationStatus.Success;
                    Field = GetProp(func2.Body);
                    return this;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Expression wrong or Expression contains null reference object");
            }

        }


        private string GetProp(Expression exp)
        {
            try
            {
                if (exp.Type == typeof(string))
                    return exp.ToString();
                return ((MemberExpression)((UnaryExpression)exp).Operand).Member.Name;
            }
            catch (Exception ex)
            {

                dynamic dt = exp;
                return dt.Member.Name;

            }

        }

        public ValidationResult Invalid(FeatureValidationErrorEnum valEnum, AttributeInfo<Validate> info = null)
        {
            ValidationRulesInfo = info;
            if (info != null)
                Field = info.Name;
            ValidationEnum = valEnum;
            Status = ValidationStatus.Invalid;
            return this;
        }
        public ValidationResult NotAuthorize()
        {
            Status = ValidationStatus.NotAuthorize;
            return this;
        }

        public ValidationResult Successful()
        {
            Status = ValidationStatus.Success;
            return this;
        }

        /// <summary>
        /// It can be used when occur an error for validation... IT SHOULD BE IN TRY CATCH  
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public ValidationResult ErrorInTryCatch(FeatureValidationErrorEnum valEnum)
        {
            ValidationEnum = valEnum;
            Status = ValidationStatus.Error;
            return this;
        }
    }

}
