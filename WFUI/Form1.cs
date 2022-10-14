using DemoLib;
using System.Data.SqlTypes;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace WFUI;

public partial class Form1 : Form
{
    List<AccountEvents> ListAccEvents { get; set; }
    Account _Account { get; set; }
    public Form1()
    {
        InitializeComponent();
        dataGridView1.Parent = this.splitContainer1.Panel2;     
        ListAccEvents = new List<AccountEvents>();
        dataGridView1.DataSource = ListAccEvents;
         
        this.Controls.OfType<Button>().ToList().ForEach(x=>x.Click += new EventHandler(Button_Click));
         
    }

   
    private void Button_Click(object? sender, EventArgs e)
    {
       
        if (sender == null)
            return;
        if (sender is Button btn)
        {
            if (btn.Text.Equals("Create Account"))
            { 
            
                
                
            _Account = new Account(this.Account_Notify, 0.00M);
            _Account.AlgCashBack += (decimal sumBuy) =>
                {
                    if (sumBuy > 500M)
                        return 0.10M;
                    else if (sumBuy > 100M)
                        return 0.01M;
                    else
                        return 0.00M;
                };
                
                return;
            }

            if (btn.Text.Equals("Add Money"))
            {
                Decimal money=0.00M;
                if(Decimal.TryParse(textBox1.Text, out money))
                        _Account.Add(money);
                return;
            }



        }
        

        
    }

    private void Account_Notify(Account sender, AccountEventArgs e)
    {
        ListAccEvents.Add(new AccountEvents(
              idOperation: e.IdOperation,
              idOperationAccount: sender.IdOperationAccount,
              idAccount:e.IdAccount,
              message:e.Message,
              sumOperation:e.SumOperation,
              sumAccount:sender.SumAccount,
              sumBuy: sender.SumBuy,
              cashBack: sender.CashBack
            ) );
        //var myMessage = String.Join("  ",
        //                            // $"Дата:[{DateTime.Now}]",
        //                            $"Сквозной номер транзакции:[{AccountEventArgs.IdOperation}]",
        //                            $"Номер транзакции по счету:[{sender.IdOperationAccount}]",
        //                            $"Cчет:[{e.IdAccount}]",
        //                            $"Операция:[{e.Message}]",
        //                            $"Сумма:[{e.SumOperation:C2}]",
        //                            $"Баланс:[{sender.SumAccount:C2}]",
        //                            $"Сумма покупок:[{sender.SumBuy:C2}]",
        //                            $"Общий кэшбэк:[{sender.CashBack:C2}]"

        //    );




    }
   

   
}
