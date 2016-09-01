using Lohas.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SlideShow_Manager_SlideShow_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    SlideShowItem _SlideShow;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));
        }

        switch (_Argument["Mode"])
        {
            case "Add":
                _SlideShow = new SlideShowItem("", true, "");
                break;
            case "Edit":
                _SlideShow = SlideShowItemManager.Get(_Argument["Id"]);
                break;
        }
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SlideShowItemManager.Remove(_SlideShow);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "window.parent.$.fancybox.close();", true);
        }
        catch (Exception Exception)
        {
            LeftHand.Gadget.Dialog.Alert(Exception.Message);
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //連結
            string Url = this.vUrl.Text.Trim();

            //顯示
            bool Enable = bool.Parse(this.vEnable.SelectedValue);

            //排序
            int Sort;
            if (int.TryParse(this.vSort.Text.Trim(), out Sort) == false) { Errors.Add("排序請輸入數字"); }

            //圖片
            if (_Argument["Mode"] == "Add" && this.FileUpload1.HasFile == false) { Errors.Add("圖片不能為空"); }

            string FileName = _SlideShow.Image;
            if (this.FileUpload1.HasFile == true)
            {
                FileName = _SlideShow.Id + System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName).ToLower();
                this.FileUpload1.SaveAs(SlideShowItemManager.GetPhysicalUploadPath() + FileName);
            }

            if (Errors.Count > 0) { LeftHand.Gadget.Dialog.Alert(Errors); }

            _SlideShow.Url = Url;
            _SlideShow.Enable = Enable;
            _SlideShow.Sort = Sort;
            _SlideShow.Image = FileName;
            SlideShowItemManager.Save(_SlideShow);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception Exception)
        {
            LeftHand.Gadget.Dialog.Alert(Exception.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_SlideShow();
    }

    private void Render_SlideShow()
    {
        if (this.Page.IsPostBack == true) { return; }

        //圖片
        this.vImage.ImageUrl = SlideShowItemManager.GetUploadPath() + _SlideShow.Image;
        this.vImage.Visible = (_Argument["Mode"] == "Edit");

        //連結
        this.vUrl.Text = _SlideShow.Url;

        //顯示
        this.vEnable.SelectedValue = _SlideShow.Enable.ToString();

        //排序
        this.vSort.Text = _SlideShow.Sort.ToString();

        //刪除
        this.vDelete.OnClientClick = "return confirm('確定刪除此內容');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }
}