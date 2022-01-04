using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErkerCore.Entities
{
    public interface ICanExtraData
    {
        public string ExtraData { get; set; }
        public void SetExtra();
    }
    public interface ICanExtraData<T> : ICanExtraData where T:new()
    {
      
    }
}
