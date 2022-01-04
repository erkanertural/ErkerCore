using ErkerCore.Entities;
using ErkerCore.Library;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.View
{
    [TestableEntity]
    public partial class ViewContactRelationType : BaseView, IFeature
    {
        public override string Name { get; set; }
        public string Key { get; set; }
        public long ContactId { get; set; }
        public long FeatureId { get; set; }
  
    }
}
