using ErkerCore.Entities;
using ErkerCore.View;
using System.Collections.Generic;

namespace ErkerCore.Message.Model
{
    public class ModelContactPerson : BaseModel
    {
        public ContactPerson ContactPerson { get; set; }
        public Adres Adres { get; set; }
    }
}