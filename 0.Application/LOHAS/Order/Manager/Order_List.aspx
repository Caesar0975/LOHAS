<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="Order_List.aspx.cs" Inherits="Issue_Manager_Plan_List" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Manager/_Css/Order_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Order_List">
        <div>
            <h2>門市列表</h2>
        </div>
        <asp:Repeater ID="vShopItemList" OnItemDataBound="vShopItemList_ItemDataBound" runat="server">
            <ItemTemplate>
                <div class="SlectItem">
                    <asp:LinkButton ID="vShopName" CssClass="Name" runat="server" OnClick="vShop_Click"></asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <div class="Title1" style="display: inline-block">
            預約管理
        </div>
        <asp:Calendar ID='Calendar1' runat='server' CssClass='Calendar' DayNameFormat="Shortest" PrevMonthText='上個月' NextMonthText='下個月' OnDayRender='Calendar1_DayRender' FirstDayOfWeek="Monday" CellPadding="0" CellSpacing="1" BorderStyle="None" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" OnSelectionChanged="Calendar1_SelectionChanged">
            <TitleStyle CssClass='Title' BorderStyle="None" />
            <DayHeaderStyle CssClass='DayHeader' ForeColor="White" />
            <DayStyle CssClass="DateCell" />
            <SelectedDayStyle CssClass="SelectedDay" />
            <NextPrevStyle CssClass='NextPrev' ForeColor="White" />
            <TodayDayStyle BackColor="#DBEEFB" />
        </asp:Calendar>
        <div style="clear: both; height: 10px;"></div>
        <div class="Title1" style="display: inline-block; color: blue;">訂單列表</div>
        <table class="Table1 Detail">
            <colgroup>
                <col style="width: 150px" />
                <col style="width: 160px" />
                <col style="width: auto;" />
                <col style="width: 210px;" />
                <col style="width: 80px;" />
                <col style="width: 40px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>訂單編號</th>
                    <th>會員資料</th>
                    <th>門市資料</th>
                    <th>預約資訊</th>
                    <th>訂單狀態</th>
                    <th>編輯</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vOrderList" runat="server" OnItemDataBound="vOrderList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                (<asp:Literal ID="vOrderNumber" runat="server" Text=""></asp:Literal>)
                                <br />
                                <asp:Label ID="vCreateTime" Style="font-size: 11px" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vMemberAccount" runat="server" Text="帳號"></asp:Literal><br />
                                <asp:Literal ID="vMemberName" runat="server" Text="趙好人"></asp:Literal><br />
                                <asp:Literal ID="vMemberPhone" runat="server" Text="0932857752"></asp:Literal><br />
                                <asp:Literal ID="vMemberSchool" runat="server" Text="高雄應用科技大學"></asp:Literal>
                            </td>
                            <td style="text-align: left;">
                                <asp:Literal ID="vShopName" runat="server" Text="LOHAS熱河門市"></asp:Literal><br />
                                <asp:Literal ID="vShopPhone" runat="server" Text="0911234567"></asp:Literal><br />
                                <asp:Literal ID="vShopAddress" runat="server" Text="高雄市三民區自由一路95號"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="vOrderDate" Style="margin-right: 4px;" runat="server" Text=""></asp:Label>
                                (<asp:Label ID="vOrderRound" runat="server" Text=""></asp:Label>)
                                <br />
                                <asp:Label ID="vSyatemRemark" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: center; vertical-align: top;">
                                <asp:RadioButtonList ID="vStateType" CssClass="OrderStateType" AutoPostBack="true" OnSelectedIndexChanged="vStateType_SelectedIndexChanged" RepeatLayout="Flow" runat="server"></asp:RadioButtonList>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" runat="server" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

