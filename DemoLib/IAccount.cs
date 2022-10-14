namespace DemoLib
{
    public interface IAccount
    {
        decimal CashBack { get; }
        int IdOperationAccount { get; }
        decimal Sum { get; }
        decimal SumBuy { get; }
    }
}