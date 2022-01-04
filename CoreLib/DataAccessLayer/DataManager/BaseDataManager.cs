using ErkerCore.Library.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using ErkerCore.Entities;

using ErkerCore.Library;

using ErkerCore.Message.Request;

using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;
using ErkerCore.Message.Response;
using ErkerCore.DataAccessLayer.Helper;

namespace ErkerCore.DataAccessLayer
{
    public class BaseDataManager<C> : IConnectionInfo, IDataManager<C> where C : DbContext, new()
    {

        //static BaseDataManager curr = null;
        //static BaseDataManager currLog = null;
        List<EntityHistory> entityHistoryCache = new List<EntityHistory>();
        public BaseDataManager()
        {

        }
        public BaseDataManager(string connectionStr)
        {

            Context = new C();
            (Context as IConnectionInfo).ConnectionString = connectionStr;
        }
        //public static BaseDataManager GetInstance()
        //{
        //    if (curr == null)
        //    {
        //        curr = new BaseDataManager(new RflxProDbContext());
        //    }
        //    return curr;
        //}
        //public static BaseDataManager GetInstanceForLog()
        //{
        //    if (currLog == null)
        //    {
        //        currLog = new BaseDataManager(new RflxProLogDbContext());
        //    }
        //    return currLog;
        //}

        #region Properties
        public List<BaseDetail> GetAllEntityAndView()
        {
            List<BaseDetail> tables = new List<BaseDetail>();
            string current = "";
            Assembly[] a = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.Contains("Entities")).ToArray();
            int index = 0;
            foreach (Type item in a[0].GetTypes())
            {
                TestableEntityAttribute attr = item.GetCustomAttribute<TestableEntityAttribute>();
                if (attr != null)
                {
                    try
                    {
                        BaseEntity ent = Activator.CreateInstance(item) as BaseEntity;
                        if (ent != null)
                        {
                            current = ent.GetType().Name;
                            tables.Add(new BaseDetail { Name = item.Name, Value = ent.GetType(), Id = index });
                            index++;
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                }

            }
            return tables;
        }

        public long UserId { get; set; }
        public C Context { get; set; }
        public List<EntityHistory> EntityHistoryCache
        {
            get
            {
                return entityHistoryCache;
            }
            private set
            {
                entityHistoryCache = value;
            }
        }

        public string ConnectionString { get; set; }

        public int ExecuteSql(string sql)
        {
            return Context.Database.ExecuteSqlRaw(sql);
        }
        public T ExecuteScalar<T>(string table, string column, string conditionColumn, object conditionValue)
        {
            conditionValue = conditionValue is string ? "'" + conditionValue.ToString() + "'" : conditionValue;
            string sql = string.Format("select  {0}  from  {1}   where {2}", column, table, conditionColumn == "" ? "" : conditionColumn + "=" + conditionValue + "");
            return ExecuteScalar<T>(sql);
        }

        public T ExecuteScalar<T>(string sql)
        {
            IDbConnection conn = Context.Database.GetDbConnection();
            using (IDbCommand command = conn.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                if (conn.State != ConnectionState.Open)
                    Context.Database.OpenConnection();

                using (IDataReader result = command.ExecuteReader())
                {
                    if (result.Read())
                        return (T)(result.GetValue(0) == DBNull.Value ? 0 : result.GetValue(0));
                }
            }
            return default(T);
        }
        public virtual long GetSequenceNumber(string sequenceName)
        {
            return Context.Database.ExecuteSqlRaw("select next value for " + sequenceName);
        }

        #endregion

        #region Get
        public virtual DataTable GetDataTable(string sql)
        {
            IDbConnection conn = Context.Database.GetDbConnection();
            using (IDbCommand command = conn.CreateCommand())
            {
                command.CommandText = sql;
                IDataAdapter adap = new SqlDataAdapter(command as SqlCommand);
                DataSet dt = new DataSet();
                adap.Fill(dt);
                return dt.Tables[0];
            }
        }
        public int GetCount<T>(Expression<Func<T, bool>> expression, BaseRequest request) where T : class, IUnique, new()
        {
            return WhereNoTracking<T>(DeletedAndOwner(expression, request)).Count();
        }
        public IQueryable<T> WhereNoTracking<T>(Expression<Func<T, bool>> expression) where T : class, IUnique, new()
        {
            return Context.Set<T>().AsNoTracking().Where(expression);
        }
        public Expression<Func<T, bool>> DeletedAndOwner<T>(Expression<Func<T, bool>> exp, BaseRequest request) where T : new()
        {
            Expression<Func<T, bool>> newExp = exp.AndIsDeleted<T>(request.IncludeDeletedRecord).AndOnlyOwnerId<T>(request.OwnerId);
            return newExp;
        }

        public Response<ListPagination<T>> GetListWithSql<T>(string sql) where T : struct
        {
            try
            {
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ListPagination<T> result = new ListPagination<T>();
                    dt.Rows.OfType<DataRow>().Select(o => (T)o[0]).ToList().ForEach(o => result.Add(o));

                    return Response<ListPagination<T>>.Successful(result);
                }
                else
                    return Response<ListPagination<T>>.Fail(null);
            }
            catch (Exception ex)
            {
                return Response<ListPagination<T>>.InternalServerError(ex);
            }
        }

