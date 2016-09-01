<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Round_Modify.aspx.cs" Inherits="Shop_Manager_Round_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Shop/Manager/_Css/Round_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Round_Modify">
                <table class="Table1" style="width: 100%">
                    <colgroup>
                        <col style="width: auto;" />
                        <col style="width: auto;" />
                        <col style="width: 50px;" />
                        <col style="width: 30px;" />
                    </colgroup>
                    <thead>
                        <tr>
                            <th>開始時間</th>
                            <th>結束時間</th>
                            <th>組數</th>
                            <th>編輯</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td style="text-align: center;">
                                <asp:TextBox ID="vAddStartTimeHour" runat="server" MaxLength="2" Width="30px" TextMode="DateTime"></asp:TextBox>：<asp:TextBox ID="vAddStartTimeMinute" runat="server" MaxLength="2" Width="30px" TextMode="DateTime"></asp:TextBox>
                            </td>
                            <td style="text-align: center;">
                                <asp:TextBox ID="vAddEndTimeHour" runat="server" MaxLength="2" Width="30px" TextMode="DateTime"></asp:TextBox>：<asp:TextBox ID="vAddEndTimeMinute" runat="server" MaxLength="2" Width="30px" TextMode="DateTime"></asp:TextBox>
                            </td>
                            <td style="text-align: center;">
                                <asp:TextBox ID="vAddLimitPairAmount" runat="server" MaxLength="2" Width="30px"></asp:TextBox></td>
                            <td style="text-align: center;">
                                <asp:LinkButton ID="vAdd" runat="server" OnClick="vAdd_Click" CssClass="Button1 Add" Text=" "></asp:LinkButton>
                            </td>
                        </tr>
                        <asp:Repeater ID="vRoundList" runat="server" OnItemDataBound="vRoundList_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Literal ID="vStartTime" runat="server"></asp:Literal>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Literal ID="vEndTime" runat="server"></asp:Literal>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Literal ID="vLimitPairAmount" runat="server"></asp:Literal>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:LinkButton ID="vDelete" runat="server" CssClass="Button1 Delete" Text=" " OnClick="vDelete_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

