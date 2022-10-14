namespace DemoLib
{
    public interface IAccountEventArgs
    {
        
        Guid IdAccount { get; }
        string Message { get; }
        decimal SumOperation { get; }
    }
}