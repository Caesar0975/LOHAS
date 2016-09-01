<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor_Member.master" AutoEventWireup="true" CodeFile="Questionnaire_Form.aspx.cs" Inherits="Order_Member_Questionnaire_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Member/_Css/Questionaire_Form.css" rel="stylesheet" />

    <script src="https://connect.facebook.net/zh_TW/all.js" type="text/javascript"></script>
    <script type='text/javascript'>
        FB.init({
            appId: '1060323287408467',
            status: true,
            cookie: true,
            xfbml: true,
            oauth: true
        });

        function BindFB(FbShareName, FbShareCaption, FbShareDescription, Link, Image) {
            $("#vFbButton").click(function () {
                FB.login(function (response) {
                    if (response.authResponse) {
                        FB.api('/me', function (response) {
                            FB.ui({
                                method: 'feed',
                                name: FbShareName,
                                link: Link,
                                picture: Image,
                                caption: FbShareCaption,
                                description: FbShareDescription,
                            },
                           function (response) {
                               if (response && response.post_id) {
                                   $("#vSystemRemark").val($('#vFbButton').attr("SystemRemark"));
                                   eval($('#vSendButton').attr('href'));
                               } else { }
                           })
                        });
                    } else {
                        alert('必須登入Facebook,才能繼續進行');
                    }
                },
                { scope: '' }
                );
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Questionnaire_Form">

        <p class="Title">
            <asp:Literal ID="vTitle" runat="server"></asp:Literal>
        </p>

        <div class="Questionnaire_List">
            <asp:Repeater ID="vQuestionnaire_List" OnItemDataBound="vQuestionnaire_List_ItemDataBound" runat="server">
                <ItemTemplate>

                    <div class="Questionaire_Item">
                        <p class="Title">
                            <asp:Literal ID="vTitle" runat="server"></asp:Literal>
                        </p>
                        <asp:RadioButtonList ID="vRadioButtonList" Visible="false" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
                        </asp:RadioButtonList>
                        <asp:CheckBoxList ID="vCheckBoxList" Visible="false" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
                        </asp:CheckBoxList>
                        <asp:Repeater ID="vTextBoxList" Visible="false" OnItemDataBound="vTextBoxList_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <div style="margin-bottom: 5px">
                                    <asp:Label ID="vOption" runat="server" Text="左眼"></asp:Label>
                                    <asp:TextBox ID="vResult" runat="server"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div style="margin-top: 40px; text-align: center;">
            <asp:HiddenField ID="vSystemRemark" ClientIDMode="Static" runat="server" />
            <asp:LinkButton ID="vSendButton" ClientIDMode="Static" CssClass="SendButton" OnClick="vSendButton_Click" runat="server">完成預約</asp:LinkButton>
            <asp:HyperLink ID="vFbButton" CssClass="FbSendButton" NavigateUrl="javascript:void(0);" ClientIDMode="Static" runat="server"></asp:HyperLink>
        </div>

    </div>
</asp:Content>

