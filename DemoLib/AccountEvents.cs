using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class AccountEvents : IAccount, IAccountEventArgs
    {
        public AccountEvents(int idOperation,decimal cashBack, int idOperationAccount, decimal sumAccount, decimal sumBuy, Guid idAccount, string message, decimal sumOperation)
        {
            IdOperation = idOperation;
            IdOperationAccount = idOperationAccount;
            IdAccount = idAccount;
            SumOperation = sumOperation;          
            SumAccount = sumAccount;
            SumBuy = sumBuy;
            CashBack = cashBack;
            Message = message;
        }

        public decimal CashBack { get; set; }

        public int IdOperation { get; set; }
        public int IdOperationAccount { get; set; }

        public decimal SumAccount{ get; set; }

        public decimal SumBuy { get; set; }

        public Guid IdAccount { get; set; }

        public string Message { get; set; } = "";

        public decimal SumOperation { get; set; }
    }
}
