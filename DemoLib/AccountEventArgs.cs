namespace DemoLib
{
    /// <summary>
    /// Класс передающий при возникновении события информацию о событии
    /// </summary>
    public class AccountEventArgs
    {
        /// <summary>
        /// Сквозной номер транзакции
        /// </summary>
        public static int IdOperation { get; private set; } = 0;

        ///// <summary>
        ///// Номер транзакции в пределах счета
        ///// </summary>
        //public int IdOperationAccount { get; private set; } = 0;
        
        
        
        /// <summary>
        /// Номер счета
        /// </summary>
        public Guid IdAccount { get; }

        /// <summary>
        /// Название операции
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal SumOperation { get; }
        public AccountEventArgs(string NameOperation, decimal SumOperation, Guid IdAccount)
        {
            this.Message = NameOperation;
            this.SumOperation = SumOperation;
            this.IdAccount = IdAccount;
            IdOperation++;
            //IdOperationAccount++;
        }
    }
}
