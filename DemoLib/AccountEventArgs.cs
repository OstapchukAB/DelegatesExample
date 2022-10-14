namespace DemoLib
{
    /// <summary>
    /// Класс событий передающий информацию о движении средств по счету
    /// </summary>
    public class AccountEventArgs : IAccountEventArgs
    {
        /// <summary>
        /// Сквозной номер транзакции
        /// </summary>
        public int IdOperation { get; private set; } = 0;


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
