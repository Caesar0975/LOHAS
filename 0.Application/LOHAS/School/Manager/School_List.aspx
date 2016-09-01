<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="School_List.aspx.cs" Inherits="School_Manager_School_List" %>

<%@ Register Src="/_Element/Pagger/Pagger.ascx" TagName="Pagger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/School/Manager/_Css/School_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="School_List">
        <div>
            <h2>地區列表</h2>
            <asp:HyperLink ID="vAreaItemAdd" NavigateUrl="/School/Manager/Area_Modify.aspx" CssClass="Button1 Add fancybox" runat="server"></asp:HyperLink>
        </div>
        <asp:Repeater ID="vAreaItemList" OnItemDataBound="vAreaItemList_ItemDataBound" runat="server">
            <ItemTemplate>
                <div class="SlectItem">
                    <asp:LinkButton ID="vAreaName" CssClass="Name" runat="server" OnClick="vArea_Click"></asp:LinkButton>
                    <asp:HyperLink ID="vAreaEdit" CssClass="Button1 Edit fancybox" runat="server"></asp:HyperLink>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div>
            <h2>學校列表</h2>
            <asp:HyperLink ID="vSchoolItemAdd" runat="server" NavigateUrl="/School/Manager/School_Modify.aspx" CssClass="Button1 Add fancybox" Text=" "></asp:HyperLink>
        </div>
        <table class="Table1 Detail">
            <colgroup>
                <col style="width: 300px" />
                <col style="width: 60px;" />
                <col style="width: 60px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>名稱</th>
                    <th>排序</th>
                    <th>編輯</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vSchoolList" runat="server" OnItemDataBound="vSchoolList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="vName" runat="server" Text="正修科技大學"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vSort" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/School/Manager/School_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

