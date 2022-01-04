using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErkerCore.BusinessLogic.Managers;
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
using System.Diagnostics;
using System.Transactions;

namespace ErkerCoreUnitTest
{

    [TestClass]
    public class CrudTest : BaseTest
    {

        //[TestMethod]
        public void TestTemplateAllInOne()
        {
            SMProductCategory pm = new SMProductCategory();
            BaseRequestT<FeatureValue> createR = null;
            BaseRequestT<FeatureValue> editR = null;
            BaseRequestT<FeatureValue> getR = null;
            BaseRequestT<FeatureValue> searchR = null;
            BaseRequest removeR = null;
            Response<bool> result = pm.TestAllInOneCrud(null, null, null, getR, searchR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);

        }



        [TestMethod]
        public void TestAddressAllInOne()
        {
            SMAddress pm = new SMAddress();
            Adres ent = new Adres { ContactId = 1, FullAddress = "tam adres", FeatureCountryId = 498, FeatureCityId = 808, FeatureDistrictId = 1396, IsSending = IsLogic.Yes, IsInvoice = IsLogic.Yes };
            BaseRequestT<Adres, ViewContactAddressDetail, BaseModel, ExtendNothing> createR = new BaseRequestT<Adres, ViewContactAddressDetail, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<Adres, ViewContactAddressDetail, BaseModel, ExtendNothing> editR = new BaseRequestT<Adres, ViewContactAddressDetail, BaseModel, ExtendNothing> { Entity = ent.Clone<Adres>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.FullAddress = "changed";
            BaseRequestT<Adres> getR = new BaseRequestT<Adres> { Id = ent.Id };
            BaseRequestT<Adres> searchR = new BaseRequestT<Adres> { OwnerId = 1 };
            BaseRequestT<Adres> removeR = new BaseRequestT<Adres> { };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, searchR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);

        }

