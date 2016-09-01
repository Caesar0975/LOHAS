<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor.master" AutoEventWireup="true" CodeFile="DefaultVisitor.aspx.cs" Inherits="DefaultVisitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/_Css/DefaultVisitor.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            $('.SlideShowList').slick({
                dots: true,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                autoplay: true,
                autoplaySpeed: 3000,
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="DefaultVisitor">
        <div class="SlideShowList">
            <asp:Repeater ID="vSlideShowList" OnItemDataBound="vSlideShowList_ItemDataBound" runat="server">
                <ItemTemplate>
                    <asp:HyperLink ID="vSlideShowItem" CssClass="SlideShowItem" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="Activity">
            <h3>活動說明</h3>
            <asp:Label ID="vActivity" class="Explain" runat="server" Text=""></asp:Label>
        </div>

        <div class="Activity Counter">
            已經參加這個活動的同學有
            <asp:Label ID="vCounter" class="Counter" runat="server" Text="0"></asp:Label>人
        </div>

        <div class="Activity">
            <asp:Literal ID="vActivity2" runat="server"></asp:Literal>
        </div>

        <a class="RegistorButton" href="/Order/Member/Order_Form.aspx">立即報名</a>
    </div>
</asp:Content>

