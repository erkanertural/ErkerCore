using RitmaFlexPro.Message;
using System;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Message.Request;

using RitmaFlexPro.Message.Response;
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

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _AuthManager : BaseManager<BaseEntity,RequestUserAccount,BaseRequest, RequestUserAccount, RequestBaseSearch>
    {

        #region Login
        public TaskResult CheckLogin(RequestLogin request)
        {
            try
            {
                ValidationResult validation = LoginValidate(request);
                if (validation.Success)
                {
                    string passwordHash = request.Password.ComputeSHA256();
                    string password = request.Password;

                    SubscriberUserAccount user = DataManager.Get<SubscriberUserAccount>(x => (x.Username == request.EmailOrUsername || x.Email == request.EmailOrUsername) && x.Password == password);

                    if (user != null && user.IsDeleted == false)
                    {
                        ResponseLogin userWithToken = GetUserWithToken(user);
                        return TaskResult.Successful(userWithToken);
                    }
                    else
                        return TaskResult.Fail("Geçersiz kullanıcı adı şifre");
                }
                else
                    return TaskResult.BadRequest();
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
        public static ResponseLogin GetUserWithToken(SubscriberUserAccount userAccount)
        {
            TaskResult result = new TaskResult();
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
                    new Claim(ClaimTypes.Email, userAccount.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            ResponseLogin response = new ResponseLogin(userAccount, "");
            result.Success = true;
            result.Data = response;

            return response;
        }
        public static ValidationResult LoginValidate(RequestLogin request)
        {
            if (request == null)
                return ValidationResult.Fail("Request null olamaz", ErrorEnums.RequestMustNotNull);
            if (string.IsNullOrEmpty(request.EmailOrUsername) || string.IsNullOrEmpty(request.Password))
                return ValidationResult.Fail("Geçersiz Kullanıcı Adı ve Şifre", ErrorEnums.Uncertain);

            return ValidationResult.Successful();
        }
        #endregion

        #region Get
        public TaskResult GetUser(BaseRequest request)
        {
            SubscriberUserAccount userAccount = DataManager.Get<SubscriberUserAccount>(request.Id);
            return TaskResult.Successful(userAccount);
        }
        public TaskResult GetAllUsers(BaseRequest request)
        {
            List<SubscriberUserAccount> userAccountList = DataManager.GetList<SubscriberUserAccount>(x => true);
            return TaskResult.Successful(userAccountList);
        }
        #endregion

        #region Create
        public TaskResult CreateUser(RequestUserAccount request)
        {
            try
            {
                ValidationResult validation = CreateUserValidate(request);
                if (validation.Success)
                {
                    DataManager.Add<SubscriberUserAccount>(request.UserAccount);
                    return TaskResult.Successful();
                }
                else
                {
                    return TaskResult.Fail(validation.Message);
                }
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        private ValidationResult CreateUserValidate(RequestUserAccount request)
        {
            if (request == null)
                return ValidationResult.Fail("Request null olamaz", ErrorEnums.RequestMustNotNull);

            if (string.IsNullOrEmpty(request.UserAccount.NameSurname))
                return ValidationResult.Fail("Ad soyad boş bırakılamaz", ErrorEnums.FieldMustNotNull);

            if (IsEmailExist(request.UserAccount.Email))
                return ValidationResult.Fail("Email adresine ait kayıt zaten var.", ErrorEnums.Uncertain);

            return ValidationResult.Successful();
        }
        private bool IsEmailExist(string email)
        {
            SubscriberUserAccount user = DataManager.Get<SubscriberUserAccount>(x => x.Email == email);
            return user == null;
        }
        #endregion

        #region Update
        public TaskResult UpdateUser(RequestUserAccount request)
        {
            try
            {
                ValidationResult validation = UpdateUserValidate(request);
                if (validation.Success)
                {
                    DataManager.Update(request.Entity);
                    return TaskResult.Successful();
                }
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
            return TaskResult.Fail();
        }
        private ValidationResult UpdateUserValidate(RequestUserAccount request)
        {
            return ValidationResult.Successful();
        }
        #endregion

        #region Delete
        public TaskResult DeleteUser(BaseRequest request)
        {
            try
            {
                ValidationResult validation = DeleteUserValidate(request);
                if (validation.Success)
                {
                    SubscriberUserAccount userAccount = DataManager.Get<SubscriberUserAccount>(request.Id);
                    if (userAccount == null)
                    {
                        return TaskResult.NotFound();
                    }
                    userAccount.IsDeleted = true;
                    DataManager.Update(userAccount);
                    return TaskResult.Successful();
                }
                else
                {
                    return TaskResult.Fail(validation.Message, error: ErrorEnums.FieldMustNotNull);
                }
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
        private ValidationResult DeleteUserValidate(BaseRequest request)
        {
            return ValidationResult.Successful();
        }
        #endregion


        #region Validation

        #endregion

    }
}
