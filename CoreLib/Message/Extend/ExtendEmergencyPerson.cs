using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ExtendEmergencyPerson : BaseModel
    {
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
    }
}



