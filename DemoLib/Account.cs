﻿namespace DemoLib
{

    public delegate void AccountHandler(string message);
    public delegate void AccountHandlerEvent(Account sender, AccountEventArgs e);

    /// <summary>
    /// Аккаунт банковского счета
    /// </summary>
    public class Account
    {
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
        public decimal Sum { get; private set; } 

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
            this.Sum = firstSum;

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



        // добавить средства на счет
        public void Add(decimal sum)
        {
            IdOperationAccount++;
            this.Sum += sum;
            notify?.Invoke(this, new AccountEventArgs($"Добавление средств", sum, IdAccount));
           
        }

        // взять деньги с счета
        public void Take(decimal sum)
        {
            IdOperationAccount++;
            // берем деньги, если на счете достаточно средств иначе отказ в списании
            if (this.Sum >= sum)
            {
                this.Sum -= sum;
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
           
            if (this.Sum >= sum)
            {
                this.Sum -= sum;
               
                this.SumBuy += sum;
                if (this.SumBuy > 100)
                {
                    this.CashBack += this.SumBuy / 100;
                    this.SumBuy = 0.00M;
                }
                notify?.Invoke(this, new AccountEventArgs($"Покупка", sum, IdAccount));

            }
            else
            {
                notify?.Invoke(this, new AccountEventArgs($"Отказ в покупке", sum, IdAccount));
            }
        }

    }
}
