namespace RitmaFlexPro.Message.Request
{
    public class RequestLogin : BaseRequest
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}