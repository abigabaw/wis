<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="GOUAllowance.aspx.cs" Inherits="WIS.GOUAllowance" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 GOUAllowance Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Nikitha.S.B
 * @Created Date 08-Oct-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
    <asp:Panel ID="pnlSave" Visible="true" runat="server">
        <fieldset class="icePnlinner">
        <legend>GOU Allowance</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td>
                        <asp:Label ID="lblGOUAllowanceCat" runat="server" Text="GOUAllowance Category" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGOUAllowanceCat" runat="server" CssClass="iceTextBox" MaxLength="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqeGOUAllowancecat" ControlToValidate="txtGOUAllowanceCat"
                            ErrorMessage="Enter GOUAllowance Category" Display="None" ValidationGroup="ProjectDet"
                            runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteGOUAllowanceCat" FilterType="UppercaseLetters,LowercaseLetters"
                            ValidChars=" " TargetControlID="txtGOUAllowanceCat" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:Label ID="lblGOUAllowanceVal" runat="server" Text="GOUAllowance Value" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGOUAllowanceVal" runat="server" CssClass="iceTextBox" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqeGOUAllowanceVal" ControlToValidate="txtGOUAllowanceVal"
                            ErrorMessage="Enter GOUAllowance Value" Display="None" ValidationGroup="ProjectDet"
                            runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"
                            ValidChars="," TargetControlID="txtGOUAllowanceVal" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <div align="center" style="margin-top: 12px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="ProjectDet"
                                OnClick="btnSave_Click" />&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear"
                                    CssClass="icebutton" OnClick="btnClear_Click" />
                            <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ProjectDet"
                                runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
    </div>
    <asp:GridView ID="gv_Details" runat="server" CellPadding="4" GridLines="None" AutoGenerateColumns="False"
        Height="50px" Width="100%" AllowSorting="True" CellSpacing="1"
        AllowPaging="True" PageSize="10" OnPageIndexChanging="gv_Details_PageIndexChanging"
        OnRowCommand="gv_Details_RowCommand">
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
            <asp:BoundField DataField="GOUALLOWANCECATEGORY" HeaderText="GOUAllowance Category"
                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" />
            <asp:BoundField DataField="GOUALLOWANCEVALUE" HeaderText="GOUAllowance Value" DataFormatString="{0:N0}"  HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                        CommandArgument='<%#Eval("GOUALLOWANCECATEGORYID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-Width="7%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                        AutoPostBack="true" OnCheckedChanged="IsObsolete_CheckedChanged" />
                </ItemTemplate>
                <HeaderStyle Width="5%"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("GOUALLOWANCECATEGORYID") %>'
                        OnClientClick="return DeleteRecord();" runat="server" />
                    <asp:Literal ID="litSchoolDropID" Text='<%#Eval("GOUALLOWANCECATEGORYID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }
    </script>
</asp:Content>
