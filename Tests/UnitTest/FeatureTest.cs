using ErkerCore.Entities;
using ErkerCore.Message;
using ErkerCore.Message.Request;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

using ErkerCore.BusinessLogic.Managers;
using System.Net.Http;
using ErkerCore.Message.Result;
using ErkerCore.Library;
using ErkerCore.DataAccessLayer;
using ErkerCore.Library.Enums;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using ErkerCore.Message.Model;
using ErkerCore.BusinessLogic.Helpers;

namespace ErkerCoreUnitTest
{

    [TestClass]
    public class FeatureTest
    {

        [TestMethod]
        public void TestSelectAllEntity()
        {
            StringBuilder sb = new StringBuilder();
            MsSqlDataManager<MainDbContext> manager = new MsSqlDataManager<MainDbContext>(BusinessUtility.GetConnStr());
            List<BaseDetail> list = manager.GetAllEntityAndView();
            bool errorFlag = false;
            foreach (var item in list)
            {
                try
                {
                    Type type = (Type)item.Value;
                    BaseEntity ent = manager.Get(type, 1);

                }
                catch (System.Exception ex)
                {
                    errorFlag = true;
                    sb.Append($"\r\n XXXXX {item.Name} -> Error: " + ex.Message + "  \r\n");

                }
            }
            Debug.WriteLine(sb.ToString());
            Assert.AreEqual(false, errorFlag);
        }


        [TestMethod]
        public void TestAllFeature()
        {
            List<Feature> flist = BaseServiceManager.DataManager.GetList<Feature>(o => o.ContactId == ConstContact.X.ToInt64(), new BaseRequest() { AllRecord = true });
            List<BaseDetail> elist = ExtensionUtility.GetAllFeatureEnum();
            StringBuilder r = new StringBuilder();

            List<string> notHasUncertain = elist.Select(o => o.Parent.ToString()).Distinct().Except(elist.Where(o => o.Id == -1).Select(o => o.Parent.ToString())).ToList();

            if (notHasUncertain.Count > 0)
                r.Append(" Uncertain Eklenmek zorundadır !! [ " + string.Join(",", notHasUncertain.Select(o => o).ToList()) + "]\r\n");

            foreach (BaseDetail enumItem in elist)
            {

                if (enumItem.Id == -1)
                    continue;
                //List<Feature> currents = flist.Where(o => o.ParentId == item.Id).ToList();
                //  if (currents.Count > 0)
                //  r.Append(item.Id + " - " + item.Parent + ">" + item.Name + " alanın birden fazla parenti var !! [ " + string.Join(",", currents.Select(o => o.Id).ToList()) + "]\r\n");
                Feature curr = flist.FirstOrDefault(o => o.Id == enumItem.Id);
                Feature parent = curr == null ? null : flist.FirstOrDefault(o => o.Id == curr.FeatureId);
     
                string parentFeatureName = enumItem.Parent.ToString().Replace("Feature", "");
                string key = enumItem.Parent.ToString() == "Features" ? enumItem.Name : parentFeatureName + "->" + enumItem.Name;


                if (enumItem.Description.IsNullOrEmpty())
                    r.Append(FeatureFormat(enumItem, curr, "[Description] attribute ve açıklama metni olmak zorundadır. !!"));
                if (curr != null)
                {
                    if (curr.FeatureId != -1 && curr.Key != key)
                        r.Append(FeatureFormat(enumItem, curr, "'Key' bilgisi uyuşmuyor !!"));
                    if (parent != null && !curr.Key.Contains("->"))
                        r.Append(FeatureFormat(enumItem, curr, "'Key=Parent->Name' formatı ile uyuşmuyor !!"));
                    if (parent != null && !curr.Key.StartsWith(parentFeatureName + "->"))
                        r.Append(FeatureFormat(enumItem, curr, curr.Key.Split('>')[0] + " FeatureParent uyuşmuyor !!"));
                    if (curr.Description.IsNullOrEmpty())
                        r.Append(FeatureFormat(enumItem, curr, "DB->'Description' sutunu null veya boş !!"));
                    if (parent != null && parent.Name != parentFeatureName)
                        r.Append(FeatureFormat(enumItem, curr, curr.FeatureId + " Id'li FeatureId uyumsuz !!"));
                    if (enumItem.Id != 4 && curr.FeatureId != 7 && enumItem.Id != curr.Id)
                    {
                        r.Append(FeatureFormat(enumItem, curr, " XXXXXXXXXXXXXXXXXXXXX"));
                    }

                }

                else
                {  
                    Feature other2 = flist.FirstOrDefault(o => o.Key == key );
                    if (other2 == null)
                    {
                   
                        r.Append(FeatureFormat(enumItem, new Feature(), "--- DB'de kayıtlı değil !!"));
                        Feature f3 = new Feature();
                        f3.FeatureModuleId = -1; // burası hesaplanabilir.
                        f3.Name = enumItem.Name;
                        f3.Key = key;
                        f3.FeatureId = parent == null ? -1 : parent.Id;
                        f3.Description = enumItem.Description;
                   /*    BaseEntity added = BaseServiceManager.DataManager.Add(f3);
                     
                        AddTranslatedText(added.Id, f3.Description, FeatureLanguage.Tr);
                        AddTranslatedText(added.Id, enumItem.DescriptionEn, FeatureLanguage.EnUs);
                        Debug.WriteLine(" ########## " + f3.Name + " : " + added.Id);  */

                    }

                }


                List<BaseDetail> uniqueSearchList = elist.Where(o => o.Id > -1 && o.Id == enumItem.Id && o.Name != enumItem.Name).ToList();
                foreach (BaseDetail sub in uniqueSearchList)
                {
                    r.Append(FeatureFormat(enumItem, curr, "DUPLICATE !!"));
                }
            }
            Debug.WriteLine(r.Length > 0 ? r.ToString() : ExtensionUtility.Ok + " Hata Bulunamadı.");
            Assert.AreEqual(false, r.Length > 0);
        }
        int count = 1;
        private string FeatureFormat(BaseDetail d, Feature f, string extra)
        {
            return $"{count++}| [{ d.Parent}.{d.Name}={d.Id}] DB->[ { (f == null ? "0-{}" : f.Key + "=" + f.Id) }] {extra} \r\n";
        }

