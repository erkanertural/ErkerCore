using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library.Validator
{
   public class CustomValidation
    {
        public Expression<Func<object, bool>> Expr { get; set; }
        public FeatureValidationErrorEnum ErrorEnum { get; set; }
        public Expression< Func<object,object>> Column { get; set; }

        public ValidationOperationType OperationType { get; set; }
    }
}
