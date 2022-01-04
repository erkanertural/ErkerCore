using ErkerCore.Library.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library
{
    public class TranslateAttribute : Attribute
    {
        public FeatureTranslateKey TranslateKey { get; set; }
        public TranslateAttribute(FeatureTranslateKey translateKey)
        {
            TranslateKey = translateKey;
            
        }
    }
}