        [TestMethod]

        public void TestAllCreateMethods()
        {
            // bütün servislerin içerisindeki createleri çağır...

        }





        public void AddFeaturesErrorEnum()
        {
            Feature f = new Feature();
            string engDescr = "";
            f.Name = FeatureValidationErrorEnum.NotFoundRecord.ToString();
            f.Key = Features.ValidationErrorEnum.ToString() + "->" + f.Name;
            f.FeatureId = Features.ValidationErrorEnum.ToInt64();
            f.ParentId = -1;
            f.ContactId = -1;
            f.Description = f.Name.DescFromName<FeatureValidationErrorEnum>();
            f.IsExtendable = 0;
            Feature fres = BaseServiceManager.DataManager.Get<Feature>(o => o.Key == f.Key);
            if (fres == null && (!f.Key.IsNullOrEmpty() && !f.Name.IsNullOrEmpty()))
            {
                BaseEntity added = BaseServiceManager.DataManager.Add(f);
                AddTranslatedText(added.Id, f.Description, FeatureLanguage.Tr);
                AddTranslatedText(added.Id, engDescr, FeatureLanguage.EnUs);

                Debug.WriteLine(" ########## " + f.Name + " : " + added.Id);
            }

        }



        [TestMethod]
        public void TestFeatureSetGet()

        {
            Contact c = new Contact();

            //   c.Extended.Department.Address = new Address();
            // c.Extended.Department.Address.FeatureCityId = 55;
            var l = c.GetAttr<DescriptionAttribute>(true);
            object v = c.GetPropValue(l[44].FullName);
            c.SetPropValue(l[44].FullName, 78);
            object v2 = c.GetPropValue(l[44].FullName);
        }
        [TestMethod]
        public void AddFeatures()
        {
            Feature f = new Feature();
            long willBeAddfeatureId = Features.EventStatusSettings.ToInt64();
            string englishDesc = "Event Status Theme";
            f.Name = ((Features)willBeAddfeatureId).ToString();
            f.Key = f.Name;
            f.ParentId = -1;
            f.ContactId = -1;
            f.Description = willBeAddfeatureId.DescFromValue<Features>();
            f.IsExtendable = 0;
            Feature fres = BaseServiceManager.DataManager.Get<Feature>(o => o.Key == f.Key);
            if (fres == null && (!f.Key.IsNullOrEmpty() && !f.Name.IsNullOrEmpty() && !f.Description.IsNullOrEmpty()))
            {

                BaseEntity added = BaseServiceManager.DataManager.Add(f, 1);
                //**************************************
                // eğer türkçe dil karşılığında görmek istersen f.description kullanabilirsin, yok kullanmak istemedin , o zaman kendin karşılığını vermen gerekir.
                //**************************************
                AddTranslatedText(added.Id, f.Description, FeatureLanguage.Tr);
                AddTranslatedText(added.Id, englishDesc, FeatureLanguage.EnUs);



                Debug.WriteLine(" ########## " + f.Key + " : " + added.Id);

            }
            else
                throw new System.Exception("Duplicate Feature or (Please check parentid /name / key / description )");

        }


