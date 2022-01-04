using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ExtendBank : BaseModel
    {

        public string LogoUrl { get; set; }
        public string MersisNo { get; set; }
        public override string Description { get; set; }
        public override string Name { get; set; }
        public override object Parent { get; set; }
        public override object Value { get; set; }
        public override long Id { get; set; }
    }
}