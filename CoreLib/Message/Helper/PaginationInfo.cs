using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Message
{
    public class PaginationInfo
    {
        public PaginationInfo()
        {
            PageNo = 0;
            PageSize = 100;
        }
        public int PageNo { get; set; } //2
        public int PageSize { get; set; } // 10
        public int TotalPage { get; set; }  //2                         <<<1 |2| 3...101>>
        public int Count { get; set; }//3
        public int TotalCount { get; set; } // 13
        public bool HasMoreData { get;  set; }

        public bool OrderByDescending { get; set; }
    }
}
