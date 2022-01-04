using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library.Helpers
{
    public interface IParentChild<T>
    {
        public List<T> Childs { get; set; }
        public long ParentId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

    }
    public interface IParentChildCustomUnique

    {
        public string UniqueChildKey { get; }
        public string UniqueParentKey { get; }
    }
}
