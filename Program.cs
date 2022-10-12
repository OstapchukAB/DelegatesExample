using DelegatesExample;

class Programm
{

    delegate void Message(string s);
    delegate void MessageEmpty();
    delegate int Operation(int x,int y);
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

        void PrintSimpleMessage(string message) => Console.WriteLine(message);

        void PrintColorMessageGreen(string message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }
        void PrintColorMessageRed(string message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }

        // создаем банковский счет
        Account account = new Account(PrintSimpleMessage);

       // account.UnregisterHandler(PrintSimpleMessage);
      //  account.RegisterHandler(PrintColorMessageGreen);

        // Добавляем в делегат ссылку на метод PrintSimpleMessage
        // account.RegisterHandler(PrintSimpleMessage);
        //account.taken = PrintSimpleMessage;

        account.Add(200.00M);

       // account.UnregisterHandler(PrintColorMessageGreen);
       // account.RegisterHandler(PrintColorMessageRed);
        // Два раза подряд пытаемся снять деньги
        account.Take(100.00M);
        account.Take(150.00M);

        Console.WriteLine();
        Console.Write("Для выхода нажмите любую клавишу");
        Console.ReadKey();
        Environment.Exit(-1);   

    }

}