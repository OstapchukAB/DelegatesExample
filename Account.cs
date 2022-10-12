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
        static int id { get; set; } =0;


        public Account(AccountHandler delegat) 
        {
            RegisterHandler(delegat);
            //taken = delegat; 
            Notify?.Invoke($"{DateTime.Now} {id++} Внимание! Действие со счетом");  
            taken?.Invoke($"{DateTime.Now} {id++} Счет создан. Баланс:{this.sum:C2}");
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
            Notify?.Invoke($"{DateTime.Now} {id++} Внимание! Попытка положить средства на счет");
            this.sum += sum;

            taken?.Invoke($"{DateTime.Now} {id++} На счет положена сумма {sum:C2}. Баланс:{this.sum:C2}");
        }
        
        // взять деньги с счета
        public void Take(decimal sum)
        {
            Notify?.Invoke($"{DateTime.Now} {id++}  Внимание! Попытка снять средства со счёта");
            taken?.Invoke($"{DateTime.Now} {id++}  Списать со счета :{sum:C2}");
            // берем деньги, если на счете достаточно средств
            if (this.sum >= sum)
            {
                //Console.WriteLine($"Со счета списано {sum} у.е.");
                this.sum -= sum;
                // вызываем делегат, передавая ему сообщение
                taken?.Invoke($"{DateTime.Now} {id++}  Со счета списано {sum:C2}. Баланс:{this.sum:C2}");
                Notify?.Invoke($"{DateTime.Now} {id++}  Внимание!  Списание средств со счета произведено");
            }
            else
            {
                Notify?.Invoke($"{DateTime.Now} {id++}  Внимание!  Неудачная попытка снять средства со счета");
                taken?.Invoke($"{DateTime.Now} {id++}  Недостаточно средств. Баланс:{this.sum}");
            }
        }
    }
}
