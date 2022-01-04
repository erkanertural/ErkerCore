using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;

namespace ErkerCore.Message.Model
{
    public class ExtendContactBank : BaseModel
    {
        [Validate(ValidationTypeEnum.IdCheck, FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero)]
        public long FeatureBankId { get; set; }
        public string BankName { get; set; }
        public string BankLogo { get; set; }
        [Validate(ValidationTypeEnum.StringNullOrEmpty, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string BranchName { get; set; }
        [Validate(ValidationTypeEnum.StringNullOrEmpty, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string BranchCode { get; set; }
        [Validate(ValidationTypeEnum.StringNullOrEmpty, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Iban { get; set; }
        [Validate(ValidationTypeEnum.StringNullOrEmpty, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string CurrencyType { get; set; }
        [Validate(ValidationTypeEnum.StringNullOrEmpty, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string AccountNo { get; set; }

    }
}
