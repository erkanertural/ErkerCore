using RitmaFlexPro.Message;
using System;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Message.Request;


using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


using System.Collections.Generic;
using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Message.Result;
using RitmaFlexPro.Library;
using System.IdentityModel.Tokens.Jwt;
using RitmaFlexPro.DataAccessLayer;
using RitmaFlexPro.View;
using RitmaFlexPro.Message.Response;
using RitmaFlexPro.Message.Model;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _SMLogin : AbstractServiceManager<ContactPersonAccount, BaseModel,BaseRequestCreateT<ContactPersonAccount>,  BaseRequestRemove,BaseRequestEditT<ContactPersonAccount>, BaseRequest>
    {
        public  ValidationResult LoginValidation(BaseRequest request)
        {
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
            if (vlr.Success == false)
                return vlr;
            if (request.UserName.IsNullOrEmpty())
                return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);
            if (request.Value.IsNullOrEmpty())
                return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);

            ViewContactLoginUser user = ContextDataManager.Get<ViewContactLoginUser>(x => (x.Username == request.UserName || x.Email == request.UserName) && x.Password == request.Value.ComputeSHA256());
            vlr.Cache.Add("user", user);
            if (user == null)
                return vlr.Invalid(FeatureValidationErrorEnum.UserNameOrPassswordInCorrect);
            return vlr.Successful();
        }
        public  Response<ViewContactLoginUser> Login(BaseRequest request)
        {
         
            try
            {
                ValidationResult vlr = LoginValidation(request);
                if (vlr.Success)
                    return GetUserWithToken(vlr.Cache["user"] as ViewContactLoginUser);
                else
                    return Response<ViewContactLoginUser>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<ViewContactLoginUser>();
            }
        }

         static Response<ViewContactLoginUser> GetUserWithToken(ViewContactLoginUser userAccount)
        {
            Response<ViewContactLoginUser> result = new Response<ViewContactLoginUser>();
            string secret = "MIIBljCCAUACCQCIDMpqK7WfWDANBgkqhkiG9w0BAQsFADBSMQswCQYDVQQGEwJV";
            byte[] key = Encoding.ASCII.GetBytes(secret);
            string role = String.Empty;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", userAccount.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userAccount.Username),
                    new Claim(ClaimTypes.Name, userAccount.NameSurname),
                    //new Claim(ClaimTypes.Email, userAccount.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            result.Data = new ViewContactLoginUser { Id = userAccount.Id, Username = userAccount.Username, Email = userAccount.Email, Token = tokenString };
            return result;
        }


    


     

        public override ValidationResult CreateValidation(BaseRequestCreateT<ContactPersonAccount> request)
        {
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
            if (vlr.Success == false)
                return vlr;

            if (string.IsNullOrEmpty(request.Entity.NameSurname))
                return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);
            ViewContactPersonUser userCase1 = ContextDataManager.Get<ViewContactPersonUser>(x => x.Email == request.Entity.Username);
            if (userCase1 != null)
                return vlr.Invalid(FeatureValidationErrorEnum.UserAlreadyRegistered);

            ViewContactPersonUser userCase2 = ContextDataManager.Get<ViewContactPersonUser>(o => o.Id == request.UserId); // bir adam 2 tane maille kaydolamasın
            if (userCase2 != null)
                return vlr.Invalid(FeatureValidationErrorEnum.UserAlreadyRegistered);
            return vlr.Successful();
            return base.CreateValidation(request);
        }

        public override Response<long> Create(BaseRequestCreateT<ContactPersonAccount> request)
        {
            try
            {
                ValidationResult vlr = this.CreateValidation(request);
                if (vlr.Success)
                {
                    ContextDataManager.Add<ContactPersonAccount>(request.Entity);
                    return Response<long>.Fail(vlr);
                }
                else
                    return Response<long>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<long>();
            }

        }


 

     

    }
}
