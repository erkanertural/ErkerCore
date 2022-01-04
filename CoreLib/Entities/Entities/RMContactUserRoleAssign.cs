using System;
using System.Collections.Generic;



namespace RitmaFlexPro.TempEntities
{
    public partial class RMContactUserRoleAssign : RitmaFlexPro.Entities.BaseEntity
    {
        public long ContactUserId { get; set; }
        public long RoleId { get; set; }
        public string CustomActionIds { get; set; }
        public string Type { get; set; }
    }
}
