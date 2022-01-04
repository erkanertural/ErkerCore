using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Entities
{
    public interface IExtend<T> 

    {
   
        public T Extended { get; set; }

    }
}
