using RitmaFlexPro.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


using RitmaFlexPro.Message.Response;
using RitmaFlexPro.Message;
using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Result;
using RitmaFlexPro.DataAccessLayer;
using System.Reflection;
using RitmaFlexPro.Library;

namespace RitmaFlexPro.DataAccessLayer
{
    public static class DataManager 
    {

        #region Base
        public static List<T> GetList<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                return context.Set<T>().AsNoTracking().Where(expression).ToList();
            }
        }

        public static List<T> GetListPercall<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            using (var context = BaseRepository.GetCurrentDbContext())
            {
                return context.Set<T>().AsNoTracking().Where(expression).ToList();
            }
        }

        public static DataTable GetDataTable(string sql)
        {
            //todo:bu satır düzeltilmeli 2
            return null;// OrmADOAccess.GetDataTable(sql);
        }

        public static int GetCount<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                return context.Set<T>().AsNoTracking().Where(expression).Count();
            }
        }

        public static ResponseList<T> GetResponseList<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                return new ResponseList<T>(context.Set<T>().AsNoTracking().Where(expression).ToList());
            }
        }

        public static TaskResult GetResultList<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                List<T> result = context.Set<T>().AsNoTracking().Where(expression).ToList();
                return new TaskResult { Data = new ResponseList<T> { List = result }, Status = TaskStatus.Success };
            }
        }

        public static TaskResult GetResultList<T>(Expression<Func<T, bool>> expression, int pageNo, int pageSize) where T : BaseEntity, new()
        {
            return new TaskResult { Data = GetList(expression, pageNo, pageSize), Status = TaskStatus.Success };
        }

        public static TaskResult GetResultList(string sql)
        {
            try
            {
                //todo:bu satır düzeltilmeli 1
                DataTable dt = null;// OrmADOAccess.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<string> v = dt.Rows.OfType<DataRow>().Select(o => o[0].ToString()).ToList();
                    return new TaskResult { Data = new ResponseList<string> { List = v }, Status = TaskStatus.Success };
                }
                else
                    return new TaskResult { Data = null, Status = TaskStatus.NotFound };
            }
            catch (Exception)
            {
                return new TaskResult { Data = null, Status = TaskStatus.ServerInternalError };
            }
        }

        public static List<T> GetList<T>(string sql) where T : BaseEntity, new()
        {
            //if (typeof(T) == typeof(string))
            //{
            //    using (var context = BaseRepository.GetDbContext<AppDBContext>())
            //    {
            //        return context..FromSqlRaw(sql).Select(o => o as T).ToList();
            //    }
            //}
            return null;
        }

        public static List<T> GetList<T, H>(Expression<Func<T, bool>> expression, Expression<Func<T, H>> order, int pageNo, int count, bool desc = false) where T : BaseEntity, new()
        {

            pageNo++;
            if (pageNo > 0)
                using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
                {
                    if (desc == false)
                        return new List<T>(context.Set<T>().AsNoTracking().Where(expression).OrderBy(order).Skip(count * (pageNo - 1)).Take(count).ToList());
                    else
                        return new List<T>(context.Set<T>().AsNoTracking().Where(expression).OrderByDescending(order).Skip(count * (pageNo - 1)).Take(count).ToList());
                }
            else
            {
                using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
                {
                    if (desc == false)
                        return new List<T>(context.Set<T>().AsNoTracking().Where(expression).OrderBy(order).ToList());
                    else
                        return new List<T>(context.Set<T>().AsNoTracking().Where(expression).OrderByDescending(order).ToList());

                }
            }
        }

        public static ResponseList<T> GetList<T>(Expression<Func<T, bool>> expression, int pageNo, int count) where T : BaseEntity, new()
        {

            pageNo++;
            if (pageNo > 0)
                using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
                {
                    return new ResponseList<T>(context.Set<T>().AsNoTracking().Where(expression).Skip(count * (pageNo - 1)).Take(count).ToList());
                }
            else
                using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
                {
                    return new ResponseList<T>(context.Set<T>().AsNoTracking().Where(expression).ToList());
                }
        }

        public static T Get<T>(long id) where T : BaseEntity
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                T r = context.Set<T>().AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                if (r != null)
                    context.Entry(r).State = EntityState.Detached;
                return r;
            }
        }

        public static BaseEntity Get(Type t, long id)
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                BaseEntity ent = context.Find(t, id) as BaseEntity;
                return ent;
            }
        }

        public static BaseEntity Get(BaseEntity ent)
        {
            return Get(ent.GetType(), ent.Id);
        }

        public static T Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                T r = context.Set<T>().AsNoTracking().Where(expression).FirstOrDefault();
                if (r != null)
                    context.Entry(r).State = EntityState.Detached;
                return r;
            }
        }

        public static T Get<T, H>(Expression<Func<T, bool>> expression, Expression<Func<T, H>> order, bool desc = false) where T : BaseEntity, new()
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                if (desc == false)
                    return context.Set<T>().AsNoTracking().Where(expression).OrderBy(order).FirstOrDefault();
                else
                    return context.Set<T>().AsNoTracking().Where(expression).OrderByDescending(order).FirstOrDefault();
            }
        }

        public static T Add<T>(BaseEntity entity, BaseRequest request = null, long userId = 0) where T : BaseEntity
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                //EntityEntry resp = context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.C.ToString(), UserId = userId, RecordDateTime = DateTime.Now, EntityObject = entity.Serialize(), EntityDescription = request?.EntityDescription });
                context.Set<T>().Add((T)entity);
                context.SaveChanges();
                return (T)entity;
            }
        }

        public static BaseEntity Add(BaseEntity entity, long userId, string description = null)
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                //context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.C.ToString(), EntityObject = entity.Serialize(), UserId = userId, RecordDateTime = DateTime.Now, EntityDescription = description });
                context.Set<BaseEntity>(entity.GetType().Name);
                context.Add(entity);
                //    context.Set<BaseEntity>().Add(entity);

                entity.AffectedRowsCount = 1;
                context.SaveChanges();
                return entity;
            }




            //OrmADOAccess.Context.Database.BeginTransaction();

            //EntityEntry resp = OrmADOAccess.Context.Set<BaseEntity>().Add(entity);
            //OrmADOAccess.Context.SaveChanges();


            //OrmADOAccess.Context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = ((BaseEntity)resp.Entity).Id, OperationType = OperationType.C.ToString(), UserId = userId, RecordDateTime = DateTime.Now, EntityObject = entity.Serialize(), EntityDescription = description });

            //OrmADOAccess.Context.SaveChanges();
            //OrmADOAccess.Context.Database.CommitTransaction();
            //return entity;
        }

        public static BaseEntity UpdateAffectedRows(BaseEntity entity)
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {

                context.Set<BaseEntity>(entity.GetType().Name);
                context.Update(entity);
                entity.AffectedRowsCount = context.SaveChanges();
                return entity;
            }
        }
        public static BaseEntity DeleteSoft(BaseEntity entity, long userId, string description = null)
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                //context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.U.ToString(), EntityObject = entity.Serialize(), UserId = userId, RecordDateTime = DateTime.Now, EntityDescription = description });
                ICanSoftDelete can = entity as ICanSoftDelete;
                if (can != null)
                {
                    context.Set<BaseEntity>(entity.GetType().Name);
                    (entity as ICanSoftDelete).IsDeleted = true;
                    context.Update(entity);
                    entity.AffectedRowsCount = context.SaveChanges();
                    return entity;
                }
                else
                    throw new Exception("");
            }
        }
        public static T DeleteSoft<T>(BaseEntity entity, long userId, string description = null) where T:BaseEntity
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                //context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.U.ToString(), EntityObject = entity.Serialize(), UserId = userId, RecordDateTime = DateTime.Now, EntityDescription = description });
                
                (entity as ICanSoftDelete) .IsDeleted = true;
               context.Set<T>().Update((T)entity);
                entity.AffectedRowsCount = context.SaveChanges();
                return (T)entity;
            }
        }
        public static BaseEntity Update(BaseEntity entity, long userId, string description = null)
        {
            using (var context = BaseRepository.GetDbContext<RflxProDbContext>())
            {
                //context.Set<EntityHistory>().Add(new EntityHistory { TableName = entity.GetType().Name, PrimaryId = entity.Id, OperationType = OperationType.U.ToString(), EntityObject = entity.Serialize(), UserId = userId, RecordDateTime = DateTime.Now, EntityDescription = description });
                context.Set<BaseEntity>(entity.GetType().Name);
                context.Update(entity);
                entity.AffectedRowsCount = context.SaveChanges();
                return entity;
            }
        }



        public static List<BaseDetail> GetAllEntityAndView()
        {
            List<BaseDetail> tables = new List<BaseDetail>();
            string current = "";
            Assembly[] a = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.Contains("Entities")).ToArray();
            int index = 0;
            foreach (var item in a[0].GetTypes())
            {
                bool valid = (item.FullName.Contains("RitmaFlexPro.Entities") | item.FullName.Contains("RitmaFlexPro.View")) & !( (item.Name=="BaseEntity"));
                if (valid)
                {
                    BaseEntity ent = Activator.CreateInstance(item) as BaseEntity;
                    current = ent.GetType().Name;
                    tables.Add(new BaseDetail { Name = item.Name, Value = ent.GetType(),Id= index });
                    index++;
                }
               
            }
            return tables;
        }

        #endregion
    }
}