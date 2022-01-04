using ErkerCore.Library.Enums;
using System;
using System.Collections.Generic;

namespace ErkerCore.Message.Model
{
    public class ExtendEmploymentEntry : BaseModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public FeatureEmploymentType EmploymentType { get; set; }
        public FeatureWorkPosition WorkPosition { get; set; }
        public List<long> InterviewerId { get; set; }
        public long RecruiterId { get; set; }
        public string Wage { get; set; }
        public FeatureResignType ResignType { get; set; }
        public string ResignReason { get; set; }
        public long PeriodsOfNotice { get; set; }
        public string Compansation { get; set; }
        public IsLogic IsActive { get; set; }
    }
}



