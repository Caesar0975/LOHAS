<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="QuestionnaireItem_Modify.aspx.cs" Inherits="QuestionnaireItem_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Manager/_Css/QuestionnaireItem_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="QuestionnaireItem_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 100px; text-align: center;" />
            </colgroup>
            <tbody>
                <tr>
                    <th>問題</th>
                    <td>顯示在前台的問題項目
                        <br />
                        <asp:TextBox ID="vTitle" Width="99%" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>問題簡述</th>
                    <td>顯示在後台預約單上的問題簡述
                        <br />
                        <asp:TextBox ID="vShortTitle" Width="99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>選項類型</th>
                    <td>
                        <asp:RadioButtonList ID="vOptionType" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"></asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>選項</th>
                    <td >每個選項請用半形逗號 ( , ) 隔開
                        <br />
                        <asp:TextBox ID="vOptions" Width="99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>排序</th>
                    <td>
                        <asp:TextBox ID="vSort" TextMode="Number" Style="text-align: right;" runat="server" Width="30px" MaxLength="3"></asp:TextBox></td>
                </tr>
            </tbody>
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