        public void AddTranslatedText(long featureId, string translatedText, FeatureLanguage lang)
        {
            if (featureId <= 0)
                throw new System.Exception("FeatureId 0 olamaz.");
            FeatureValueTranslate ftvCurr = BaseServiceManager.DataManager.Get<FeatureValueTranslate>(o => o.FeatureId == featureId && o.FeatureLanguageId == lang);
            if (ftvCurr == null)
            {
                FeatureValueTranslate ftv = new FeatureValueTranslate();
                ftv.FeatureId = featureId;
                ftv.TranslatedText = translatedText;
                ftv.FeatureLanguageId = lang;
                BaseServiceManager.DataManager.Add(ftv, 1);
            }
            else throw new System.Exception("FeatureTranslate  -> already exists");

        }
        [TestMethod]
        public void AddSubFeatures()
        {
            Feature f = new Feature();

            long willBeAddfeatureId = FeatureModule.ContactProductTrack.ToInt64();
            string engDescription = "ContactProductTrack";

            List<BaseDetail> b = FeatureModule.ContactProductTrack.GetEnumDescriptionAttribute();
            f.Name = FeatureModule.ContactProductTrack.ToString();
            f.Key = b[0].Parent.ToString().Replace("Feature", "") + "->" + f.Name;
            f.FeatureId = Features.Module.ToInt64();
            f.ContactId = -1;
            f.ParentId = -1;
            f.Description = f.Name.DescFromName<FeatureModule>();
            f.IsExtendable = 0;


            Feature fres = BaseServiceManager.DataManager.Get<Feature>(o => o.Key == f.Key && o.ParentId == f.ParentId);

            if (fres == null && (!f.Key.IsNullOrEmpty() && !f.Name.IsNullOrEmpty() && f.FeatureId > 0))
            {
                BaseEntity added = BaseServiceManager.DataManager.Add(f);
                AddTranslatedText(added.Id, f.Description, FeatureLanguage.Tr);
                AddTranslatedText(added.Id, engDescription, FeatureLanguage.EnUs);

                Debug.WriteLine(" ########## " + f.Name + " : " + added.Id);
            }
            else
                throw new System.Exception((fres == null ? "" : fres.Id) + " -> Duplicate Feature or (doesn't exist parentid or name or key)");


        }


        public void AddFeatureTranslateFunc(string translatedText, long featureId, FeatureLanguage featureLanguage)
        {
            FeatureValueTranslate ftr = new FeatureValueTranslate();

            ftr.TranslatedText = translatedText;
            ftr.FeatureId = featureId;
            ftr.FeatureValueId = -1;
            ftr.FeatureLanguageId = featureLanguage;
            FeatureValueTranslate ftv = BaseServiceManager.DataManager.Get<FeatureValueTranslate>(o => o.FeatureId == ftr.FeatureId && o.FeatureLanguageId == ftr.FeatureLanguageId);
            if (ftv == null)
                BaseServiceManager.DataManager.Add(ftr, 1);
            else throw new System.Exception("FeatureTranslate  -> already exists");


        }


        public void AddFeatureValueFunction(Features featureKey, string Value, long ParentId)
        {
            FeatureValue fv = new FeatureValue();
            fv.Key = featureKey.ToString();
            fv.PrimaryId = -1;
            fv.FeatureId = featureKey.ToInt64();
            fv.Value = Value;
            fv.ParentId = ParentId;

            BaseEntity added = BaseServiceManager.DataManager.Add(fv, 1);
            Debug.WriteLine(" ########## " + fv.Key + " : " + fv.Value + " : " + added.Id);
        }
    }
}