using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;

namespace RitmaFlexPro.Message.Request
{
    public class RequestBankAccount : BaseRequest
    {
        // for querying
        public FeatureAdres FeatureAddressType { get; set; }
        public ContactBankAccount BankAccount { get; set; }


        public Adres Adres { get; set; }
    }
}