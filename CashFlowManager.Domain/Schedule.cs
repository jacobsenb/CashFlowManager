//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CashFlowManager.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        public Schedule()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Sequence { get; set; }
    
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
