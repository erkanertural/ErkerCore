using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ErkerCore.DataAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.DataAccessLayer
{
    public class MsSqlDataManager<C> : BaseDataManager<C> where C : DbContext,new()

    {
        public MsSqlDataManager(string connectionString):base(connectionString)
        {
            ConnectionString = connectionString;
        }
      
    }
}
