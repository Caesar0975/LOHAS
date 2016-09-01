using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lohas.School;
using System.Text;

public partial class School_Manager_School_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    SchoolItem _SchoolItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        switch (_Argument["Mode"])
        {
            case "Add":
                _SchoolItem = new SchoolItem(_Argument["AreaItemId"],"");
                break;

            case "Edit":
                _SchoolItem = SchoolItemManager.Get(_Argument["Id"]);
                break;
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            //名稱
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrWhiteSpace(Name) == true) { throw new Exception("名稱不能為空"); }
            if (_Argument["Mode"] == "Add" && SchoolItemManager.NameCheck(Name) != null) { throw new Exception("學校名稱重複"); }

            //排序
            int Sort;
            if (int.TryParse(this.vSort.Text, out Sort) == false) { throw new Exception("排序格式錯誤，請輸入整數數字"); }

            _SchoolItem.Id = (_Argument["Mode"] == "Add") ? Guid.NewGuid().ToString() : _SchoolItem.Id;
            _SchoolItem.Name = Name;
            _SchoolItem.Sort = Sort;

            SchoolItemManager.Save(_SchoolItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
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
            SchoolItemManager.Remove(_SchoolItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('刪除成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_SchoolItem();
    }

    private void Render_SchoolItem()
    {
        if (this.IsPostBack == false)
        {
            //名稱
            this.vName.Text = _SchoolItem.Name;

            //排序
            this.vSort.Text = _SchoolItem.Sort.ToString();
        }

        this.vDelete.OnClientClick = "return confirm('你確定要刪除此使用者');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }
}