namespace DemoLib
{
    public interface IAccountEventArgs
    {
       static int IdOperation { get; }
        Guid IdAccount { get; }
        string Message { get; }
        decimal SumOperation { get; }
    }
}