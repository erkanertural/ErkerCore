using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;

namespace RitmaFlexPro.Message.Request
{
    public class RequestLicense : BaseRequest
    {
        public FeatureLicenseType FeatureLicenseType { get; set; }
        public License License { get; set; }
    }
}
