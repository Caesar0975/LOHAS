using Lohas.Area;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class School_Manager_Area_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    AreaItem _AreaItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));

        switch (_Argument["Mode"])
        {
            case "Add":
                _AreaItem = new AreaItem("",-1);
                break;
            case "Edit":
                _AreaItem = AreaItemManager.GetById(_Argument["Id"]);
                break;
        }
    }
    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            string Name = this.vName.Text;
            if (string.IsNullOrWhiteSpace(Name) == true) { Errors.Add("請輸入區域名稱"); }

            int Sort;
            if (int.TryParse(this.vSort.Text, out Sort) == false) { Errors.Add("排序格式錯誤，請輸入整數數字。"); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _AreaItem.Name = Name;
            _AreaItem.Sort = Sort;

            AreaItemManager.Save(_AreaItem);

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
            //判斷有無分店
            //if (ShopItemManager.GetByArea(_AreaItem.Id).Count != 0) { throw new Exception("此區域尚有分店，請先將分店刪除！"); }

            AreaItemManager.Remove(_AreaItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('刪除成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Area();
    }

    private void Render_Area()
    {
        //名稱
        this.vName.Text = _AreaItem.Name;

        //排序
        this.vSort.Text = _AreaItem.Sort.ToString();

        //刪除按鈕
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }
}