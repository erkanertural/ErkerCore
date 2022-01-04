using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.Entities;
using System;
using System.Collections.Generic;
using RitmaFlexPro.Message.Model;
using RitmaFlexPro.View;

namespace RitmaFlexPro.Message.Request
{
    public class RequestContact : BaseRequestT<Contact, ViewContactDetail, ModelContact>
    {
        public Adres Adres { get; set; }
    }
}