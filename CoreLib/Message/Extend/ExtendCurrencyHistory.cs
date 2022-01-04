using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ExtendCurrencyHistory : BaseModel
    {
        public float Sell { get; set; }
        public float Buy { get; set; }
        public DateTime CurrencyCheckDateTime { get; set; }
    }

}



