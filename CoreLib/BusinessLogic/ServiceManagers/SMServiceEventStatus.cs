using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMServiceEventStatus : AbstractServiceManager<FeatureValue, ViewFeatureServiceStatus,BaseModel, ExtendEventStatusSettings>
    {
        public SMServiceEventStatus()
        {
            Target = Features.ServiceStatus;
        }
    }
}