        public ListPagination<T> GetList<T>(Expression<Func<T, bool>> expression, BaseRequest request, List<Expression<Func<T, object>>> orders = null) where T : class, IUnique, new()
        {

            orders = new List<Expression<Func<T, object>>> { { o => o.Id } };
            if (request != null && request.Pagination != null)
            {

                request.Pagination.PageNo++;
                if (request.Pagination.OrderByDescending)
                {
                    ListPagination<T> result = new ListPagination<T>(WhereNoTracking<T>(DeletedAndOwner(expression, request)).OrderByDescending(orders[0]).Skip(request.Pagination.PageSize * (request.Pagination.PageNo - 1)).Take(request.Pagination.PageSize).ToList());
                    result.ForEach(o => (o as BaseEntity).SetSourceFromBackend());
                    if (request.Pagination.TotalCount <= 0)
                        result.TotalCount = GetCount<T>(o => true, request);
                    result.PageNo = request.Pagination.PageNo;
                    result.PageSize = request.Pagination.PageSize;

                    result.HasMoreData = (request.Pagination.PageSize * (request.Pagination.PageNo + 1)) != result.TotalCount && result.Count == request.Pagination.PageSize;
                    if (result.TotalCount > 0)
                        result.TotalPage = result.TotalCount / request.Pagination.PageSize;
                    return result;
                }
                else
                {
                    // 236 
                    ListPagination<T> result = new ListPagination<T>(WhereNoTracking<T>(DeletedAndOwner(expression, request)).OrderBy(orders[0]).Skip(request.Pagination.PageSize * (request.Pagination.PageNo - 1)).Take(request.Pagination.PageSize).ToList());
                    result.ForEach(o => (o as BaseEntity).SetSourceFromBackend());
                    if (request.Pagination.TotalCount <= 0)
                        result.TotalCount = GetCount<T>(o => true, request);
                    result.PageNo = request.Pagination.PageNo;
                    result.PageSize = request.Pagination.PageSize;
                    result.HasMoreData = (request.Pagination.PageSize * (request.Pagination.PageNo + 1)) != result.TotalCount && result.Count == request.Pagination.PageSize;
                    if (result.TotalCount > 0)
                        result.TotalPage = result.TotalCount / request.Pagination.PageSize;
                    return result;
                }
            }
            else if (request.AllRecord)
            {
                return new ListPagination<T>(WhereNoTracking<T>(expression).ToList());
            }
            else
            {

                ListPagination<T> result = new ListPagination<T>(WhereNoTracking<T>(DeletedAndOwner(expression, request)).OrderBy(orders[0]).Take(101).ToList());


                result.ForEach(o => (o as BaseEntity).SetSourceFromBackend());

                if (result.Count > 100)
                    throw new Exception("Maksimum 100 adet kayıt pagination olmadan çekilebilir..Data almak pagination bilgileri vererek yeniden deneyiniz");
                return result;



            }
        }


