<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FormNeverAttendedSchool.aspx.cs" Inherits="WIS.FormNeverAttendedSchool" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Form Never Attended School Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Sunil Kumar
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     <div id="divAll">
     <asp:Panel ID="pnlSave" Visible="true" runat="server">
    <fieldset class="icePnlinner">
    <legend>Never Attended School</legend>
        <table align="center" border="0" width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="ReasonforNeverAttendingSchoolLabel" runat="server" Text="Reason for Never Attending School"
                        CssClass="iceLable" />
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="NeverAttndSchlTextBox" runat="server" CssClass="iceTextBox" MaxLength="100"
                        Width="220px" />
                    <asp:RequiredFieldValidator ID="rqeNeverAttndSchl" ControlToValidate="NeverAttndSchlTextBox"
                        ErrorMessage="Enter Reason for Never Attending School" Display="None" ValidationGroup="Reason"
                        runat="server"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNeverAttndSchlTextBox" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                        ValidChars=" " TargetControlID="NeverAttndSchlTextBox" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <asp:Label ID="DescriptionLabel" runat="server" Text="Description" CssClass="iceLable" />
                </td>
                <td align="left">
                    <asp:TextBox ID="DescriptionTextBox" runat="server" MaxLength="200" CssClass="iceTextAera" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="Reason"
                                    OnClick="saveButton_Click" />
                                <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                    ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="Reason"
                                    runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    </asp:Panel>
    <asp:GridView ID="grdNASchool" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdNASchool_RowCommand"
        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
        <RowStyle CssClass="gridRowStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NVR_ATT_SCH_REASON" HeaderText="Reason for Never Attending School"
                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25%" />
            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("NVR_ATT_SCH_REASONID") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("NVR_ATT_SCH_REASONID") %>'
                        OnClientClick="return DeleteRecord();" runat="server" />
                    <asp:Literal ID="litNeverattentedSchoolID" Text='<%#Eval("NVR_ATT_SCH_REASONID") %>'
                        Visible="false" runat="server"></asp:Literal>
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
            var btn = document.getElementById("<%= saveButton.ClientID  %>");
            var tat1 = document.getElementById("<%= NeverAttndSchlTextBox.ClientID  %>");
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
