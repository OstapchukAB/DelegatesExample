using DemoLib;
using System.Data.SqlTypes;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace WFUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.Controls.OfType<Button>().ToList().ForEach(x=>x.Click += new EventHandler(Button_Click)); 
    }

    Account account;
    private void Button_Click(object? sender, EventArgs e)
    {
        if (sender == null)
            return;
        if (sender is Button btn)
        {
            if (btn.Text.Equals("Create Account"))
            { 
            account = new Account(this.Account_Notify, 0.00M);
            account.AlgCashBack += this.Account2_AlgCashBack;
                return;
            }

            if (btn.Text.Equals("Add Money"))
            {
                Decimal money=0.00M;
                if(Decimal.TryParse(textBox1.Text, out money))
                        account.Add(money);
                return;
            }



        }
        

        
    }

    private void Account_Notify(Account sender, AccountEventArgs e)
    {
        var myMessage = String.Join("  ",
                                    // $"����:[{DateTime.Now}]",
                                    $"�������� ����� ����������:[{AccountEventArgs.IdOperation}]",
                                    $"����� ���������� �� �����:[{sender.IdOperationAccount}]",
                                    $"C���:[{e.IdAccount}]",
                                    $"��������:[{e.Message}]",
                                    $"�����:[{e.SumOperation:C2}]",
                                    $"������:[{sender.Sum:C2}]",
                                    $"����� �������:[{sender.SumBuy:C2}]",
                                    $"����� ������:[{sender.CashBack:C2}]"

            );

        this.listView1.Items.Add(myMessage);  
     
    }
    private decimal Account2_AlgCashBack(decimal sumBuy)
    {
        if (sumBuy > 500M)
            return 0.10M;
        else if (sumBuy > 100M)
            return 0.01M;
        else
            return 0.00M;
    }

   
}
