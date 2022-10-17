using DemoLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WFUI;

public partial class Form1 : Form
{
    /// <summary>
    /// Список событий по аккаунту
    /// </summary>
    List<AccountEvents> ListAccEvents { get; set; }
    
    /// <summary>
    /// Список аккаунтов
    /// </summary>
    List<Account> ListAccount { get; set; }
   
    public delegate void GuiGrid<Tlist>( DataGridView  grid, List<Tlist> selList );

    public delegate void GuiSingle<T>(Object ob, List<T> list, T? select);

    readonly GuiGrid<AccountEvents> DelegatGrid;

    readonly GuiSingle<Account> DelegatComboBox;

   
    public Form1()
    {
       
        InitializeComponent();
        this.Text = "Демо версия банк-клиент";
        this.comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

        DelegatGrid += GridRefresh;
        DelegatComboBox += ComboRefresh;
         
        
        ListAccount = new List<Account>();
        ListAccEvents = new List<AccountEvents>();

        DelegatGrid(dataGridView1, SelAcEv(ListAccEvents));

        //Привязываем одного обработчика на нажатие всех кнопок
        this.Controls.OfType<Button>().ToList().ForEach(x => x.Click += new EventHandler(Buttons_Click));

        //привязываем combobox1
        DelegatComboBox(this.comboBox1, ListAccount, null);

    }

    static List<AccountEvents> SelAcEv(List<AccountEvents> list, Account? ac = null)
    {
        if (ac == null)
            return list;

        var selList = list.FindAll(x => x.IdAccount.Equals(ac.IdAccount));
        return selList;
    }

    private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Guid guid = (Guid)comboBox1.SelectedValue;
        var acResult = ListAccount.Where(x => x.IdAccount.Equals(guid)).FirstOrDefault();
        if (acResult == null)
            return;
        Account account = (Account)acResult;

        DelegatGrid(dataGridView1, SelAcEv(ListAccEvents, account));
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
                Account account = new Account(this.Account_Notify, 0.00M);
                ListAccount.Add(account);
                DelegatComboBox(this.comboBox1, ListAccount, account);
                account.AlgCashBack += (decimal sumBuy) =>
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
        DelegatGrid(dataGridView1, SelAcEv(ListAccEvents));      
    }


   

    void GridRefresh<Tlist>(DataGridView grid, List<Tlist> selList)
    {
                
        Action action = () =>
            {
                var grdList = new BindingList<Tlist>(selList);
                grid.DataSource = grdList;
                grid.AllowUserToAddRows = false;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            };
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
    }

    void ComboRefresh(object ob, List<Account> lst, Account? ac=null)
    {
        if (ob == null)
            return;
        Action action = () =>
        {
            if (ob is not ComboBox comb)
                return; 
                var ls = lst.Select(x => x.IdAccount).ToList();
                comb.DataSource = ls;

                if (ac != null)
                    comb.SelectedItem = ac.IdAccount;
            

        };
        if (InvokeRequired)
            BeginInvoke(action);
        else
            action();
    }





}
