using ErkerCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ErkerCore.Library;
namespace ErkerCore.DataAccessLayer
{
   public static class DataLayerExtension
    {

        public static Expression<Func<T, bool>> AndIsDeleted<T>( this Expression<Func<T, bool>> expression, bool includeIsDeleted) where T :new() 
        {
            ICanSoftDelete softDeleteAble = new T() as ICanSoftDelete;
            if (softDeleteAble != null && includeIsDeleted == false)
                expression = expression.AndAlso<T>(x => ((ICanSoftDelete)x).IsDeleted == false);
            return expression;
        }

        public static Expression<Func<T, bool>> AndOnlyOwnerId<T>(this Expression<Func<T, bool>> expression, long ownerId) where T : new()
        {
            IContactable contactable = new T() as IContactable;
            if (contactable != null && ownerId>0)
                expression = expression.AndAlso<T>(x => ((IContactable)x).ContactId == ownerId ||( (IContactable)x).ContactId == -1   );
            return expression;
        }
    }
}
