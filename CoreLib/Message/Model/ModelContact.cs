using ErkerCore.Entities;
using ErkerCore.View;
using System.Collections.Generic;

namespace ErkerCore.Message.Model
{
    public class ModelContact : BaseModel
    {
        public Contact Contact { get; set; }
        public Adres Adres { get; set; }
        public List<ViewContactDetail> Contacts { get; set; }
        public List<ExtendContactBank> BankAccount { get; set; }
        public List<ViewContactPersonUser> ContactPerson { get; set; }
        public List<ViewContactPersonUser> ContactLoginUser { get; set; }
        public List<ViewContactAddressDetail> AdresView { get; set; }
        public ContactPerson DefaultAuthority { get; set; }
        public ViewContactAddressDetail DefaultAddress { get; set; }
        public string SectorName { get; set; }
        public string EmployeeCountInfo { get; set; }
        public string TaxOfficeName { get; set; }
        public double Points { get; set; }
        /// <summary>
        /// It is represent to the 'AKTİF' on the FrontEnd
        /// </summary>
        public bool IsTransactionValid { get; set; }

    }
}