        public T Get<T>(long id) where T : class, IUnique, new()
        {

            return Get<T>(x => x.Id == id);
        }
        public BaseEntity Get(Type t, long id)
        {

            BaseEntity r = Context.Find(t, id) as BaseEntity;


            if (r != null)
            {
                r.SetSourceFromBackend();
                Context.Entry(r).State = EntityState.Detached;
            }
            return r;

        }
        public BaseEntity Get(BaseEntity ent)
        {
            return Get(ent.GetType(), ent.Id);
        }
        public T Get<T>(Expression<Func<T, bool>> expression) where T : class, IUnique, new()
        {
         
            // todo: isdeleted lar otomatik eklenecek

            T ent = WhereNoTracking(expression).FirstOrDefault(expression);
            if (ent != null)
            {
                if (ent is BaseEntity)
                    (ent as BaseEntity).SetSourceFromBackend();
                //         Context.Entry(ent).State = EntityState.Detached;
                return ent;
            }
            else return default(T);
        }
        public T Get<T, H>(Expression<Func<T, bool>> expression, Expression<Func<T, H>> order, bool desc = false) where T : class, IUnique, new()
        {
            if (desc == false)
            {
                T ent = WhereNoTracking<T>(expression).OrderBy(order).FirstOrDefault();
                if (ent is BaseEntity)
                    (ent as BaseEntity).SetSourceFromBackend();
                return ent;
            }
            else
            {
                T ent = WhereNoTracking<T>(expression).OrderByDescending(order).FirstOrDefault();
                if (ent is BaseEntity)
                    (ent as BaseEntity).SetSourceFromBackend();
                return ent;
            }
        }
        #endregion
        #region Add
        public T Add<T>(T entity) where T : BaseEntity
        {
            if (entity.Id.IsDefaultValue())
                entity.Id = 0;
            Context.Entry(entity).State = EntityState.Added;
            Context.Set<T>();
            if (entity is ICanExtraData)
                (entity as ICanExtraData).SetExtra();
            Context.Add(entity);

            Context.SaveChanges();
            entity.SetSourceFromBackend();
            Context.Entry(entity).State = EntityState.Detached;


            AddEntityHistoryToCache(entity, OperationType.C);
            return entity;

        }
        public BaseEntity Add(BaseEntity entity, long userId, string description = null)
        {
            Add<BaseEntity>(entity);
            entity.SetSourceFromBackend();
            return entity;
        }
        #endregion
        #region Update


        public T Update<T>(T entity, bool softDelete = false) where T : BaseEntity, IUnique, new()
        {
            try
            {
                // todo: update sonrası duplicate hatası 
                Context.Set<T>();
                Context.ChangeTracker.Clear();
                ICanSoftDelete ican = entity as ICanSoftDelete;
                if (entity is ICanExtraData)
                    (entity as ICanExtraData).SetExtra();
                if (softDelete && ican != null)
                    (entity as ICanSoftDelete).IsDeleted = true;
                if (entity.GetSourceFromBackend() == false)
                {

                    T ent = Get<T>(entity.Id);
                    if (ent != null)
                    {
                        Context.ChangeTracker.Clear();
                        entity.CloneGeneric(ent, onlyNotDefaultValue: true);
                        Context.Update(ent);
                    }
                    else
                    {
                        entity.IsProcessedOnDB = false;
                        return entity;
                    }
                }
                else
                {
                    Context.Update(entity);
                    entity.IsProcessedOnDB = false;


                }
                entity.AffectedRowsCount = Context.SaveChanges();
                entity.IsProcessedOnDB = true;
                Context.Entry(entity).State = EntityState.Detached;
                // todo: if (aktif bir transaction varsa cache ekle yoksa DİREKT DATABASE E ÇAK LOGU)
                AddEntityHistoryToCache(entity, (softDelete ? OperationType.S : OperationType.U));

                return entity;
            }
            catch (Exception ex)
            {
                entity.IsProcessedOnDB = false;
                entity.OperationDescription = ex.Message + "\r\n \r\n \t Detail : " + ex.InnerException.Message;
                return entity;
            }

        }
        #endregion
        #region Delete
        public void Delete<T>(long id) where T : BaseEntity, IUnique, new()
        {
            Delete<T>(new T() { Id = id });
        }
        public void Delete<T>(T entity) where T : BaseEntity, IUnique, new()
        {
            if (entity != null)
            {
                try
                {
                    Context.Set<T>().Remove((T)entity);
                    Context.SaveChanges();
                    entity.IsProcessedOnDB = true;
                    AddEntityHistoryToCache(entity, OperationType.D);
                }
                catch (Exception ex)
                {

                    entity.IsProcessedOnDB = false; ;
                }

            }
        }
        public T DeleteSoft<T>(T entity) where T : BaseEntity, IUnique, new()
        {
            return Update<T>(entity, true);
        }

        public void AddEntityHistoryToCache(IUnique entity, OperationType operationType)
        {
            //todo: description eklenecek.
            EntityHistoryCache.Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = operationType.ToString(), UserId = UserId, InsertDateTime = DateTime.Now, EntityObject = entity.Serialize(), OperationDescription = "Entity.OperationDescription" });
        }
        #endregion


        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(Context);
            GC.SuppressFinalize(this);
        }


    }
}