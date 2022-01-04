using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErkerCore.Entities
{
    public interface IContactable
    {
        public long ContactId{ get; set; }

    }
}
