using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMWorkOrderType : AbstractServiceManager<FeatureValue, ViewFeatureWorkOrderType, BaseModel, ExtendNothing>
    {
        public SMWorkOrderType()
        {
            Target = Features.WorkOrderType;
        }
    }
}