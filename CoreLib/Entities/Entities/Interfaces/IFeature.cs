using ErkerCore.Library.Enums;

namespace ErkerCore.Entities
{
    public interface IFeature : IUnique
    {
        public long FeatureId { get; set; }
       
    }

    public interface IFeatureExt : IFeature,IFormatableValue
    {
    
        public long PrimaryId { get; set; }
        public long ContactId { get; set; }
        public long FeatureModuleId { get; set; }
        public long ParentId { get; set; }
        public long RelatedPrimaryId { get; set; } // join record id
  
    }
}
