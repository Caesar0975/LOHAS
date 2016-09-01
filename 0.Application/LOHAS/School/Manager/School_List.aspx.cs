using Lohas.Area;
using Lohas.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class School_Manager_School_List : System.Web.UI.Page
{
    //選取的區域
    string _AreaSelected { get { return ViewState["_AreaSelected"].ToString(); } set { ViewState["_AreaSelected"] = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            _AreaSelected = (AreaItemManager.GetAll().Count == 0) ? "" : AreaItemManager.GetAll()[0].Id;
        }
        else
        {
            //判斷選項是否存在
            _AreaSelected = (AreaItemManager.GetById(_AreaSelected) != null) ? _AreaSelected : "";
        }
    }

    protected void vArea_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vArea = (LinkButton)sender;

            _AreaSelected = vArea.CommandArgument;
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vAreaItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            AreaItem AreaItem = (AreaItem)e.Item.DataItem;

            //vAreaName
            LinkButton vAreaName = (LinkButton)e.Item.FindControl("vAreaName");
            vAreaName.Text = AreaItem.Name;
            vAreaName.CommandArgument = AreaItem.Id;

            if (AreaItem.Id == _AreaSelected) { vAreaName.CssClass += " Selected"; }

            //vAreaEdit
            HyperLink vAreaEdit = (HyperLink)e.Item.FindControl("vAreaEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument["Mode"] = "Edit";
            Argument["Id"] = AreaItem.Id;
            vAreaEdit.NavigateUrl = "/School/Manager/Area_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_AreaItemAdd();
        Render_AreaItemList();

        Render_SchoolItemAdd();
        Render_SchoolItemList();
    }

    private void Render_AreaItemAdd()
    {
        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument["Mode"] = "Add";
        Argument["Id"] = _AreaSelected;

        this.vAreaItemAdd.NavigateUrl = "/School/Manager/Area_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
    }

    private void Render_AreaItemList()
    {
        this.vAreaItemList.DataSource = AreaItemManager.GetAll();
        this.vAreaItemList.DataBind();
    }

    private void Render_SchoolItemAdd()
    {
        this.vSchoolItemAdd.Visible = (string.IsNullOrWhiteSpace(_AreaSelected) == false);

        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Id", "");
        Argument.Add("AreaItemId", _AreaSelected);
        this.vSchoolItemAdd.NavigateUrl = "/School/Manager/School_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));

    }

    private void Render_SchoolItemList()
    {
        this.vSchoolList.DataSource = SchoolItemManager.GetByArea(AreaItemManager.GetById(_AreaSelected));
        this.vSchoolList.DataBind();
    }

    protected void vSchoolList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SchoolItem SchoolItem = (SchoolItem)e.Item.DataItem;

            //vName
            Literal vName = (Literal)e.Item.FindControl("vName");
            vName.Text = SchoolItem.Name;

            //Sort
            Literal vSort = (Literal)e.Item.FindControl("vSort");
            vSort.Text = SchoolItem.Sort.ToString();

            //Edit
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", SchoolItem.Id);
            vEdit.NavigateUrl = "/School/Manager/School_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));

        }
    }
}