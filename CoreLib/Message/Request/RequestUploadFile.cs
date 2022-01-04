using ErkerCore.Library.Enums;
using ErkerCore.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ErkerCore.Message.Request
{
    public class RequestFileUpload : BaseRequest
    {
        public object File { get; set; }
        public FileDocument Document { get; set; }

    }
}