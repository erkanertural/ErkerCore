using RitmaFlexPro.Entities;
using RitmaFlexPro.Message.Helper;

namespace RitmaFlexPro.Message.Request
{
    public class RequestContactProduct : BaseRequest
    {
        [Validate(ValidationTypeEnum.ValueCheck, Library.FeatureValidationErrorEnum.ObjectCannotBeNull)]
        public Product ContactProduct { get; set; }

    }
}