        [TestMethod]
        public void TestBankAccountAllInOne()
        {
            SMBank cm = new SMBank();
            FeatureValue ent = new FeatureValue { Key = Features.Bank.ToString(), FeatureId = Features.Bank.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> createR = new BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> editR = new BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.Bank.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.Bank.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.Bank.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestBankAllInOne()
        {
            SMBank cm = new SMBank();
            FeatureValue ent = new FeatureValue { Key = Features.Bank.ToString(), FeatureId = Features.Bank.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> createR = new BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> editR = new BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendBank> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.Bank.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.Bank.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.Bank.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductTypeAllInOne()
        {
            SMProductType pm = new SMProductType();
            FeatureValue ent = new FeatureValue { Key = Features.ProductType.ToString(), FeatureId = Features.ProductType.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureProductType, BaseModel, ExtendProductType> createR = new BaseRequestT<FeatureValue, ViewFeatureProductType, BaseModel, ExtendProductType> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureProductType, BaseModel, ExtendProductType> editR = new BaseRequestT<FeatureValue, ViewFeatureProductType, BaseModel, ExtendProductType> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductType.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999, FeatureId = Features.ProductType.ToInt64() } };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductType.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestContactAllInOne()
        {
            SMContact pm = new SMContact();
            Contact ent = new Contact
            {
                Name = "Deneme contact",
                FeatureTaxOfficeId = 1819,
                DebtLimit = 5,
                SubscriberId = 1,
                FeatureContactTypeId = FeatureContactType.Company
            };
            ModelContact mdl = new ModelContact
            {
                Adres = new Adres
                {
                    ContactId = 1,
                    FullAddress = "tam adres",
                    FeatureCountryId = 498,
                    FeatureCityId = 808,
                    FeatureDistrictId = 1396,
                    IsSending = IsLogic.Yes,
                    IsInvoice = IsLogic.Yes
                }
            };
            BaseRequestT<Contact, ViewContactDetail, ModelContact, ExtendContactSettings> createR =
                new BaseRequestT<Contact, ViewContactDetail, ModelContact, ExtendContactSettings>
                {
                    Entity = ent,
                    Model = mdl,
                    UserId = ConstUser.Administrator.ToInt64()
                };
            BaseRequestT<Contact, ViewContactDetail, ModelContact, ExtendContactSettings> editR =
                new BaseRequestT<Contact, ViewContactDetail, ModelContact, ExtendContactSettings>
                {
                    Entity = ent.Clone<Contact>(),
                    UserId = ConstUser.Administrator.ToInt64()
                };
            editR.Entity.Name = "changed";
            BaseRequestT<Contact> getR = new BaseRequestT<Contact> { Id = ent.Id };
            BaseRequestT<Contact> searchR = new BaseRequestT<Contact> { OwnerId = 1 };
            BaseRequestT<Contact> removeR = new BaseRequestT<Contact> { };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, searchR);

            //Bunu içeren testallinonecrud gerekebilir. Çoklu ekleme yapılanlar için. Örnek olarak Contact crud 
            BaseRequestT<Adres> getAdresR = new BaseRequestT<Adres> { Id = mdl.Adres.Id };
            BaseRequestT<Adres> removeAdresR = new BaseRequestT<Adres> { Id = getAdresR.Id };
            SMAddress sm = new SMAddress();
            sm.Remove(removeAdresR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);

        }

        [TestMethod]
        public void TestContactEmbezzledAllInOne()
        {
            SMContactEmbezzled cm = new SMContactEmbezzled();
            FeatureValue ent = new FeatureValue { Key = Features.ContactEmbezzled.ToString(), FeatureId = Features.ContactEmbezzled.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ContactEmbezzled.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ContactEmbezzled.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ContactEmbezzled.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestContactEmergencyAllInOne()
        {
            SMContactEmergency cm = new SMContactEmergency();
            FeatureValue ent = new FeatureValue { Key = Features.ContactEmergency.ToString(), FeatureId = Features.ContactEmergency.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ContactEmergency.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ContactEmergency.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ContactEmergency.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestContactEmployeeInfoAllInOne()
        {
            SMContactEmployeeInfo cm = new SMContactEmployeeInfo();
            FeatureValue ent = new FeatureValue { Key = Features.ContactEmployeeCount.ToString(), FeatureId = Features.ContactEmployeeCount.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureContactEmployeeCount, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewFeatureContactEmployeeCount, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureContactEmployeeCount, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewFeatureContactEmployeeCount, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ContactEmployeeCount.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ContactEmployeeCount.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ContactEmployeeCount.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestContactPersonAllInOne()
        {
            SMContactPerson pm = new SMContactPerson();
            ContactPerson ent = new ContactPerson
            {
                ContactId = 999,
                Name = "OriginalValue",
                Extended = new ExtendContactPerson { TCKN = "Testtc" }
            };

            BaseRequestT<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson> createR = new BaseRequestT<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson>
            {
                Entity = ent,
                UserId = ConstUser.Administrator.ToInt64()
            };
            BaseRequestT<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson> editR = new BaseRequestT<ContactPerson, ViewContactPerson, ModelContactPerson, ExtendContactPerson>
            {
                Entity = ent.Clone<ContactPerson>(),
                UserId = ConstUser.Administrator.ToInt64()
            };
            editR.Entity.Name = "changed";
            BaseRequestT<ContactPerson> getR = new BaseRequestT<ContactPerson> { Id = ent.Id };
            BaseRequestT<ContactPerson> searchR = new BaseRequestT<ContactPerson> { Id = -1 };
            BaseRequestT<ContactPerson> removeR = new BaseRequestT<ContactPerson> { };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, searchR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestContactProductTrackAllInOne()
        {
            SMContactProductTrack pm = new SMContactProductTrack();
            FeatureValue ent = new FeatureValue
            {
                Key = Features.ContactProductTrack.ToString(),
                FeatureId = Features.ContactProductTrack.ToInt64(),
                Name = "OriginalValue",
                PrimaryId = 999,
                RelatedPrimaryId = 999
            };
            ExtendContactProductTrack mdl = new ExtendContactProductTrack
            {
                IsRival = false,
                Description = "lorem ipsum"
            };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendContactProductTrack> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendContactProductTrack>
            {
                Entity = ent,
                Model = mdl,
                UserId = ConstUser.Administrator.ToInt64()
            };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendContactProductTrack> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendContactProductTrack>
            {
                Entity = ent.Clone<FeatureValue>(),
                UserId = ConstUser.Administrator.ToInt64()
            };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue>
            {
                Id = ent.Id,
                FeatureId = Features.ContactProductTrack.ToInt64()
            };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue>
            {
                Entity = new FeatureValue { ContactId = 999 },
                FeatureId = Features.ContactProductTrack.ToInt64()
            };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue>
            {
                FeatureId = Features.ContactProductTrack.ToInt64()
            };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            // todo : getlistview , modellist , getmodel, getview  methodlarıda generic test yapısına eklenecek.
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);

        }

        [TestMethod]
        public void TestContactTypeAllInOne()
        {
            SMContactType cm = new SMContactType();
            FeatureValue ent = new FeatureValue { Key = Features.ContactType.ToString(), FeatureId = Features.ContactType.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureContactType, BaseModel, ExtendContactTypeSettings> createR = new BaseRequestT<FeatureValue, ViewFeatureContactType, BaseModel, ExtendContactTypeSettings> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureContactType, BaseModel, ExtendContactTypeSettings> editR = new BaseRequestT<FeatureValue, ViewFeatureContactType, BaseModel, ExtendContactTypeSettings> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ContactType.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ContactType.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ContactType.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestCurrencyHistoryAllInOne()
        {
            SMCurrencyHistory cm = new SMCurrencyHistory();
            FeatureValue ent = new FeatureValue { Key = Features.CurrencyHistory.ToString(), FeatureId = Features.CurrencyHistory.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureCurrencyHistory, BaseModel, ExtendCurrencyHistory> createR = new BaseRequestT<FeatureValue, ViewFeatureCurrencyHistory, BaseModel, ExtendCurrencyHistory> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureCurrencyHistory, BaseModel, ExtendCurrencyHistory> editR = new BaseRequestT<FeatureValue, ViewFeatureCurrencyHistory, BaseModel, ExtendCurrencyHistory> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.CurrencyHistory.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999, FeatureId = Features.CurrencyHistory.ToInt64() }, FeatureId = Features.CurrencyHistory.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.CurrencyHistory.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestDepartmentAllInOne()
        {
            SMDepartment cm = new SMDepartment();
            FeatureValue ent = new FeatureValue { Key = Features.Department.ToString(), FeatureId = Features.Department.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureDepartment, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewFeatureDepartment, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureDepartment, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewFeatureDepartment, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.Department.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.Department.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.Department.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestEmploymentStatusAllInOne()
        {
            SMEmploymentEntry cm = new SMEmploymentEntry();
            FeatureValue ent = new FeatureValue { Key = Features.EmploymentEntry.ToString(), FeatureId = Features.EmploymentEntry.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> createR = new BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> editR = new BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.EmploymentEntry.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.EmploymentEntry.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.EmploymentEntry.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestEventStatusAllInOne()
        {
            SMEventStatus pm = new SMEventStatus();
            FeatureValue ent = new FeatureValue
            {
                Key = Features.EventStatus.ToString(),
                FeatureId = Features.EventStatus.ToInt64(),
                Name = "OriginalValue",
                ParentId = -1,
                ContactId = 999,
                Id = 0
            };
            ExtendEventStatusSettings mdl = new ExtendEventStatusSettings
            {
                IsEmail = true,
                IsSms = false
            };
            BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings> createR = new BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings>
            {
                Entity = ent,
                Model = mdl,
                UserId = ConstUser.Administrator.ToInt64()
            };
            BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings> editR = new BaseRequestT<FeatureValue, ViewFeatureEventStatus, BaseModel, ExtendEventStatusSettings>
            {
                Entity = ent.Clone<FeatureValue>(),
                UserId = ConstUser.Administrator.ToInt64()
            };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue>
            {
                Id = ent.Id,
                FeatureId = Features.EventStatus.ToInt64()
            };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue>
            {
                Entity = new FeatureValue { ContactId = 999 },
                FeatureId = Features.EventStatus.ToInt64()
            };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue>
            {
                FeatureId = Features.EventStatus.ToInt64()
            };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestEventStatusSettingsAllInOne()
        {
            SMEventStatusSettings pm = new SMEventStatusSettings();
            FeatureValue ent = new FeatureValue { Key = Features.EventStatusSettings.ToString(), FeatureId = Features.EventStatusSettings.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0, PrimaryId = 1000218 };
            ExtendEventStatusSettings mdl = new ExtendEventStatusSettings { ColorCode = "Red" };
            BaseRequestT<FeatureValue, ViewFeatureValue, BaseModel, ExtendEventStatusSettings> createR = new BaseRequestT<FeatureValue, ViewFeatureValue, BaseModel, ExtendEventStatusSettings> { Entity = ent, Model = mdl, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureValue, BaseModel, ExtendEventStatusSettings> editR = new BaseRequestT<FeatureValue, ViewFeatureValue, BaseModel, ExtendEventStatusSettings> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.EventStatusSettings.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999, FeatureId = Features.EventStatusSettings.ToInt64() }, FeatureId = Features.EventStatusSettings.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.EventStatusSettings.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestFailureCauseAllInOne()
        {
            SMFailureCause pm = new SMFailureCause();
            FeatureValue ent = new FeatureValue { Key = Features.FailureCause.ToString(), FeatureId = Features.FailureCause.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureFailureCause, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewFeatureFailureCause, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureFailureCause, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewFeatureFailureCause, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.FailureCause.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.FailureCause.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.FailureCause.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestLinkedEquipmentAllInOne()
        {
            SMContactEmergency cm = new SMContactEmergency();
            FeatureValue ent = new FeatureValue { Key = Features.ContactEmergency.ToString(), FeatureId = Features.ContactEmergency.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmergencyPerson> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.LinkedEquipment.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.LinkedEquipment.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.LinkedEquipment.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestMeasureOfUnitAllInOne()
        {
            SMMeasureOfUnit cm = new SMMeasureOfUnit();
            FeatureValue ent = new FeatureValue { Key = Features.MeasureOfUnit.ToString(), FeatureId = Features.MeasureOfUnit.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureMeasureOfUnit, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewFeatureMeasureOfUnit, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureMeasureOfUnit, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewFeatureMeasureOfUnit, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.MeasureOfUnit.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.MeasureOfUnit.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.MeasureOfUnit.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductAllInOne()
        {
            SMContactEmbezzled cm = new SMContactEmbezzled();
            FeatureValue ent = new FeatureValue { Key = Features.ContactEmbezzled.ToString(), FeatureId = Features.ContactEmbezzled.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendEmbezzled> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductBrand.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductBrand.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductBrand.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductCategoryAllInOne()
        {
            SMEmploymentEntry cm = new SMEmploymentEntry();
            FeatureValue ent = new FeatureValue { Key = Features.EmploymentEntry.ToString(), FeatureId = Features.EmploymentEntry.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> createR = new BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> editR = new BaseRequestT<FeatureValue, ViewFeatureEmploymentEntry, BaseModel, ExtendEmploymentEntry> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductCategory.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductCategory.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductCategory.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductContactTrackAllInOne()
        {
            SMBankAccount cm = new SMBankAccount();
            FeatureValue ent = new FeatureValue { Key = Features.Bank.ToString(), FeatureId = Features.Bank.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0, PrimaryId = 999, RelatedPrimaryId = 999 };
            ExtendContactBank mdl = new ExtendContactBank { Name = "OriginalValue", FeatureBankId = 171, AccountNo = "test12345", BranchCode = "1234", BranchName = "Test", CurrencyType = FeatureCurrencyType.TurkishLira.ToString(), Iban = "TR000000000001200120000" };
            BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendContactBank> createR = new BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendContactBank> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendContactBank> editR = new BaseRequestT<FeatureValue, ViewFeatureBank, BaseModel, ExtendContactBank> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductContactTrack.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductContactTrack.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductContactTrack.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            // todo : getlistview , modellist , getmodel, getview  methodlarıda generic test yapısına eklenecek.
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductModelAllInOne()
        {
            SMProductModel cm = new SMProductModel();
            FeatureValue ent = new FeatureValue { Key = Features.ProductModel.ToString(), FeatureId = Features.ProductModel.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductModel.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductModel.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductModel.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductPartAllInOne()
        {
            SMProductPart cm = new SMProductPart();
            FeatureValue ent = new FeatureValue { Key = Features.ProductPart.ToString(), FeatureId = Features.ProductPart.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductPart.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductPart.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductPart.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductRelatedAllInOne()
        {
            SMProductRelated cm = new SMProductRelated();
            FeatureValue ent = new FeatureValue { Key = Features.ProductRelated.ToString(), FeatureId = Features.ProductRelated.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0, NumericValue = 999 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductRelated.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductRelated.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductRelated.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductSimilarAllInOne()
        {
            SMProductSimilar cm = new SMProductSimilar();
            FeatureValue ent = new FeatureValue { Key = Features.ProductSimilar.ToString(), FeatureId = Features.ProductSimilar.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0, NumericValue = 999 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductSimilar.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductSimilar.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductSimilar.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestProductSupplierAllInOne()
        {
            SMProductSupplier cm = new SMProductSupplier();
            FeatureValue ent = new FeatureValue
            {
                Key = Features.ProductSupplierData.ToString(),
                FeatureId = Features.ProductSupplierData.ToInt64(),
                Name = "OriginalValue",
                ParentId = -1,
                ContactId = 999,
                Id = 0,
                PrimaryId = 999,
                RelatedPrimaryId = 999,
            };
            BaseRequestT<FeatureValue, ViewFeatureProductSupplier, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewFeatureProductSupplier, BaseModel, ExtendNothing>
            {
                Entity = ent,
                UserId = ConstUser.Administrator.ToInt64()
            };
            BaseRequestT<FeatureValue, ViewFeatureProductSupplier, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewFeatureProductSupplier, BaseModel, ExtendNothing>
            {
                Entity = ent.Clone<FeatureValue>(),
                UserId = ConstUser.Administrator.ToInt64()
            };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue>
            {
                Id = ent.Id,
                FeatureId = Features.ProductSupplierData.ToInt64()
            };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue>
            {
                Entity = new FeatureValue { ContactId = 999 },
                FeatureId = Features.ProductSupplierData.ToInt64()
            };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue>
            {
                FeatureId = Features.ProductSupplierData.ToInt64()
            };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }


        [TestMethod]
        public void TestServiceStatusAllInOne()
        {
            SMServiceEventStatus pm = new SMServiceEventStatus();
            FeatureValue ent = new FeatureValue { Key = Features.ServiceStatus.ToString(), FeatureId = Features.ServiceStatus.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureServiceStatus, BaseModel, ExtendEventStatusSettings> createR = new BaseRequestT<FeatureValue, ViewFeatureServiceStatus, BaseModel, ExtendEventStatusSettings> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureServiceStatus, BaseModel, ExtendEventStatusSettings> editR = new BaseRequestT<FeatureValue, ViewFeatureServiceStatus, BaseModel, ExtendEventStatusSettings> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ServiceStatus.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ServiceStatus.ToInt64() };
            //todo: bu getlistR içinde iki yerde featureId belirtilmezse patlıyor. Ya entity veya direkt request içindeki olması yeterli değil mi?
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ServiceStatus.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestSimilarProductAllInOne()
        {
            SMProductSimilar pm = new SMProductSimilar();
            FeatureValue ent = new FeatureValue() { Key = Features.ProductSimilar.ToString(), FeatureId = Features.ProductSimilar.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, BaseView, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.ProductSimilar.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.ProductSimilar.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ProductSimilar.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void TestTaxOfficeAllInOne()
        {
            SMTaxOffice cm = new SMTaxOffice();
            FeatureValue ent = new FeatureValue { Key = Features.TaxOffice.ToString(), FeatureId = Features.TaxOffice.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewTaxOffice, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewTaxOffice, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewTaxOffice, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewTaxOffice, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.TaxOffice.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999 }, FeatureId = Features.TaxOffice.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.TaxOffice.ToInt64() };
            Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

       

        [TestMethod]
        public void TestWorkOrderTypeAllInOne()
        {
            SMWorkOrderType pm = new SMWorkOrderType();
            FeatureValue ent = new FeatureValue { Key = Features.WorkOrderType.ToString(), FeatureId = Features.WorkOrderType.ToInt64(), Name = "OriginalValue", ParentId = -1, ContactId = 999, Id = 0 };
            BaseRequestT<FeatureValue, ViewFeatureWorkOrderType, BaseModel, ExtendNothing> createR = new BaseRequestT<FeatureValue, ViewFeatureWorkOrderType, BaseModel, ExtendNothing> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValue, ViewFeatureWorkOrderType, BaseModel, ExtendNothing> editR = new BaseRequestT<FeatureValue, ViewFeatureWorkOrderType, BaseModel, ExtendNothing> { Entity = ent.Clone<FeatureValue>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.Name = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = ent.Id, FeatureId = Features.WorkOrderType.ToInt64() };
            BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999, FeatureId = Features.WorkOrderType.ToInt64() }, FeatureId = Features.WorkOrderType.ToInt64() };
            BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.WorkOrderType.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }














        //        var fd = new SMFileDocument().GetList(new BaseRequestT<FileDocument> { Id = 8, ValueNumber = FeatureTable.Product.ToInt64() });


        //[TestMethod]
        //public void TestBankAccountAllInOne()
        //{
        //    SMBankAccount cm = new SMBankAccount();
        //    ModelContactBank mdl = new ModelContactBank { Name = "OriginalValue", FeatureBankId = 171, AccountNo = "test12345", BranchCode = "1234", BranchName = "Test", CurrencyType = FeatureCurrencyType.TurkishLira.ToString(), Iban = "TR000000000001200120000" };
        //    BaseRequestT<FeatureValue, BaseView, ModelContactBank> createR = new BaseRequestT<FeatureValue, BaseView, ModelContactBank> { Model = mdl, FeatureId = Features.ContactBankData.ToInt64(), UserId = ConstUser.Administrator.ToInt64() };
        //    BaseRequestT<FeatureValue, BaseView, ModelContactBank> editR = new BaseRequestT<FeatureValue, BaseView, ModelContactBank> { Model = mdl.Clone<ModelContactBank>(), UserId = ConstUser.Administrator.ToInt64() };
        //    editR.Model.AccountNo = "chgd123";
        //    BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue> { Id = mdl.Id, FeatureId = Features.ContactBankData.ToInt64() };
        //    BaseRequestT<FeatureValue> getListR = new BaseRequestT<FeatureValue> { Entity = new FeatureValue { ContactId = 999, FeatureId = Features.ContactBankData.ToInt64() } };
        //    BaseRequestT<FeatureValue> removeR = new BaseRequestT<FeatureValue> { FeatureId = Features.ContactBankData.ToInt64() };
        //    Response<bool> result = cm.TestAllInOneCrud(createR, removeR, editR, getR, getListR);
        //    Debug.WriteLine(result.Message);
        //    Assert.AreEqual(true, result.Success);
        //}




        //[TestMethod]
        public void TestProductMultiCategoryAllInOne()
        {
            SMProductMultiCategory pm = new SMProductMultiCategory();
            FeatureValue ent = new FeatureValue()
            {
                Key = Features.ProductMultiCategory.ToString(),
                FeatureId = Features.ProductMultiCategory.ToInt64(),
                Value = "OriginalValue",
                ParentId = -1,
                ContactId = 999
            };
            BaseRequestT<FeatureValue> createR = new BaseRequestT<FeatureValue>
            {
                Entity = ent,
                UserId = ConstUser.Administrator.ToInt64()
            };

            BaseRequestT<FeatureValue> editR = new BaseRequestT<FeatureValue>
            {
                Entity = ent.Clone<FeatureValue>(),
                UserId = ConstUser.Administrator.ToInt64()
            };
            editR.Entity.Value = "changed";
            BaseRequestT<FeatureValue> getR = new BaseRequestT<FeatureValue>
            {
                Id = ent.Id,
                FeatureId = Features.ProductMultiCategory.ToInt64()
            };
            BaseRequestT<FeatureValue> searchR = new BaseRequestT<FeatureValue>
            {
                Entity = new FeatureValue { ContactId = 999, FeatureId = Features.ProductMultiCategory.ToInt64() }
            };
            BaseRequest removeR = new BaseRequest
            {
                Id = ent.Id,
                FeatureId = Features.ProductMultiCategory.ToInt64()
            };
            Response<bool> result = pm.TestAllInOneCrud(null, null, null, getR, searchR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        //[TestMethod]
        public void TestFeatureValueTranslateAllInOne()
        {
            SMFeatureValueTranslate pm = new SMFeatureValueTranslate();

            FeatureValueTranslate ent = new FeatureValueTranslate() { Key = Features.Sector.ToString(), FeatureId = Features.Sector.ToInt64(), TranslatedText = "OriginalValue", ContactId = 999 };
            BaseRequestT<FeatureValueTranslate> createR = new BaseRequestT<FeatureValueTranslate> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            ent.TranslatedText = "changed";
            BaseRequestT<FeatureValueTranslate> editR = new BaseRequestT<FeatureValueTranslate> { Entity = ent, UserId = ConstUser.Administrator.ToInt64() };
            BaseRequestT<FeatureValueTranslate> getR = new BaseRequestT<FeatureValueTranslate> { Id = ent.Id/*, NumericValue = Features.TranslateKey.ToInt64()*/ };
            BaseRequestT<FeatureValueTranslate> searchR = new BaseRequestT<FeatureValueTranslate> { Entity = new FeatureValueTranslate { ContactId = 999, FeatureId = Features.TranslateKey.ToInt64() } };
            BaseRequest removeR = new BaseRequest { Id = ent.Id, FeatureId = Features.TranslateKey.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(null, null, null, getR, searchR);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

        //[TestMethod]
        public void TestProductTranslateAllInOne()
        {
            SMProductTranslate pm = new SMProductTranslate();

            FeatureValueTranslate ent = new FeatureValueTranslate()
            {
                Key = Features.TranslateKey.ToString(),
                FeatureId = FeatureTranslateKey.ProductDescription.ToInt64(),
                TranslatedText = "OriginalValue",
                ContactId = 999,
                PrimaryId = 0,
                FeatureLanguageId = FeatureLanguage.Tr
            };
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add(FeatureTranslateKey.ProductDescription.ToString(), "Ürün açıklama");
            dict.Add(FeatureTranslateKey.ProductSeoKeywords.ToString(), "Ürün Seo");
            dict.Add(FeatureTranslateKey.ProductName.ToString(), "Ürün Ad");
            dict.Add(FeatureTranslateKey.ProductSeoTitle.ToString(), "Ürün Başlık");
            BaseRequestT<FeatureValueTranslate> createR = new BaseRequestT<FeatureValueTranslate>
            {
                Entity = ent.Clone<FeatureValueTranslate>(),
                KeyValue = dict,
                UserId = ConstUser.Administrator.ToInt64()
            };


            BaseRequestT<FeatureValueTranslate> editR = new BaseRequestT<FeatureValueTranslate> { Entity = ent.Clone<FeatureValueTranslate>(), UserId = ConstUser.Administrator.ToInt64() };
            editR.Entity.TranslatedText = "changed";
            BaseRequestT<FeatureValueTranslate> getR = new BaseRequestT<FeatureValueTranslate> { Id = ent.Id };
            BaseRequestT<FeatureValueTranslate> searchR = new BaseRequestT<FeatureValueTranslate> { Entity = new FeatureValueTranslate { ContactId = 999, FeatureId = Features.TranslateKey.ToInt64() } };
            BaseRequest removeR = new BaseRequest { Id = ent.Id, FeatureId = Features.TranslateKey.ToInt64() };
            Response<bool> result = pm.TestAllInOneCrud(null, null, null, null, null);
            Debug.WriteLine(result.Message);
            Assert.AreEqual(true, result.Success);
        }

    }

}
