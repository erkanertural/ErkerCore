using ErkerCore.Library;
using ErkerCore.Library.Enums;
using System;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class ViewContactPerson : BaseView, IContactable, ICanSoftDelete, IFormatableValue
    {
        public long ContactId { get; set; }
        public string Gender { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public long ImageFileDocumentId { get; set; }
        public string FilePath { get; set; }
        public string Value { get; set; }
        public long AdresId { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public IsLogic IsOutSource { get; set; }

    }
}