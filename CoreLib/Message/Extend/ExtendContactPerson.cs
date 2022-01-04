using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Library.Enums;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErkerCore.Message.Model
{
    public class ExtendContactPerson :BaseModel
    {
        public long DepartmentId { get; set; }
        public string TCKN { get; set; }
        public FeatureMilitaryService MilitaryService { get; set; }
        public string MilitaryServiceExemption { get; set; }
        public FeatureMaritalStatus MaritalStatus { get; set; }
        public FeatureSpouseJobStatus SpouseJobStatus { get; set; }
        public string SpouseName { get; set; }
        public string ChildrenCount { get; set; }
        public string FixedPhone { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }

        public IsLogic IsDiseased { get; set; }
        public string DiseaseDescription { get; set; }
        public IsLogic HasHealthReport { get; set; }
        public FeatureBloodGroup BloodGroup { get; set; }
        public IsLogic HasContagiousDisease { get; set; }

        public FeatureDisabilityStatus DisabilityStatus { get; set; }
        public float DisabilityPercent { get; set; }
        public string DisabilityDescription { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}