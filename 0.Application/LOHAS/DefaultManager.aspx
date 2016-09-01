<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base.master" AutoEventWireup="true" CodeFile="DefaultManager.aspx.cs" Inherits="_ManagerLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/_Css/DefaultManager.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="DefaultManager" ClientIDMode="Static" DefaultButton="vLoginButton" runat="server">
        <table class="LoginTable">
            <tr>
                <td>
                    <label>Account</label>
                    <asp:TextBox ID="vAccount" MaxLength="20" Width="260" autocomplete="off" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Password</label>
                    <asp:TextBox ID="vPassword" MaxLength="20" TextMode="Password" Width="260" autocomplete="off" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Valid Code</label>
                    <asp:TextBox ID="vValidCode" MaxLength="10" Width="184" autocomplete="off" runat="server"></asp:TextBox>
                    <asp:Image ID="vValidCodeImage" ImageUrl="/_Element/ValidCode/_Images/Image_RandomNumberDemo.gif" CssClass="ValidCodeItem" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:LinkButton ID="vLoginButton" runat="server" CssClass="LoginButton" OnClick="vLoginButton_Click">LogIn</asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>


