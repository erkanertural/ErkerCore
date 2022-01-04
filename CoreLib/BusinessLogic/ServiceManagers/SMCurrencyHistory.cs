using ErkerCore.Entities;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Model;
using ErkerCore.View;

namespace ErkerCore.BusinessLogic.Managers
{
    public class SMCurrencyHistory : AbstractServiceManager<FeatureValue, ViewFeatureCurrencyHistory, BaseModel, ExtendCurrencyHistory>
    {
        public SMCurrencyHistory() { Target = Features.CurrencyHistory; }

    }
}
