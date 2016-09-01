<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor_Member.master" AutoEventWireup="true" CodeFile="Finish_Form.aspx.cs" Inherits="Order_Member_Finish_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Member/_Css/Finish_Form.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Finish_Form">

        <h1>恭喜您完成預約!</h1>
        <div class="Content">
            <p>親愛的 <asp:Literal ID="vName" runat="server"></asp:Literal> 同學您好：</p>

            <table class="Detail" style="width:99%;">
                <colgroup>
                    <col style="width: 150px" />
                    <col style="width: 200px;" />
                    <col style="width: auto;" />
                    <col style="width: 150px;" />
                </colgroup>
                <thead>
                    <tr>
                        <th>身分證字號</th>
                        <th>預約門市</th>
                        <th>預約日期</th>
                        <th>預約時段</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Literal ID="vMemberId" runat="server" Text=""></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <asp:Literal ID="vShopName" runat="server" Text="LOHAS熱河門市"></asp:Literal><br />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="vOrderDate" Style="margin-right: 4px;" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="text-align: center; vertical-align: top;">
                            <asp:Label ID="vOrderRound" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <asp:Literal ID="vFinishFormDescription" runat="server"></asp:Literal>
            </p>
        </div>

        <div class="LinkPanel">
            <a class="MoreLink" href="http://goo.gl/10mY6m" target="_blank">更多活動詳情</a>
            <a class="FbLink" href="https://www.facebook.com/sg.lohas.tw/" target="_blank"></a>
        </div>

    </div>
</asp:Content>

