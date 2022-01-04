using RitmaFlexPro.Entities;

namespace RitmaFlexPro.TempEntities
{
    public partial class RMAction : BaseEntity
    {
        public string ActionKey { get; set; }
        public long ComponentId { get; set; }
    }
}