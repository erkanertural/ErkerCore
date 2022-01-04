using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMFailureCause : AbstractServiceManager<FeatureValue, ViewFeatureFailureCause, BaseModel, ExtendNothing>
    {
        public SMFailureCause()
        {
            Target = Features.FailureCause;
        }
    }
}