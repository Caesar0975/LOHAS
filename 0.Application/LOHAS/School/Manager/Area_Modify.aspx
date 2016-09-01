<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Area_Modify.aspx.cs" Inherits="School_Manager_Area_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/School/Manager/_Css/Area_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="Area_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 120px" />
                <col style="width: auto" />
            </colgroup>
            <tbody>
                <tr>
                    <th>名稱</th>
                    <td>
                        <asp:textbox id="vName" maxlength="100" style="width: 99%;" runat="server"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <th>排序</th>
                    <td>
                        <asp:textbox id="vSort" maxlength="2" style="width: 20px; text-align: right;" runat="server"></asp:textbox>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="2">
                        <asp:linkbutton id="vDelete" cssclass="Button2 Red" style="float: left;" visible="false" onclick="vDelete_Click" onclientclick="return confirm('你確定要刪除嗎?');" runat="server">刪除</asp:linkbutton>
                        <asp:linkbutton id="vSave" cssclass="Button2 Blue" style="float: right;" onclick="vSave_Click" runat="server">儲存</asp:linkbutton>
                        <div style="clear: both; height: 1px"></div>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>

