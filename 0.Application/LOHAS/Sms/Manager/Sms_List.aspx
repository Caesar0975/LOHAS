<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Manager.master" AutoEventWireup="true" CodeFile="Sms_List.aspx.cs" Inherits="Sms_Sms_List" %>

<%@ Register Src="/_Element/Pagger/Pagger.ascx" TagName="Pagger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Sms/Manager/_Css/Sms_List.css" rel="stylesheet" />

    <script type="text/javascript">

        function BindDatepicker() {
            $(".Datepicker").datepicker({
                monthNames: [".1", ".2", ".3", ".4", ".5", ".6", ".7", ".8", ".9", ".10", ".11", ".12"],
                dateFormat: "yy-mm-dd",
                showMonthAfterYear: true,
                firstDay: 1
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="Sms_List">
                <h2>簡訊管理</h2>

                <div class="Block">
                    <h4>發送對象</h4>
                    <asp:CheckBoxList ID="vStateTypeList" RepeatLayout="Flow"  RepeatDirection="Horizontal" AutoPostBack="true" runat="server" />
                    <asp:TextBox ID="vStartDate" CssClass="Datepicker" MaxLength="10" AutoPostBack="true" runat="server"></asp:TextBox>
                    <label>至 </label>
                    <asp:TextBox ID="vEndDate" CssClass="Datepicker" MaxLength="10" AutoPostBack="true" runat="server"></asp:TextBox>

                    <h4>發送內容</h4>
                    <asp:TextBox ID="vContent" MaxLength="1000" Width="99%" Rows="6" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="vSaveBtn" CssClass="Button2 Blue SendButton" OnClick="vSaveBtn_Click" runat="server">發送</asp:LinkButton>
                </div>
                <div class="Block">

                    <h4>發送對象</h4>
                    <table class="Table1 Detail">
                        <colgroup>
                            <col style="width: 80px;" />
                            <col />
                            <col style="width: 120px;" />
                            <col style="width: 120px;" />
                            <col style="width: 160px;" />
                            <col style="width: auto;" />
                            <col style="width: 20px;" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th>姓名</th>
                                <th>身份證字號</th>
                                <th class="auto-style1">電話</th>
                                <th>地區</th>
                                <th>學校</th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="vMemberList" runat="server" OnItemDataBound="vMemberList_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Literal ID="vName" runat="server"></asp:Literal>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Literal ID="vAccount" runat="server"></asp:Literal>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Literal ID="vPhone" runat="server"></asp:Literal>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Literal ID="vArea" runat="server"></asp:Literal>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Literal ID="vSchool" runat="server"></asp:Literal>
                                        </td>
                                        <td style="text-align: center;"></td>
                                        <td style="text-align: center;">
                                            <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/User/Manager/Member_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <div>
                        <uc1:Pagger ID="Pagger1" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

