using Microsoft.IdentityModel.Tokens;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using ErkerCore.View;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMContactPersonAccount : AbstractServiceManager<ContactPersonAccount, ViewContactLoginUser, BaseModel, ExtendNothing>
    {
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
                    new Claim("userId", userAccount.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userAccount.Username),
                    new Claim(ClaimTypes.Name, userAccount.NameSurname),
                    //new Claim(ClaimTypes.Email, userAccount.Email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("ownerId", userAccount.ContactId.ToString()),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            result.Data = new ViewContactLoginUser { Id = userAccount.Id, Username = userAccount.Username, Email = userAccount.Email, Token = tokenString };
            return Response<ViewContactLoginUser>.Successful(result.Data);
        }
        public override ValidationResult ValidationCreate(BaseRequestT<ContactPersonAccount, ViewContactLoginUser, BaseModel, ExtendNothing> request)
        {
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
            if (vlr.Success == false)
                return vlr;

            if (string.IsNullOrEmpty(request.Entity.NameSurname))
                return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);
            ViewContactPersonUser userCase1 = ContextDataManager.Get<ViewContactPersonUser>(x => x.Email == request.Entity.UserName);
            if (userCase1 != null)
                return vlr.Invalid(FeatureValidationErrorEnum.UserAlreadyRegistered);

            ViewContactPersonUser userCase2 = ContextDataManager.Get<ViewContactPersonUser>(o => o.Id == request.UserId); // bir adam 2 tane maille kaydolamasın
            if (userCase2 != null)
                return vlr.Invalid(FeatureValidationErrorEnum.UserAlreadyRegistered);
            return vlr.Successful();

        }



        public override ValidationResult ValidationGetView(BaseRequestT<ViewContactLoginUser> request)
        {
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
            if (vlr.Fail)
                return vlr;
            if (request.UserName.IsNullOrEmpty())
                return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);
            if (request.Value.IsNullOrEmpty())
                return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);

            ViewContactLoginUser user = ContextDataManager.Get<ViewContactLoginUser>(x => x.Username == request.UserName && x.Password == request.Value.ComputeSHA256());

            if (user == null)
            {
                return vlr.Invalid(FeatureValidationErrorEnum.UserNameOrPassswordInCorrect);
            }
            else
                vlr.Cache.Add("user", user);
            return vlr.Successful();
        }
        public override Response<ViewContactLoginUser> GetView(BaseRequestT<ViewContactLoginUser> request)
        {
            try
            {
                ValidationResult vlr = ValidationGetView(request);
                if (vlr.Success)
                {
                    return GetUserWithToken(vlr.Cache["user"] as ViewContactLoginUser);
                }
                else
                    return Response<ViewContactLoginUser>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<ViewContactLoginUser>();
            }
        }
    }
}
