using ErkerCore.Library.Enums;
using Microsoft.EntityFrameworkCore;
using ErkerCore.Entities;
using ErkerCore.Library;

using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ErkerCore.Message.Response;
using ErkerCore.Message.Request;

namespace ErkerCore.DataAccessLayer
{
    interface IDataManager<C> where C : DbContext
    {

        public int GetCount<T>(Expression<Func<T, bool>> expression,BaseRequest request) where T : class, IUnique, new();

        public ListPagination<T> GetList<T>(Expression<Func<T, bool>> expression, BaseRequest request, List<Expression<Func<T, object>>> orders = null) where T : class, IUnique, new();
        public Response<ListPagination<T>> GetListWithSql<T>(string sql) where T : struct;
        public DataTable GetDataTable(string sql);


        public T Get<T>(long id) where T :class, IUnique,  new();
        public BaseEntity Get(Type t, long id);
        public BaseEntity Get(BaseEntity ent);
        public T Get<T>(Expression<Func<T, bool>> expression) where T : class,IUnique,new();
        public T Get<T, H>(Expression<Func<T, bool>> expression, Expression<Func<T, H>> order, bool desc = false) where T : class, IUnique, new();
        public T Add<T>(T entity) where T : BaseEntity;
        public BaseEntity Add(BaseEntity entity, long userId, string description = null);

        public T Update<T>(T entity,bool softDelete) where T :BaseEntity, IUnique,new();
        public void Delete<T>(long id) where T :BaseEntity, IUnique,new();
        public void Delete<T>(T entity) where T : BaseEntity, IUnique,new();
  
        public T DeleteSoft<T>(T entity) where T :BaseEntity, IUnique,new();
        public void AddEntityHistoryToCache(IUnique entity, OperationType operationType);
        public List<BaseDetail> GetAllEntityAndView();
        public long UserId { get; set; }
        public C Context { get; set; }
        public int ExecuteSql(string sql);
        public T ExecuteScalar<T>(string table, string column, string whereColumn, object whereValue);
        public T ExecuteScalar<T>(string sql);
        public long GetSequenceNumber(string sequenceName);
      
    }

}
