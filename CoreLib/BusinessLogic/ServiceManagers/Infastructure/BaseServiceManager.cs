using ErkerCore.DataAccessLayer;
using ErkerCore.Entities;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.BusinessLogic.Managers
{
    public class BaseServiceManager : AbstractServiceManager<BaseEntity,BaseView, BaseModel, ExtendNothing>
    {
        public BaseServiceManager()
        {
            
        }
        static BaseServiceManager s=null;
        public static BaseDataManager<MainDbContext> DataManager
        {

            get
            {
                if (s == null)
                    s = new BaseServiceManager();
                return s.ContextDataManager;
              
             }
        }
        public static BaseServiceManager ServiceManager
        {

            get
            {
                if (s == null)
                    s = new BaseServiceManager();
                return s;

            }
        }

    }

}
