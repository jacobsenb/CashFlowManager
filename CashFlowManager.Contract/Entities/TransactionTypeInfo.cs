using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.BaseClass;

namespace CashFlowManager.Contract.Entities
{
    
    [DataContract]
    public class TransactionTypeInfo: EntityBase<TransactionTypeInfo>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public int Sequence { get; set; }

    }
}
