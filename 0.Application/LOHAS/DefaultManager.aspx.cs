using LeftHand.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _ManagerLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void vLoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            string Account = this.vAccount.Text.Trim();
            if (string.IsNullOrWhiteSpace(Account) == true) { Errors.Add("帳號不可空白"); }

            string Password = this.vPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(Password) == true) { Errors.Add("密碼不可空白"); }

            string ValidCode = this.vValidCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(ValidCode) == true) { Errors.Add("驗證碼不可空白"); }

            if (Errors.Count() > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            User User = UserManager.GetUser(Account);
            if (User == null) { Errors.Add("帳號或密碼錯誤"); }
            else if (User.Roles.Intersect(new RoleKey[] { RoleKey.Manager, RoleKey.BranchManager }).Count() == 0) { Errors.Add("帳號或密碼錯誤"); }

            if (Errors.Count() > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            SwitchUserManager.SwitchUser(Account, Password, ValidCode, false, "/Order/Manager/Order_List.aspx");
        }
        catch (Exception ex)
        {
            this.vValidCode.Text = "";

            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        SwitchUserManager.GetNewValidCode();
        this.vValidCodeImage.ImageUrl = "/_Element/ValidCode/RandomNumberImage.ashx";
        this.Page.Title = "管理者登入";
    }
}