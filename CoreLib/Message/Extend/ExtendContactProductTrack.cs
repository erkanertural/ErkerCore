using ErkerCore.Library.Enums;
using System;

namespace ErkerCore.Message.Model
{
    public class ExtendContactProductTrack : BaseModel
    {
        public bool IsRival { get; set; }
        public string ChasisNo { get; set; }
        public string SerialNo { get; set; }
        public string VehiclePlateNo { get; set; }
        public long AddressId { get; set; }
        public DateTime SetupDateTime { get; set; }
        public FeatureContactType ContactType { get; set; }
        public long ContactPersonId { get; set; }
        public DateTime GuaranteeStartDateTime { get; set; }
        public DateTime GuaranteeEndDateTime { get; set; }
    }
}



