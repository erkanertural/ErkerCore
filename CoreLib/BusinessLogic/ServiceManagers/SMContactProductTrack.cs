using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMContactProductTrack : AbstractServiceManager<FeatureValue, BaseView, BaseModel, ExtendContactProductTrack>
    {
        public SMContactProductTrack()
        {
            Target = Features.ContactProductTrack;
        }
    }
}
