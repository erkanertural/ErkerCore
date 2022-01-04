using Library.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using RitmaFlexPro.DataAccessLayer;
using RitmaFlexPro.DataAccessLayer;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


    namespace RitmaFlexPro.DataAccessLayer
{
        public class TransactionDataManager : IDisposable
        {
            #region Base
            RflxProDbContext context = null;

            public TransactionDataManager(long userId)
            {
                context = BaseRepository.GetDbContext<RflxProDbContext>();
                UserId = userId;
            }

            private long UserId { get; set; }

            public List<T> GetList<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
            {
                return context.Set<T>().AsNoTracking().Where(expression).ToList();
            }

            public ResponsePagination<T> GetList<T>(Expression<Func<T, bool>> expression, int pageNo, int count) where T : BaseEntity, new()
            {
                ResponsePagination<T> res = new ResponsePagination<T>();
                res.List = pageNo > 0 ?
                          res.List = context.Set<T>().AsNoTracking().Where(expression).Skip(count * (pageNo - 1)).Take(count).ToList() : context.Set<T>().AsNoTracking().Where(expression).ToList();
                return res;
            }

            public DbContext Context
            {
                get { return context; }
            }

            public T Get<T>(long id) where T : BaseEntity
            {
                using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
                {
                    T r = context.Set<T>().AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                    if (r != null)
                        context.Entry<T>(r).State = EntityState.Detached;
                    return r;
                }
            }

            public T Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
            {
                using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
                {
                    T r = context.Set<T>().AsNoTracking().Where(expression).FirstOrDefault();
                    if (r != null)
                        context.Entry<T>(r).State = EntityState.Detached;
                    return r;
                }
            }

            public T Add<T>(BaseEntity entity, BaseRequest request = null) where T : BaseEntity
            {
                EntityEntry resp = context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.C.ToString(), UserId = UserId, RecordDateTime = DateTime.Now, EntityObject = entity.Serialize(), EntityDescription = request?.EntityDescription });
                context.Set<T>().Add((T)entity);
                context.SaveChanges();
                return (T)entity;
            }

            public BaseEntity Add(BaseEntity entity, BaseRequest request = null)
            {
                EntityEntry resp = context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.C.ToString(), UserId = UserId, RecordDateTime = DateTime.Now, EntityObject = entity.Serialize(), EntityDescription = request?.EntityDescription });

                context.Set<BaseEntity>().Add(entity);
                context.SaveChanges();
                return entity;
            }

            public BaseEntity Update(BaseEntity entity, BaseRequest request = null)
            {
                EntityEntry resp = context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.U.ToString(), UserId = UserId, RecordDateTime = DateTime.Now, EntityObject = entity.Serialize(), EntityDescription = request?.EntityDescription });

                context.Entry(entity).State = EntityState.Modified;
                context.Set<BaseEntity>().Update(entity);
                context.SaveChanges();
                return entity;
            }

            public void Delete<T>(long id, BaseRequest request = null) where T : BaseEntity
            {
                EntityEntry resp = context.Set<EntityHistory>().Add(new EntityHistory { TableName = typeof(T).Name, PrimaryId = id, OperationType = OperationType.D.ToString(), UserId = UserId, RecordDateTime = DateTime.Now, EntityDescription = request?.EntityDescription });
                context.Set<T>().Remove(Get<T>(id));
                context.SaveChanges();
            }

            public void Delete<T>(T entity, BaseRequest request = null) where T : BaseEntity
            {
                EntityEntry resp = context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.D.ToString(), UserId = UserId, RecordDateTime = DateTime.Now, EntityDescription = request?.EntityDescription });

                context.Set<T>().Remove((T)entity);
                context.SaveChanges();
            }

            public IDbContextTransaction GetTransaction()
            {
                return context.Database.BeginTransaction();
            }

            public void Dispose()
            {
                context.Dispose();
                GC.SuppressFinalize(context);
                GC.SuppressFinalize(this);
            }
            #endregion
        }
    }

