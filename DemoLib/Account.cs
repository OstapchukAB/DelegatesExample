namespace DemoLib
{

    public delegate void AccountHandler(string message);
    public delegate void AccountHandlerEvent(Account sender, AccountEventArgs e);

    /// <summary>
    /// Аккаунт банковского счета
    /// </summary>
    public class Account
    {
        public decimal Sum { get; private set; } // Переменная для хранения суммы

        //public event AccountHandler? Notify;
        /// <summary>
        /// сквозной номер транзакции
        /// </summary>
        static int Id { get; set; } = 0;
        readonly Guid IdAccount;


        public Account(AccountHandlerEvent? _notify, decimal firstSum)
        {
            Notify += _notify;
            IdAccount = Guid.NewGuid();
            this.Sum = firstSum;

            notify?.Invoke(this, new AccountEventArgs($"Создание счета", firstSum, IdAccount, Id++));
        }

        AccountHandlerEvent? notify;
        public event AccountHandlerEvent? Notify
        {
            add
            {
                notify += value;
                //Console.WriteLine($"{value?.Method.Name} добавлен");
            }
            remove
            {
                notify -= value;
                // Console.WriteLine($"{value?.Method.Name} удален");
            }
        }



        // добавить средства на счет
        public void Add(decimal sum)
        {
            this.Sum += sum;
            notify?.Invoke(this, new AccountEventArgs($"Добавление средств", sum, IdAccount, Id++));
        }

        // взять деньги с счета
        public void Take(decimal sum)
        {

            // берем деньги, если на счете достаточно средств иначе отказ в списании
            if (this.Sum >= sum)
            {
                this.Sum -= sum;
                notify?.Invoke(this, new AccountEventArgs($"Списание средств", sum, IdAccount, Id++));
            }
            else
            {
                notify?.Invoke(this, new AccountEventArgs($"Отказ в списании", sum, IdAccount, Id++));
            }
        }
    }
}
