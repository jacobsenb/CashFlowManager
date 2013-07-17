using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.BaseClass;

namespace CashFlowManager.Contract.Entities
{
    [Table("UserProfile")]
    public class UserProfile : EntityBase<UserProfile>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Guid PracticeId { get; set; }
        public String Email { get; set; }
    }
}
