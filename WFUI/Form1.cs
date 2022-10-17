using DemoLib;
using System.ComponentModel;

namespace WFUI;

public partial class Form1 : Form
{
    List<AccountEvents> ListAccEvents { get; set; }
    Account? _Account { get; set; }
    List<Account> ListAccount { get; set; }

    public delegate void Gui<T>(Object ob, List<T> lst, Account? ac);
    Gui<AccountEvents> DelegatGrid;
    Gui<Account> DelegatComboBox;
   
    


    public Form1()
    {
       
        InitializeComponent();
        this.Text = "Демо версия банк-клиент";
        DelegatGrid += GridRefresh;
        DelegatComboBox += ComboRefresh;
        this.comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;  
        ListAccount = new List<Account>();

        ListAccEvents = new List<AccountEvents>();
        DelegatGrid(dataGridView1, ListAccEvents,null);

        this.Controls.OfType<Button>().ToList().ForEach(x => x.Click += new EventHandler(Buttons_Click));
        DelegatComboBox(this.comboBox1, ListAccount,null);

    }

    private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Guid guid = (Guid)comboBox1.SelectedValue;
        var acResult = ListAccount.Where(x => x.IdAccount.Equals(guid)).FirstOrDefault();
        if (acResult == null)
            return;
        Account ac = (Account)acResult;

        DelegatGrid(dataGridView1, ListAccEvents, ac);
    }

    private void Buttons_Click(object? sender, EventArgs e)
    {
        var moneyTxt = "";
        if (textBox1.Text.Length > 0)
            moneyTxt = textBox1.Text;
        textBox1.Text = "";

        if (sender == null)
            return;
        if (sender is Button btn)
        {
            if (btn.Text.Equals("Create Account"))
            {
                _Account = new Account(this.Account_Notify, 0.00M);
                ListAccount.Add(_Account);
                DelegatComboBox(this.comboBox1, ListAccount,_Account);
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
                if (Decimal.TryParse(moneyTxt, out money))
                    if (money > 0)
                        ac.Add(money);
            }
            else if (btn.Text.Equals("Take Money"))
            {
                Decimal money = 0.00M;
                if (Decimal.TryParse(moneyTxt, out money))
                    if(money >0)
                    ac.Take(money);
            }
            else if (btn.Text.Equals("Buy"))
            {
                Decimal money = 0.00M;
                if (Decimal.TryParse(moneyTxt, out money))
                {
                    if (money>0)
                    ac.Buy(money);
                }
            }
            else
                return;
            DelegatComboBox(this.comboBox1, ListAccount, ac);



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
        DelegatGrid(dataGridView1, ListAccEvents,null);      
    }


   

    void GridRefresh<T>(Object ob, List<T> lst,Account? ac) 
    {
        if (ob == null)
            return;
        if (ob is not DataGridView MyGrid)
            return; 

        if (lst is List<AccountEvents> == false)
            return; 
           var ls = lst as List<AccountEvents>;
        if (ls == null)
            return;

        List<AccountEvents> list=ls;
        if (ac != null)
        {
            list = ls.FindAll(x => x.IdAccount.Equals(ac.IdAccount));
            if (list == null || list.Count == 0)
                return;
        }
            
        Action action = () =>
            {

                var grdList = new BindingList<AccountEvents>(list);
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

    void ComboRefresh(object ob, List<Account> lst, Account? ac)
    {
        if (ob == null)
            return;
        Action action = () =>
        {
            if (ob is ComboBox comb)
            {
                var ls = lst.Select(x => x.IdAccount).ToList();
                comb.DataSource = ls;

                if (ac != null)
                    comb.SelectedItem = ac.IdAccount;
             }

        };
        if (InvokeRequired)
            BeginInvoke(action);
        else
            action();
    }





}
