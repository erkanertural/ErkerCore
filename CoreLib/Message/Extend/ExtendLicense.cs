using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ExtendLicense: BaseModel 
    {
        public override string Name { get; set; }
        public long SubscriberId { get; set; }
        public string LicenseCode { get; set; }
        public long LicenseType { get; set; }
        public int UserCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IsLogic IsMultipleAccess { get; set; }
        public IsLogic IsTimeLimited { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime LastAccessDate { get; set; }
        public string SubscriberInfo { get; set; }
        public string InstallationPlace { get; set; }
        public int LastAccess30Days { get; set; }
    }

}



