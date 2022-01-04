using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ErkerCore.Library.Enums;
using ErkerCore.Library;
using ErkerCore.Entities;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ErkerCore.Entities
{
   

    public class BaseEntity : IUnique
    {
        public BaseEntity()
        {
            this.SetAllPropertiesToDefaultValue();
        }


        #region Properties

        [NotMapped]
        public dynamic Extended { get; set; }

        public long Id { get; set; }
        public virtual string Name { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int AffectedRowsCount { get; set; }
        [NotMapped]
        [JsonIgnore]
        public bool IsProcessedOnDB { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string OperationDescription { get; set; }



        [NotMapped]
        [JsonIgnore]
        private bool IsSourceFromBackend { get; set; }
        public void SetSourceFromBackend(bool value = true)
        {
            this.IsSourceFromBackend = value;
        }
        public bool GetSourceFromBackend()
        {
            return this.IsSourceFromBackend;
        }
        #endregion







    }
}