using RitmaFlexPro.Entities;

namespace RitmaFlexPro.TempEntities
{
    public partial class UserRoleActions : BaseEntity
    {
        public long UserId { get; set; }
        public long ActionId { get; set; }
        public string ActionKey { get; set; }
        public string ActionName { get; set; }
        public bool IsShowOnForm { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsGetRecord { get; set; }
        public bool IsPrint { get; set; }
        public bool IsExecute { get; set; }
        public bool IsShowOnMenu { get; set; }
        public string Type { get; set; }
    }
}