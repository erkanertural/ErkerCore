using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.DataAccessLayer.Helper
{
    public interface IConnectionInfo
    {
        public string ConnectionString { get; set; }
    }
}
