using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.BaseClass;

namespace CashFlowManager.Contract.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ScheduleInfo : EntityBase<ScheduleInfo>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember]
        public Guid Id { get; set; }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Sequence { get; set; }

    }
}
