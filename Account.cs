using System;

namespace DelegatesExample
{
    public delegate void AccountHandler(string message);
    class Account
    {
        decimal sum; // Переменная для хранения суммы

        // Создаем переменную делегата
        public AccountHandler? taken;

        // через конструктор устанавливается начальная сумма на счете
        public Account(AccountHandler delegat) 
        {
            taken = delegat; 
            taken?.Invoke($"Счет создан. Баланс:{this.sum:C2}");
        }

        //// Регистрируем делегат
        public void RegisterHandler(AccountHandler delegat)
        {
            taken = delegat;
        }

        // добавить средства на счет
        public void Add(decimal sum) 
        { 
            this.sum += sum;

            taken?.Invoke($"На счет положена сумма {sum:C2}. Баланс:{this.sum:C2}");
        }
        
        // взять деньги с счета
        public void Take(decimal sum)
        {
            // берем деньги, если на счете достаточно средств
            if (this.sum >= sum)
            {
                //Console.WriteLine($"Со счета списано {sum} у.е.");
                this.sum -= sum;
                // вызываем делегат, передавая ему сообщение
                taken?.Invoke($"Со счета списано {sum:C2}. Баланс:{this.sum:C2}");
            }
            else
            {
                taken?.Invoke($"Недостаточно средств. Невозможно списать:{sum:C2}. Баланс:{this.sum}");
            }
        }
    }
}
