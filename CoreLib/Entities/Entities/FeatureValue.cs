using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Library.Helpers;
using ErkerCore.Message.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ErkerCore.Entities
{

 
    public class FeatureValue : BaseFeatureValue
    {

    }

    public class BaseFeatureValue : BaseView, ICanSoftDelete, IParentChild<FeatureValue>, IParentChildCustomUnique, IContactable, IFeatureExt
    {

        public virtual string Key { get; set; }
        public long PrimaryId { get; set; } // record id

        public virtual long ContactId { get; set; }
        public long FeatureModuleId { get; set; }
        public virtual long ParentId { get; set; }
        public virtual long RelatedPrimaryId { get; set; } // join record id
        public virtual long FeatureId { get; set; }
        public virtual FeatureTable FeatureTableId { get; set; }
        public virtual string Value { get; set; }
        public virtual long NumericValue { get; set; }
        public virtual string Description { get; set; }
        public override string Name { get; set; }
        public virtual IsLogic IsExtendable { get; set; }
        public virtual bool IsDeleted { get; set; }
        [NotMapped]

        public virtual List<FeatureValue> Childs { get; set; }
        [NotMapped]

        public virtual string UniqueChildKey { get { return Id + "#" + ContactId; } }
        [NotMapped]
        [JsonIgnore]
        public virtual string UniqueParentKey { get { return ParentId + "#" + ContactId; } }
        [NotMapped]
        public int Level { get; set; }

        public void SetValue()
        {

        }

        public override string ToString()
        {
            return Id + " > " + Name;
        }


    }
}