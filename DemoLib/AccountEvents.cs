using System;
using System.ComponentModel;

namespace DemoLib
{
    /// <summary>
    /// Класс визуализации в Гриде
    /// </summary>
    public class AccountEvents : IAccount, IAccountEventArgs
    {
        public AccountEvents(
            DateTime datetime,
            int idOperation,decimal cashBack, int idOperationAccount,
            decimal sumAccount, decimal sumBuy, Guid idAccount,
            string message, decimal sumOperation)
        {
            Datetime = datetime;
            IdOperation = idOperation;
            IdOperationAccount = idOperationAccount;
            IdAccount = idAccount;
            SumOperation = sumOperation;          
            SumAccount = sumAccount;
            SumBuy = sumBuy;
            CashBack = cashBack;
            Message = message;
        }


        [DisplayName("Дата и время транзакции")]
        public DateTime Datetime { get; set; }

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
