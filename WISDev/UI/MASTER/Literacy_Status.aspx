<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Literacy_Status.aspx.cs" Inherits="WIS.Literacy_Status" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		  Literacy Status UI screen   
 * @package		  Literacy Status
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  17-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
    <fieldset class="icePnlinner">
        <legend>Literacy Status</legend>
        <table align="center" border="0" cellpadding="3" width="80%">
            <tr>
                <td style="width: 15%">
                    <asp:Label ID="lblLiteracyStatus" runat="server" Text="Literacy Status" CssClass="iceLable" />
                    <span class="mandatory">*</span>
                </td>
                <td style="width: 38%">
                    <asp:TextBox ID="txtLiteracyStatus" runat="server" CssClass="iceTextBox" MaxLength="100"
                        Width="200px" />
                    <asp:RequiredFieldValidator ID="rqeLiteracyStatus" ControlToValidate="txtLiteracyStatus"
                        ErrorMessage="Enter Literacy Status" Display="None" ValidationGroup="Literacy"
                        runat="server"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteLiteracyStatus" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                        ValidChars=" " TargetControlID="txtLiteracyStatus" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 12%">
                    <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="iceLable" />
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                        MaxLength="200" Height="50" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <div style="margin-top: 12px;">
                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="Literacy"
                            OnClick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="Literacy"
                            runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:GridView ID="grdLitStatus" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
        PageSize="10" OnRowCommand="grdLitStatus_RowCommand" OnPageIndexChanging="grdLitStatus_PageIndexChanging">
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
            <asp:BoundField DataField="LTR_STATUS" HeaderText="Literacy Status" HeaderStyle-HorizontalAlign="Left"
                HeaderStyle-Width="20%" />
            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("LTR_STATUSID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("LTR_STATUSID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litLiteracyStatusID" Text='<%#Eval("LTR_STATUSID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }

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
            var tat1 = document.getElementById("<%= txtLiteracyStatus.ClientID  %>");
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
