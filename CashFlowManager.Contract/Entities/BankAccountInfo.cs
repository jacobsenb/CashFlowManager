using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.BaseClass;

namespace CashFlowManager.Contract.Entities
{
    [DataContract]
    public class BankAccountInfo : EntityBase<BankAccountInfo>
    {
        [DataMember]
        public Guid Id { get; set; }
        
        [DataMember]
        [Required]
        public string AccountName { get; set; }

        [DataMember]
        [Required]
        public string AccountNumber { get; set; }
        
        [DataMember]
        [DisplayName("Account Balance")]
        [Required]
        public Nullable<decimal> Balance { get; set; }

        [DataMember]
        [Required]
        public Nullable<System.Guid> ClientId { get; set; }
    }
}
