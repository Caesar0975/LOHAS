<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="Shop_List.aspx.cs" Inherits="Shop_Manager_Shop_List" %>

<%@ Register Src="/_Element/Pagger/Pagger.ascx" TagName="Pagger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Shop/Manager/_Css/Shop_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Shop_List">
        <div>
            <h2>門市列表</h2>
            <asp:HyperLink ID="vShopItemAdd" runat="server" NavigateUrl="/Shop/Manager/Shop_Modify.aspx" CssClass="Button1 Add fancybox" Text=" "></asp:HyperLink>
        </div>
        <table class="Table1 Detail">
            <colgroup>
                <col style="width: auto;" />
                <col style="width: 180px;" />
                <col style="width: 430px;" />
                <col style="width: 60px;" />
                <col style="width: 40px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>門市</th>
                    <th>開放場次</th>
                    <th>相關學校</th>
                    <th>排序</th>
                    <th>編輯</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vShopList" runat="server" OnItemDataBound="vShopList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="vName" runat="server" Text="陽明"></asp:Literal><br />
                                <asp:Literal ID="vPhone" runat="server" Text="087654321"></asp:Literal><br />
                                <asp:Literal ID="vAddress" runat="server" Text="高雄市三民區陽明路17號"></asp:Literal><br />
                                經度：<asp:Literal ID="vLatitude" runat="server" Text="22.642794"></asp:Literal><br />
                                緯度：<asp:Literal ID="vLongitude" runat="server" Text="120.342042"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vRoundsEdit" CssClass="fancybox" Style="color: blue; text-decoration: underline;" runat="server" NavigateUrl="/Shop/Manager/Round_Modify.aspx">場次編輯</asp:HyperLink><br />
                                <asp:Literal ID="vRounds" runat="server" Text=""></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="vSchools" runat="server" Text=""></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vSort" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/Shop/Manager/Shop_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

