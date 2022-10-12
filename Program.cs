using DelegatesExample;

class Programm
{
    public static void Main()
    {
        void PrintSimpleMessage(string message) => Console.WriteLine(message);

        // создаем банковский счет
        Account account = new Account(200);
        
        // Добавляем в делегат ссылку на метод PrintSimpleMessage
       // account.RegisterHandler(PrintSimpleMessage);
        account.taken = PrintSimpleMessage;

        // Два раза подряд пытаемся снять деньги
        account.Take(100);
        account.Take(150);
        
       Console.WriteLine();
        Console.Write("Для выхода нажмите любую клавишу");
        Console.ReadKey();
        Environment.Exit(-1);   

    }

}