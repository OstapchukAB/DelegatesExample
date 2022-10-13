﻿using DemoLib;

namespace ConsoleUI
{

    class Programm
    {
        public static void Main()
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            //// создаем банковский счет        
            //Account account = new Account(Account_Notify, 1.00M);
            //account.Add(200.00M);
            //// Два раза подряд пытаемся снять деньги
            //account.Take(100.00M);
            //account.Take(150.00M);
            //account.Buy(99.00M);  


            //***************create account 2 * *******
            Account account2 = new Account(Account_Notify, 0.00M);
            account2.Add(500.00M);

          
            account2.Buy(200.00M);
          
            account2.Take(50.00M);
            
            account2.Add(100.00M);
            
            account2.Take(50.00M);
            //********************

            Console.WriteLine();
            Console.Write("Для выхода нажмите любую клавишу");
            Console.ReadKey();
            Environment.Exit(-1);

        }

        private static void Account_Notify(Account sender, AccountEventArgs e)
        {
            var myMessage = String.Join("  ",
                                       // $"Дата:[{DateTime.Now}]",
                                        $"Сквозной номер транзакции:[{AccountEventArgs.IdOperation}]",
                                        $"Номер транзакции по счету:[{sender.IdOperationAccount}]",
                                        $"Cчет:[{e.IdAccount}]",
                                        $"Операция:[{e.Message}]",
                                        $"Сумма:[{e.SumOperation:C2}]",
                                        $"Баланс:[{sender.Sum:C2}]",
                                        $"Кэшбэк:[{sender.CashBack:C2}]"

                );
            Console.WriteLine(myMessage);
        }
    }

}