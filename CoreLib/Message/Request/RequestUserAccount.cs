using RitmaFlexPro.Entities;
using RitmaFlexPro.View;

namespace RitmaFlexPro.Message.Request
{
    public class RequestUserAccount : BaseRequest
    {
        public ViewContactPersonUser UserAccount { get; set; }
    }
}