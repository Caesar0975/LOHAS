<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="BranchManager_List.aspx.cs" Inherits="User_Manager_Member_List" %>

<%@ Register Src="/_Element/Pagger/Pagger.ascx" TagName="Pagger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Manager/_Css/BranchManager_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="BranchManager_List">
        <div class="Title1">分店長管理</div>
        <table class="Table1 Detail">
            <colgroup>
                <col style="width: 110px;" />
                <col style="width: 110px" />
                <col style="width: 160px;" />
                <col style="width: auto;" />
                <col style="width: 20px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>姓名</th>
                    <th>帳號</th>
                    <th>分店</th>
                    <th>備註</th>
                    <th style="text-align: center;">
                        <asp:HyperLink ID="vAdd" runat="server" NavigateUrl="/User/Manager/BranchManager_Modify.aspx" CssClass="Button1 Add fancybox" Text=" "></asp:HyperLink>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vMemberList" runat="server" OnItemDataBound="vMemberList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="vName" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vAccount" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vBranchShop" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="vRemark" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/User/Manager/BranchManager_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div>
            <uc1:Pagger ID="Pagger1" runat="server" />
        </div>
    </div>
</asp:Content>

