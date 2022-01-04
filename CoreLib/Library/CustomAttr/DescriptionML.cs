using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library
{
    public class DescriptionML : DescriptionAttribute
    {
        public string DescriptionEn { get; set; }
        public DescriptionML(string descriptionTr,string descriptionEn="")
        {
            this.DescriptionValue = descriptionTr;
            this.DescriptionEn = descriptionEn;
        }
    }
}
