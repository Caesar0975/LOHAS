<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="QuestionnaireItem_List.aspx.cs" Inherits="QuestionnaireItem_List" ValidateRequest="false" %>

<%@ Register Src="/_Element/Pagger/Pagger.ascx" TagName="Pagger" TagPrefix="uc1" %>
<%@ Register Src="~/_Element/HtmlEditor/HtmlEditor.ascx" TagPrefix="uc1" TagName="HtmlEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Order/Manager/_Css/QuestionnaireItem_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="QuestionnaireItem_List">
        <div>
            <h2>問卷列表</h2>
        </div>
        <h3>活動概述</h3>
        <uc1:HtmlEditor ID="vHtmlEditor" runat="server" />
        <asp:LinkButton ID="vSaveButton" CssClass="Button2 Blue" Style="display:block; width:80px; margin: 10px auto 20px auto" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>
        <div style="clear: both; height: 1px;"></div>
        <h3>問題列表</h3>
        <table class="Table1 vQuestionnaireItem_List">
            <colgroup>
                <col style="width: auto;" />
                <col style="width: 170px;" />
                <col style="width: 40px;" />
                <col style="width: 40px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>題目</th>
                    <th>選項</th>
                    <th>排序</th>
                    <th>
                        <asp:HyperLink ID="vQuestionnaireItemAdd" runat="server" NavigateUrl="/Order/Manager/QuestionnaireItem_Modify.aspx" CssClass="Button1 Add fancybox" Text=" "></asp:HyperLink></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vQuestionnaireItem" runat="server" OnItemDataBound="vQuestionnaireItem_List_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:Label ID="vTitle" CssClass="Title" runat="server" Text="vTitle"></asp:Label><br />
                                <asp:Label ID="vShortTitle" CssClass="ShortTitle" runat="server" Text="vShortTitle"></asp:Label>
                            </td>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:Label ID="vOptionType" runat="server" Text="vOptionType"></asp:Label><br />
                                項目：<br />
                                <asp:Repeater ID="vOption_List" OnItemDataBound="vOption_List_ItemDataBound" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="vOption_Item" runat="server" Text="vOption_Item"></asp:Label><br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vSort" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/Order/Manager/QuestionnaireItem_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

