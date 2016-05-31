<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Vaccination.aspx.cs" Inherits="WIS.Vaccination" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		  Vaccination UI screen   
 * @package		  WIS
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  19-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Vaccination</legend>
            <table align="center" border="0" width="50%">
                <tr>
                    <td align="left" style="width:25%">
                        <asp:Label ID="VaccinationLabel" runat="server" Text="Vaccination" CssClass="iceLable" /><span
                            class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="VaccinationTextBox" runat="server" CssClass="iceTextBox" MaxLength="100" Width="250px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="vaccin" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                            ValidChars=" ," TargetControlID="VaccinationTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqVaccination" runat="server" ErrorMessage=" Enter Vaccination "
                            Display="None" ControlToValidate="VaccinationTextBox" ValidationGroup="Vaccination"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="VACCINATIONIDTextBox" runat="server" CssClass="iceTextBoxLarge"
                            Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top:12px">
                        <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Vaccination" runat="server" />
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" OnClick="SaveButton_Click"
                            ValidationGroup="Vaccination" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdVaccination" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdVaccination_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
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
                <%-- <asp:BoundField DataField="ConcernID" HeaderText="ConcernID" HeaderStyle-HorizontalAlign="Left" />--%>
                <asp:BoundField DataField="VACCINATIONNAME" HeaderText="Vaccination Name" HeaderStyle-HorizontalAlign="Left" />
                <%--<asp:BoundField DataField="RoleID" HeaderText="Role" HeaderStyle-HorizontalAlign="Left" />--%>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("VACCINATIONID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("ISDELETED ").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("VACCINATIONID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litVACCINATIONID" Text='<%#Eval("VACCINATIONID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    </div>
</asp:Content>
