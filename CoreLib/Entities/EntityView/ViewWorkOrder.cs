using ErkerCore.Library;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErkerCore.Entities
{
    [TestableEntity]
    public class ViewWorkOrder : BaseView, IContactable, ICanSoftDelete
    {
        [NotMapped]
        public override string Name { get; set; }
        public long ContactId { get; set; }
        public string WorkOrderType { get; set; }
        public string RelatedContactPerson { get; set; }
        public string LinkedEquipment { get; set; }
        public string ResponsibleContactType { get; set; }
        public string ResponsibleContact { get; set; }
        public string ResponsibleContactPerson { get; set; }
        public string KitNo { get; set; }
        public DateTime FailureReportingDateTime { get; set; }
        public string FailureCause { get; set; }
        public string RelatedContactStatement { get; set; }
        public DateTime ServiceStartDateTime { get; set; }
        public DateTime ServiceEndDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}