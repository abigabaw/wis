<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Major_shocks.aspx.cs" Inherits="WIS.Major_shocks" %>

<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
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
<%--/**
 * 
 * @version		 0.1 Major_shocks UI screen   
 * @package		 Major_shocks
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Mahalakshmi
 * @Created Date 23-April-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div><br />
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Shock Details </legend>
            <table align="center" border="0" cellpadding="0" cellspacing="2" id="table1">
                <tr>
                    <td>
                        <asp:Label ID="typeofshockLabel" runat="server" Text="Type of Shock" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="typeofshockDropDownList" runat="server" CssClass="iceDropDown" 
                            Width="300px" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="typeofshockDropDownList"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="reqShocklabel" runat="server" ErrorMessage="Select Type of Shock"
                            ControlToValidate="typeofshockDropDownList" InitialValue="0" Display="None" ValidationGroup="MajorShocks"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="copingmechLabel" runat="server" Text="Coping Mechanism" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="copingmechDropDownList" runat="server" CssClass="iceDropDown"
                            Width="100px"  AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="copingmechDropDownList"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="reqcoping" runat="server" ErrorMessage="Select Coping Mechanism"
                            ControlToValidate="copingmechDropDownList" InitialValue="0" Display="None" ValidationGroup="MajorShocks"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="helpedLabel" runat="server" Text="Who Helped Most ?" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="helpedDropDownList" runat="server" CssClass="iceDropDown" Width="200px"  AppendDataBoundItems=true>
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="helpedDropDownList"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="reqHelped" runat="server" ErrorMessage="Select 'Who Helped most?'"
                            ControlToValidate="helpedDropDownList" InitialValue="0" Display="None" ValidationGroup="MajorShocks"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="pap_shockidTextBox" Visible="false" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="lnkMajorSchock" runat="server" Text="Change Request" CssClass="icebutton" Width="120px" Visible="false" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="MajorShocks" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="StatusMajorSchock" runat="server" Style="text-decoration: blink; color: Red;font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="valsumMajorShocks" runat="server" ShowSummary="false"
                ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                ValidationGroup="MajorShocks" />
        </fieldset>
        &nbsp;
        <asp:GridView ID="grdshocks" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AllowPaging="true" PageSize="10" AutoGenerateColumns="False"
            OnPageIndexChanging="ChangePage" Width="100%" OnRowCommand="grdUsers_RowCommand">
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
                <asp:BoundField DataField="SHOCKEXPERIENCED1" HeaderText="Type Of Shock" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="COP_MECHANISM1" HeaderText="Coping Mechanism" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SUPPORTEDBY1" HeaderText="Helped By" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("PAP_SHOCKID1") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PAP_SHOCKID1") %>'
                            OnClientClick="return DeleteRecord();" runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("PAP_SHOCKID1") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
     <script language="javascript" type="text/javascript">
         function DeleteRecord() {
             return confirm('Are you sure you want to Delete?');
         }
         function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
             var left = (screen.width - 600) / 2;
             var top = (screen.height - 500) / 4;
             open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
         }
         spnpnldiv = document.getElementById('table1');
         if (spnpnldiv != null) {
             scrWidth = screen.availWidth;
             spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
         }

    </script>
</asp:Content>
