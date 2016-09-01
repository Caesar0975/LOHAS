using LeftHand.MemberShip;
using Lohas.Area;
using Lohas.Order;
using Lohas.Round;
using Lohas.School;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_Member_Order_Form : System.Web.UI.Page
{
    User _CurrentUser;

    List<AreaItem> _AllAreas;
    List<SchoolItem> _AllSchool;
    List<ShopItem> _AllShops;

    OrderItem _CurrentOrderItem { get { return (OrderItem)Session["_CurrentOrderItem"]; } set { Session["_CurrentOrderItem"] = value; } }

    bool _IsUpdateMode { get { return (bool)ViewState["_IsUpdateMode"]; } set { ViewState["_IsUpdateMode"] = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = SwitchUserManager.GetCurrentUser();
        _AllAreas = AreaItemManager.GetAll();
        _AllShops = ShopItemManager.GetAll();

        if (Page.IsPostBack == false)
        {
            string AES_OrderItemId = Server.UrlDecode(Request.QueryString.ToString());
            if (string.IsNullOrWhiteSpace(AES_OrderItemId))
            {
                _IsUpdateMode = false;
                _CurrentOrderItem = new OrderItem(null, null, null, DateTime.Now.Date, "");
            }
            else
            {
                _IsUpdateMode = true;
                _CurrentOrderItem = OrderItemManager.GetById(decimal.Parse(LeftHand.Gadget.Encoder.AES_Decryption(AES_OrderItemId)));
            }
        }
    }

    //選擇Area
    protected void vAreaSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        _CurrentOrderItem.MemberArea = this.vAreaSelector.SelectedItem.Text;
        _CurrentOrderItem.MemberSchool = "";

        //ShopInfos
        _CurrentOrderItem.ShopId = "";
        _CurrentOrderItem.ShopName = "";
        _CurrentOrderItem.ShopPhone = "";
        _CurrentOrderItem.ShopAddress = "";
        _CurrentOrderItem.Shoplatitude = "";
        _CurrentOrderItem.Shoplongitude = "";

        //OrderRound
        _CurrentOrderItem.OrderRound = "";
    }

    //選擇School
    protected void vSchoolSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        _CurrentOrderItem.MemberSchool = this.vSchoolSelector.SelectedItem.Text;

        //ShopInfos
        _CurrentOrderItem.ShopId = "";
        _CurrentOrderItem.ShopName = "";
        _CurrentOrderItem.ShopPhone = "";
        _CurrentOrderItem.ShopAddress = "";
        _CurrentOrderItem.Shoplatitude = "";
        _CurrentOrderItem.Shoplongitude = "";

        //OrderRound
        _CurrentOrderItem.OrderRound = "";
    }

    //選擇Shop
    protected void vShopItem_Click(object sender, EventArgs e)
    {
        LinkButton LinkButton = (LinkButton)sender;

        ShopItem ShopItem = ShopItemManager.GetById(LinkButton.CommandArgument);
        _CurrentOrderItem.ShopId = ShopItem.Id;
        _CurrentOrderItem.ShopName = ShopItem.Name;
        _CurrentOrderItem.ShopPhone = ShopItem.Phone;
        _CurrentOrderItem.ShopAddress = ShopItem.Address;
        _CurrentOrderItem.Shoplatitude = ShopItem.Latitude;
        _CurrentOrderItem.Shoplongitude = ShopItem.Longitude;

        //OrderRound
        _CurrentOrderItem.OrderRound = "";

        //UpdateGoogleMap
        string Url = string.Format("http://maps.google.com.tw/maps?f=q&hl=zh-TW&geocode=&q={0},{1}({2})&z=16&output=embed&t=p", ShopItem.Latitude, ShopItem.Longitude, ShopItem.Name);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UpdateGoogleMap", "UpdateGoogleMap('" + Url + "');", true);
    }

    //選擇Round
    protected void vRoundItem_Click(object sender, EventArgs e)
    {
        LinkButton LinkButton = (LinkButton)sender;

        _CurrentOrderItem.OrderRound = LinkButton.CommandArgument;
    }

    //送出預約
    protected void vSendButton_Click(object sender, EventArgs e)
    {
        try
        {
            //檢查是否所有資都填了
            List<string> Errors = new List<string>();

            #region MemberInfo

            //vMemberName
            string MemberName = this.vMemberName.Text.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(MemberName) == true) { Errors.Add("請輸入 大學生姓名"); }

            //MemberIdCardNumber
            string MemberIdCardNumber = this.vMemberAccount.Text.Replace(" ", "").ToUpper();
            if (string.IsNullOrWhiteSpace(MemberIdCardNumber) == true) { Errors.Add("請輸入 身分證字號"); }
            else if (UserManager.IdCheck(MemberIdCardNumber) == false) { Errors.Add("身分證字號m錯誤"); }
            else if (ConfigManager.GetByConfigKey(ConfigKey.黑名單).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Contains(MemberIdCardNumber) == true) { Errors.Add("身份證字號無效"); }

            //vMemberPhone
            string MemberPhone = this.vMemberPhone.Text.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(MemberPhone) == true) { Errors.Add("請輸入 行動電話"); }
            else if (MemberPhone.Contains("09") == false) { Errors.Add("行動電話格式不正確"); }

            #endregion

            #region MemberArea

            string Area = this.vAreaSelector.SelectedItem.Text;
            if (Area == "請選擇") { Errors.Add("請選擇 地區"); }

            string School = this.vSchoolSelector.SelectedItem.Text;
            if (School == "請選擇") { Errors.Add("請選擇 學校"); }

            #endregion

            #region ShopInfos

            if (string.IsNullOrWhiteSpace(_CurrentOrderItem.ShopId) == true) { Errors.Add("請選擇 預約LOHAS門市"); }
            else if (string.IsNullOrWhiteSpace(_CurrentOrderItem.ShopName) == true) { Errors.Add("請選擇 預約LOHAS門市"); }
            else if (string.IsNullOrWhiteSpace(_CurrentOrderItem.ShopPhone) == true) { Errors.Add("請選擇 預約LOHAS門市"); }
            else if (string.IsNullOrWhiteSpace(_CurrentOrderItem.ShopAddress) == true) { Errors.Add("請選擇 預約LOHAS門市"); }
            else if (string.IsNullOrWhiteSpace(_CurrentOrderItem.Shoplatitude) == true) { Errors.Add("請選擇 預約LOHAS門市"); }
            else if (string.IsNullOrWhiteSpace(_CurrentOrderItem.Shoplongitude) == true) { Errors.Add("請選擇 預約LOHAS門市"); }

            #endregion

            #region OrderRound

            DateTime OrderDate;
            if (DateTime.TryParse(this.vSelectedDate.Value.Trim(), out OrderDate) == false) { Errors.Add("請選擇 預約日期"); }

            if (string.IsNullOrWhiteSpace(_CurrentOrderItem.OrderRound) == true) { Errors.Add("請選擇 預約時段"); }
            #endregion

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            //檢查 重複報名            
            if (_IsUpdateMode == false && OrderItemManager.Get(MemberIdCardNumber, MemberPhone) != null) { Errors.Add("每人僅有一次機會，不可重複預約"); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }


            //檢查 是否額滿
            int SameRoundOrderAmount = OrderItemManager.GetRoundCount(_CurrentOrderItem.ShopId, (DateTime)_CurrentOrderItem.OrderDate, _CurrentOrderItem.OrderRound);
            int LimitPairAmount = ShopItemManager.GetById(_CurrentOrderItem.ShopId).RoundItems.FirstOrDefault(r => r.Name == _CurrentOrderItem.OrderRound).LimitPairAmount;
            if ((SameRoundOrderAmount + 1) > LimitPairAmount) { Errors.Add("場次已經額滿，請挑選新的場次"); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _CurrentOrderItem.MemberName = MemberName;
            _CurrentOrderItem.MemberAccount = MemberIdCardNumber;
            _CurrentOrderItem.MemberPhone = MemberPhone;

            _CurrentOrderItem.MemberArea = Area;
            _CurrentOrderItem.MemberSchool = School;

            _CurrentOrderItem.OrderDate = OrderDate;

            OrderItemManager.SaveTemp(_CurrentOrderItem);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "top.location.replace('Questionnaire_Form.aspx');", true);

            //OrderItemManager.Save(_CurrentOrderItem);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "top.location.replace('Finish_Form.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.AES_Encryption(_CurrentOrderItem.Id.ToString())) + "');", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.vMemberName.Text = _CurrentOrderItem.MemberName;
            this.vMemberAccount.Text = _CurrentOrderItem.MemberAccount;
            this.vMemberPhone.Text = _CurrentOrderItem.MemberPhone;
        }

        //vAreaSelector
        this.vAreaSelector.Items.Clear();
        this.vAreaSelector.Items.Add(new ListItem("請選擇", ""));
        this.vAreaSelector.Items.AddRange(_AllAreas.Select(a => new ListItem(a.Name, a.Id)).ToArray());
        ListItem AreaListItem = this.vAreaSelector.Items.FindByText(_CurrentOrderItem.MemberArea);
        if (AreaListItem != null) { AreaListItem.Selected = true; }

        //vSchoolSelector
        this.vSchoolSelector.Items.Clear();
        this.vSchoolSelector.Items.Add(new ListItem("請選擇", ""));
        this.vSchoolSelector.Items.AddRange(SchoolItemManager.GetByArea(AreaItemManager.GetByName(_CurrentOrderItem.MemberArea)).Select(a => new ListItem(a.Name, a.Id)).ToArray());
        ListItem SchoolListItem = this.vSchoolSelector.Items.FindByText(_CurrentOrderItem.MemberSchool);
        if (SchoolListItem != null) { SchoolListItem.Selected = true; }

        //vShopList
        this.vShopList.DataSource = ShopItemManager.GetBySchool(SchoolItemManager.GetByName(_CurrentOrderItem.MemberSchool));
        this.vShopList.DataBind();

        //Calandar
        DateTime MinDate = DateTime.Now.Date.AddDays(1);
        string MinDateString = string.Format("new Date({0},{1},{2})", MinDate.Year, MinDate.Month - 1, MinDate.Day);

        DateTime MaxDate = DateTime.Now.AddDays(60).Date;
        string MaxDateString = string.Format("new Date({0},{1},{2})", MaxDate.Year, MaxDate.Month - 1, MaxDate.Day);

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "BindCalander", string.Format("BindCalander({0},{1});", MinDateString, MaxDateString), true);

        if (string.IsNullOrWhiteSpace(this.vSelectedDate.Value) == false) { _CurrentOrderItem.OrderDate = DateTime.Parse(this.vSelectedDate.Value).Date; }

        //RoundList    
        ShopItem SelectedShop = ShopItemManager.GetById(_CurrentOrderItem.ShopId);
        if (SelectedShop != null && SelectedShop.RoundItems != null && _CurrentOrderItem.OrderDate != null)
        {
            //還沒有額滿的回合，包含當日還沒有愈時的
            List<RoundItem> NotFullRounds = new List<RoundItem>();

            Dictionary<string, int> RoundCount = OrderItemManager.GetRoundCount(_CurrentOrderItem.ShopId, (DateTime)_CurrentOrderItem.OrderDate);
            foreach (RoundItem Round in SelectedShop.RoundItems)
            {
                DateTime OrderDate = (DateTime)_CurrentOrderItem.OrderDate;

                if (OrderDate <= DateTime.Now && Round.StartTime < DateTime.Now.TimeOfDay) { continue; }
                if (RoundCount.ContainsKey(Round.Name) == false) { NotFullRounds.Add(Round); continue; }
                if (RoundCount[Round.Name] < Round.LimitPairAmount == true) { NotFullRounds.Add(Round); continue; }
            }

            if (NotFullRounds.Count > 0)
            {
                this.vNoRound.Text = "";

                this.vRoundList.DataSource = NotFullRounds;
                this.vRoundList.DataBind();
            }
            else
            {
                this.vNoRound.Text = "所有時段都已被預訂，請選擇其他日期或其他分店。";

                this.vRoundList.DataSource = null;
                this.vRoundList.DataBind();
            }

        }

        this.vSendButton.Text = "下一步";

        //Bind_IdCardNumberCheck
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Bind_IdCardNumberCheck", "Bind_IdCardNumberCheck();", true);

    }

    protected void vShopList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ShopItem ShopItem = (ShopItem)e.Item.DataItem;

            //vShopItem
            LinkButton vShopItem = (LinkButton)e.Item.FindControl("vShopItem");
            vShopItem.CommandArgument = ShopItem.Id;
            vShopItem.CssClass = (ShopItem.Id == _CurrentOrderItem.ShopId) ? "ShopItem Selected" : "ShopItem";

            //vName
            Label vName = (Label)e.Item.FindControl("vName");
            vName.Text = ShopItem.Name;

            //vAddress
            Label vAddress = (Label)e.Item.FindControl("vAddress");
            vAddress.Text = ShopItem.Address;
        }
    }

    protected void vRoundList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RoundItem RoundItem = (RoundItem)e.Item.DataItem;

            //vRoundItem
            LinkButton vRoundItem = (LinkButton)e.Item.FindControl("vRoundItem");
            vRoundItem.CommandArgument = RoundItem.Name;
            vRoundItem.Text = RoundItem.Name;
            vRoundItem.CssClass = (RoundItem.Name == _CurrentOrderItem.OrderRound) ? "RoundItem Selected" : "RoundItem";
        }
    }

}