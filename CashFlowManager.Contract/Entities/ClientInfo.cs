using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.Attributes;
using CashFlowManager.Common.BaseClass;
using CashFlowManager.Common.Enumerations;

namespace CashFlowManager.Contract.Entities
{
    [DataContract]
    public class ClientInfo : EntityBase<ClientInfo>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public Nullable<System.Guid> PracticeId { get; set; }

        [IgnoreProperty]
        public List<ClientPositionIndicator> ClientPosition { get; set; }
    }

    public class ClientPositionIndicator
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public decimal ExpenseToCashOnHandPercentage { get; set; }

        public Enumerations.Zone Position { get; set; }
    }
}
