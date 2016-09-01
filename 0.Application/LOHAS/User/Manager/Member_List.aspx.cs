using LeftHand.MemberShip;
using Lohas.Order;
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
            OrderItem OrderItem = (OrderItem)e.Item.DataItem;

            //vName
            Literal vName = (Literal)e.Item.FindControl("vName");
            vName.Text = OrderItem.MemberName;

            //vAccount
            Literal vAccount = (Literal)e.Item.FindControl("vAccount");
            vAccount.Text = OrderItem.MemberAccount;

            //vPhone
            Literal vPhone = (Literal)e.Item.FindControl("vPhone");
            vPhone.Text = OrderItem.MemberPhone;

            //vArea
            Literal vArea = (Literal)e.Item.FindControl("vArea");
            vArea.Text = OrderItem.MemberArea;

            //vSchool
            Literal vSchool = (Literal)e.Item.FindControl("vSchool");
            vSchool.Text = OrderItem.MemberSchool;

            //Edit
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", OrderItem.Id.ToString());

            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            vEdit.NavigateUrl = "/User/Manager/Member_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_OrderList();
    }

    private void Render_OrderList()
    {
        this.Pagger1.PageSize = 30;
        this.Pagger1.DataAmount = OrderItemManager.GetTotalAmount();

        this.vMemberList.DataSource = OrderItemManager.GetByPageIndex(this.Pagger1.DataStartIndex, this.Pagger1.DataEndIndex);
        this.vMemberList.DataBind();
    }
}