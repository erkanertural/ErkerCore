using System;
namespace ErkerCore.Library
{
    public class AttributeInfo<T> 
    {
        public T Attribute { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public object PropertiesValue { get; set; }
        public Type DataType { get; set; }
  

        public override string ToString()
        {
            return $"{ FullName}-> {PropertiesValue} : {DataType.Name}";
        }
        //public T CurrentAttribute
        //{
        //    get
        //    {
        //        this.Attribute != null
        //            return (T)this.Attribute;
        //        else
        //            return Attribute2;
        //    }

        //}
    }

  


}
