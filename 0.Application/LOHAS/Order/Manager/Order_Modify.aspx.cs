using LeftHand.MemberShip;
using Lohas.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_Manager_Order_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    User _CurrentUser;
    OrderItem _OrderItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false) { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        _CurrentUser = SwitchUserManager.GetCurrentUser();

        switch (_Argument["Mode"])
        {
            case "Add":
                //暫時沒有新增的需要
                break;

            case "Edit":
                _OrderItem = OrderItemManager.GetById(decimal.Parse(_Argument["Id"].ToString()));
                break;
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            OrderItemManager.Save(_OrderItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Exception", "alert('" + ex.Message + "');", true);
        }
    }

    protected void vDeleteButton_Click(object sender, EventArgs e)
    {
        try
        {
            OrderItemManager.Remove(_OrderItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('刪除成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Issue();
    }

    private void Render_Issue()
    {
        if (this.Page.IsPostBack == true) { return; }

        //vId
        this.vId.Text = string.Format("( {0} )", _OrderItem.Id.ToString());

        //vCreateTime
        this.vCreateTime.Text = _OrderItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

        //vMemberName
        this.vMemberName.Text = _OrderItem.MemberName.ToString();

        //vMemberPhone
        this.vMemberPhone.Text = _OrderItem.MemberPhone.ToString();

        //vMemberSchool
        this.vMemberSchool.Text = _OrderItem.MemberSchool.ToString();

        //vShopName
        this.vShopName.Text = _OrderItem.ShopName.ToString();

        //vShopPhone
        this.vShopPhone.Text = _OrderItem.ShopPhone.ToString();

        //vShopAddress
        this.vShopAddress.Text = _OrderItem.ShopAddress.ToString();

        //vStateType
        this.vStateType.Text = _OrderItem.State.ToString();

        //vQuestionResult
        this.vQuestionResult.Text = _OrderItem.QuestionResult;

        //vOrderDate
        this.vOrderDate.Text = ((DateTime)_OrderItem.OrderDate).ToString("yyyy-MM-dd");

        //vOrderRound
        this.vOrderRound.Text = _OrderItem.OrderRound;

        //vDeleteButton
        this.vDeleteButton.OnClientClick = "return confirm('確定要刪除此預約紀錄?');";

    }




}