using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ErkerCore.API;
using ErkerCore.API.Helpers;
using ErkerCore.Message.Request;
using ErkerCore.BusinessLogic.Managers;
using ErkerCore.Message.Result;
using ErkerCore.Entities;
using ErkerCore.Message.Response;
using ErkerCore.Message.Model;

namespace ErkerCore.API.Controllers
{    /// <summary>
     /// dsada 
     /// </summary>
     /// <remarks>
     /// <example>
     /// <code>
     /// BaseRequestCreateT«<b>FeatureValueTranslate</b>» rc2 = new BaseRequestCreateT«FeatureValueTranslate»();  <br></br>
     /// request.Entity = new FeatureValueTranslate();                                                            <br></br>
     /// request.Entity.FeatureTableId = FeatureTable.Component.ToInt64();                                        <br></br>
     /// request.Entity.ContactId = request.OwnerId;                                                              <br></br>
     /// request.Entity.PrimaryId = request.Id;                                                                   <br></br>
     /// request.Entity.FeatureLanguageId = request.LangCode;                                                     <br></br>
     /// request.KeyValue = new System.Collections.Generic.Dictionary«object, object» { { 2, "ShortName" } };     <br></br>
     /// request.Entity.TranslatedText= request.Value;    translate değerini yaz                                  <br></br><br></br>
     /// <u>FEATURE VE FEATUREVALUE İÇİN </u>                                                                     <br></br>
     ///                                                                                                           <br></br>
     ///request.Entity.ContactId = request.OwnerId;                                                               <br></br>
     ///request.Entity.PrimaryId = request.Id;                                                                    <br></br>
     ///request.Entity.FeatureLanguageId = request.LangCode;                                                      <br></br>
     ///request.Entity.TranslatedText = request.Value;   "translate değerini yaz";                                <br></br>
     /// </code>
     /// </example>
     /// </remarks>
     /// <param name="request"></param>
     /// <returns></returns>
    public class FeatureValueTranslateController : BaseController<SMFeatureValueTranslate, FeatureValueTranslate,BaseView, BaseModel, ExtendNothing>
    {    }

}