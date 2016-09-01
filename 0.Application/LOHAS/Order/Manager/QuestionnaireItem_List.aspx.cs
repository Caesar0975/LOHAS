using Lohas.Area;
using Lohas.Order;
using Lohas.School;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionnaireItem_List : System.Web.UI.Page
{
    Dictionary<ConfigKey, string> _Configs;

    protected void Page_Load(object sender, EventArgs e)
    {
        _Configs = ConfigManager.GetAll();
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            _Configs[ConfigKey.問卷概述] = this.vHtmlEditor.Content;

            ConfigManager.Save(_Configs);

            LeftHand.Gadget.Dialog.Alert("儲存成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_vHtmlEditor();

        Render_vQuestionnaireItemAdd();
        Render_vQuestionnaireItemList();
    }

    private void Render_vHtmlEditor()
    {
        if (Page.IsPostBack == true) { return; }

        this.vHtmlEditor.Width = 920;
        this.vHtmlEditor.Height = 130;

        this.vHtmlEditor.Content = _Configs[ConfigKey.問卷概述];
    }

    private void Render_vQuestionnaireItemAdd()
    {
        if (Page.IsPostBack == true) { return; }

        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Id", "");

        this.vQuestionnaireItemAdd.NavigateUrl = "/Order/Manager/QuestionnaireItem_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
    }

    private void Render_vQuestionnaireItemList()
    {
        this.vQuestionnaireItem.DataSource = QuestionnaireItemManager.GetAll();
        this.vQuestionnaireItem.DataBind();
    }
    protected void vQuestionnaireItem_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            QuestionnaireItem QuestionnaireItem = (QuestionnaireItem)e.Item.DataItem;

            //vTitle
            Label vTitle = (Label)e.Item.FindControl("vTitle");
            vTitle.Text = QuestionnaireItem.Title;

            //vShortTitle
            Label vShortTitle = (Label)e.Item.FindControl("vShortTitle");
            vShortTitle.Text = string.Format("( {0} )", QuestionnaireItem.ShortTitle);

            //vOptionType
            Label vOptionType = (Label)e.Item.FindControl("vOptionType");
            vOptionType.Text = string.Format("類型：{0}", QuestionnaireItem.OptionType.ToString());

            //vOption_List
            Repeater vOption_List = (Repeater)e.Item.FindControl("vOption_List");
            vOption_List.DataSource = QuestionnaireItem.Options;
            vOption_List.DataBind();

            //Sort
            Literal vSort = (Literal)e.Item.FindControl("vSort");
            vSort.Text = QuestionnaireItem.Sort.ToString();

            //Edit
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", QuestionnaireItem.Id);

            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            vEdit.NavigateUrl = "/Order/Manager/QuestionnaireItem_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }
    protected void vOption_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string Option = (string)e.Item.DataItem;

            //vOption_Item
            Label vOption_Item = (Label)e.Item.FindControl("vOption_Item");
            vOption_Item.Text = string.Format("({0}){1}", e.Item.ItemIndex + 1, Option);
        }
    }


}