namespace DemoLib
{
    public interface IAccountEventArgs
    {
        DateTime Datetime { get; } 
        static int IdOperation { get; }
        Guid IdAccount { get; }
        string Message { get; }
        decimal SumOperation { get; }
    }
}