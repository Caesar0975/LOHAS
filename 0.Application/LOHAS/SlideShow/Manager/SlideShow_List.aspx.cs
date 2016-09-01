using Lohas.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SlideShow_Manager_SlideShow_List : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void vSlideShowList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SlideShowItem SlideShowItem = (SlideShowItem)e.Item.DataItem;

            //圖片
            Image vImage = (Image)e.Item.FindControl("vImage");
            vImage.ImageUrl = SlideShowItemManager.GetUploadPath() + SlideShowItem.Image;

            //連結網址
            Label vUrl = (Label)e.Item.FindControl("vUrl");
            vUrl.Text = SlideShowItem.Url;

            //顯示
            Literal vIsEnable = (Literal)e.Item.FindControl("vIsEnable");
            vIsEnable.Text = (SlideShowItem.Enable == true) ? "V" : "";

            //排序
            Literal vSort = (Literal)e.Item.FindControl("vSort");
            vSort.Text = SlideShowItem.Sort.ToString();

            //編輯
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", SlideShowItem.Id.ToString());
            vEdit.NavigateUrl = "SlideShow_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_SlideShowList();
        Render_Add();
    }

    private void Render_SlideShowList()
    {
        this.vSlideShowList.DataSource = SlideShowItemManager.GetAll();
        this.vSlideShowList.DataBind();
    }

    private void Render_Add()
    {
        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Id", "");
        Add.NavigateUrl = "SlideShow_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
    }
}