namespace DemoLib
{
    public interface IAccountEventArgs
    {
       int IdOperation { get; }
        Guid IdAccount { get; }
        string Message { get; }
        decimal SumOperation { get; }
    }
}