using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.MemberShip;
using System.Text;
using Lohas.Area;
using Lohas.School;
using Lohas.Order;

public partial class User_Manager_Member_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    OrderItem _OrderItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));
        }

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
            List<string> Errors = new List<string>();

            //身分證字號
            string Id = this.vId.Text.Trim();
            if (UserManager.IdCheck(Id) == false) { Errors.Add("身分證字號格式不正確"); }
            else if (_Argument["Mode"] == "Add" && UserManager.GetUser(Id) != null) { Errors.Add("此身分證字號已經被使用"); }

            //姓名
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrWhiteSpace(Name) == true) { Errors.Add("姓名不可空白"); }

            //聯絡電話
            string Phone = this.vPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(Phone) == true) { Errors.Add("連絡電話不可空白"); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors.ToArray())); }

            _OrderItem.MemberAccount = Id;
            _OrderItem.MemberName = Name;
            _OrderItem.MemberPhone = Phone;

            OrderItemManager.Save(_OrderItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }



    protected void vDelete_Click(object sender, EventArgs e)
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
        vId.Text = _OrderItem.MemberAccount.ToString();

        //vName
        vName.Text = _OrderItem.MemberName.ToString();

        //vPhone
        vPhone.Text = _OrderItem.MemberPhone.ToString();

        //vArea
        vArea.Text = _OrderItem.MemberArea.ToString();

        //vSchool
        vSchool.Text = _OrderItem.MemberSchool.ToString();

        this.vDelete.OnClientClick = "return confirm('確定要刪除?');";
        this.vDelete.Visible = true;
    }


}