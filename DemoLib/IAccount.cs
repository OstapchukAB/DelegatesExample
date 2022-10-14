namespace DemoLib
{
    public interface IAccount
    {
        decimal CashBack { get; }
        int IdOperationAccount { get; }
        decimal SumAccount { get; }
        decimal SumBuy { get; }
    }
}