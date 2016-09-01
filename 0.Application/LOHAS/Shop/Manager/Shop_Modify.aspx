<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Shop_Modify.aspx.cs" Inherits="Shop_Manager_Shop_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Shop/Manager/_Css/Shop_Modify.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {



            $(".AllAreaSchool.Selected").on("click", function () {
                $(this).siblings().children("input[type=checkbox]").prop("checked", true);
            });

            $(".AllAreaSchool.UnSelected").on("click", function () {
                $(this).siblings().children("input[type=checkbox]").prop("checked", false);
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="Shop_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 100px; text-align: center;" />
            </colgroup>
            <tbody>
                <tr>
                    <th style="text-align: center;">名稱</th>
                    <td>
                        <asp:TextBox ID="vName" runat="server" placeholder="請輸入門市名稱" Width="99%" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <th style="text-align: center;">電話</th>
                    <td>
                        <asp:TextBox ID="vPhone" runat="server" placeholder="請輸入連絡電話" Width="99%" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="text-align: center;">地址</th>
                    <td>
                        <asp:TextBox ID="vAddress" runat="server" placeholder="請輸入門市地址" Width="99%" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <th style="text-align: center;">緯度</th>
                    <td>
                        <asp:TextBox ID="vLatitude" runat="server" placeholder="請輸入門市緯度" Width="100px" MaxLength="12"></asp:TextBox></td>
                </tr>
                <tr>
                    <th style="text-align: center;">經度</th>
                    <td>
                        <asp:TextBox ID="vLongitude" runat="server" placeholder="請輸入門市經度" Width="100px" MaxLength="12"></asp:TextBox></td>
                </tr>
                <tr>
                    <th style="text-align: center;">排序</th>
                    <td>
                        <asp:TextBox ID="vSort" TextMode="Number" Style="text-align: right;" runat="server" Width="30px" MaxLength="3"></asp:TextBox></td>
                </tr>
                <tr>
                    <th style="text-align: center;">選擇學校</th>
                    <td>
                        <asp:Repeater ID="vAreaList" runat="server" OnItemDataBound="vAreaList_ItemDataBound">
                            <ItemTemplate>
                                <div class="AreaItem">
                                    <asp:Label ID="vArea" runat="server" Text="區域"></asp:Label>
                                    <span class="AllAreaSchool Selected">全選</span>
                                    <span class="AllAreaSchool UnSelected">全不選</span>
                                    <hr />
                                    <asp:CheckBoxList ID="vSchoolList" RepeatLayout="Flow" RepeatColumns="3" runat="server"></asp:CheckBoxList>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
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

