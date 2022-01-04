using RitmaFlexPro.Message.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RitmaFlexPro.Message.Result
{
    public delegate ValidationResult ValidationDelegate<T>(T request) where T:BaseRequest;
    public class Validator
    {
   
        List<Task<ValidationResult>> list = new List<Task<ValidationResult>>();
        Dictionary<ValidationDelegate<BaseRequest>, BaseRequest> dict = new Dictionary<ValidationDelegate<BaseRequest>, BaseRequest>();

        public ValidationResult FirstValidation { get; set; }

   
     
        public TaskResult ValidateTask(object data)
        {

            foreach (Task<ValidationResult> item in list)
            {
                item.RunSynchronously();
                if (!item.Result.Success)
                    return TaskResult.Invalid(item.Result);
            }
            return TaskResult.Successful(data);
        }
        public TaskResult Validate(object data)
        {

            foreach (var item in dict)
            {
               ValidationResult r= item.Key.Invoke(item.Value);
                if (!r.Success)
                    return TaskResult.Invalid(r);
            }
            return TaskResult.Successful(data);
        }


    


    }

}
