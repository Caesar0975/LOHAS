<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Order_Modify.aspx.cs" Inherits="Order_Manager_Order_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Manager/_Css/Order_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="Order_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 120px" />
                <col style="width: auto" />
            </colgroup>
            <tr>
                <th>訂單資訊</th>
                <td>
                    <asp:Literal ID="vId" runat="server">(20160825002)</asp:Literal><br />
                    <asp:Literal ID="vCreateTime" runat="server">(2015-08-12 00:00:00)</asp:Literal><br />
                    <asp:Literal ID="vMemberName" runat="server">趙好人</asp:Literal><br />
                    <asp:Literal ID="vMemberPhone" runat="server">0932857752</asp:Literal><br />
                    <asp:Literal ID="vMemberSchool" runat="server">高雄應用科技大學</asp:Literal>
                </td>
            </tr>
            <tr>
                <th>門市資料</th>
                <td>
                    <asp:Literal ID="vShopName" runat="server" Text="LOHAS熱河門市"></asp:Literal><br />
                    <asp:Literal ID="vShopPhone" runat="server" Text="0911234567"></asp:Literal><br />
                    <asp:Literal ID="vShopAddress" runat="server" Text="高雄市三民區自由一路95號"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>問卷資訊</th>
                <td>
                    <asp:Literal ID="vQuestionResult" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>處理狀態</th>
                <td>
                    <asp:Literal ID="vStateType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>預約資訊</th>
                <td>
                    <asp:Literal ID="vOrderDate" runat="server" Text=""></asp:Literal>&nbsp;
                    (<asp:Literal ID="vOrderRound" runat="server" Text=""></asp:Literal>)
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:LinkButton ID="vDeleteButton" CssClass="Button2 Red" Style="float: left;" runat="server" OnClick="vDeleteButton_Click">刪除</asp:LinkButton>
                    <%--<asp:LinkButton ID="vSave" CssClass="Button2 Blue" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>--%>

                    <div style="clear: both; height: 1px"></div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

