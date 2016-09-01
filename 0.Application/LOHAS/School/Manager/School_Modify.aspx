<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="School_Modify.aspx.cs" Inherits="School_Manager_School_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/School/Manager/_Css/School_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="School_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 100px; text-align: center;" />
            </colgroup>
            <tbody>
                <tr>
                    <th style="text-align: center;">名稱</th>
                    <td>
                        <asp:TextBox ID="vName" runat="server" placeholder="請輸入學校名稱" Width="99%" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <th style="text-align: center;">排序</th>
                    <td>
                        <asp:TextBox ID="vSort" TextMode="Number" Style="text-align: right;" runat="server" Width="30px" MaxLength="3"></asp:TextBox></td>
                </tr>
            </tbody>
        </table>

        <table class="Table2">
            <colgroup>
                <col style="width: 100px; text-align: center;" />
            </colgroup>
            <tfoot>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="vDelete" CssClass="Button2 Red" Style="margin-top: 10px" runat="server" OnClick="vDelete_Click">刪除</asp:LinkButton>
                        <asp:LinkButton ID="vSave" CssClass="Button2 Blue" Style="margin-top: 10px" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>
                        <div style="clear: both; height: 1px"></div>
                </tr>
            </tfoot>
        </table>

    </div>
</asp:Content>

