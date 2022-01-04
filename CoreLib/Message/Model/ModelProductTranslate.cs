using ErkerCore.Entities;
using ErkerCore.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ModelProductTranslate : BaseModel
    {

        [Translate(Library.Enums.FeatureTranslateKey.ProductName)]
        public string ProductName { get; set; }

        [Translate(Library.Enums.FeatureTranslateKey.ProductDescription)]
        public string ProductDescription { get; set; }


        [Translate(Library.Enums.FeatureTranslateKey.ProductSeoKeywords)]
        public string ProductSeoHeader { get; set; }

        [Translate(Library.Enums.FeatureTranslateKey.ProductSeoDescription)]
        public string ProductSeoDescription { get; set; }

        [Translate(Library.Enums.FeatureTranslateKey.ProductSeoTitle)]
        public string ProductSeoTitle { get; set; }
    }
}
