<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="SlideShow_Modify.aspx.cs" Inherits="SlideShow_Manager_SlideShow_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/SlideShow/Manager/_Css/SlideShow_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="SlideShow_Modify">
        <table class="Table1">
            <colgroup>
                <col style="width: 100px" />
                <col style="width: auto" />
            </colgroup>
            <tr>
                <th>圖片<br/>(950 x 364)</th>
                <td>
                    <asp:Image ID="vImage" runat="server" ImageUrl="~/_MasterPage/_Image/Base_Manager/Img1.jpg" />
                    <br />
                    <asp:FileUpload ID="FileUpload1" Style="width: 100%;" runat="server" />
                </td>
            </tr>
            <tr>
                <th>連結</th>
                <td>
                    <asp:TextBox ID="vUrl" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>顯示</th>
                <td>
                    <asp:RadioButtonList ID="vEnable" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="是" Value="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="False"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>排序</th>
                <td>
                    <asp:TextBox ID="vSort" runat="server" MaxLength="3" Width="30px" Style="text-align: right;"></asp:TextBox></td>
            </tr>
        </table>
        <asp:LinkButton ID="vDelete" CssClass="Button2 Red" runat="server" OnClick="vDelete_Click">刪除</asp:LinkButton>
        <asp:LinkButton ID="vSave" CssClass="Button2 Blue" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>
        <div style="clear: both; height: 1px;"></div>
    </div>
</asp:Content>

