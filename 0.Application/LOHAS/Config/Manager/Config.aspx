<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="Config.aspx.cs" Inherits="Config_Manager_Config" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/_Element/HtmlEditor/HtmlEditor.ascx" TagPrefix="uc1" TagName="HtmlEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Config/Manager/_Css/Config.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Config">
        <h2>系統設定</h2>

        <table class="Table1">
            <colgroup>
                <col style="width: 120px;" />
                <col style="width: auto;" />
            </colgroup>
            <tbody>
                <tr>
                    <th>壹元簡訊</th>
                    <td>
                        <label>簡訊帳號</label><br />
                        <asp:TextBox ID="vSmsAccount" MaxLength="100" Width="300px" placeholder="請輸入簡訊帳號" runat="server"></asp:TextBox><br />
                        <label>簡訊密碼</label><br />
                        <asp:TextBox ID="vSmsPassword" MaxLength="100" Width="300px" placeholder="請輸入簡訊密碼" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>黑名單</th>
                    <td>
                        <label>請輸入身分證字號，多筆請用半形逗點(,)分隔</label><br />
                        <asp:TextBox ID="vBlacklist" MaxLength="1000" Width="99%" placeholder="身分證字號" Rows="5" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>活動說明1</th>
                    <td>
                        <uc1:HtmlEditor ID="vActivityDescription" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>活動說明2</th>
                    <td>
                        <uc1:HtmlEditor ID="vActivityDescription2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>報名成功說明</th>
                    <td>
                        <uc1:HtmlEditor ID="vFinishFormDescription" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>前台計數器</th>
                    <td>
                        <asp:TextBox ID="vCounter" Width="50px" MaxLength="10" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>FB分享設定</th>
                    <td>
                        <div class="Right">
                            <label>FB分享名稱 ( 60字內 )</label><br />
                            <asp:TextBox ID="vFbShareName" CssClass="FbShareName" MaxLength="60" placeholder="FB分享 名稱" runat="server"></asp:TextBox>
                            <label>FB分享標題 ( 60字內 )</label><br />
                            <asp:TextBox ID="vFbShareCaption" CssClass="FbShareCaption" MaxLength="60" placeholder="FB分享 標題" runat="server"></asp:TextBox>
                            <label>FB分享內文 ( 120字內 )</label><br />
                            <asp:TextBox ID="vFbShareDescription" CssClass="FbShareContext" TextMode="MultiLine" placeholder="FB分享 內文" runat="server"></asp:TextBox>
                        </div>
                        <div class="Left">
                            <label>FB分享圖片( 600px x 315px )</label>
                            <asp:Image ID="vFbShareImage" CssClass="FbShareImage" runat="server" />
                            <asp:FileUpload ID="vFbShareImageUploder" Width="100%" runat="server" />
                        </div>
                        <div style="height: 1px; clear: both;"></div>
                    </td>
                </tr>
                <tr>
                    <th>SEO設定</th>
                    <td>
                        <label>Title</label><br />
                        <asp:TextBox ID="vSeoTitle" Width="99%" placeholder="SeoTitle" runat="server"></asp:TextBox><br />
                        <label>Description</label><br />
                        <asp:TextBox ID="vSeoDescription" Width="99%" placeholder="SeoDescription" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>流量分析代碼</th>
                    <td>
                        <asp:TextBox ID="vAnalyticsCode" TextMode="MultiLine" Rows="14" Width="99%" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:LinkButton ID="vSaveBtn" CssClass="Button2 Blue" Style="float: right" OnClick="vSaveBtn_Click" runat="server">儲存</asp:LinkButton>
    </div>
</asp:Content>

