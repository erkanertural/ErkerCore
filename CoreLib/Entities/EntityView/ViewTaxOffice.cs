using ErkerCore.Entities;
using ErkerCore.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.View
{
    [TestableEntity]
    public partial class ViewTaxOffice : BaseView
    {
        public string Town { get; set; }
        public string City { get; set; }
        public long CityId { get; set; }
        public long TownId { get; set; }
    }
}
