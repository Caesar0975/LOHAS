using Lohas.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_Member_Finish_Form : System.Web.UI.Page
{
    OrderItem _CurrentOrderItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentOrderItem = OrderItemManager.GetTemp();

        if (_CurrentOrderItem == null) { Response.Redirect("Order_Form.aspx"); }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.vName.Text = _CurrentOrderItem.MemberName;
        this.vMemberId.Text = _CurrentOrderItem.MemberAccount;
        this.vShopName.Text = _CurrentOrderItem.ShopName;
        this.vOrderDate.Text = ((DateTime)_CurrentOrderItem.OrderDate).ToString("yyyy/MM/dd");
        this.vOrderRound.Text = _CurrentOrderItem.OrderRound;

        this.vFinishFormDescription.Text = ConfigManager.GetByConfigKey(ConfigKey.報名成功說明);


    }
}