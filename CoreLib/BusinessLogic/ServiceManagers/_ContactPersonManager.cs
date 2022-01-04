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
    public class _ContactPersonManager : BaseManager<ContactPerson,RequestContactPerson,BaseRequest,RequestContactPerson,RequestBaseSearch>
    {


        public TaskResult GetContactPerson(RequestBaseSearch request)
        {
            try
            {
                Expression<Func<ContactPerson, bool>> exp = (x => false);
                if (request.Id > -1)
                    exp = (x => x.Id == request.Id);
                else if (request.Value.Length > 2 && request.FieldName == "Name")
                    exp = (x => x.Name.StartsWith(request.Value) && x.IsDeleted == false);
                List<ContactPerson> list = DataManager.GetList<ContactPerson, object>(exp, x => x.Name, request.PageNo, request.PageSize);
                return TaskResult.Successful(new ResponseList<ContactPerson>(list));
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult GetContactPersonById(BaseRequest request)
        {
            try
            {
                return TaskResult.Successful(new BaseResponse() { Entity = DataManager.Get<ContactPerson>(request.Id) });
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult AddContactPerson(RequestContactPerson request)
        {
            try
            {
                DataManager.Add<ContactPerson>(request.ContactPerson);
                return TaskResult.Successful(new BaseResponse { Id = request.ContactPerson.Id });
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public  TaskResult UpdateContactPerson(RequestContactPerson request)
        {
            try
            {
                DataManager.Update(request.ContactPerson);
                return TaskResult.Successful();
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
    }
}
