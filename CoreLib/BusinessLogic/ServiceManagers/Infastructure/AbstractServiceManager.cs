using Microsoft.EntityFrameworkCore;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.DataAccessLayer;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Library.Validator;
using ErkerCore.Message.Helper;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ErkerCore.BusinessLogic.Managers
{



    // C R  E  S
    public abstract class AbstractServiceManager<Entity, View, UIModel, Extend> : IFeatureValueTreeView, IManagerValidation<Entity, View, UIModel, Extend>, IDisposable, IManagerRequest<Entity, View, UIModel, Extend> where Entity : BaseEntity, IUnique, new() where UIModel : class, IUnique, new() where View : BaseView, IUnique, new()



    {
        /*
        readonly IWebHostEnvironment env;
        public SMFeatureValue(IWebHostEnvironment env)
        {

            this.env = env;

        } */

        List<CustomValidation> cval = new List<CustomValidation>();
        public AbstractServiceManager()
        {

            UserId = 5; // todo:Jwt den bulunacak ve set edilecek
            SetDefaultDataManagers();
            Target = Features.Uncertain;
        }

        public BaseRequestT<Entity, View, UIModel, Extend> CreateRequest { get; set; }
        public BaseRequestT<Entity, View, UIModel, Extend> RequestEdit { get; set; }
        public BaseRequestT<View> RequestGetListView { get; set; }
        public BaseRequestT<Entity> RequestGetListModel { get; set; }
        public BaseRequestT<Entity> RequestGetModel { get; set; }
        public BaseRequestT<View> RequestGetView { get; set; }
        public BaseRequestT<UIModel> RequestGetViewModel { get; set; }
        public BaseRequestT<Entity> RequestGetList { get; set; }
        public BaseRequestT<Entity> RequestGet { get; set; }
        public BaseRequestT<Entity> RequestRemove { get; set; }


        public void AddCustomValidation(Expression<Func<object, bool>> func, FeatureValidationErrorEnum error, Expression<Func<object, object>> field, ValidationOperationType validationOperationType)
        {
            cval.Add(new CustomValidation { Expr = func, ErrorEnum = error, Column = field, OperationType = validationOperationType });
        }

        public virtual void SetDefaultDataManagers()
        {

            ContextDataManager = new MsSqlDataManager<MainDbContext>(BusinessUtility.ConnectionString);
            LogContextDataManager = new MsSqlDataManager<MainLogDbContext>(BusinessUtility.ConnectionStringLog);
            FileContextDataManager = new MsSqlDataManager<MainFileDbContext>(BusinessUtility.ConnectionStringFile);
        }
        public long UserId { get; private set; }
        public Features Target { get; set; }
        public BaseDataManager<MainDbContext> ContextDataManager { get; set; }
        protected BaseDataManager<MainLogDbContext> LogContextDataManager { get; set; }

        protected BaseDataManager<MainFileDbContext> FileContextDataManager { get; set; }

        #region CRUDS




        public virtual ValidationResult ValidationCreate(BaseRequestT<Entity, View, UIModel, Extend> request)
        {
            CreateRequest = request;
            ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Create);
            if (vlr.Fail)
                return vlr;
            return vlr.Successful();
        }
        public virtual Response<long> Create(BaseRequestT<Entity, View, UIModel, Extend> request)
        {

            try
            {




                ValidationResult vlr = ValidationCreate(request);
                if (vlr.Success)
                {

                    if (request.Entity is Feature)
                    {
                        (request.Entity as Feature).Key = (request.Entity as Feature).FeatureId.NameFromValue<Features>() + "->" + request.Entity.Name;

                    }
                    else if (request.Entity != null && request.Entity is ContactPersonAccount)
                    {
                        (request.Entity as ContactPersonAccount).Password = (request.Entity as ContactPersonAccount).Password.ComputeSHA256();

                    }
                    else if (request.Entity is FeatureValue && Target.ToInt64() > -1)
                    {

                        FeatureValue fv = request.Entity as FeatureValue;
                        fv.FeatureId = Target.ToInt64();
                        fv.Key = Target.ToString() + "->" + request.Entity.Name;
                        fv.FeatureTableId = FeatureTable.Uncertain;
                        fv.ContactId = request.OwnerId;
                        // todo: sadece default olmayanlar ( değişenler dbye kaydedilmeli)


                    }
                    if (request.SaveExtend && request.Entity != null && request.Extend != null && request.Entity is IFormatableValue)
                        (request.Entity as IFormatableValue).Value = request.Extend.Serialize();
                    ContextDataManager.Add(request.Entity);
                    return Response<long>.Successful(request.Entity.Id);
                }
                else
                    return Response<long>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<long>();
            }
        }

        public virtual ValidationResult ValidationRemoveSoft(BaseRequestT<Entity> request)
        {
            RequestRemove = request;
            return this.CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Remove);
        }
        public virtual Response<bool> RemoveSoft(BaseRequestT<Entity> request)
        {
            try
            {
                ValidationResult vlr = ValidationRemove(request);
                if (vlr.Success)
                {
                    Entity entity = null;
                    if (!request.Entity.GetSourceFromBackend())
                        entity = ContextDataManager.Get<Entity>(o => o.Id == request.Id);
                    entity = request.Entity;
                    if (entity != null)
                    {
                        ContextDataManager.DeleteSoft(entity);
                        if (request.Entity.IsProcessedOnDB)
                            return Response<bool>.Successful(true);
                        else
                            return Response<bool>.Fail(vlr.Failure(FeatureValidationErrorEnum.UpdateError, ""));

                    }
                    else
                        return Response<bool>.NotFound();
                }
                else
                    return Response<bool>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<bool>();
            }
        }

        public virtual ValidationResult ValidationRemove(BaseRequestT<Entity> request)
        {
            return this.CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Remove);
        }

        public virtual Response<bool> Remove(BaseRequestT<Entity> request)
        {
            try
            {
                RequestRemove = request;
                ValidationResult vlr = ValidationRemove(request);
                if (vlr.Success)
                {

                    ContextDataManager.Delete<Entity>(request.Id);
                    return Response<bool>.Successful(true);


                }
                else
                    return Response<bool>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<bool>();
            }
        }

        public virtual ValidationResult ValidationEdit(BaseRequestT<Entity, View, UIModel, Extend> request)
        {
            RequestEdit = request;
            return this.CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Edit);

        }
        public virtual Response<bool> Edit(BaseRequestT<Entity, View, UIModel, Extend> request)
        {
            try
            {
                ValidationResult vlr = ValidationEdit(request);
                if (vlr.Success)
                {
                    if (request.Entity != null && request.Entity is ContactPersonAccount)
                    {
                        (request.Entity as ContactPersonAccount).Password = (request.Entity as ContactPersonAccount).Password.ComputeSHA256();

                    }
                    if (Target.ToInt64() > -1 && request.Entity is FeatureValue)
                    {
                        FeatureValue fv = request.Entity as FeatureValue;
                        fv.Key = Target.ToString() + "->" + request.Entity.Name;
                        fv.FeatureTableId = FeatureTable.Uncertain;
                        fv.ContactId = request.OwnerId;
                        if (request.SaveExtend && request.Extend != null)
                            fv.Value = request.Extend.Serialize();
                    }
                    if (request.SaveExtend && request.Entity != null && request.Extend != null && request.Entity is IFormatableValue)
                        (request.Entity as IFormatableValue).Value = request.Extend.Serialize();
                    ContextDataManager.Update(request.Entity);
                    if (request.Entity.IsProcessedOnDB)
                        return Response<bool>.Successful(true);
                    else
                        return Response<bool>.Fail(vlr.Failure(FeatureValidationErrorEnum.UpdateError, ""));
                }
                else
                    return Response<bool>.Fail(vlr);

            }
            catch (Exception ex)
            {
                return ex.Throw<bool>();
            }
        }

        public virtual ValidationResult ValidationGet(BaseRequestT<Entity> request)
        {
            RequestGet = request;
            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
            if (request.Entity is FeatureValue)
            {
                FeatureValue fv = ContextDataManager.Get<FeatureValue>(request.Id);
                if (vlr.IsInvalid(o => fv == null, FeatureValidationErrorEnum.NotFoundRecord, p => request.Id).Fail)
                    return vlr;
                if (Target.ToInt64() > -1 && vlr.IsInvalid(o => Target.ToInt64() != fv.FeatureId, FeatureValidationErrorEnum.InvalidValueError, p => fv.FeatureId).Fail)
                    return vlr;
                vlr.Cache.Add("entity", fv);
                return vlr.Successful();
            }
            else
                return vlr;

        }

        public virtual Response<Entity> Get(BaseRequestT<Entity> request)
        {
            try
            {
                RequestGet = request;
                Entity entity = null;
                ValidationResult vlr = ValidationGet(request);
                if (vlr.Success)
                {

                    if (new Entity() is BaseFeatureValue)
                        entity = vlr.Cache["entity"] as Entity;
                    else
                    {

                        entity = ContextDataManager.Get<Entity>(o => request.Id == o.Id);
                    }
                    SetModel(entity);

                    return Response<Entity>.Successful(entity);
                }
                else
                    return Response<Entity>.Fail(vlr);
            }
            catch (Exception ex)
            {
                return ex.Throw<Entity>();
            }
        }

        public virtual ValidationResult ValidationGetList(BaseRequestT<Entity> request)
        {
            RequestGetList = request;

            ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Listing);
            if (vlr.Fail)
                return vlr;
            if (Target.ToInt64() > -1 && request.Entity is FeatureValue && vlr.IsInvalid(o => Target.ToInt64() != request.FeatureId, FeatureValidationErrorEnum.InvalidValueError, p => request.FeatureId).Fail)
                return vlr;

            return vlr.Successful();

        }
        public virtual Response<ListPagination<Entity>> GetList(BaseRequestT<Entity> request)
        {
            try
            {
                ValidationResult vlr = ValidationGetList(request);
                if (vlr.Success)
                {
                    Expression<Func<Entity, bool>> exp = BasicFilter<Entity>(request);

                    List<Entity> result = ContextDataManager.GetList<Entity>(exp, request);
                    if (new Entity() is IFormatableValue)
                        foreach (Entity item in result)
                            SetModel(item);

                    return Response<ListPagination<Entity>>.Successful(new ListPagination<Entity>(result));
                }
                else
                    return Response<ListPagination<Entity>>.Fail(vlr);

            }
            catch (Exception ex)
            {
                return ex.Throw<ListPagination<Entity>>();
            }
        }
        public virtual ValidationResult ValidationGetListModel(BaseRequestT<Entity> request)
        {
            RequestGetListModel = request;
            return this.CheckAuthorizeAndAttributeValidation(request, ValidationOperationType.Get);
        }
        public virtual Response<ListPagination<UIModel>> GetListModel(BaseRequestT<Entity> request)
        {
            throw new Exception();
            //try
            //{
            //    ValidationResult vlr = ValidationGetListModel(request);
            //    if (vlr.Success)
            //    {
            //        Expression<Func<Entity, bool>> exp = BasicFilter<Entity>(request);
            //        if (Target.ToInt64() > -1 && new Entity() is IFeature)
            //        {
            //            exp = exp.AndAlso(o => (o as IFeature).FeatureId == Target.ToInt64());

            //        }
            //        List<Entity> fvlist = ContextDataManager.GetList<Entity>(exp, request);
            //        var modelList = fvlist.Select(o => new { id = o.Id, model = (o as IFeatureExt).Value.DeSerialize<UIModel>() }).ToList();
            //        ListPagination<UIModel> result = new ListPagination<UIModel>();
            //        modelList.ForEach(o => { o.model.Id = o.id; result.Add(o.model); });
            //        return Response<ListPagination<UIModel>>.Successful(result);
            //    }
            //    else
            //        return Response<ListPagination<UIModel>>.Fail(vlr);
            //}
            //catch (Exception ex)
            //{
            //    return ex.Throw<ListPagination<UIModel>>();
            //}
        }
        public virtual ValidationResult ValidationGetView(BaseRequestT<View> request)
        {
            RequestGetView = request;
            return this.CheckAuthorizeAndAttributeValidation(request);
        }
        public virtual Response<View> GetView(BaseRequestT<View> request)
        {
            try
            {

                ValidationResult vlr = ValidationGetView(request);
                if (vlr.Success)
                {
                    Expression<Func<View, bool>> exp = BasicFilter<View>(request);
                    View result = ContextDataManager.Get<View>(exp);
                    SetModel(result as BaseEntity);
                    return Response<View>.Successful(result);
                }
                else
                    return Response<View>.Fail(vlr);
            }
            catch (Exception ex)
            {

                return ex.Throw<View>();
            }
        }

        public virtual ValidationResult ValidationGetListView(BaseRequestT<View> request)
        {
            RequestGetListView = request;
            ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request);
            return vlr;
        }
        // todo: model için request içinde custom expression parametre olarak ayarlanacak ?
        public virtual Response<ListPagination<View>> GetListView(BaseRequestT<View> request)
        {
            try
            {
                ValidationResult vlr = ValidationGetListView(request);
                if (vlr.Success)
                {

                    Expression<Func<View, bool>> exp = BasicFilter<View>(request);
                    ListPagination<View> result = ContextDataManager.GetList<View>(exp, request);


                    if (new View() is IFormatableValue)
                        foreach (View item in result)
                            SetModel(item);
                    return Response<ListPagination<View>>.Successful(result);
                }
                else
                    return Response<ListPagination<View>>.Fail(vlr);

            }
            catch (Exception ex)
            {

                return ex.Throw<ListPagination<View>>();
            }
        }
        public void SetModel(BaseEntity item)
        {
            if (item != null)
            {
                string data = (item as IFormatableValue)?.Value;

                bool valid = data.IsNullOrEmpty() == false && data.Contains("{");
                if (valid)
                {
                    Extend obj = (Extend)data.DeSerialize(typeof(Extend));
                    item.Extended = obj;
                    item.Extended.Id = item.Id;
                }
            }
        }

        public virtual ValidationResult ValidationGetModel(BaseRequestT<Entity> request)
        {
            RequestGetModel = request;
            return CheckAuthorizeAndAttributeValidation(request);
        }

        private Expression<Func<T, bool>> BasicFilter<T>(BaseRequestT<T> request) where T : IUnique
        {

            Expression<Func<T, bool>> exp = (o => true);
            if (request.ExprFunc != null)
            {
                Expression<Func<T, bool>> expC = (Expression<Func<T, bool>>)request.ExprFunc;
                if (expC != null)
                {
                    exp = exp.AndAlso(expC);
                }
            }

            else if (request.Id > -1)
                exp = (x => x.Id == request.Id);
            else if (request.Entity is IFeature && request.FeatureId > 0 && request.Module.ToInt64() > 0)
                exp = (o => (o as IFeature).FeatureId == request.FeatureId && (o as IFeatureExt).FeatureModuleId == request.Module.ToInt64());
            else if (request.OwnerId > 0 && request.FeatureId > 0)
                exp = (o => (o as IFeature).FeatureId == request.FeatureId);

            else if (request.Entity is IFeature && request.FeatureId > 0 && (request.Entity as IFeatureExt).PrimaryId > 0)
                exp = (o => (o as IFeature).FeatureId == request.FeatureId && (o as IFeatureExt).PrimaryId == (request.Entity as IFeatureExt).PrimaryId);


            else if (!request.Value.IsNullOrEmpty() && request.Value.Length > 2 && request.FieldName == "Name" && request.Entity is IFeature && request.Entity is ICanSoftDelete)
                exp = (o => o.Name.StartsWith(request.Value) && (o as IFeature).FeatureId == request.FeatureId && (o as ICanSoftDelete).IsDeleted == false);
            else if (Target.ToInt64() > -1 && new Entity() is IFeature && Target.ToInt64() != Features.TaxOffice.ToInt64())
            {
                exp = exp.AndAlso(o => (o as IFeature).FeatureId == Target.ToInt64());

            }
            else if (request.Entity != null && request.Entity is IContactable && (request.Entity as IContactable).ContactId < 0 && request.Entity is IFeature && request.FeatureId > 0)
                exp = (o => (o as IFeature).FeatureId == request.FeatureId);

            return exp;
        }

        public virtual Response<UIModel> GetModel(BaseRequestT<Entity> request)
        {
            throw new NotImplementedException();
        }



        #endregion



        public ValidationResult CheckAuthorizeAndAttributeValidation(BaseRequest request, ValidationOperationType operationType = ValidationOperationType.Uncertain)
        {
            // todo : remove için entity kısmı dahil edilecek.

            ValidationResult vlr = ValidationResult.GetInstance(request);
            vlr.TranslateMethod = BusinessUtility.GetErrorTranslatedText;
            if (request == null)
                return vlr.Invalid(FeatureValidationErrorEnum.RequestCannotBeNull, new AttributeInfo<Validate> { Name = "Request" });

            List<AttributeInfo<Validate>> attr = request.GetAttr<Validate>();

            if (!IsAuthority(request.UserId, request.FlexAction)) // yetkilendirme kontrolleri burada yapılacak.
                return vlr.NotAuthorize();

            if (operationType == ValidationOperationType.Create || operationType == ValidationOperationType.Edit)
            {

                if (cval.Count > 0)
                    foreach (var item in cval.Where(o => o.OperationType == ValidationOperationType.Create))
                    {
                        if (vlr.IsInvalid(item.Expr, item.ErrorEnum, item.Column).Fail)
                            return vlr;
                    }
                foreach (AttributeInfo<Validate> item in attr)
                {

                    if (item.PropertiesValue.IsDefaultValue())// item.DataType))
                        continue;
                    Validate validate = item.Attribute as Validate;
                    //stringnullor empty
                    if (validate.ValidationType == ValidationTypeEnum.StringNullOrEmpty && item.PropertiesValue != null && string.IsNullOrEmpty(item.PropertiesValue?.ToString()))
                        return vlr.Invalid(validate.ReturnValidation, item);


                    if (validate.ValidationType == ValidationTypeEnum.IdCheck && item.PropertiesValue != null && item.PropertiesValue.ToInt64() <= 0)
                        return vlr.Invalid(validate.ReturnValidation, item);
                    if (validate.ValidationType == ValidationTypeEnum.MinCharLength && (item.PropertiesValue == null || item.PropertiesValue.ToString().Length < validate.Min))
                        return vlr.Invalid(validate.ReturnValidation, item);
                    if (validate.ValidationType == ValidationTypeEnum.MaxCharLength && item.PropertiesValue != null && item.PropertiesValue.ToString().Length > validate.Max)
                        return vlr.Invalid(validate.ReturnValidation, item);
                    //between charlength
                    if (validate.ValidationType == ValidationTypeEnum.Between && item.DataType == typeof(string) && (item.PropertiesValue == null || validate.Min != double.MinValue && validate.Max != double.MaxValue && (item.PropertiesValue.ToString().Length > validate.Max || item.PropertiesValue.ToString().Length < validate.Min)))
                        return vlr.Invalid(validate.ReturnValidation, item);


                    if (validate.ValidationType == ValidationTypeEnum.MaxValue && (item.DataType == typeof(int) || item.DataType == typeof(long) || item.DataType == typeof(double) || item.DataType == typeof(float) || item.DataType == typeof(byte)) && item.PropertiesValue != null && item.PropertiesValue.ToInt64() > validate.Max)
                        return vlr.Invalid(validate.ReturnValidation, item);
                    if (validate.ValidationType == ValidationTypeEnum.MinValue && (item.DataType == typeof(int) || item.DataType == typeof(long) || item.DataType == typeof(double) || item.DataType == typeof(float) || item.DataType == typeof(byte)) && item.PropertiesValue != null && item.PropertiesValue.ToInt64() < validate.Min)
                        return vlr.Invalid(validate.ReturnValidation, item);

                    //between double
                    if (validate.ValidationType == ValidationTypeEnum.Between && (item.DataType == typeof(int) || item.DataType == typeof(long) || item.DataType == typeof(double) || item.DataType == typeof(float) || item.DataType == typeof(byte)) && item.PropertiesValue != null && validate.Min != double.MinValue && validate.Max != double.MaxValue && (item.PropertiesValue.ToDoubleTry() > validate.Max || item.PropertiesValue.ToDoubleTry() < validate.Min))
                        return vlr.Invalid(validate.ReturnValidation, item);




                    if (validate.ValidationType == ValidationTypeEnum.MinValue && item.DataType == typeof(DateTime) && item.PropertiesValue != null && validate.DateType != DateTypeEnum.Uncertain)
                    {
                        DateTime mindate = validate.CalculatedMinDate;
                        DateTime propDate = DateTime.Parse(item.PropertiesValue.ToString());
                        if (propDate < mindate)
                            return vlr.Invalid(validate.ReturnValidation, item);
                    }
                    if (validate.ValidationType == ValidationTypeEnum.MaxValue && item.DataType == typeof(DateTime) && item.PropertiesValue != null && validate.DateType != DateTypeEnum.Uncertain)
                    {
                        DateTime maxdate = validate.CalculatedMaxDate;
                        DateTime propDate = DateTime.Parse(item.PropertiesValue.ToString());
                        if (propDate > maxdate)
                            return vlr.Invalid(validate.ReturnValidation, item);
                    }
                    // between
                    if (validate.ValidationType == ValidationTypeEnum.Between && item.DataType == typeof(DateTime) && item.PropertiesValue != null && !validate.MaxDate.IsNullOrEmpty())
                    {
                        DateTime maxdate = validate.CalculatedMaxDate;
                        DateTime mindate = validate.CalculatedMinDate;
                        DateTime propDate = DateTime.Parse(item.PropertiesValue.ToString());
                        if (propDate < mindate || propDate > maxdate)
                            return vlr.Invalid(validate.ReturnValidation, item);
                    }

                    //valuecheck

                    if (validate.ValidationType == ValidationTypeEnum.ValueCheck && item.PropertiesValue == validate.UnwantedValue)
                        return vlr.Invalid(validate.ReturnValidation, item);

                    if ((validate.UnwantedValue != item.PropertiesValue) && item.PropertiesValue?.ToString() == validate.UnwantedValue?.ToString())
                        return vlr.Invalid(validate.ReturnValidation, item);

                }
            }
            else if (operationType == ValidationOperationType.Remove)
            {
                return vlr.IsInvalid(o => request.Id <= -1, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero, p => request.Id);

            }
            else if (operationType == ValidationOperationType.Listing)
            {
                AttributeInfo<Validate> at = attr.FirstOrDefault(o => o.FullName == "Entity");
                if (at != null)
                {
                    string Entity = "";
                    return vlr.IsInvalid(o => at.PropertiesValue == null, FeatureValidationErrorEnum.ObjectCannotBeNull, p => Entity);
                }
            }



            return vlr.Successful();
        }


        public bool IsAuthority(long userId, FeatureFlexAction flexAction)
        {
            FeatureModule module = Library.ExtensionUtility.FindModule(flexAction);

            return true || UserId == 0 && flexAction == FeatureFlexAction.Uncertain;


        }
        public Response<long> RunWithTransactionScope(Task<Response<long>> task)
        {



            // Transaction  System...
            //
            using (TransactionScope ent = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    task.RunSynchronously();
                    using (TransactionScope logT = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        foreach (EntityHistory item in ContextDataManager.EntityHistoryCache)
                        {
                            LogContextDataManager.Add(item);


                        }
                        //   int a = "f".ToInt32();
                        logT.Complete();
                        this.ContextDataManager.EntityHistoryCache.Clear();
                    }
                    ent.Complete();
                    return task.Result;

                }
                catch (Exception ex)
                {
                    return ex.Throw<long>();
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="createRequest"></param>
        /// <param name="removeRequest"></param>
        /// <param name="editRequest"></param>
        /// <param name="searchRequest"></param>
        /// <remarks>Eklenen Id otomatik olarak diğer request parametre değerlerine Set edilmektedir.</remarks>
        /// <returns></returns>
        public Response<bool> TestAllInOneCrud(BaseRequestT<Entity, View, UIModel, Extend> createRequest, BaseRequestT<Entity> removeRequest, BaseRequestT<Entity, View, UIModel, Extend> editRequest, BaseRequestT<Entity> getRequest, BaseRequestT<Entity> searchRequest)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                string message = "";
                Response<long> taskAdd = Create(createRequest);
                if (taskAdd.Success)
                {
                    editRequest.Id = taskAdd.Data;
                    editRequest.Entity.SetSourceFromBackend();
                    editRequest.Entity.Id = taskAdd.Data;
                    Response<bool> taskEdit = Edit(editRequest);
                    if (taskEdit.Failure)
                        return Response<bool>.FailWithMessage("Edit X :" + taskEdit.GenerateTestDebugMessage());
                    getRequest.Id = taskAdd.Data;
                    Response<Entity> taskGet = Get(getRequest);
                    if (taskGet.Failure || taskGet.Data == null)
                        return Response<bool>.FailWithMessage("Get X :" + taskGet.GenerateTestDebugMessage());
                    searchRequest.Id = taskAdd.Data;
                    Response<ListPagination<Entity>> taskSearch = GetList(searchRequest);
                    if (taskSearch.Failure || taskSearch.Data == null)
                        return Response<bool>.FailWithMessage("Search X :" + taskSearch.GenerateTestDebugMessage());
                    removeRequest.Id = taskAdd.Data;
                    Response<bool> taskRemove = Remove(removeRequest);
                    if (taskRemove.Failure)
                        return Response<bool>.FailWithMessage("Remove X :" + taskRemove.GenerateTestDebugMessage());

                }
                else
                {
                    return Response<bool>.FailWithMessage("Create X :" + taskAdd.GenerateTestDebugMessage());

                }

                message = "✓✓✓ Mükemmel -> " + taskAdd.Data + " nolu kayıt eklenip silindi.";
                ts.Complete();
                return Response<bool>.Successful(true, message: message);


            }
        }

        public virtual Response<List<FeatureValue>> GetTreeview(BaseRequest request)
        {

            List<FeatureValue> result = ContextDataManager.GetList<FeatureValue>(o => o.FeatureId == Target.ToInt64(), request);
            return Response<List<FeatureValue>>.Successful(result.Traverse(TraverseResult.ComboTreeview, request.ValueNumber.ToInt32()));
        }

        public void Dispose()
        {
            ContextDataManager.Dispose();
            LogContextDataManager.Dispose();
            FileContextDataManager.Dispose();

        }


    }
}
