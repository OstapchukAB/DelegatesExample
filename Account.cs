namespace DelegatesExample
{
    public delegate void AccountHandler(string message);
    class Account
    {
        int sum; // Переменная для хранения суммы

        // Создаем переменную делегата
        public AccountHandler? taken;

        // через конструктор устанавливается начальная сумма на счете
        public Account(int sum) => this.sum = sum;

        //// Регистрируем делегат
        //public void RegisterHandler(AccountHandler delegat)
        //{
        //    taken = delegat;
        //}

        // добавить средства на счет
        public void Add(int sum) => this.sum += sum;
        // взять деньги с счета
        public void Take(int sum)
        {
            // берем деньги, если на счете достаточно средств
            if (this.sum >= sum)
            {
                //Console.WriteLine($"Со счета списано {sum} у.е.");
                this.sum -= sum;
                // вызываем делегат, передавая ему сообщение
                taken?.Invoke($"Со счета списано {sum} у.е.");
            }
            else
            {
                taken?.Invoke($"Недостаточно средств. Баланс: {this.sum} у.е.");
            }
        }
    }
}
