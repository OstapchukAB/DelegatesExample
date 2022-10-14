namespace DemoLib
{

    public delegate void AccountHandler(string message);
    public delegate decimal AccountAlgoritmCashBack(decimal sumBuy);
    public delegate void AccountHandlerEvent(Account sender, AccountEventArgs e);
   

    /// <summary>
    /// Аккаунт банковского счета
    /// </summary>
    public class Account : IAccount
    {

        public List<AccountEvents> ListAccEvents { get; set; } = new List<AccountEvents>();
        /// <summary>
        /// 
        /// </summary>
        enum TypeOperation
        {
            CreateAnAccount,
            DepositMoneyToTheAccount,
            TakeMoneyFromTheAccount,
            MakeAPurchase,
            OperationFailure,
            MoneyRefund
        }
        /// <summary>
        /// Номер транзакции в пределах счета
        /// </summary>
        public int IdOperationAccount { get; private set; } = 0;

        /// <summary>
        /// Баланс клиента
        /// </summary>
        public decimal SumAccount { get; private set; }

        /// <summary>
        /// Сумма покупок
        /// </summary>
        public decimal SumBuy { get; private set; }

        public decimal CashBack { get; private set; }

        /// <summary>
        /// Номер счета клиента
        /// </summary>
        readonly Guid IdAccount;

        /// <summary>
        /// Создание счета
        /// </summary>
        /// <param name="NameMetod">Имя метода для регистрации событий</param>
        /// <param name="firstSum">Первоначальная сумма при открытии счета</param>
        public Account(AccountHandlerEvent? NameMetod, decimal firstSum)
        {
            IdOperationAccount++;
            Notify += NameMetod;
            IdAccount = Guid.NewGuid();
            this.SumAccount = firstSum;

            notify?.Invoke(this, new AccountEventArgs($"Создание счета", firstSum, IdAccount));
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

        AccountAlgoritmCashBack? algCashBack;
        public event AccountAlgoritmCashBack? AlgCashBack
        {
            add
            {
                algCashBack += value;
                //Console.WriteLine($"{value?.Method.Name} добавлен");
            }
            remove
            {
                algCashBack -= value;
                // Console.WriteLine($"{value?.Method.Name} удален");
            }
        }


        // добавить средства на счет
        public void Add(decimal sum)
        {
            IdOperationAccount++;
            this.SumAccount += sum;
            notify?.Invoke(this, new AccountEventArgs($"Добавление средств", sum, IdAccount));

        }

        // взять деньги с счета
        public void Take(decimal sum)
        {
            IdOperationAccount++;
            // берем деньги, если на счете достаточно средств иначе отказ в списании
            if (this.SumAccount >= sum)
            {
                this.SumAccount -= sum;
                notify?.Invoke(this, new AccountEventArgs($"Списание средств", sum, IdAccount));

            }
            else
            {
                notify?.Invoke(this, new AccountEventArgs($"Отказ в списании", sum, IdAccount));
            }
        }
        public void Buy(decimal sum)
        {
            IdOperationAccount++;

            if (this.SumAccount >= sum)
            {
                this.SumAccount -= sum;

                this.SumBuy += sum;

                notify?.Invoke(this, new AccountEventArgs($"Покупка", sum, IdAccount));

                var CurentCachBack = decimal.Multiply(sum, algCashBack?.Invoke(SumBuy) ?? 0.00M);
                if (CurentCachBack > 0)
                {

                    this.CashBack = this.CashBack + CurentCachBack;
                    this.SumBuy = 0;
                    notify?.Invoke(this, new AccountEventArgs($"Вам начислен кэшбэк", CurentCachBack, IdAccount));
                }
                //if (this.SumBuy > 100)
                //{
                //    this.CashBack += this.SumBuy / 100;
                //    this.SumBuy = 0.00M;
                //}


            }
            else
            {
                notify?.Invoke(this, new AccountEventArgs($"Отказ в покупке", sum, IdAccount));
            }
        }

    }
}
