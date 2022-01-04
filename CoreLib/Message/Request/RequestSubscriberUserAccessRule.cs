using RitmaFlexPro.Entities;

namespace RitmaFlexPro.Message.Request
{
    public class RequestSubscriberUserAccessRule :BaseRequest
    {
        public SubscriberUserAccessRule SubscriberUserAccessRule { get; set; }
    }
}
