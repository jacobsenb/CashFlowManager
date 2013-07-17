using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.BaseClass;

namespace CashFlowManager.Contract.Entities
{
    [DataContract]
    public class PracticeInfo : EntityBase<PracticeInfo>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
    }
}
