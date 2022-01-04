using RitmaFlexPro.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitmaFlexPro.TempEntities
{
    public class RMRoleAction : BaseEntity
    {
        [NotMapped]
        public override string Name { get; set; }
        public string IncludeActionIds { get; set; } // extra action ids on role
        public string ExcludeActionIds { get; set; } // ignore action ids 

        #region Actions
        
        public bool IsCreate { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsShow { get; set; }
        public bool IsExecute { get; set; }
        public bool IsPrint { get; set; }

        #endregion

        #region Relations
        
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long ActionId { get; set; }
        
        #endregion
    }
}