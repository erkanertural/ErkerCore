using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Entities
{
  public  interface IUnique
    {
        public  long Id  { get; set; }
        public  string Name { get; set; }

    }
}
