using DemoLib;

namespace ConsoleUI
{

    class Programm
    {

        delegate void Message(string s);
        delegate void MessageEmpty();
        delegate int Operation(int x, int y);
        public static void Main()
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            // создаем банковский счет        
            Account account = new Account(Account_Notify, 1.00M);
            account.Add(200.00M);
            // Два раза подряд пытаемся снять деньги
            account.Take(100.00M);
            account.Take(150.00M);


            //***************create account 2 * *******
            Account account2 = new Account(Account_Notify, 0.00M);
            account2.Add(500.00M);

            // Два раза подряд пытаемся снять деньги
            account2.Take(499.99M);
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
            var myMessage = String.Join("\t",
                                        $"Дата:[{DateTime.Now}]",
                                        $"Номер транзакции:[{e.IdOperation}]",
                                        $"Cчет:[{e.IdAccount}]",
                                        $"Операция:[{e.Message}]",
                                        $"Сумма:[{e.Sum:C2}]",
                                        $"Баланс:[{sender.Sum:C2}]"
                );
            Console.WriteLine(myMessage);
        }
    }

}