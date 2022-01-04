using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Helper;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ErkerCore.BusinessLogic.Managers
{

    public interface IManagerRequest<Entity, View, Model, Extend> where Entity : BaseEntity, IUnique, new() where Model : class, IUnique, new() where View : class, IUnique, new()

    {

        public Response<long> Create(BaseRequestT<Entity, View, Model,Extend> request);
        public Response<bool> Edit(BaseRequestT<Entity, View, Model,Extend> request);
        public Response<bool> Remove(BaseRequestT<Entity> request);
        public Response<bool> RemoveSoft(BaseRequestT<Entity> request);
        public Response<Entity> Get(BaseRequestT<Entity> request);
        public Response<View> GetView(BaseRequestT<View> request);
        public Response<Model> GetModel(BaseRequestT<Entity> request);
        public Response<ListPagination<Entity>> GetList(BaseRequestT<Entity> request);
        public Response<ListPagination<View>> GetListView(BaseRequestT<View> request);
        public Response<ListPagination<Model>> GetListModel(BaseRequestT<Entity> request);


    }


    public interface IManagerValidation<Entity, View, Model, Extend>
    {
        public ValidationResult ValidationCreate(BaseRequestT<Entity, View, Model, Extend> request);
        public ValidationResult ValidationEdit(BaseRequestT<Entity, View, Model, Extend> request);
        public ValidationResult ValidationRemove(BaseRequestT<Entity> request);
        public ValidationResult ValidationRemoveSoft(BaseRequestT<Entity> request);

        public ValidationResult ValidationGet(BaseRequestT<Entity> request);
        public ValidationResult ValidationGetView(BaseRequestT<View> request);
        public ValidationResult ValidationGetModel(BaseRequestT<Entity> request);

        public ValidationResult ValidationGetList(BaseRequestT<Entity> request);
        public ValidationResult ValidationGetListView(BaseRequestT<View> request);
        public ValidationResult ValidationGetListModel(BaseRequestT<Entity> request);
    }









}
