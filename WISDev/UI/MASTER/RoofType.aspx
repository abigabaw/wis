<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RoofType.aspx.cs" Inherits="WIS.RoofType" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 RoofType Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Victory
 * @Created Date 21-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div id="divAll">
    <div style="width: 100%">
        <asp:Panel ID="pnlRoofType" runat="server">
            <fieldset class="icePnlinner">
                <legend>Roof Types</legend>
                <table align="center" border="0" width="40%">
                    <tr>
                        <td style="width: 30%" align="left">
                            <asp:Label ID="lblRoofType" runat="server" Text="Roof Type" CssClass="iceLable" /> <span
                                class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRoofType" runat="server" CssClass="iceTextBox" MaxLength="100" Width="250px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteRoofType" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=",- " TargetControlID="txtRoofType" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rqeRoofType" ControlToValidate="txtRoofType" ErrorMessage="Enter Roof Type"
                                Display="None" ValidationGroup="VsRoofType" runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="VsRoofType"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:ValidationSummary ID="VsRoof" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsRoofType" runat="server" />
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdRoofType" runat="server" CssClass="gridStyle" CellPadding="4"
            AllowPaging="True" AllowSorting="True" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
            Width="100%" PageSize="10" OnRowCommand="grdRoofType_RowCommand" OnPageIndexChanging="grdRoofType_PageIndexChanging">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="SI No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RoofTypeID" HeaderText="RoofTypeID" Visible="false" />
                <asp:BoundField DataField="RoofTypeName" HeaderText="Roof Type" HeaderStyle-HorizontalAlign="Left" />
                
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("RoofTypeID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Obsolete Column Added Here--%>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("RoofTypeID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litRoofTypeID" Text='<%#Eval("RoofTypeID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }
        document.getElementById('divAll').onclick = function () {
            isDirty = 0;
            setTimeout(function () { isDirty = 1; }, 100);
        };

        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                //isDirty = 2;
                return '';
            }
        }
    </script>
</asp:Content>
