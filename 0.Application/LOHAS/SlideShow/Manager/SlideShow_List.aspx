<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="SlideShow_List.aspx.cs" Inherits="SlideShow_Manager_SlideShow_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/SlideShow/Manager/_Css/SlideShow_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="SlideShow_List">
        <div class="Title1" style="display: inline-block">
            橫幅管理
        </div>
        <table class="Table1">
            <colgroup>
                <col style="width: auto;" />
                <col style="width: 50px;" />
                <col style="width: 50px;" />
                <col style="width: 30px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>圖片</th>
                    <th>顯示</th>
                    <th>排序</th>
                    <th>
                        <asp:HyperLink ID="Add" class="fancybox Button1" runat="server"></asp:HyperLink>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vSlideShowList" runat="server" OnItemDataBound="vSlideShowList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <div class="Image">
                                    <asp:Image ID="vImage" runat="server" Width="100%" />
                                </div>
                                <asp:Label ID="vUrl" CssClass="Url" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vIsEnable" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vSort" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" CssClass="Button1 Edit fancybox" runat="server" NavigateUrl="/SlideShow/Manager/SlideShow_Modify.aspx"></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

