using System;

namespace DelegatesExample
{
    public delegate void AccountHandler(string message);
    class Account
    {
        decimal sum; // Переменная для хранения суммы

        // Создаем переменную делегата
        public AccountHandler? taken;

 
        public Account(AccountHandler delegat) 
        {
            RegisterHandler(delegat);
            //taken = delegat; 
            taken?.Invoke($"Счет создан. Баланс:{this.sum:C2}");
        }

        //// Регистрируем делегат
        public void RegisterHandler(AccountHandler delegat)
        {
            taken = delegat;
        }
        public void UnregisterHandler(AccountHandler delegat)
        {
            taken -= delegat; // удаляем делегат
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
            taken?.Invoke($"Списать со счета :{sum:C2}");
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
                taken?.Invoke($"Недостаточно средств. Баланс:{this.sum}");
            }
        }
    }
}
