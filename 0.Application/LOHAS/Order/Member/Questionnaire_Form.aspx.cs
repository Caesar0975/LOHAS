using Lohas.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_Member_Questionnaire_Form : System.Web.UI.Page
{
    OrderItem _CurrentOrderItem;
    List<QuestionnaireItem> _QuestionnaireItems;
    List<string> _Results;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentOrderItem = OrderItemManager.GetTemp();
        if (_CurrentOrderItem == null) { Response.Redirect("Order_Form.aspx"); }

        _QuestionnaireItems = QuestionnaireItemManager.GetAll();

        _Results = _CurrentOrderItem.QuestionResult.Split(new string[] { "<br />" }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    protected void vSendButton_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            #region QuestionResult;

            string QuestionResult = "";
            int QuestionnaireItemIndex = 0;
            foreach (RepeaterItem RepeaterItem in this.vQuestionnaire_List.Items)
            {
                QuestionnaireItem QuestionnaireItem = _QuestionnaireItems[QuestionnaireItemIndex];

                switch (QuestionnaireItem.OptionType)
                {
                    case OptionType.單選:
                        RadioButtonList vRadioButtonList = (RadioButtonList)RepeaterItem.FindControl("vRadioButtonList");

                        if (string.IsNullOrWhiteSpace(vRadioButtonList.SelectedValue)) { Errors.Add(string.Format("請填寫第{0}題的答案", QuestionnaireItem.Sort)); }
                        QuestionResult += string.Format("{0}：{1}<br />", QuestionnaireItem.ShortTitle, vRadioButtonList.SelectedValue);
                        break;

                    case OptionType.多選:
                        CheckBoxList vCheckBoxList = (CheckBoxList)RepeaterItem.FindControl("vCheckBoxList");

                        if (string.IsNullOrWhiteSpace(vCheckBoxList.SelectedValue)) { Errors.Add(string.Format("請填寫第{0}題的答案", QuestionnaireItem.Sort)); }
                        QuestionResult += string.Format("{0}：{1}<br />", QuestionnaireItem.ShortTitle, vCheckBoxList.SelectedValue);
                        break;

                    case OptionType.單行文字方塊:
                        List<string> OptionResult = new List<string>();

                        Repeater vTextBoxList = (Repeater)RepeaterItem.FindControl("vTextBoxList");
                        foreach (RepeaterItem TextBoxItem in vTextBoxList.Items)
                        {
                            string Option = ((TextBox)TextBoxItem.FindControl("vResult")).Text.Trim().Replace(" ", "");
                            if (string.IsNullOrWhiteSpace(Option)) { Errors.Add(string.Format("請填寫第{0}題第{1}項的答案", QuestionnaireItem.Sort, TextBoxItem.ItemIndex + 1)); }

                            OptionResult.Add(Option);
                        }

                        QuestionResult += string.Format("{0}：{1}<br />", QuestionnaireItem.ShortTitle, string.Join(",", OptionResult));
                        break;
                }

                QuestionnaireItemIndex += 1;
            }

            #endregion

            #region vSystemRemark;

            string SystemRemark = this.vSystemRemark.Value.Trim();

            #endregion

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _CurrentOrderItem.QuestionResult = QuestionResult;
            _CurrentOrderItem.SyatemRemark = SystemRemark;

            OrderItemManager.Save(_CurrentOrderItem);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "top.location.replace('Finish_Form.aspx');", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //vTitle
        this.vTitle.Text = ConfigManager.GetByConfigKey(ConfigKey.問卷概述);

        //vSystemRemark
        this.vSystemRemark.Value = "";

        Render_Questionnaire_List();
        Render_FbShare();
        Render_SendButton();
    }

    private void Render_FbShare()
    {
        if (Page.IsPostBack == false)
        {
            string FbShareName = ConfigManager.GetByConfigKey(ConfigKey.FB分享Name);
            string FbShareCaption = ConfigManager.GetByConfigKey(ConfigKey.FB分享Caption);
            string FbShareDescription = ConfigManager.GetByConfigKey(ConfigKey.FB分享Description);
            string Link = string.Format("http://{0}", Request.Url.Authority);
            string Image = string.Format("http://{0}{1}{2}", Request.Url.Authority, ConfigManager.UploadPath, ConfigManager.GetByConfigKey(ConfigKey.FB分享圖));

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "BindFB", string.Format("BindFB('{0}','{1}','{2}','{3}','{4}');", FbShareName, FbShareCaption, FbShareDescription, Link, Image), true);
        }
    }

    private void Render_SendButton()
    {
        if (string.IsNullOrWhiteSpace(_CurrentOrderItem.SyatemRemark) == true)
        {
            this.vFbButton.CssClass = "FbSendButton";
            this.vFbButton.Text = "臉書分享並送出預約單<br/>(加贈抗UV400鏡片功能)<span class=\"FbSendRemark\">臉書分享僅適用於電腦版</span>";
            this.vFbButton.Attributes["SystemRemark"] = "臉書分享加贈抗UV400鏡片功能";

            this.vSendButton.CssClass = "SendButton";
            this.vSendButton.Text = "不用分享直接送出預約單";
        }
        else
        {
            this.vFbButton.CssClass = "";
            this.vFbButton.Text = "";
            this.vFbButton.Attributes["SystemRemark"] = "";
            this.vFbButton.Visible = false;

            this.vSendButton.CssClass = "SendButton";
            this.vSendButton.Text = "修改訂單完成";
        }
    }

    private void Render_Questionnaire_List()
    {
        if (this.Page.IsPostBack == true) { return; }

        this.vQuestionnaire_List.DataSource = _QuestionnaireItems;
        this.vQuestionnaire_List.DataBind();
    }
    protected void vQuestionnaire_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            QuestionnaireItem QuestionnaireItem = (QuestionnaireItem)e.Item.DataItem;

            string Result = "";
            if (_Results.Count() > 0) { Result = _Results[e.Item.ItemIndex].Split('：')[1]; }


            //vTitle
            Literal vTitle = (Literal)e.Item.FindControl("vTitle");
            vTitle.Text = string.Format("{0}. {1}", QuestionnaireItem.Sort.ToString(), QuestionnaireItem.Title);

            //QuestionnaireItem
            switch (QuestionnaireItem.OptionType)
            {
                case OptionType.單選:
                    RadioButtonList vRadioButtonList = (RadioButtonList)e.Item.FindControl("vRadioButtonList");
                    vRadioButtonList.Visible = true;

                    foreach (string Option in QuestionnaireItem.Options) { vRadioButtonList.Items.Add(new ListItem(Option, Option)); }
                    vRadioButtonList.SelectedValue = Result;
                    break;

                case OptionType.多選:
                    CheckBoxList vCheckBoxList = (CheckBoxList)e.Item.FindControl("vCheckBoxList");
                    vCheckBoxList.Visible = true;

                    foreach (string Option in QuestionnaireItem.Options) { vCheckBoxList.Items.Add(new ListItem(Option, Option)); }
                    vCheckBoxList.SelectedValue = Result;
                    break;

                case OptionType.單行文字方塊:
                    Repeater vTextBoxList = (Repeater)e.Item.FindControl("vTextBoxList");
                    vTextBoxList.Visible = true;

                    //將單行文字方塊要 Render 的格式改成 "標題：答案"，並傳給 ItemDataBound
                    List<string> QAs = new List<string>();
                    for (int i = 0; i < QuestionnaireItem.Options.Count(); i++)
                    {
                        string Option = QuestionnaireItem.Options[i];
                        QAs.Add(string.Format("{0}：{1}", Option, (string.IsNullOrWhiteSpace(Result)) ? "" : Result.Split(',')[i]));
                    }
                    vTextBoxList.DataSource = QAs;
                    vTextBoxList.DataBind();
                    break;
            }
        }
    }
    protected void vTextBoxList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string Option = ((string)e.Item.DataItem).Split('：')[0];
            string Result = ((string)e.Item.DataItem).Split('：')[1];

            //vOption
            Label vOption = (Label)e.Item.FindControl("vOption");
            vOption.Text = Option.Trim();

            //vResult
            TextBox vResult = (TextBox)e.Item.FindControl("vResult");
            vResult.Text = Result.Trim();
        }
    }
}