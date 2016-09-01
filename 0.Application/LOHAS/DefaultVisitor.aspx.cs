using LeftHand.MemberShip;
using Lohas.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultVisitor : System.Web.UI.Page
{
    User _CurrentUser;
    Dictionary<ConfigKey, string> _AllConfigs;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = SwitchUserManager.GetCurrentUser();
        _AllConfigs = ConfigManager.GetAll();

        Update_Counter();
    }

    private void Update_Counter()
    {
        int OldCounter = int.Parse(ConfigManager.GetByConfigKey(ConfigKey.計數器));
        int Incremental = (new Random()).Next(0, 3);

        _AllConfigs[ConfigKey.計數器] = (OldCounter + Incremental).ToString();

        ConfigManager.Save(_AllConfigs);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_vActivity();
        Render_vSlideShowList();
    }

    private void Render_vActivity()
    {
        if (Page.IsPostBack == true) { return; }

        this.vActivity.Text = _AllConfigs[ConfigKey.活動說明];
        this.vCounter.Text = _AllConfigs[ConfigKey.計數器];
        this.vActivity2.Text = _AllConfigs[ConfigKey.活動說明2];
    }

    private void Render_vSlideShowList()
    {
        if (Page.IsPostBack == true) { return; }

        this.vSlideShowList.DataSource = SlideShowItemManager.GetAll();
        this.vSlideShowList.DataBind();
    }
    protected void vSlideShowList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SlideShowItem SlideShowItem = (SlideShowItem)e.Item.DataItem;

            //SlideShowItem
            HyperLink vSlideShowItem = (HyperLink)e.Item.FindControl("vSlideShowItem");
            vSlideShowItem.ImageUrl = SlideShowItemManager.GetUploadPath() + SlideShowItem.Image;
            if (string.IsNullOrWhiteSpace(SlideShowItem.Url) == false) { vSlideShowItem.NavigateUrl = SlideShowItem.Url; }
        }
    }


}