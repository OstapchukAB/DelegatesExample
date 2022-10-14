using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        // $"Дата:[{DateTime.Now}]",
        //                            $"Сквозной номер транзакции:[{AccountEventArgs.IdOperation}]",
        //                            $"Номер транзакции по счету:[{sender.IdOperationAccount}]",
        //                            $"Cчет:[{e.IdAccount}]",
        //                            $"Операция:[{e.Message}]",
        //                            $"Сумма:[{e.SumOperation:C2}]",
        //                            $"Баланс:[{sender.SumAccount:C2}]",
        //                            $"Сумма покупок:[{sender.SumBuy:C2}]",
        //                            $"Общий кэшбэк:[{sender.CashBack:C2}]"

       
        
        [DisplayName("Сквозной номер транзакции")]
        public int IdOperation { get; set; }
        
        [DisplayName("Номер транзакции по счету")]
        public int IdOperationAccount { get; set; }
        
        [DisplayName("Cчет")]
        public Guid IdAccount { get; set; }

        [DisplayName("Операция")]
        public string Message { get; set; } = "";


        [DisplayName("Сумма операции")]
        public decimal SumOperation { get; set; }


        [DisplayName("Баланс")]
        public decimal SumAccount{ get; set; }
        
        
        [DisplayName("Сумма покупок")]
        public decimal SumBuy { get; set; }
        
        [DisplayName("Общий кэшбэк")]
        public decimal CashBack { get; set; }
    }
}
