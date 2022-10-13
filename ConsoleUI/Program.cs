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


            //MessageEmpty Msempty = () => Console.WriteLine("Пустой делегат");
            //Msempty(); 

            ////Это анонимный метод
            //Message ms = delegate (string s) { Console.WriteLine($" {s}"); };
            //ms("вызов анонимного метода для делегата с параметром");

            ////Это анонимный метод и использованием лямбды
            //var msg = (string s) => { Console.WriteLine($"{s}"); };
            //msg("Hello");
            //int x = 4;
            //int y = 6;

            //Message messg = s =>  Console.WriteLine($"{s}");
            //messg("упрощенная запись для одного параметра"); 


            //Operation add = (int x, int y) => { return x + y; };
            //msg($"Результат сложения чисел {x} и {y} равно {add(x,y)}");



            // создаем банковский счет
           
            Account account = new Account(Account_Notify, 1.00M);
           
            account.Add(200.00M);
            // Два раза подряд пытаемся снять деньги
            account.Take(100.00M);
            account.Take(150.00M);


            //***************create account 2********
            //Account account2 = new Account(new AccountHandlerEvent(Account_Notify), 0.00M);
            //account2.Add(500.00M);

            //// Два раза подряд пытаемся снять деньги
            //account2.Take(499.99M);
            //account2.Take(50.00M);
            //account2.Add(100.00M);
            //account2.Take(50.00M);
            ////********************

            //Console.WriteLine();
            //Console.Write("Для выхода нажмите любую клавишу");
            //Console.ReadKey();
            //Environment.Exit(-1);

        }

        private static void Account_Notify(Account sender, AccountEventArgs e)
        {
            Console.WriteLine("");
            Console.Write($"Дата:[{DateTime.Now}]\t");
            Console.Write($"Номер транзакции:[{e.IdOperation}]\tCчет№:[{e.IdAccount}]\tсумма:[{e.Sum}]\t");  
            Console.Write($"Операция:[{e.Message}]\tБаланс:[{sender.Sum}]");

        }
    }
    
}