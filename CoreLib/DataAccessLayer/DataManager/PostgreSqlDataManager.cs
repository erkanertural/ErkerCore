using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.DataAccessLayer
{
    public class PostgreSqlDataManager<T> : BaseDataManager<MainDbContext> where T:DbContext,new()
    {
        public PostgreSqlDataManager(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public override long GetSequenceNumber(string sequenceName)
        {
            return base.GetSequenceNumber(sequenceName);
        }
        public override DataTable GetDataTable(string sql)
        {
            IDbConnection conn =  Context.Database.GetDbConnection();
            using (IDbCommand command = conn.CreateCommand())
            {
                command.CommandText = sql;
                IDataAdapter adap = null;// new MySqlDataAdapter(command as MySqlCommand);
                DataSet dt = new DataSet();
                adap.Fill(dt);
                return dt.Tables[0];
            }
        }

    }
}
