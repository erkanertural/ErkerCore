using ErkerCore.BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCoreUnitTest
{
   public  class BaseTest
    {
        public BaseTest()
        {
            BusinessUtility.IsDevelopment = true;
        }
      
    }
}
