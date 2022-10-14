using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class AccountEvents : IAccount, IAccountEventArgs
    {
        public decimal CashBack => throw new NotImplementedException();

        public int IdOperationAccount => throw new NotImplementedException();

        public decimal Sum => throw new NotImplementedException();

        public decimal SumBuy => throw new NotImplementedException();

        public Guid IdAccount => throw new NotImplementedException();

        public string Message => throw new NotImplementedException();

        public decimal SumOperation => throw new NotImplementedException();
    }
}
