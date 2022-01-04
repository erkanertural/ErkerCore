using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMContactEmbezzled : AbstractServiceManager<FeatureValue, BaseView,BaseModel, ExtendEmbezzled>
    {
        public SMContactEmbezzled() { Target = Features.ContactEmbezzled; }
    }
}