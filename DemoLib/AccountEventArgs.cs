namespace DemoLib
{
    /// <summary>
    /// Класс передающий при возникновении события информацию о событии
    /// </summary>
    public class AccountEventArgs
    {

        public int IdOperation { get; }
        public Guid IdAccount { get;}

        // Сообщение 
        public string Message { get; }

        /// <summary>
        /// Сумма, на которую изменился счет
        /// </summary>
        public decimal Sum { get; }
        public AccountEventArgs(string message, decimal sum, Guid _IdAccount,int _idOperation)
        {
            Message = message;
            Sum = sum;
           IdAccount=_IdAccount;
            IdOperation = _idOperation; 
        }
    }
}
