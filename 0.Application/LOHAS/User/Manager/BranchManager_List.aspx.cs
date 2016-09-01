using LeftHand.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Manager_Member_List : System.Web.UI.Page
{
    User _CurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = SwitchUserManager.GetCurrentUser();
    }

    protected void vMemberList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            User User = (User)e.Item.DataItem;

            //vAccount
            Literal vAccount = (Literal)e.Item.FindControl("vAccount");
            vAccount.Text = User.Account;

            //vName
            Literal vName = (Literal)e.Item.FindControl("vName");
            vName.Text = User.Profiles[ProfileKey.姓名];

            //vBranchShop
            Literal vBranchShop = (Literal)e.Item.FindControl("vBranchShop");
            vBranchShop.Text = User.Profiles[ProfileKey.分店];

            //vRemark
            Literal vRemark = (Literal)e.Item.FindControl("vRemark");
            vRemark.Text = User.Profiles[ProfileKey.備註];

            //Edit
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Account", User.Account);
            vEdit.NavigateUrl = "/User/Manager/BranchManager_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_MemberAdd();
        Render_MembershipList();
    }

    private void Render_MemberAdd()
    {
        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Account", "");
        this.vAdd.NavigateUrl = "/User/Manager/BranchManager_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        this.vAdd.Visible = _CurrentUser.Roles.Contains(RoleKey.Manager) == true;
    }

    private void Render_MembershipList()
    {
        List<User> Members = UserManager.GetRoleUsers(RoleKey.BranchManager);

        this.Pagger1.PageSize = 30;
        this.Pagger1.DataAmount = Members.Count;

        this.vMemberList.DataSource = Members.Where((a, index) => index >= this.Pagger1.DataStartIndex - 1 && index <= this.Pagger1.DataEndIndex - 1);
        this.vMemberList.DataBind();
    }
}