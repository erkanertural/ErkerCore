using ErkerCore.Library;
using System;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; }
        public DateTime RecordDate { get; set; }
        public int SubscriptionPeriod { get; set; } // Abonelik süresi (Ay olarak)
        public int IsPremium { get; set; }
        public int UserCount { get; set; }
    }
}