<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SocioConcerns.aspx.cs" Inherits="WIS.Concerns" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
    <fieldset class="icePnlinner">
        <legend>Concern Detail</legend>
        <table width="100%" align="center">
            <tr>
                <td>
                    <label class="iceLable">PAP Concern</label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="PAPConcernsDropDownList" CssClass="iceDropDown" runat="server"
                        Width="255px" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="lsePAPConcernsDropDownList" runat="server"
                        TargetControlID="PAPConcernsDropDownList"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:TextBox ID="PAPConcernsIDTextBox" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator ID="rfvPAPConcernsDropDownList" runat="server" ControlToValidate="PAPConcernsDropDownList"
                        ValidationGroup="vgSConcern" Text="Mandatory" InitialValue="0" ErrorMessage="Select a PAP Concerns"
                        Display="None">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                    <label class="iceLable">Description</label>
                </td>
                <td style="vertical-align:top">
                    <asp:TextBox ID="PAPOtherConcernsTextBox" runat="server" ClientIDMode="Static" MaxLength="400"
                        TextMode="MultiLine" CssClass="iceTextBox" Width="800px" Height="60px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="lnkSocailConcerns" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                        ValidationGroup="vgSConcern" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="StatusSocailConcerns" runat="server" Style="text-decoration: blink;
                        color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSConcern" runat="server" />
    </fieldset>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
    <asp:GridView ID="grdSocioConcerns" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AllowPaging="true" PageSize="10" AutoGenerateColumns="False"
        OnPageIndexChanging="ChangePage" Width="100%" OnRowCommand="grdSocioConcerns_RowCommand">
        <RowStyle CssClass="gridRowStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="concernName" HeaderText="Concern" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="OtherConcern" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("PapConcernID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("PapConcernID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litConcernID" Text='<%#Eval("PapConcernID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </asp:Panel>
    <script type="text/javascript" language="javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this record?');
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 80).toString() + "px";
        }

        /*
        function EnableDisableOtherConcern(src) {
            occupantStatus = src.options[src.selectedIndex].text;

            if (occupantStatus == 'Other') {
                document.getElementById('PAPOtherConcernsTextBox').disabled = '';
                document.getElementById('PAPOtherConcernsTextBox').focus();
            }
            else {
                document.getElementById('PAPOtherConcernsTextBox').disabled = 'disabled';
                //                    document.getElementById('PAPOtherConcernsTextBox').value = '';
            }
        }
        */

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
