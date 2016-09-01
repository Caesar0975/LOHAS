<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="BranchManager_Modify.aspx.cs" Inherits="User_Manager_Member_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Manager/_Css/BranchManager_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="BranchManager_Modify">
                <table class="Table2">
                    <colgroup>
                        <col style="width: 50px" />
                    </colgroup>
                    <tbody>
                        <tr>
                            <th>帳號</th>
                            <td>
                                <asp:TextBox ID="vAccount" runat="server" placeholder="請輸入身分證字號" Width="150px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>密碼</th>
                            <td>
                                <asp:TextBox ID="vPassword" TextMode="Password" runat="server" placeholder="請輸入密碼" Width="150px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <td>
                                <asp:TextBox ID="vName" runat="server" placeholder="請輸入姓名" MaxLength="20" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>分店</th>
                            <td>
                                <asp:DropDownList ID="vShopSelector" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>備註</th>
                            <td>
                                <asp:TextBox ID="vRemark" Width="99%" TextMode="MultiLine" Rows="5" placeholder="請輸入備註" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2">
                                <div style="clear: both; height: 10px"></div>
                                <asp:LinkButton ID="vDelete" CssClass="Button2 Red" runat="server" OnClick="vDelete_Click">刪除</asp:LinkButton>
                                <asp:LinkButton ID="vSave" CssClass="Button2 Blue" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>
                                <div style="clear: both; height: 1px"></div>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

