using Lohas.Area;
using Lohas.Order;
using Lohas.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sms_Sms_List : System.Web.UI.Page
{
    List<StateType> _StateTypes { get { return (List<StateType>)ViewState["_StateTypes"]; } set { ViewState["_StateTypes"] = value; } }
    DateTime _StartDate { get { return (DateTime)ViewState["_StartDate"]; } set { ViewState["_StartDate"] = value; } }
    DateTime _EndDate { get { return (DateTime)ViewState["_EndDate"]; } set { ViewState["_EndDate"] = value; } }

    List<OrderItem> _OrderItems;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            if (Page.IsPostBack == false)
            {
                _StateTypes = new List<StateType>();
                _StartDate = DateTime.Now.Date.AddDays(-1 * DateTime.Now.Day + 1);
                _EndDate = _StartDate.AddMonths(1).AddSeconds(-1).Date;
            }
            else
            {
                //StateType  
                _StateTypes = new List<StateType>();
                foreach (ListItem ListItem in this.vStateTypeList.Items)
                {
                    if (ListItem.Selected == false) { continue; }

                    _StateTypes.Add((StateType)Enum.Parse(typeof(StateType), ListItem.Text.Trim()));
                }

                //SateRange
                DateTime StartDate;
                if (DateTime.TryParse(this.vStartDate.Text.Trim(), out StartDate) == false)
                { Errors.Add("開始時間錯誤"); }
                else
                { _StartDate = StartDate; }

                DateTime EndDate;
                if (DateTime.TryParse(this.vEndDate.Text.Trim(), out EndDate) == false)
                { Errors.Add("結束時間錯誤"); }
                else
                { _EndDate = EndDate.AddDays(1).AddSeconds(-1); }


                if (_StartDate >= _EndDate)
                {
                    _EndDate = _StartDate.AddDays(1).AddSeconds(-1);
                    Errors.Add("結束時間不可早於開始時間");
                }
            }

            if (Errors.Count > 0)
            {
                _OrderItems = new List<OrderItem>();
                throw new Exception(string.Join("\\r\\n", Errors));
            }
            else
            {
                _OrderItems = OrderItemManager.GetBySearch(_StateTypes, _StartDate, _EndDate.AddDays(1));
            }
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            string SmsAccount = ConfigManager.GetByConfigKey(ConfigKey.壹元簡訊帳號);
            string SmsPassword = ConfigManager.GetByConfigKey(ConfigKey.壹元簡訊密碼);
            string SmsContent = this.vContent.Text.Trim().Replace("\r\n", "");
            string SmsReciverPhone = string.Join("\r\n", _OrderItems.Select(o => o.MemberPhone));
            Sms.APITimingMode TimingMode = Sms.APITimingMode.Immediate;
            bool LongFlag = true;//長簡訊

            Sms.APIServiceClient asc = new Sms.APIServiceClient();
            Sms.StringResult sr = asc.CallSendSMS(SmsAccount, SmsReciverPhone, SmsContent, TimingMode, LongFlag, DateTime.Now, null);

            if (sr.Success == false) { Errors.Add("簡訊發送失敗：" + sr.ErrorMsg); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\r\n", Errors)); }

            LeftHand.Gadget.Dialog.Alert("簡訊已發送");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_StateTypesList();
        Render_DateSelector();
        Render_OrderList();

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "BindDatepicker", "BindDatepicker();", true);
    }

    private void Render_StateTypesList()
    {
        if (this.Page.IsPostBack == true) { return; }

        this.vStateTypeList.DataSource = Enum.GetNames(typeof(StateType)).ToList();
        this.vStateTypeList.DataBind();
    }

    private void Render_DateSelector()
    {
        this.vStartDate.Text = _StartDate.ToString("yyyy-MM-dd");
        this.vEndDate.Text = _EndDate.ToString("yyyy-MM-dd");
    }

    private void Render_OrderList()
    {
        //Pagger1
        this.Pagger1.PageSize = 20;
        this.Pagger1.DataAmount = _OrderItems.Count;

        //vMemberList
        this.vMemberList.DataSource = _OrderItems.Skip(this.Pagger1.DataStartIndex - 1).Take(20);
        this.vMemberList.DataBind();
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
}