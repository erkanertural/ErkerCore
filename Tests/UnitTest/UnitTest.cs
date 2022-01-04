using ErkerCore.Entities;

using ErkerCore.Message.Request;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Model;
using ErkerCore.DataAccessLayer;
using ErkerCore.Message.Result;
using System.Collections.Generic;
using ErkerCore.Library.Enums;
using ErkerCore.Library;
using System.Net.Http;
using Newtonsoft.Json;
using System.Transactions;
using System;
using ErkerCore;
using ErkerCore.BusinessLogic.Helpers;
using System.Linq;
using ErkerCore.View;
using ErkerCore.Message.Helper;
using System.Diagnostics;
using System.Text;
using System.Linq.Expressions;
using ErkerCore.Message.Response;
using ErkerCore.Message;
using System.IO;

namespace ErkerCoreUnitTest
{
    [TestClass]
    public class UnitTest : BaseTest
    {
        [TestMethod]
        public void CustomTest()
        {
           

            var sm = new SMEventStatus().GetListView(new BaseRequestT<ViewFeatureEventStatus> { Module = FeatureModule.Service, FeatureId = Features.EventStatus.ToInt64(), OwnerId = 1 , Entity= new ViewFeatureEventStatus()});
            Debug.WriteLine(sm.Data.Count.ToString());


        }
    }
}
