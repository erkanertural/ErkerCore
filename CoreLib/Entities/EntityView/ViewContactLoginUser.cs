using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.View
{
    [Table("viewContactPersonUser")]
    [TestableEntity]
    public class ViewContactLoginUser : BaseView
    {
        public long ContactId { get; set; }
        public string Title { get; set; }
        public long RoleId { get; set; }
        public int IsAdmin { get; set; }
        public string Email { get; set; }
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Username { get; set; }
        public string NameSurname { get; set; }
        public string MobilePhone1 { get; set; }
        public string MobilePhone2 { get; set; }
        public int IsLoginable { get; set; }
        [HashAttribute]
        [Validate(ValidationTypeEnum.ValueCheck, FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull)]
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }



    }
}
