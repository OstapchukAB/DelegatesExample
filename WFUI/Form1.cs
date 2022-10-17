using DemoLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

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
        textBox2.Text = account.CashBack.ToString("C2");     
    }

    private void Buttons_Click(object? sender, EventArgs e)
    {
        var moneyTxt = "";
        var moneyCash = "";
        if (textBox1.Text.Length > 0)
            moneyTxt = textBox1.Text;
        textBox1.Text = "";
        if (textBox2.Text.Length > 0)
            moneyCash = textBox2.Text;
        textBox2.Text = "";

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
                Decimal cash = 0.00M;
                if (Decimal.TryParse(moneyTxt, out money))
                {
                    if (Decimal.TryParse(moneyCash, out cash))
                        if (money > 0 && cash >= 0)
                        {
                            if (this.checkBox1.Checked == false) 
                                cash = 0.00M;
                            else
                                this.checkBox1.Checked = false;

                            ac.Buy(money, cash);
                        }
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
              datetime:e.Datetime, 
              idOperation: AccountEventArgs.IdOperation,
              idOperationAccount: sender.IdOperationAccount,
              idAccount: e.IdAccount,
              message: e.Message,
              sumOperation: e.SumOperation,
              sumAccount: sender.SumAccount,
              sumBuy: sender.SumBuy,
              cashBack: sender.CashBack
            ));      
        DelegatGrid(dataGridView1, SelAcEv(ListAccEvents,sender));      
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

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch=  (CheckBox)sender;
        if (ch.Checked)
        {
            textBox2.ReadOnly = true;
            Decimal sum1, cash;

            Decimal.TryParse(textBox1.Text, out sum1);
            Decimal.TryParse(textBox2.Text, out cash);
            textBox3.Text = (sum1 + cash).ToString("C2");
        }
        else
        {
            textBox2.ReadOnly = false;
            textBox3.Text = "";
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        //финишная проверка
        //Regex rg = new Regex(@"\d+\,\d{2}$");
        Regex rg = new Regex(@"^[0-9]+,?\d{0,2}$");

        if (rg.Match(textBox1.Text).Success)
            return;
        else
            textBox1.Text = "";  
    }
}
