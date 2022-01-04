using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ModelContactProduct : BaseModel
    {
        public ViewProduct CurrentProduct { get; set; }
        public List<ViewProduct> SimilarProduct { get; set; }

        public List<ViewProduct> RelatedProduct { get; set; }
        public List<FeatureValue> ProductCategoryList { get; set; }
        public List<FeatureValueTranslate> ProductTranslate { get; set; }

        public List<ViewContactDetail> ProductSupplier { get; set; }

    }
}
