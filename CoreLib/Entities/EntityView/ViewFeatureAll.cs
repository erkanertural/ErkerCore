using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.View
{

    public partial class BaseViewFeatureValue : BaseFeatureValue
    {
        [NotMapped]
        public override long NumericValue { get; set; }
        [NotMapped]
        public override IsLogic IsExtendable { get; set; }

        public override long RelatedPrimaryId { get; set; }
    }
    public partial class ViewFeatureValue : BaseFeatureValue
    {
        [NotMapped]
        public override long NumericValue { get; set; }
        [NotMapped]
        public override IsLogic IsExtendable { get; set; }
        [NotMapped]
        public override long RelatedPrimaryId { get; set; }
    }
    public partial class ViewFeatureProductType : BaseViewFeatureValue { }
    public class ViewFeatureContactEmployeeCount : BaseViewFeatureValue { }
    public class ViewFeatureCurrencyType : BaseViewFeatureValue { }
    public class ViewFeatureDepartment : BaseViewFeatureValue { }
    public class ViewFeatureMeasureOfUnit : BaseViewFeatureValue { }
    public class ViewFeatureProductCategory : BaseViewFeatureValue { }
    public class ViewFeatureSector : BaseViewFeatureValue { }
    public class ViewFeatureContactType : BaseViewFeatureValue { }
    public class ViewFeatureBank : BaseViewFeatureValue { }
    public class ViewFeatureProductSupplier : BaseViewFeatureValue { }
    public class ViewFeatureServiceStatus : BaseViewFeatureValue { }
    public class ViewFeatureWorkOrderType : BaseViewFeatureValue { }
    public class ViewFeatureLinkedEquipment : BaseViewFeatureValue { }
    public class ViewFeatureFailureCause : BaseViewFeatureValue { }
    public class ViewFeatureEmploymentEntry : BaseViewFeatureValue { }
    public class ViewFeatureCurrencyHistory : BaseViewFeatureValue { }
    public class ViewFeatureEventStatus : BaseViewFeatureValue { }
}
