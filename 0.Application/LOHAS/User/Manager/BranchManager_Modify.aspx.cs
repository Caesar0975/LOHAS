using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.MemberShip;
using System.Text;
using Lohas.Area;
using Lohas.School;
using Lohas.Shop;

public partial class User_Manager_Member_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    User _BranchManager;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        switch (_Argument["Mode"])
        {
            case "Add":
                _BranchManager = UserManager.GetNewBranchManager("", "");
                break;

            case "Edit":
                _BranchManager = UserManager.GetUser(_Argument["Account"]);
                break;
        }
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            UserManager.RemoveUser(_BranchManager);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('刪除成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //帳號
            string Account = this.vAccount.Text.Trim();
            if (string.IsNullOrWhiteSpace(Account) == true) { Errors.Add("帳號不可空白"); }

            //密碼
            string Password = this.vPassword.Text.Trim();
            if (_Argument["Mode"] == "Add")
            {
                if (string.IsNullOrWhiteSpace(Password) == true) { Errors.Add("密碼不可空白"); }
            }
            else if (_Argument["Mode"] == "Edit")
            {
                if (string.IsNullOrWhiteSpace(Password) == true) { Password = _BranchManager.Password; }
            }

            //姓名
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrWhiteSpace(Name) == true) { Errors.Add("姓名不可空白"); }

            //分店
            string BranchShop = this.vShopSelector.SelectedItem.Text.Replace("請選擇", "");
            if (string.IsNullOrWhiteSpace(BranchShop) == true) { Errors.Add("請選擇分店"); }

            //備註
            string Remark = this.vRemark.Text;

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _BranchManager.Account = Account;
            _BranchManager.Password = Password;
            _BranchManager.Profiles[ProfileKey.姓名] = Name;
            _BranchManager.Profiles[ProfileKey.分店] = BranchShop;
            _BranchManager.Profiles[ProfileKey.備註] = Remark;

            UserManager.SaveUser(_BranchManager);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_UserItem();
    }

    private void Render_UserItem()
    {
        if (Page.IsPostBack == true) { return; }

        //帳號
        this.vAccount.Text = _BranchManager.Account;
        this.vAccount.ReadOnly = (_Argument["Mode"] == "Edit");

        //姓名
        this.vName.Text = _BranchManager.Profiles[ProfileKey.姓名];

        //分店
        List<ShopItem> AllShop = ShopItemManager.GetAll();
        this.vShopSelector.Items.Add(new ListItem("請選擇", ""));
        this.vShopSelector.Items.AddRange(AllShop.Select(s => new ListItem(s.Name, s.Id)).ToArray());

        string BranchManagerShop = _BranchManager.Profiles[ProfileKey.分店];
        if (AllShop.FirstOrDefault(s => s.Name == BranchManagerShop) != null) { this.vShopSelector.Items.FindByText(BranchManagerShop).Selected = true; }

        //備註
        this.vRemark.Text = _BranchManager.Profiles[ProfileKey.備註];

        //vDelete
        this.vDelete.OnClientClick = "return confirm('確定要刪除?');";
        this.vDelete.Visible = true;
    }

}