using System.ComponentModel;

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
