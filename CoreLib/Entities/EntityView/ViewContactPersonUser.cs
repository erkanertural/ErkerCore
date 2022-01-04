using ErkerCore.Entities;
using ErkerCore.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.View
{
    [TestableEntity]
    public class ViewContactPersonUser : BaseEntity
    {
        public long ContactId { get; set; }
        public string Title { get; set; }
        public long RoleId { get; set; }
        public int IsAdmin { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string NameSurname { get; set; }
        public string MobilePhone { get; set; }
        public string MobilePhone1 { get; set; }
        public string MobilePhone2 { get; set; }
        public string ImagePath { get; set; }
        public int IsLoginable { get; set; }

    }
}
