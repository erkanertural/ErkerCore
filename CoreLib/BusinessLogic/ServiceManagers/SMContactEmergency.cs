using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMContactEmergency : AbstractServiceManager<FeatureValue, BaseView,BaseModel, ExtendEmergencyPerson>
    {
        public SMContactEmergency() { Target = Features.ContactEmergency; }
    }
}