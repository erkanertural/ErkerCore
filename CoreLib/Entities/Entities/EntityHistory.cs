using ErkerCore.Library;
using System;
namespace ErkerCore.Entities
{
    public partial class EntityHistory : BaseEntity
    {
        public EntityHistory()
        {
            InsertDateTime = DateTime.Now;
            UpdateDateTime = DateTime.Now.DefaultDateTime();
            TableName = "".DefaultString();
            PrimaryId = -1;

        }

        public string TableName { get; set; }
        public long PrimaryId { get; set; }
        public string OperationType { get; set; }
        public string EntityObject { get; set; }



        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public long UserId { get; set; }
    }
}