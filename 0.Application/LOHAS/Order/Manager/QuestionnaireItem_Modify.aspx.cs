using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Lohas.Order;

public partial class QuestionnaireItem_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    QuestionnaireItem _QuestionnaireItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        switch (_Argument["Mode"])
        {
            case "Add":
                _QuestionnaireItem = new QuestionnaireItem("", "", OptionType.單選);
                break;

            case "Edit":
                _QuestionnaireItem = QuestionnaireItemManager.GetById(_Argument["Id"]);
                break;
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //Title
            string Title = this.vTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(Title) == true) { Errors.Add("問題不能為空"); }

            //ShortTitle
            string ShortTitle = this.vShortTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(ShortTitle) == true) { Errors.Add("問題簡述不能為空"); }

            //vOptionType
            OptionType OptionType = (OptionType)Enum.Parse(typeof(OptionType), this.vOptionType.SelectedValue);

            //vOptions
            string Options = this.vOptions.Text.Trim().Replace(" ", "");
            if (string.IsNullOrWhiteSpace(Options) == true) { Errors.Add("選項不能為空"); }

            //Sort
            int Sort;
            if (int.TryParse(this.vSort.Text.Trim(), out Sort) == false) { Errors.Add("排序格式不正確"); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors.ToArray())); }

            _QuestionnaireItem.Title = Title;
            _QuestionnaireItem.ShortTitle = ShortTitle;
            _QuestionnaireItem.OptionType = OptionType;
            _QuestionnaireItem.Options = Options.Split(',').ToList();
            _QuestionnaireItem.Sort = Sort;

            QuestionnaireItemManager.Save(_QuestionnaireItem);

            LeftHand.Gadget.Dialog.AlertWithCloseFancybox("儲存成功");
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
            QuestionnaireItemManager.Remove(_QuestionnaireItem);

            LeftHand.Gadget.Dialog.AlertWithCloseFancybox("刪除成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_QuestionnaireItem();
    }

    private void Render_QuestionnaireItem()
    {
        if (this.IsPostBack == true) { return; }

        //vTitle
        this.vTitle.Text = _QuestionnaireItem.Title;

        //vShortTitle
        this.vShortTitle.Text = _QuestionnaireItem.ShortTitle;

        //vOptionType
        this.vOptionType.Items.Clear();
        this.vOptionType.Items.AddRange(((string[])Enum.GetNames(typeof(OptionType))).Select(o => new ListItem(o.ToString())).ToArray());
        this.vOptionType.SelectedValue = _QuestionnaireItem.OptionType.ToString();

        //vOptions
        this.vOptions.Text = string.Join(",", _QuestionnaireItem.Options);

        //vSort
        this.vSort.Text = _QuestionnaireItem.Sort.ToString();


        this.vDelete.OnClientClick = "return confirm('確定要刪除?');";
    }

}