using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Result;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ErkerCore.Library;
using ErkerCore.Message.Request;
using ErkerCore.DataAccessLayer;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Response;

namespace ErkerCore.BusinessLogic.Helpers
{
    public static class BusinessUtility
    {
        public static ProjectEnviroment enviroment = ProjectEnviroment.Local;
        public static bool IsDevelopment { get; set; }
        public static Response<T> Throw<T>(this Exception ex)
        {
            return Response<T>.InternalServerError(IsDevelopment ? ex : null);
        }

        public static string GetErrorTranslatedText(FeatureValidationErrorEnum valEnum, BaseRequest request)
        {


            using (BaseServiceManager sm = new BaseServiceManager())
            {
                FeatureLanguage lang = request != null ? request.LangCode : FeatureLanguage.Tr;
                FeatureValueTranslate fvt = sm.ContextDataManager.Get<FeatureValueTranslate>(o => o.FeatureId == valEnum.ToInt64() && o.FeatureLanguageId == lang);
                if (fvt != null)
                    return fvt.TranslatedText;
                else
                    return valEnum.ToInt64().DescFromValue<FeatureValidationErrorEnum>();
            }


        }
        static string cstr = "";
        public static string ConnectionString
        {
            get
            {
#if DEBUG

                return GetConnStr();

#else
            return cstr;
#endif       
            }


            set { cstr = value; }
        }
        public static string ConnectionStringLog { get; set; }
        public static string ConnectionStringFile { get; set; }
        public static string GetConnStr()
        {
#if DEBUG
            bool local = false;
            bool admin = false;
            string Ip = "10.255.101.60,1433";
            string user = "FlexPro";
            string password = "rtm_123";
            string connStr = $"Server ={Ip}; Database =ErkerCore; User Id ={user}; Password ={password}; Pooling = true; ";
            return connStr;

#else
            return "Publish (Release) version has to use the appsettings";
#endif
        }
    }

}