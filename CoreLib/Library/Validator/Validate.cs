using ErkerCore.Library;
using ErkerCore.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ErkerCore.Message.Helper
{
    #region Enums

    public enum ValidationOperationType
    {
        Uncertain = 1,
        Create,
        Edit,
        Remove,
        Get,
        Listing,
        Custom
    }
    public enum ValidationTypeEnum
    {
        Uncertain = -1,
        IdCheck,
        MinCharLength,
        MaxCharLength,
        MaxValue,
        MinValue,
        ValueCheck,
        StringNullOrEmpty,
        Between
    }
    public enum DateTypeEnum
    {
        Uncertain = -1,
        MaxDate,
        MinDate
    }
    public enum TimeTypeEnum
    {
        Uncertain = -1,


        YearMinus,
        DateMinus,
        HourMinus,
        MinuteMinus,
        SecondMinus,


        YearBonus,
        DateBonus,
        /// <summary>
        /// Saat olarak arttırır
        /// </summary>
        HourBonus,
        /// <summary>
        /// Dakika olarak arttırır
        /// </summary>
        MinuteBonus,
        SecondBonus
    }
    #endregion
    public partial class Validate : Attribute
    {
      public const string DateTimeNow = "DateTime.Now";





        public Validate(ValidationTypeEnum type, FeatureValidationErrorEnum val, double min = double.MinValue, double max = double.MaxValue)
        {
     
            DateType = DateTypeEnum.Uncertain;
            MinDateTimeType = TimeTypeEnum.Uncertain;
            MaxDateTimeType = TimeTypeEnum.Uncertain;
            Max = max;
            Min = min;
            ReturnValidation = val;
            ValidationType = type;


            UnwantedValue = "".DefaultString();
        }
        public Validate(ValidationTypeEnum type, FeatureValidationErrorEnum val, object unwandtedValue)
        {


            UnwantedValue = unwandtedValue;
            DateType = DateTypeEnum.Uncertain;
            MinDateTimeType = TimeTypeEnum.Uncertain;
            MaxDateTimeType = TimeTypeEnum.Uncertain;
            Max = double.MaxValue;
            Min = double.MinValue;
            ReturnValidation = val;
            ValidationType = type;

        }
  //      public object PropertyValue { get; set; }
        public Validate(ValidationTypeEnum type, FeatureValidationErrorEnum val, string date, DateTypeEnum dateType, TimeTypeEnum timeType = TimeTypeEnum.Uncertain, int minusBonusOrValueOfDateTime = 0)
        {

            if (dateType == DateTypeEnum.MinDate)
            {
                MinDateTimeType = timeType;
                MinDate = date;
                MinDateMinusOrBonusValueOfDateTime = minusBonusOrValueOfDateTime;
            }
            else
            {
                MaxDateTimeType = timeType;
                MaxDate = date;
                MaxDateMinusOrBonusValueOfDateTime = minusBonusOrValueOfDateTime;
            }
            DateType = dateType;
            ReturnValidation = val;
            ValidationType = type;
            Max = double.MaxValue;
            Min = double.MinValue;
            UnwantedValue = "".DefaultString();
        }
        // between
        public Validate(ValidationTypeEnum type, FeatureValidationErrorEnum val, string minDate, string maxDate, TimeTypeEnum minDate_TimeType, int minDate_minusBonusValueOfDateTime, TimeTypeEnum maxdate_timeType, int maxdate_minusBonusValueOfDateTime)
        {

            DateType = DateTypeEnum.Uncertain;
            MaxDate = maxDate;
            MinDate = minDate;
            MaxDateTimeType = maxdate_timeType;
            MinDateTimeType = minDate_TimeType;
            MinDateMinusOrBonusValueOfDateTime = minDate_minusBonusValueOfDateTime;
            MaxDateMinusOrBonusValueOfDateTime = maxdate_minusBonusValueOfDateTime;

            ReturnValidation = val;
            ValidationType = type;
            UnwantedValue = "".DefaultString();
            if (CalculatedMaxDate < CalculatedMinDate)
                throw new Exception("MaxDateTime cannot be less than MinDateTime !");
        }

        #region Properties

        public double Min { get; set; }
        public double Max { get; set; }
        public string MinDate { get; set; }

        public TimeTypeEnum MaxDateTimeType { get; set; }
        public TimeTypeEnum MinDateTimeType { get; set; }
        public string MaxDate { get; set; }
        public DateTypeEnum DateType { get; set; }
        public int MaxDateMinusOrBonusValueOfDateTime { get; set; }
        public int MinDateMinusOrBonusValueOfDateTime { get; set; }
        public object UnwantedValue { get; set; }
        public FeatureValidationErrorEnum ReturnValidation { get; set; }
        public ValidationTypeEnum ValidationType { get; set; }
        public ValidationOperationType OperationType { get; private set; }

        public DateTime CalculatedMaxDate
        {

            get
            {
                if (MaxDate == null)
                    throw new Exception("Please be sure Min or max to compare OR check the value  ");
                DateTime maxdate = DateTime.MaxValue;
                if (MaxDate == DateTimeNow)
                    maxdate = DateTime.Now;
                else
                    maxdate = DateTime.Parse(MaxDate);
                maxdate = CalculateDateTime(maxdate, MaxDateTimeType, DateTypeEnum.MaxDate);
                return maxdate;
            }
        }

        public DateTime CalculatedMinDate
        {

            get
            {
                if (MinDate == null)
                    throw new Exception("Please be sure Min or max to compare OR check the value  ");
                DateTime mindate = DateTime.MinValue;
                if (MinDate == DateTimeNow)
                    mindate = DateTime.Now;
                else
                    mindate = DateTime.Parse(MinDate);
                mindate = CalculateDateTime(mindate, MinDateTimeType, DateTypeEnum.MinDate);
                return mindate;
            }
        }

        #endregion

        private DateTime CalculateDateTime(DateTime date, TimeTypeEnum timeType, DateTypeEnum dateType)
        {
            switch (timeType)
            {
                case TimeTypeEnum.YearMinus:
                    date = date.AddYears(dateType == DateTypeEnum.MinDate ? -MinDateMinusOrBonusValueOfDateTime : -MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.DateMinus:
                    date = date.AddDays(dateType == DateTypeEnum.MinDate ? -MinDateMinusOrBonusValueOfDateTime : -MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.HourMinus:
                    date = date.AddHours(dateType == DateTypeEnum.MinDate ? -MinDateMinusOrBonusValueOfDateTime : -MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.MinuteMinus:
                    date = date.AddMinutes(dateType == DateTypeEnum.MinDate ? -MinDateMinusOrBonusValueOfDateTime : -MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.SecondMinus:
                    date = date.AddSeconds(dateType == DateTypeEnum.MinDate ? -MinDateMinusOrBonusValueOfDateTime : -MaxDateMinusOrBonusValueOfDateTime);
                    break;

                case TimeTypeEnum.YearBonus:
                    date = date.AddYears(dateType == DateTypeEnum.MinDate ? MinDateMinusOrBonusValueOfDateTime : MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.DateBonus:
                    date = date.AddDays(dateType == DateTypeEnum.MinDate ? MinDateMinusOrBonusValueOfDateTime : MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.HourBonus:
                    date = date.AddHours(dateType == DateTypeEnum.MinDate ? MinDateMinusOrBonusValueOfDateTime : MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.MinuteBonus:
                    date = date.AddMinutes(dateType == DateTypeEnum.MinDate ? MinDateMinusOrBonusValueOfDateTime : MaxDateMinusOrBonusValueOfDateTime);
                    break;
                case TimeTypeEnum.SecondBonus:
                    date = date.AddSeconds(dateType == DateTypeEnum.MinDate ? MinDateMinusOrBonusValueOfDateTime : MaxDateMinusOrBonusValueOfDateTime);
                    break;
                default:
                    break;
            }
            return date;
        }
    }


}
