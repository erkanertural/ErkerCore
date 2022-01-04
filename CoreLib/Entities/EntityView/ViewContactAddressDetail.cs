using ErkerCore.Entities;
using ErkerCore.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.View
{
    [TestableEntity]
    public partial class ViewContactAddressDetail : BaseView, ICanSoftDelete
    {
        public long AddressId { get; set; }
        public long ContactId { get; set; }
        public override string Name { get; set; }
        public long FeatureCountryId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public long FeatureCityId { get; set; }
        public string District { get; set; }
        public long FeatureDistrictId { get; set; }
        public string FullAddress { get; set; }
        public string ZipCode { get; set; }
        public int FeatureTableId { get; set; }
        public bool IsDeleted { get; set; }
    }
}