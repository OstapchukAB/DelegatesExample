using DemoLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using static WFUI.Form1;

namespace WFUI;

public partial class Form1 : Form
{
    List<AccountEvents> ListAccEvents { get; set; }
    Account? _Account { get; set; }
    public delegate void GridGui<T>(DataGridView  grd, List<T> lst);
    GridGui<AccountEvents> Grid;
    List<Account> ListAccount { get; set; }
    BindingSource BindCombo => new BindingSource();


    public Form1()
    {
        InitializeComponent();
        Grid += GridRefresh;
        ListAccount = new List<Account>();

        ListAccEvents = new List<AccountEvents>();
        Grid(dataGridView1, ListAccEvents);

        this.Controls.OfType<Button>().ToList().ForEach(x => x.Click += new EventHandler(Buttons_Click));
       

    }


    private void Buttons_Click(object? sender, EventArgs e)
    {

        if (sender == null)
            return;
        if (sender is Button btn)
        {
            if (btn.Text.Equals("Create Account"))
            {
                _Account = new Account(this.Account_Notify, 0.00M);
                ListAccount.Add(_Account);
                GuiComboBoxRefresh(this.comboBox1, ListAccount);
                _Account.AlgCashBack += (decimal sumBuy) =>
                    {
                        if (sumBuy > 100M)
                            return 0.10M;
                        else if (sumBuy <= 100M)
                            return 0.01M;
                        else if (sumBuy > 1000M)
                            return 0.2M;
                        else
                            return 0.00M;
                    };
                return;
            }

            if (_Account ==null)
                return;
            Account ac = (Account)comboBox1.SelectedValue; 
            if (ac==null)
                return;

            if (btn.Text.Equals("Add Money"))
            {
                Decimal money = 0.00M;
                if (Decimal.TryParse(textBox1.Text, out money))
                    if (money > 0)
                        ac.Add(money);
            }
            else if (btn.Text.Equals("Take Money"))
            {
                Decimal money = 0.00M;
                if (Decimal.TryParse(textBox1.Text, out money))
                    if(money >0)
                    ac.Take(money);
            }
            else if (btn.Text.Equals("Buy"))
            {
                Decimal money = 0.00M;
                if (Decimal.TryParse(textBox1.Text, out money))
                {
                    if (money>0)
                    ac.Buy(money);
                }
            }
            else
                return;

           

        }



    }

    private void Account_Notify(Account sender, AccountEventArgs e)
    {
        ListAccEvents.Add(new AccountEvents(
              idOperation: AccountEventArgs.IdOperation,
              idOperationAccount: sender.IdOperationAccount,
              idAccount: e.IdAccount,
              message: e.Message,
              sumOperation: e.SumOperation,
              sumAccount: sender.SumAccount,
              sumBuy: sender.SumBuy,
              cashBack: sender.CashBack
            ));

       
        Grid(dataGridView1, ListAccEvents);

        // var myMessage = String.Join("  ",
        //                            // $"Дата:[{DateTime.Now}]",
        //                            $"Сквозной номер транзакции:[{e.IdOperation}]",
        //                            $"Номер транзакции по счету:[{sender.IdOperationAccount}]",
        //                            $"Cчет:[{e.IdAccount}]",
        //                            $"Операция:[{e.Message}]",
        //                            $"Сумма:[{e.SumOperation:C2}]",
        //                            $"Баланс:[{sender.SumAccount:C2}]",
        //                            $"Сумма покупок:[{sender.SumBuy:C2}]",
        //                            $"Общий кэшбэк:[{sender.CashBack:C2}]"

        //    );
        //MessageBox.Show(myMessage);

       
    }


   

    void GridRefresh<T>(DataGridView MyGrid, List<T> lst) 
    {
        if (MyGrid == null)
            return;

            Action action = () =>
            {
                var grdList = new BindingList<T>(lst);
                MyGrid.DataSource = grdList;
                MyGrid.AllowUserToAddRows = false;
                MyGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                MyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                MyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                MyGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


                // Grid.Refresh();
            };
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
    }


    void GuiComboBoxRefresh(object ob, List<Account> lst)
    {
        if (ob == null)
            return;
        Action action = () =>
        {
            if (ob is ComboBox comb)
            {
                // var bindingSource1 = new BindingSource();
                BindCombo.DataSource = lst;

                comb.DataSource = BindCombo.DataSource;

                comb.DisplayMember = "IdAccount";
                comb.ValueMember = "IdAccount";
                comb.Refresh();
            }



        };
        if (InvokeRequired)
            BeginInvoke(action);
        else
            action();


    }


}
