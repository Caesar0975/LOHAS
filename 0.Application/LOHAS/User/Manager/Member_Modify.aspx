<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Member_Modify.aspx.cs" Inherits="User_Manager_Member_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Manager/_Css/Member_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Member_Modify">
                <table class="Table2">
                    <colgroup>
                        <col style="width: 120px" />
                    </colgroup>
                    <tbody>
                        <tr>
                            <th>身分證字號</th>
                            <td>
                                <asp:TextBox ID="vId" runat="server" placeholder="請輸入身分證字號" Width="150px" MaxLength="12"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <td>
                                <asp:TextBox ID="vName" runat="server" placeholder="請輸入姓名" MaxLength="12" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>聯絡電話</th>
                            <td>
                                <asp:TextBox ID="vPhone" runat="server" placeholder="請輸入聯絡電話" Width="150px" MaxLength="10"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>地區</th>
                            <td>
                                <asp:Literal ID="vArea" runat="server" Text="高雄市"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>學校</th>
                            <td>
                                <asp:Literal ID="vSchool" runat="server" Text="高雄應用科技大學"></asp:Literal></td>
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

