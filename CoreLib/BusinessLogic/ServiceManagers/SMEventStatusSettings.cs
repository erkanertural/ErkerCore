using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    [ExtendWith(typeof(ExtendEventStatusSettings))]
    public class SMEventStatusSettings : AbstractServiceManager<FeatureValue, ViewFeatureValue,BaseModel, ExtendEventStatusSettings>
    {
        public SMEventStatusSettings()
        {
            Target = Features.EventStatusSettings;
        }
       
    }
}
