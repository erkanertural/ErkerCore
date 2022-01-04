using ErkerCore.Entities;
using ErkerCore.Message.Request;
using ErkerCore.Message.Response;
using ErkerCore.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.BusinessLogic.Helpers
{
   public interface IFeatureValueTreeView
    {
        public Response<List<FeatureValue>> GetTreeview(BaseRequest request);
      
    }
}
