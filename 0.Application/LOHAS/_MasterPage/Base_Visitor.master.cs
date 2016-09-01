using LeftHand.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _MasterPage_Base_Member : System.Web.UI.MasterPage
{
    User _CurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = SwitchUserManager.GetCurrentUser();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_SeoTag();
        Render_vGoogleAnalytics();
    }

    private void Render_SeoTag()
    {
        if (Page.IsPostBack == true) { return; }

        //Title
        this.Page.Title = ConfigManager.GetByConfigKey(ConfigKey.SeoTitle);

        //Description
        this.Page.MetaDescription = ConfigManager.GetByConfigKey(ConfigKey.SeoDescription);
    }

    private void Render_vGoogleAnalytics()
    {
        if (Page.IsPostBack == true) { return; }

        this.vAnalyticsCode.Text = ConfigManager.GetByConfigKey(ConfigKey.AnalyticsCode);
    }
}
