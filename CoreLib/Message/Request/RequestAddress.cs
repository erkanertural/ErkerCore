using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;

namespace RitmaFlexPro.Message.Request
{
    public class RequestAddress : BaseRequest
    {
        // for querying
        public FeatureAddress FeatureAddressType { get; set; }
        public Adres Address { get; set; }
    }
}