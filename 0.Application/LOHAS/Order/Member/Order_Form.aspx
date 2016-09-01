<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor_Member.master" AutoEventWireup="true" CodeFile="Order_Form.aspx.cs" Inherits="Order_Member_Order_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Member/_Css/Order_Form.css" rel="stylesheet" />

    <script type="text/javascript">
        function UpdateGoogleMap(Url) {
            $("#vGoogleMap").height(400).attr("src", Url);
        }
    </script>

    <script type="text/javascript">

        function BindCalander(MinDate, MaxDate) {

            $(".Calandar3").datepicker({
                showMonthAfterYear: true,
                monthNames: [".1", ".2", ".3", ".4", ".5", ".6", ".7", ".8", ".9", ".10", ".11", ".12"],
                //dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
                firstDay: 1, //開始的星期 (1：星期一、0：星期日)
                numberOfMonths: 3,
                minDate: MinDate,
                maxDate: MaxDate,
                hideIfNoPrevNext: true,
                onSelect: function (date, e) {
                    var Date = $(".Calandar3").datepicker("getDate");
                    $("#vSelectedDate").val(Date.getMonth() + 1 + "/" + Date.getDate() + "/" + Date.getFullYear());

                    __doPostBack('vMemberName', '');
                },
            });

            $(".Calandar3").datepicker("setDate", $("#vSelectedDate").val());
        }
    </script>
    <script type="text/javascript">

        function Bind_IdCardNumberCheck() {
            $("#vMemberAccount").keyup(function () {

                var id = $(this).val().toUpperCase();

                if (id.search(/^[A-Z](1|2)\d{8}$/i) == -1) { $(".MemberAccountErrorMessage").show(); $(this).addClass("Error"); return; }

                var city = new Array(1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11, 20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30)
                id = id.split('');
                var total = city[id[0].charCodeAt(0) - 65];
                for (var i = 1; i <= 8; i++) { total += eval(id[i]) * (9 - i); }
                total += eval(id[9]);
                if (total % 10 != 0) { $(".MemberAccountErrorMessage").show(); $(this).addClass("Error"); return; }

                $(".MemberAccountErrorMessage").hide();
                $(this).removeClass("Error");
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div id="Order_Form">

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="Block UserBlock">
                    <div class="SubTitle">
                        <h1>請填寫基本資料</h1>
                        <div class="Divider"></div>
                    </div>
                    <table class="UserInfo">
                        <colgroup>
                            <col style="width: 142px;" />
                            <col />
                        </colgroup>
                        <tr>
                            <th>大學生姓名</th>
                            <td>
                                <asp:TextBox ID="vMemberName" ClientIDMode="Static" placeholder="請輸入您的姓名" MaxLength="20" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>身分證字號</th>
                            <td>
                                <span class="MemberAccountErrorMessage">身分證字號不正確</span>
                                <asp:TextBox ID="vMemberAccount" ClientIDMode="Static" MaxLength="10" placeholder="請輸入您的身分證字號" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>行動電話</th>
                            <td>
                                <asp:TextBox ID="vMemberPhone" MaxLength="10" placeholder="請輸入您的行動電話" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>學校</th>
                            <td>
                                <asp:DropDownList ID="vAreaSelector" AutoPostBack="true" OnSelectedIndexChanged="vAreaSelector_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="vSchoolSelector" AutoPostBack="true" OnSelectedIndexChanged="vSchoolSelector_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div class="Block ShopBlock">
            <div class="SubTitle">
                <h1>請選擇預約的LOHAS門市</h1>
                <div class="Divider"></div>
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="ShopList">
                        <asp:Repeater ID="vShopList" OnItemDataBound="vShopList_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="vShopItem" CssClass="ShopItem" OnClick="vShopItem_Click" runat="server">
                                    <asp:Label ID="vName" CssClass="Name" runat="server" Text="LOHAS熱河門市"></asp:Label>
                                    <asp:Label ID="vAddress" CssClass="Address" runat="server" Text="高雄市三民區自由一路95號"></asp:Label>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div style="clear: both; height: 1px;"></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <iframe id="vGoogleMap" class="GoogleMap" visible="false" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src=""></iframe>
        </div>


        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="Block CalanderBlock">
                    <div class="SubTitle">
                        <h1>請選擇預約的日期</h1>
                        <div class="Divider"></div>
                    </div>
                    <asp:HiddenField ID="vSelectedDate" ClientIDMode="Static" runat="server" />
                    <div class="Calandar3"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>

                <div class="Block RoundBlock">
                    <div class="SubTitle">
                        <h1>請選擇預約的時段</h1>
                        <div class="Divider"></div>
                    </div>
                    <div class="RoundList">
                        <asp:Repeater ID="vRoundList" OnItemDataBound="vRoundList_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="vRoundItem" CssClass="RoundItem" OnClick="vRoundItem_Click" runat="server">18:00 ~ 20:00
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Label ID="vNoRound" CssClass="NoRound" runat="server"></asp:Label>
                        <div style="clear: both; height: 1px;"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="padding: 0 0 40px 0; text-align: center;">
            <asp:LinkButton ID="vSendButton" CssClass="SendButton" OnClick="vSendButton_Click" ClientIDMode="Static" runat="server"></asp:LinkButton>
        </div>
    </div>


</asp:Content>

