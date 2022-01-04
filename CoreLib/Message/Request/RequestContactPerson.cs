using RitmaFlexPro.Entities;

namespace RitmaFlexPro.Message.Request
{
    public class RequestContactPerson : BaseRequest
    {
        public ContactPerson ContactPerson { get; set; }
    }
}
