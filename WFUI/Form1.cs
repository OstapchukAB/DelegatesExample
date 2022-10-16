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
    

    public delegate void GuiRefresh<T>(object ob, List<T> lst);
    GuiRefresh<Account> Gui;
    List<Account> ListAccount { get; set; }
    
    //BindingSource BindCombo => new();


    public Form1()
    {
        InitializeComponent();
        Grid += GridRefresh;
        Gui += ComboRefresh;
        ListAccount = new List<Account>();

        ListAccEvents = new List<AccountEvents>();
        Grid(dataGridView1, ListAccEvents);

        this.Controls.OfType<Button>().ToList().ForEach(x => x.Click += new EventHandler(Buttons_Click));
        Gui(this.comboBox1, ListAccount);

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
                Gui(this.comboBox1, ListAccount);
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
            
            Guid guid = (Guid)comboBox1.SelectedValue;
            var acResult=ListAccount.Where(x => x.IdAccount.Equals(guid)).FirstOrDefault();
            if (acResult == null)
                return;
            Account ac = (Account)acResult;

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
            };
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
    }

    void ComboRefresh(object ob, List<Account> lst)
    {
        if (ob == null)
            return;
        Action action = () =>
        {
            if (ob is ComboBox comb)
            {
                var ls = lst.Select(x => x.IdAccount).ToList();
                comb.DataSource = ls;
            }
        };
        if (InvokeRequired)
            BeginInvoke(action);
        else
            action();
    }





}
