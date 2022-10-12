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
        public decimal Sum { get; private set; } // Переменная для хранения суммы

        // Создаем переменную делегата
        public AccountHandler? taken;
 
        public event AccountHandler? Notify;
        int id { get; set; } =0;
        readonly Guid IdAccount;


        public Account(AccountHandler delegat) 
        {
            
            IdAccount= Guid.NewGuid();
            RegisterHandler(delegat);
            //taken = delegat; 
            Notify?.Invoke($"{DateTime.Now} {id++} Внимание! Создание счета");  
            taken?.Invoke($"{DateTime.Now} {id++} Счет [{IdAccount}] создан. Баланс:{this.Sum:C2}");
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
            Notify?.Invoke($"{DateTime.Now} {id++} Внимание! Попытка положить средства на счет [{IdAccount}]");
            this.Sum += sum;

            taken?.Invoke($"{DateTime.Now} {id++} На счет положена сумма {sum:C2}. Баланс:{this.Sum:C2}");
        }
        
        // взять деньги с счета
        public void Take(decimal sum)
        {
            Notify?.Invoke($"{DateTime.Now} {id++}  Внимание! Попытка снять средства со счёта [{IdAccount}]");
            taken?.Invoke($"{DateTime.Now} {id++}  Списать со счета :{sum:C2}");
            // берем деньги, если на счете достаточно средств
            if (this.Sum >= sum)
            {
                //Console.WriteLine($"Со счета списано {sum} у.е.");
                this.Sum -= sum;
                // вызываем делегат, передавая ему сообщение
                taken?.Invoke($"{DateTime.Now} {id++}  Со счета списано {sum:C2}. Баланс:{this.Sum:C2}");
                Notify?.Invoke($"{DateTime.Now} {id++}  Внимание!  Списание средств со счета [{IdAccount}] произведено");
            }
            else
            {
                Notify?.Invoke($"{DateTime.Now} {id++}  Внимание!  Неудачная попытка снять средства со счета [{IdAccount}]");
                taken?.Invoke($"{DateTime.Now} {id++}  Недостаточно средств. Баланс:{this.Sum}");
            }
        }
    }
}
