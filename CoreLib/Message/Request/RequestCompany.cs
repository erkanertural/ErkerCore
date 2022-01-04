using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RitmaFlexPro.Message.Request
{
    public class RequestCompany:BaseRequest
    {
        /// <summary>
        /// It is represent  current company on web portal
        /// </summary>
       public long CurrentContactId { get; set; }
    }
}