using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LeftHand.MemberShip;

public partial class _MasterPage_Base_Manager : System.Web.UI.MasterPage
{
    User _CurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = SwitchUserManager.GetCurrentUser();

        if (_CurrentUser.Roles.Contains(RoleKey.Login) == false) { Response.Redirect("/Default.aspx"); }
        if (_CurrentUser.Roles.Intersect(new RoleKey[] { RoleKey.Manager, RoleKey.BranchManager }).Count() == 0) { Response.End(); }
    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        //將登入使用者換成Visitor
        SwitchUserManager.SwitchToVisitor("/Default.aspx");
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //名字(密碼修改)
        this.Account.Text = _CurrentUser.Account;

        //vMemberLink
        this.vMemberLink.Visible = (_CurrentUser.Roles.Contains(RoleKey.Manager));

        //vSlideShowLink
        this.vSlideShowLink.Visible = (_CurrentUser.Roles.Contains(RoleKey.Manager));

        Render_Tabs();
    }

    private void Render_Tabs()
    {

        List<HyperLink> AllTabs = this.MainMenu.Controls.OfType<HyperLink>().ToList();

        //將現在頁面對應的tab標記起來
        HyperLink CurrentTab = AllTabs.FirstOrDefault(t => Request.RawUrl.Contains(t.NavigateUrl));
        if (CurrentTab != null) { CurrentTab.CssClass += " Selected "; }

        //開始起該權限可以看得Tab
        AllTabs.ForEach(t => t.Visible = false);
        if (_CurrentUser.Roles.Contains(RoleKey.Manager) == true)
        {
            AllTabs.ForEach(t => t.Visible = true);
        }
        else if (_CurrentUser.Roles.Contains(RoleKey.BranchManager) == true)
        {
            this.vOrderLink.Visible = true;
        }
    }
}