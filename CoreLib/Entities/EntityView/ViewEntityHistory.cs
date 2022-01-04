using ErkerCore.Library.Enums;
using ErkerCore.Entities;
using ErkerCore.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ErkerCore.TempView
{
    public partial class ViewEntityHistory : BaseEntity
    {
        public string TableName { get; set; }
        public long PrimaryId { get; set; }
        public long UserId { get; set; }
        public DateTime DateTime { get; set; }
        public OperationType OperationType { get; set; }
        public string EntityObject { get; set; }
        public string EntityDescription { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string OperationTypeName
        { 
            get {
                return ""; // Extension.DescFromValue<OperationType>(this.OperationType.ToInt32()); 
            }
        }

    }
}
