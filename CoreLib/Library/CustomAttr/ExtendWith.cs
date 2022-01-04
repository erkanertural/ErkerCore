using ErkerCore.Library.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library
{
    public class ExtendWithAttribute : Attribute
    {
        public Type Type{ get; set; }
        public ExtendWithAttribute(Type type)
        {
            Type = type;
        }
    }
}
