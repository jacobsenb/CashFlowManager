using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using CashFlowManager.Common.Attributes;
using CashFlowManager.Common.BaseClass;
using CashFlowManager.Common.Enumerations;

namespace CashFlowManager.Contract.Entities
{
    /// <summary>
    /// Represents a transaction
    /// </summary>
    [DataContract]
    public class TransactionInfo : EntityBase<TransactionInfo>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        public string Narration { get; set; }

        [DataMember]
        [Required]
        public Nullable<decimal> Amount { get; set; }

        [DataMember]
        [DisplayName("Frequency")]
        public Guid ScheduleId { get; set; }

        [DataMember]
        [DisplayName("Type")]
        public Guid TransactionTypeId { get; set; }

        [DataMember]
        public Guid ClientId { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        [Required(ErrorMessage="The Start Date is required")]
        public DateTime? StartDate { get; set; }

        [IgnoreProperty]
        public string TransactionType
        {
            get
            {
                if (TransactionTypeId != null)
                {
                    if (TransactionTypeId == new Guid(StringEnum.GetStringValue(Enumerations.TransactionType.Income)))
                    {
                        return Enumerations.TransactionType.Income.ToString();
                    }
                    return Enumerations.TransactionType.Expense.ToString();
                }
                return string.Empty;
            }
        }

        [IgnoreProperty]
        public Enumerations.TransactionType TransactionTypeEnum
        {
            get
            {
                if (TransactionTypeId != null)
                {
                    if (TransactionTypeId == new Guid(StringEnum.GetStringValue(Enumerations.TransactionType.Income)))
                    {
                        return Enumerations.TransactionType.Income;
                    }
                    return Enumerations.TransactionType.Expense;
                }
                return Enumerations.TransactionType.Income;
            }
        }

        [IgnoreProperty]
        public string ScheduleType
        {
            get
            {
                string schType = string.Empty;
                if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.OneOff)))
                {
                    schType = Enumerations.ScheduleType.OneOff.ToString();
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Daily)))
                {
                    schType = Enumerations.ScheduleType.Daily.ToString();
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Weekly)))
                {
                    schType = Enumerations.ScheduleType.Weekly.ToString();
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Fortnightly)))
                {
                    schType = Enumerations.ScheduleType.Fortnightly.ToString();
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Monthly)))
                {
                    schType = Enumerations.ScheduleType.Monthly.ToString();
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Yearly)))
                {
                    schType = Enumerations.ScheduleType.Yearly.ToString();
                }
                    return schType;
            }
        }

        [IgnoreProperty]
        public Enumerations.ScheduleType ScheduleTypeEnum
        {
            get
            {
                Enumerations.ScheduleType schType = Enumerations.ScheduleType.OneOff;
                if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.OneOff)))
                {
                    schType = Enumerations.ScheduleType.OneOff;
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Daily)))
                {
                    schType = Enumerations.ScheduleType.Daily;
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Weekly)))
                {
                    schType = Enumerations.ScheduleType.Weekly;
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Fortnightly)))
                {
                    schType = Enumerations.ScheduleType.Fortnightly;
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Monthly)))
                {
                    schType = Enumerations.ScheduleType.Monthly;
                }
                else if (ScheduleId == new Guid(StringEnum.GetStringValue(Enumerations.ScheduleType.Yearly)))
                {
                    schType = Enumerations.ScheduleType.Yearly;
                }
                return schType;
            }
        }

    }

}
