using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System.Collections.Generic;
using System.Linq;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductTranslate : AbstractServiceManager<FeatureValueTranslate, BaseView, ModelProductTranslate, ExtendNothing>
    {
        public override Response<long> Create(BaseRequestT<FeatureValueTranslate, BaseView, ModelProductTranslate, ExtendNothing> request)
        {
            SMFeatureValueTranslate tr = new SMFeatureValueTranslate();
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request, Message.Helper.ValidationOperationType.Create);
            if (vlr.Success)
            {
                BaseRequestT<FeatureValueTranslate> rc = new BaseRequestT<FeatureValueTranslate>();
                List<AttributeInfo<TranslateAttribute>> attrs = request.Model.GetAttr<TranslateAttribute>();
                foreach (AttributeInfo<TranslateAttribute> item in attrs)
                {
                    rc.KeyValue.Add(item.Attribute.TranslateKey.ToInt64(), item.PropertiesValue);
                }
                rc.Table = FeatureTable.Product;
                return tr.Create(new BaseRequestT<FeatureValueTranslate, BaseView, BaseModel, ExtendNothing>(request) { KeyValue = rc.KeyValue });
            }
            else
                return Response<long>.Fail(vlr);
        }
        public override Response<ModelProductTranslate> GetModel(BaseRequestT<FeatureValueTranslate> request)
        {
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Get);
            if (vlr.Success)
            {
                List<FeatureValueTranslate> list = ContextDataManager.GetList<FeatureValueTranslate>(o => o.PrimaryId == request.Id && o.FeatureTableId == FeatureTable.Product.ToInt64() && o.FeatureId == Features.TranslateKey.ToInt64() && o.FeatureLanguageId == request.LangCode, request);
                ModelProductTranslate model = new ModelProductTranslate();
                List<AttributeInfo<TranslateAttribute>> attrs = model.GetAttr<TranslateAttribute>();
                foreach (AttributeInfo<TranslateAttribute> item in attrs)
                {
                    FeatureValueTranslate fvt = list.FirstOrDefault(o => o.FeatureTranslateKeyId == item.Attribute.TranslateKey.ToInt64());
                    if (fvt != null)
                        model.SetPropValue(item.FullName, fvt.TranslatedText);

                }
                return Response<ModelProductTranslate>.Successful(model);
            }
            else
                return Response<ModelProductTranslate>.Fail(vlr);
        }


    }
}
