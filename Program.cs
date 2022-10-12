using DelegatesExample;

class Programm
{
    public static void Main()
    {
        System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        void PrintSimpleMessage(string message) => Console.WriteLine(message);

        // создаем банковский счет
        Account account = new Account(PrintSimpleMessage);

        // Добавляем в делегат ссылку на метод PrintSimpleMessage
        // account.RegisterHandler(PrintSimpleMessage);
        //account.taken = PrintSimpleMessage;

        account.Add(200.00M);  
        
       

        // Два раза подряд пытаемся снять деньги
        account.Take(100.00M);
        account.Take(150.00M);
        
       Console.WriteLine();
        Console.Write("Для выхода нажмите любую клавишу");
        Console.ReadKey();
        Environment.Exit(-1);   

    }

}