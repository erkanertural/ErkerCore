using RitmaFlexPro.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitmaFlexPro.TempEntities
{
    public class RMRoleModule : BaseEntity
    {
        [NotMapped]
        public override string Name { get; set; }
        public long ModuleId { get; set; }
        public long RoleId { get; set; }
    }
}