using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{


    public class ExtendEventStatusSettings : BaseModel
    {
        public int OrderNo { get; set; }
        public string ColorCode { get; set; }
        public bool IsSms { get; set; }
        public bool IsLogging { get; set; }
        public bool IsEmail { get; set; }
        // ..
    }
}



