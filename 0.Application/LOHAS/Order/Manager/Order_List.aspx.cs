using LeftHand.MemberShip;
using Lohas.Order;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Issue_Manager_Plan_List : System.Web.UI.Page
{
    User _CurrentUser;

    //選取的門市
    string _SelectedShop { get { return ViewState["_SelectedShopId"].ToString(); } set { ViewState["_SelectedShopId"] = value; } }
    DateTime _SelectDate { get { return DateTime.Parse(ViewState["_SelectDate"].ToString()); } set { ViewState["_SelectDate"] = value; } }
    List<OrderItem> _MonthOrderItems { get { return (List<OrderItem>)Session["_MonthOrderItems"]; } set { Session["_MonthOrderItems"] = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = SwitchUserManager.GetCurrentUser();

        if (Page.IsPostBack == false)
        {
            _SelectDate = DateTime.Now.Date;

            if (_CurrentUser.Roles.Contains(RoleKey.Manager) == true)
            {
                _SelectedShop = (ShopItemManager.GetAll().Count == 0) ? "" : ShopItemManager.GetAll()[0].Id;
            }
            else if (_CurrentUser.Roles.Contains(RoleKey.BranchManager) == true)
            {
                ShopItem ShopItem = ShopItemManager.GetAll().FirstOrDefault(s => s.Name == _CurrentUser.Profiles[ProfileKey.分店]);
                _SelectedShop = (ShopItem == null) ? "" : ShopItem.Id;
            }
        }
        else
        {
            //判斷選項是否存在
            _SelectedShop = (ShopItemManager.GetById(_SelectedShop) == null) ? "" : _SelectedShop;
        }

        Load_MonthOrderItems();
    }

    private void Load_MonthOrderItems()
    {
        //取得月訂單
        DateTime StartMonth = new DateTime(_SelectDate.Year, _SelectDate.Month, 1);
        DateTime EndMonth = StartMonth.AddMonths(1).AddSeconds(-1);
        _MonthOrderItems = OrderItemManager.GetByOrderDate(_SelectedShop, StartMonth, EndMonth);
    }

    //切換Shop
    protected void vShop_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vShop = (LinkButton)sender;

            _SelectedShop = vShop.CommandArgument;

            Load_MonthOrderItems();
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    //切換Day
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        _SelectDate = Calendar1.SelectedDate;
    }

    //切換Month
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        _SelectDate = Calendar1.VisibleDate;

        DateTime StartMonth = new DateTime(_SelectDate.Year, _SelectDate.Month, 1);
        DateTime EndMonth = StartMonth.AddMonths(1).AddSeconds(-1);
        _MonthOrderItems = OrderItemManager.GetByOrderDate(_SelectedShop, StartMonth, EndMonth);
    }

    //訂單狀態修改
    protected void vStateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //取得選單上綁著的 OrderItemId
            RadioButtonList RadioButtonList = (RadioButtonList)sender;

            decimal OrderItemId = decimal.Parse(RadioButtonList.Attributes["OrderItemId"]);

            OrderItem OrderItem = _MonthOrderItems.FirstOrDefault(o => o.Id == OrderItemId);
            OrderItem.State = (StateType)Enum.Parse(typeof(StateType), RadioButtonList.SelectedValue);

            OrderItemManager.Save(OrderItem);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_ShopItemList();
        Render_Calendar();
        Render_OrderList();
    }

    private void Render_ShopItemList()
    {
        List<ShopItem> ShopItems = new List<ShopItem>();

        if (_CurrentUser.Roles.Contains(RoleKey.Manager) == true)
        {
            ShopItems = ShopItemManager.GetAll();
        }
        else if (_CurrentUser.Roles.Contains(RoleKey.BranchManager) == true)
        {
            ShopItems = ShopItemManager.GetAll().Where(s => s.Name == _CurrentUser.Profiles[ProfileKey.分店]).ToList();
        }

        this.vShopItemList.DataSource = ShopItems;
        this.vShopItemList.DataBind();
    }

    private void Render_Calendar()
    {
        this.Calendar1.SelectedDate = _SelectDate;
    }

    private void Render_OrderList()
    {
        DateTime SelectedDate = this.Calendar1.SelectedDate;

        DateTime StartTime = SelectedDate.Date;
        DateTime EndTime = SelectedDate.Date.AddDays(1).AddSeconds(-1);

        List<OrderItem> OrderItems = OrderItemManager.GetByOrderDate(_SelectedShop, StartTime, EndTime);

        this.vOrderList.DataSource = OrderItems;
        this.vOrderList.DataBind();
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        e.Cell.Controls.Clear();

        //總訂單
        int TotalAmount = _MonthOrderItems.Where(o => ((DateTime)o.OrderDate).Date == e.Day.Date).Count();

        //新預約訂單
        int UnProcessAmount = _MonthOrderItems.Where(o => ((DateTime)o.OrderDate).Date == e.Day.Date && o.State == StateType.新預約).Count();

        //處理中
        int ProcessAmount = _MonthOrderItems.Where(o => ((DateTime)o.OrderDate).Date == e.Day.Date && o.State == StateType.店長確認).Count();

        //已完成
        int FinishAmount = _MonthOrderItems.Where(o => ((DateTime)o.OrderDate).Date == e.Day.Date && o.State == StateType.服務完成).Count();

        //會員未到
        int NotComeAmount = _MonthOrderItems.Where(o => ((DateTime)o.OrderDate).Date == e.Day.Date && o.State == StateType.會員未到).Count();

        //Link
        HyperLink Link = new HyperLink();
        Link.NavigateUrl = e.SelectUrl;
        e.Cell.Controls.Add(Link);
        if (TotalAmount == 0)
        { Link.Text = e.Day.Date.Day.ToString(); }
        else
        {
            Link.Text = string.Format(
              @"<span class='Date'>{0}</span>
              <span class='TotalAmount Amount'>( {1} )</span><br />
              <span class='Amount'>
                <span class='UnProcessAmount'>{2}</span> / 
                <span class='ProcessAmount'>{3}</span> / 
                <span class='FinishAmount'>{4}</span> /
                <span class='NotComeAmount'>{5}</span>
              </span>",
              e.Day.Date.Day, TotalAmount, UnProcessAmount, ProcessAmount, FinishAmount, NotComeAmount);
        }

        //Bg
        if (this.Calendar1.SelectedDate == e.Day.Date)
        { e.Cell.CssClass = "DateCell SelectedDay"; }
        else
        { e.Cell.CssClass = "DateCell "; }

        if (TotalAmount != 0) { e.Cell.CssClass += " HalfHeight"; }

        //非本月的日期
        if (e.Day.IsOtherMonth == true)
        {
            e.Cell.Controls.Clear();

            e.Cell.CssClass = "DateCell OtherMonth";
        }

    }

    protected void vOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            OrderItem OrderItem = (OrderItem)e.Item.DataItem;

            //vOrderNumber
            Literal vOrderNumber = (Literal)e.Item.FindControl("vOrderNumber");
            vOrderNumber.Text = OrderItem.Id.ToString();

            //vCreateTime
            Label vCreateTime = (Label)e.Item.FindControl("vCreateTime");
            vCreateTime.Text = OrderItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //vMemberAccount
            Literal vMemberAccount = (Literal)e.Item.FindControl("vMemberAccount");
            vMemberAccount.Text = OrderItem.MemberAccount;

            //vMemberName
            Literal vMemberName = (Literal)e.Item.FindControl("vMemberName");
            vMemberName.Text = OrderItem.MemberName;

            //vMemberPhone
            Literal vMemberPhone = (Literal)e.Item.FindControl("vMemberPhone");
            vMemberPhone.Text = OrderItem.MemberPhone;

            //vMemberSchool
            Literal vMemberSchool = (Literal)e.Item.FindControl("vMemberSchool");
            vMemberSchool.Text = OrderItem.MemberSchool;

            //vShopName
            Literal vShopName = (Literal)e.Item.FindControl("vShopName");
            vShopName.Text = OrderItem.ShopName;

            //vShopPhone
            Literal vShopPhone = (Literal)e.Item.FindControl("vShopPhone");
            vShopPhone.Text = OrderItem.ShopPhone;

            //vShopAddress
            Literal vShopAddress = (Literal)e.Item.FindControl("vShopAddress");
            vShopAddress.Text = OrderItem.ShopAddress;

            //vOrderDate
            Label vOrderDate = (Label)e.Item.FindControl("vOrderDate");
            vOrderDate.Text = ((DateTime)OrderItem.OrderDate).ToString("yyyy-MM-dd");

            //vOrderRound
            Label vOrderRound = (Label)e.Item.FindControl("vOrderRound");
            vOrderRound.Text = OrderItem.OrderRound;

            //vSyatemRemark
            Label vSyatemRemark = (Label)e.Item.FindControl("vSyatemRemark");
            vSyatemRemark.Text = OrderItem.SyatemRemark;

            //vStateType
            RadioButtonList vStateType = (RadioButtonList)e.Item.FindControl("vStateType");
            vStateType.Attributes["OrderItemId"] = OrderItem.Id.ToString();
            foreach (string StateType in Enum.GetNames(typeof(StateType)))
            {
                ListItem ListItem = new ListItem(StateType, StateType);
                ListItem.Selected = (ListItem.Value == OrderItem.State.ToString());
                vStateType.Items.Add(ListItem);
            }

            //vEdit
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", OrderItem.Id.ToString());

            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            vEdit.NavigateUrl = "/Order/Manager/Order_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }

    protected void vShopItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ShopItem ShopItem = (ShopItem)e.Item.DataItem;

            //vShopName
            LinkButton vShopName = (LinkButton)e.Item.FindControl("vShopName");
            vShopName.Text = ShopItem.Name;
            vShopName.CommandArgument = ShopItem.Id;

            if (ShopItem.Id == _SelectedShop) { vShopName.CssClass += " Selected"; }

        }
    }
}