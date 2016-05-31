<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SchoolDropReason.aspx.cs" Inherits="WIS.SchoolDropReason" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 School Drop Reason Master UI screen   
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
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
      <div id="divAll">
    <div style="width: 100%">
     <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
      <asp:Panel ID="pnlSave" Visible="true" runat="server">
        <fieldset class="icePnlinner">
          <legend>School Drop Reason</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td>
                        <asp:Label ID="lblReasonDropped" runat="server" Text="Reason Dropped" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReasonDropped" runat="server" CssClass="iceTextBox" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqeReasonDropped" ControlToValidate="txtReasonDropped"
                            ErrorMessage="Enter School Drop Reason" Display="None" ValidationGroup="ProjectDet"
                            runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteReasonDropped" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtReasonDropped" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="iceLable"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                            MaxLength="185" Height="50" Width="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <div align="center" style="margin-top: 12px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                ValidationGroup="ProjectDet" />&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear"
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
        <asp:GridView ID="gv_Details" runat="server" CellPadding="4" GridLines="None" AutoGenerateColumns="False"
            Height="50px" Width="100%" DataKeyNames="SchooldropreasonID" AllowSorting="True"
            OnRowCommand="gv_Details_RowCommand" CellSpacing="1" OnRowDeleting="gv_Details_RowDeleting"
            AllowPaging="True" PageSize="10" OnPageIndexChanging="gv_Details_PageIndexChanging">
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
                <asp:BoundField DataField="schooldropreason" HeaderText="Reason Dropped" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="20%" />
                <asp:BoundField DataField="description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                            CommandArgument='<%#Eval("SchooldropreasonID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-Width="7%">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("SchooldropreasonID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litSchoolDropID" Text='<%#Eval("SchooldropreasonID") %>' Visible="false"
                            runat="server"></asp:Literal>
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

        function SetVisible(val) {
            var hf = document.getElementById("<%= hfVisible.ClientID  %>");
            hf.value = val;
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
            var tat1 = document.getElementById("<%= txtReasonDropped.ClientID  %>");
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
