using RitmaFlexPro.Entities;
using System;
using System.Collections.Generic;



namespace RitmaFlexPro.TempEntities
{
    public partial class RMRoleActionUser : BaseEntity
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public long ActionId { get; set; }
        public bool IsShowOnForm { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsGetRecord { get; set; }
        public bool IsPrint { get; set; }
        public bool IsExecute { get; set; }
        public bool IsShowOnMenu { get; set; }

    }
}
