using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Lohas.Shop;
using Lohas.Round;

public partial class Shop_Manager_Round_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    ShopItem _ShopItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        _ShopItem = ShopItemManager.GetById(_Argument["Id"]);
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vDelete = (LinkButton)sender;
            string RoundItemId = vDelete.CommandArgument.ToString();

            _ShopItem.RoundItems.Remove(_ShopItem.RoundItems.FirstOrDefault(r => r.Id == RoundItemId));

            ShopItemManager.Save(_ShopItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "window.parent.$.fancybox.update();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vAdd_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //StartTime 
            TimeSpan AddStartTime;
            if (TimeSpan.TryParse(this.vAddStartTimeHour.Text.Trim() + ":" + this.vAddStartTimeMinute.Text.Trim(), out AddStartTime) == false) { Errors.Add("開始時間格式錯誤"); }
            if (_ShopItem.RoundItems.FirstOrDefault(r => AddStartTime >= r.StartTime && AddStartTime <= r.EndTime) != null) { Errors.Add("時間區間重疊"); }

            //EndTime 
            TimeSpan AddEndTime;
            if (TimeSpan.TryParse(this.vAddEndTimeHour.Text.Trim() + ":" + this.vAddEndTimeMinute.Text.Trim(), out AddEndTime) == false) { Errors.Add("結束時間格式錯誤"); }
            if (AddStartTime >= AddEndTime) { Errors.Add("結束時間不可早於開始時間"); }
            if (_ShopItem.RoundItems.FirstOrDefault(r => AddEndTime >= r.StartTime && AddEndTime <= r.EndTime) != null) { Errors.Add("時間區間重疊"); }

            if (_ShopItem.RoundItems.FirstOrDefault(r => AddStartTime <= r.StartTime && AddEndTime >= r.EndTime) != null) { Errors.Add("時間區間重疊"); }

            //vAddLimitPairAmount
            int AddLimitPairAmount;
            if (int.TryParse(this.vAddLimitPairAmount.Text, out AddLimitPairAmount) == false) { Errors.Add("限制人數格式錯誤，請輸入整數數字"); }

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors.Distinct())); }

            _ShopItem.RoundItems.Add(new RoundItem(_ShopItem, AddStartTime, AddEndTime, AddLimitPairAmount));

            ShopItemManager.Save(_ShopItem);

            this.vAddStartTimeHour.Text = "";
            this.vAddStartTimeMinute.Text = "";
            this.vAddEndTimeHour.Text = "";
            this.vAddEndTimeMinute.Text = "";
            this.vAddLimitPairAmount.Text = "";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('新增成功');window.parent.$.fancybox.update();", true);

        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.vRoundList.DataSource = _ShopItem.RoundItems;
        this.vRoundList.DataBind();
    }

    protected void vRoundList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RoundItem RoundItem = (RoundItem)e.Item.DataItem;

            //vStartTime
            Literal vStartTime = (Literal)e.Item.FindControl("vStartTime");
            vStartTime.Text = RoundItem.StartTime.ToString(@"hh\:mm");

            //vEndTime
            Literal vEndTime = (Literal)e.Item.FindControl("vEndTime");
            vEndTime.Text = RoundItem.EndTime.ToString(@"hh\:mm");

            //vLimitPairAmount
            Literal vLimitPairAmount = (Literal)e.Item.FindControl("vLimitPairAmount");
            vLimitPairAmount.Text = RoundItem.LimitPairAmount.ToString();

            //vDelete
            LinkButton vDelete = (LinkButton)e.Item.FindControl("vDelete");
            vDelete.OnClientClick = "return confirm('你確定要刪除此場次？');";
            vDelete.CommandArgument = RoundItem.Id;

        }
    }

}