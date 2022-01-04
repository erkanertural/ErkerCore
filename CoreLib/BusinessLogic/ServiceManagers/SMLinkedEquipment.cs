using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMLinkedEquipment : AbstractServiceManager<FeatureValue, ViewFeatureLinkedEquipment, BaseModel, ExtendNothing>
    {
        public SMLinkedEquipment()
        {
            Target = Features.LinkedEquipment;
        }
    }
}