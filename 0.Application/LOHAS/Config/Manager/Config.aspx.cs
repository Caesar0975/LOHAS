using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Config_Manager_Config : System.Web.UI.Page
{
    Dictionary<ConfigKey, string> _Configs;

    protected void Page_Load(object sender, EventArgs e)
    {
        _Configs = ConfigManager.GetAll();
    }

    protected void vSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            //錯誤集合
            List<string> Errors = new List<string>();

            //壹元簡訊
            string SmsAccount = this.vSmsAccount.Text.Trim();
            string SmsPassword = this.vSmsPassword.Text.Trim();

            //黑名單
            string Blacklist = this.vBlacklist.Text.Trim();

            //活動說明1
            string ActivityDescription = this.vActivityDescription.Content.Trim();

            //活動說明2
            string ActivityDescription2 = this.vActivityDescription2.Content.Trim();

            //報名成功說明
            string vFinishFormDescription = this.vFinishFormDescription.Content.Trim();

            //計數器
            int Counter;
            if (int.TryParse(this.vCounter.Text.Trim(), out Counter) == false) { Errors.Add("計數器格式不正確，需填數字"); }

            //FB分享設定
            string FbShareName = this.vFbShareName.Text.Trim().Replace("\r\n", "");
            string FbShareCaption = this.vFbShareCaption.Text.Trim().Replace("\r\n", "");
            string FbShareDescription = this.vFbShareDescription.Text.Trim().Replace("\r\n", "");
            string FbShareImageName = _Configs[ConfigKey.FB分享圖];
            if (this.vFbShareImageUploder.HasFile == true)
            {
                FbShareImageName = string.Format("FbShareImage{0}.png", DateTime.Now.ToString("_yyyyMMdd_HHmmss")); ;

                Bitmap UploadImage = new Bitmap(this.vFbShareImageUploder.PostedFile.InputStream);
                Bitmap ShareImage = LeftHand.Gadget.Graphics.ResizeByScope(UploadImage, 600, 315, LeftHand.Gadget.Graphics.ScopeMode.OutScope);
                ShareImage.Save(ConfigManager.PhysicalUploadPath + FbShareImageName, System.Drawing.Imaging.ImageFormat.Png);
            }

            //SEO設定
            string SeoTitle = this.vSeoTitle.Text.Trim();
            string SeoDescription = this.vSeoDescription.Text.Trim();

            //流量分析代碼
            string vAnalyticsCode = this.vAnalyticsCode.Text.Trim();

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _Configs[ConfigKey.壹元簡訊帳號] = SmsAccount;
            _Configs[ConfigKey.壹元簡訊密碼] = SmsPassword;

            _Configs[ConfigKey.黑名單] = Blacklist;

            _Configs[ConfigKey.活動說明] = ActivityDescription;
            _Configs[ConfigKey.活動說明2] = ActivityDescription2;
            _Configs[ConfigKey.報名成功說明] = vFinishFormDescription;

            _Configs[ConfigKey.計數器] = Counter.ToString();

            _Configs[ConfigKey.FB分享Name] = FbShareName;
            _Configs[ConfigKey.FB分享Caption] = FbShareCaption;
            _Configs[ConfigKey.FB分享Description] = FbShareDescription;
            _Configs[ConfigKey.FB分享圖] = FbShareImageName;

            _Configs[ConfigKey.SeoTitle] = SeoTitle;
            _Configs[ConfigKey.SeoDescription] = SeoDescription;
            _Configs[ConfigKey.AnalyticsCode] = vAnalyticsCode;

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
        Render_Configs();
    }

    private void Render_Configs()
    {
        //壹元簡訊
        this.vSmsAccount.Text = _Configs[ConfigKey.壹元簡訊帳號];
        this.vSmsPassword.Text = _Configs[ConfigKey.壹元簡訊密碼];

        //黑名單
        this.vBlacklist.Text = _Configs[ConfigKey.黑名單];

        //活動說明
        this.vActivityDescription.Width = 790;
        this.vActivityDescription.Height = 400;
        this.vActivityDescription.Content = _Configs[ConfigKey.活動說明];

        //活動說明2
        this.vActivityDescription2.Width = 790;
        this.vActivityDescription2.Height = 500;
        this.vActivityDescription2.Content = _Configs[ConfigKey.活動說明2];

        //報名成功說明
        this.vFinishFormDescription.Width = 790;
        this.vFinishFormDescription.Height = 250;
        this.vFinishFormDescription.Content = _Configs[ConfigKey.報名成功說明];

        //計數器        
        this.vCounter.Text = _Configs[ConfigKey.計數器];

        //FB分享設定
        this.vFbShareImage.ImageUrl = ConfigManager.UploadPath + _Configs[ConfigKey.FB分享圖];
        this.vFbShareName.Text = _Configs[ConfigKey.FB分享Name];
        this.vFbShareCaption.Text = _Configs[ConfigKey.FB分享Caption];
        this.vFbShareDescription.Text = _Configs[ConfigKey.FB分享Description];

        //SEO設定
        this.vSeoTitle.Text = _Configs[ConfigKey.SeoTitle];
        this.vSeoDescription.Text = _Configs[ConfigKey.SeoDescription];

        //流量分析代碼
        this.vAnalyticsCode.Text = _Configs[ConfigKey.AnalyticsCode];

    }
}