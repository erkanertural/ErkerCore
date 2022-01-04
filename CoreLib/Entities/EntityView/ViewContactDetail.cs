using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.View
{
    [TestableEntity]
    public partial class ViewContactDetail : BaseView, ICanSoftDelete
    {
        public long SubscriberId { get; set; }
        public string ShortName { get; set; }
        public string FixedPhone { get; set; }
        public FeatureContactType FeatureContactTypeId { get; set; }
        public string Website { get; set; }
        public string Sector { get; set; }
        public string Email { get; set; }
        public string GeoLocation { get; set; }
        public string ContactType { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string ContactEmployeeCount { get; set; }

        public string DefaultAuthorityPerson { get; set; }
        public string DefaultContactPerson { get; set; }
        [NotMapped]
        public string Color { get; set; }


        public bool IsDeleted { get; set; }
    }
}
