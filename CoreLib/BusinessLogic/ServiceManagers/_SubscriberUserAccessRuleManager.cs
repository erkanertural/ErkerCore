using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Response;
using RitmaFlexPro.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _SubscriberUserAccessRuleManager: BaseManager<SubscriberUserAccessRule,RequestSubscriberUserAccessRule,BaseRequest,RequestSubscriberUserAccessRule,RequestBaseSearch>
    {


        public TaskResult GetSubscriberUserAccessRule(RequestBaseSearch request)
        {
            try
            {
                Expression<Func<SubscriberUserAccessRule, bool>> exp = (x => false);
                if (request.Id > -1)
                    exp = (x => x.Id == request.Id);
                else if (request.Value.Length > 2 && request.FieldName == "Name")
                    exp = (x => x.IpAddress.StartsWith(request.Value) && x.IsDeleted == false);
                List<SubscriberUserAccessRule> list = DataManager.GetList<SubscriberUserAccessRule, object>(exp, x => x.IpAddress, request.PageNo, request.PageSize);
                return TaskResult.Successful(new ResponseList<SubscriberUserAccessRule>(list));
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult GetSubscriberUserAccessRuleById(BaseRequest request)
        {
            try
            {
                return TaskResult.Successful(new BaseResponse() { Entity = DataManager.Get<SubscriberUserAccessRule>(request.Id) });
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult AddSubscriberUserAccessRule(RequestSubscriberUserAccessRule request)
        {
            try
            {
                DataManager.Add<SubscriberUserAccessRule>(request.SubscriberUserAccessRule);
                return TaskResult.Successful();
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult UpdateSubscriberUserAccessRule(RequestSubscriberUserAccessRule request)
        {
            try
            {

                DataManager.Update(request.SubscriberUserAccessRule);
                return TaskResult.Successful();
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
    }
}
