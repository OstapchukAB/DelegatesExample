using System;

namespace DelegatesExample
{
    /// <summary>
    /// Делегат
    /// </summary>
    /// <param name="message"></param>
    public delegate void AccountHandler(string message);
   
    class Account
    {
        decimal sum; // Переменная для хранения суммы

        // Создаем переменную делегата
        public AccountHandler? taken;
 
        public event AccountHandler? Notify;


        public Account(AccountHandler delegat) 
        {
            RegisterHandler(delegat);
            //taken = delegat; 
            Notify?.Invoke("Внимание! Действие со счетом");  
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
            Notify?.Invoke("Внимание! Попытка положить средства на счет");
            this.sum += sum;

            taken?.Invoke($"На счет положена сумма {sum:C2}. Баланс:{this.sum:C2}");
        }
        
        // взять деньги с счета
        public void Take(decimal sum)
        {
            Notify?.Invoke("Внимание! Попытка положить снять средства со счёта");
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
                Notify?.Invoke("Внимание! Попытка положить снять средства со счёта. Недостаточно средств");
                taken?.Invoke($"Недостаточно средств. Баланс:{this.sum}");
            }
        }
    }
}
