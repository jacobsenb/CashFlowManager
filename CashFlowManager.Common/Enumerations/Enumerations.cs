using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.Attributes;

namespace CashFlowManager.Common.Enumerations
{
    public class Enumerations
    {

        public enum TransactionType
        {
            [StringValue("B7FBF3D0-3EF8-4465-AF4E-E0C8D230E13C") ]
            Income,
            [StringValue("445CDD4E-CCFA-41F0-89A4-771275881DEA")]
            Expense
        }

        public enum ScheduleType
        {

            [StringValue("7D839FDC-C273-4BE4-B9F2-028F818E53BB")]
            OneOff,
            [StringValue("295DC3D3-8EBF-4D26-A8A8-6EC19B9C06E0")]
            Daily,
            [StringValue("A0D9330C-8E55-441A-B31F-6679C657B3A0")]
            Weekly,
            [StringValue("9384E00A-9332-4DA6-9D8E-E849CB0000A0")]
            Fortnightly,
            [StringValue("67160043-4E9E-4E3F-9181-C501ADF48C49")]
            Monthly,
            [StringValue("3CDD6BF9-FA88-41E6-A288-03A80F494C4C")]
            Yearly
        }

        public enum Zone
        {
            Zone0,
            Zone1,
            Zone2,
            Zone3
        }

    }
}
