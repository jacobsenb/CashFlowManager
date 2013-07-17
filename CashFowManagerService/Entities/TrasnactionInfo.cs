using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CashFowManagerService.Entities
{
    /// <summary>
    /// Represents a transaction
    /// </summary>
    [DataContract]
    public class TrasnactionInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }
}
