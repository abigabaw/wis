<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Vulnerabilty.aspx.cs" Inherits="WIS.Vulnerabilty" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Vulnerabity Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Iranna
 * @Created Date 17-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div id="divAll">
    <fieldset class="icePnlinner">
        <legend>Disability</legend>
        <table  width="40%" align="center">
            <tr>
                <td align="left" width="25%">
                    <label class="iceLable">Disability</label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtVulnerability" runat="server" CssClass="iceTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqeVulnerability" ControlToValidate="txtVulnerability"
                        ErrorMessage="Disability Name is required" Display="None" ValidationGroup="VsVul"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="padding-top:12px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                        ValidationGroup="VsVul" />
                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsVul" runat="server" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:GridView ID="dv_Details" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" CellSpacing="1" GridLines="None"
        Height="50px" PageSize="10" Width="100%" OnRowCommand="dv_Details_RowCommand"
        OnPageIndexChanging="dv_Details_PageIndexChanging">
        <RowStyle CssClass="gridRowStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="VulnerabilityType" HeaderText="Disability" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                        CommandArgument='<%#Eval("VulnerabilityID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted ").ToString())%>'
                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("VulnerabilityID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litVulnerabilityID" Text='<%#Eval("VulnerabilityID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="gridHeaderStyle" />
        <PagerStyle CssClass="gridPagerStyle" />
        <RowStyle CssClass="gridRowStyle" />
    </asp:GridView>
    </div>
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <script language="javascript" type="text/javascript">
        document.getElementById('divAll').onclick = function () {
            isDirty = 0;
            setTimeout(function () { setDirtyText(); }, 100);
        };

        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtVulnerability.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
                isDirty = 0;
            }
            else {
                isDirty = 1;
                //txtyes = 1;
            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                //isDirty = 2;
                return '';
            }
        }   
            </script>
</asp:Content>
