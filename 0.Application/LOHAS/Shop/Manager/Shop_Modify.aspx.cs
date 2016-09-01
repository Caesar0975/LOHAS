using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lohas.Shop;
using System.Text;
using Lohas.Area;
using Lohas.School;

public partial class Shop_Manager_Shop_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    List<AreaItem> _AreaItems;
    List<SchoolItem> _SchoolItems;

    ShopItem _ShopItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        switch (_Argument["Mode"])
        {
            case "Add":
                _ShopItem = new ShopItem("", "", "", "", "");
                break;

            case "Edit":
                _ShopItem = ShopItemManager.GetById(_Argument["Id"]);
                break;
        }

        //_AreaItems
        _AreaItems = AreaItemManager.GetAll();

        //_SchoolItems
        _SchoolItems = SchoolItemManager.GetAll();
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();
            //名稱
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrWhiteSpace(Name) == true) { Errors.Add("名稱不能為空"); }

            //電話
            string Phone = this.vPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(Phone) == true) { Errors.Add("電話不能為空"); }

            //地址
            string Address = this.vAddress.Text.Trim();
            if (string.IsNullOrWhiteSpace(Address) == true) { Errors.Add("地址不能為空"); }

            //緯度
            string Latitude = this.vLatitude.Text.Trim();
            if (string.IsNullOrWhiteSpace(Latitude) == true) { Errors.Add("緯度不能為空"); }

            //經度
            string Longitude = this.vLongitude.Text.Trim();
            if (string.IsNullOrWhiteSpace(Longitude) == true) { Errors.Add("經度不能為空"); }

            //排序
            int Sort;
            if (int.TryParse(this.vSort.Text, out Sort) == false) { Errors.Add("排序格式錯誤，請輸入整數數字"); }

            //學校
            List<string> SchoolItemIds = new List<string>();
            foreach (RepeaterItem RepeaterItem in vAreaList.Items)
            {
                CheckBoxList vSchoolList = (CheckBoxList)RepeaterItem.FindControl("vSchoolList");
                foreach (ListItem ListItem in vSchoolList.Items)
                {
                    if (ListItem.Selected == false) { continue; }

                    SchoolItemIds.Add(ListItem.Value);
                }
            }


            if (Errors.Count > 0) { throw new Exception (string.Join("\\r\\n", Errors.ToArray())); }

            _ShopItem.SchoolItemIds = SchoolItemIds;
            _ShopItem.Name = Name;
            _ShopItem.Phone = Phone;
            _ShopItem.Address = Address;
            _ShopItem.Latitude = Latitude;
            _ShopItem.Longitude = Longitude;
            _ShopItem.Sort = Sort;

            ShopItemManager.Save(_ShopItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Exception", "alert('" + ex.Message + "');", true);
        }
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ShopItemManager.Remove(_ShopItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('刪除成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_ShopItem();
    }

    private void Render_ShopItem()
    {
        if (this.IsPostBack == false)
        {
            //名稱
            this.vName.Text = _ShopItem.Name;

            //電話
            this.vPhone.Text = _ShopItem.Phone;

            //地址
            this.vAddress.Text = _ShopItem.Address;

            //緯度
            this.vLatitude.Text = _ShopItem.Latitude;

            //經度
            this.vLongitude.Text = _ShopItem.Longitude;

            //排序
            this.vSort.Text = _ShopItem.Sort.ToString();

            //選擇學校
            vAreaList.DataSource = _AreaItems;
            vAreaList.DataBind();
        }

        this.vDelete.OnClientClick = "return confirm('你確定要刪除此使用者');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }

    protected void vAreaList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            AreaItem AreaItem = (AreaItem)e.Item.DataItem;

            //vArea
            Label vArea = (Label)e.Item.FindControl("vArea");
            vArea.Text = AreaItem.Name;

            //vAreaSchoolList            
            CheckBoxList vSchoolList = (CheckBoxList)e.Item.FindControl("vSchoolList");
            foreach (SchoolItem SchoolItem in _SchoolItems.Where(s => s.AreaItemId == AreaItem.Id).OrderByDescending(a => a.Sort).ToList())
            {
                ListItem ListItem = new ListItem();
                ListItem.Text = SchoolItem.Name;
                ListItem.Value = SchoolItem.Id;
                ListItem.Selected = _ShopItem.SchoolItemIds.Contains(SchoolItem.Id);

                vSchoolList.Items.Add(ListItem);
            }
        }
    }

}