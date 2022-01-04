

using System.Text.Json.Serialization;

namespace ErkerCore.Library
{
    public class BaseDetail
    {
        public virtual long Id { get; set; }
 

        public virtual string Name { get; set; }
        public virtual object Value { get; set; }
      

        public virtual string Description { get; set; }
        public virtual string DescriptionEn { get; set; }

        public virtual object Parent { get; set; }
        public override string ToString()
        {
            return Id + " > " + Name + " > Value =" + Value;
        }
    }
    
}