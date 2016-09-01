using Lohas.Area;
using Lohas.School;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shop_Manager_Shop_List : System.Web.UI.Page
{
    //選取的區域
    string _AreaSelected { get { return ViewState["_AreaSelected"].ToString(); } set { ViewState["_AreaSelected"] = value; } }

    List<SchoolItem> _SchoolItems;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            _AreaSelected = (AreaItemManager.GetAll().Count == 0) ? "" : AreaItemManager.GetAll()[0].Id;
        }
        else
        {
            //判斷選項是否存在
            _AreaSelected = (AreaItemManager.GetById(_AreaSelected) != null) ? _AreaSelected : "";
        }

        //_SchoolItems
        _SchoolItems = SchoolItemManager.GetAll();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_ShopItemAdd();
        Render_ShopItemList();
    }

    private void Render_ShopItemAdd()
    {
        this.vShopItemAdd.Visible = (string.IsNullOrWhiteSpace(_AreaSelected) == false);

        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Id", "");
        Argument.Add("AreaId", _AreaSelected);
        this.vShopItemAdd.NavigateUrl = "/Shop/Manager/Shop_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));

    }

    private void Render_ShopItemList()
    {
        this.vShopList.DataSource = ShopItemManager.GetAll();
        this.vShopList.DataBind();
    }

    protected void vShopList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ShopItem ShopItem = (ShopItem)e.Item.DataItem;

            //vName
            Literal vName = (Literal)e.Item.FindControl("vName");
            vName.Text = ShopItem.Name;

            //vPhone
            Literal vPhone = (Literal)e.Item.FindControl("vPhone");
            vPhone.Text = ShopItem.Phone;

            //vAddress
            Literal vAddress = (Literal)e.Item.FindControl("vAddress");
            vAddress.Text = ShopItem.Address;

            //vRoundsEdit
            HyperLink vRoundsEdit = (HyperLink)e.Item.FindControl("vRoundsEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Id", ShopItem.Id);
            vRoundsEdit.NavigateUrl = "/Shop/Manager/Round_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));

            //vRounds
            Literal vRounds = (Literal)e.Item.FindControl("vRounds");
            vRounds.Text = string.Join("<br/>", ShopItem.RoundItems.OrderBy(r => r.StartTime).Select(s => s.Name + "&nbsp;&nbsp;(限" + s.LimitPairAmount + "人)").ToArray());

            //vSchool            
            Literal vSchools = (Literal)e.Item.FindControl("vSchools");
            vSchools.Text = string.Join(", ", _SchoolItems.Where(s => ShopItem.SchoolItemIds.Contains(s.Id)).Select(s => s.Name).ToArray());
            if (vSchools.Text.Length > 200) { vSchools.Text = vSchools.Text.Substring(0, 200) + "..."; }

            //Latitude
            Literal vLatitude = (Literal)e.Item.FindControl("vLatitude");
            vLatitude.Text = ShopItem.Latitude;

            //Longitude
            Literal vLongitude = (Literal)e.Item.FindControl("vLongitude");
            vLongitude.Text = ShopItem.Longitude;

            //Sort
            Literal vSort = (Literal)e.Item.FindControl("vSort");
            vSort.Text = ShopItem.Sort.ToString();

            //Edit
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            Argument.Add("Mode", "Edit");
            vEdit.NavigateUrl = "/Shop/Manager/Shop_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));

        }
    }
}