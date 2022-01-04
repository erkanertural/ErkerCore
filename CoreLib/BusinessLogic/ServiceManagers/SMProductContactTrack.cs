using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMProductContactTrack : AbstractServiceManager<FeatureValue, BaseView,BaseModel, ExtendContactProductTrack>
    {
        public SMProductContactTrack()
        {
            Target = Features.ProductContactTrack;
        }
    }
}