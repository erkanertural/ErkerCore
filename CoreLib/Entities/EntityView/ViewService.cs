using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.View
{
    public partial class ViewService : BaseView, ICanSoftDelete
    {
        [TestableEntity]
        public long Id { get; set; }
        [NotMapped]
        public override string Name { get; set; }
        public string KitNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerStatement { get; set; }
        public DateTime FailureReportingDateTime { get; set; }
        public string FailureCause { get; set; }
        public string WorkOrderType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
