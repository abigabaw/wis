<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Relationship.aspx.cs" Inherits="WIS.Relationship" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Relationship Item Master UI screen   
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
    <table width="100%" align="left">
        <tr>
            <td align="left">
                <fieldset class="icePnlinner">
                    <legend>Relationship</legend>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 40%;" class="iceLable">
                                <asp:Label ID="iceLable" runat="server" Text="Relationship" CssClass="iceLable" />
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtrel" runat="server" class="iceTextBox" Width="300px" MaxLength="100" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="rel" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                    ValidChars=" ," TargetControlID="txtrel" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rqeRelation" ControlToValidate="txtrel" ErrorMessage="Enter Relationship"
                                    Display="None" ValidationGroup="ValSummary" runat="server"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <br />
                                <div align="center">
                                    <asp:Button ID="btn_Save" runat="server" ValidationGroup="ValSummary" value="Save"
                                        class="icebutton" OnClick="btn_Save_Click" Text="Save" />&nbsp;
                                    <asp:Button ID="btn_Clear" value="Clear" runat="server" class="icebutton" OnClick="btn_Clear_Click"
                                        Text="Clear" />
                                    <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                        ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ValSummary"
                                        runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <%-- <div align="center" class="CSSTableGenerator">--%>
                <asp:GridView ID="grdRelationship" runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdRelationship_RowCommand"
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="grdRelationship_PageIndexChanging">
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
                        <asp:BoundField DataField="Relationship" HeaderText="Relationship" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                    CommandName="EditRow" CommandArgument='<%#Eval("RELATIONSHIPID") %>' runat="server" />
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
                                    CommandName="DeleteRow" CommandArgument='<%#Eval("RELATIONSHIPID") %>' OnClientClick="return DeleteRecord();"
                                    runat="server" />
                                <asp:Literal ID="litRELATIONSHIPID" Text='<%#Eval("RELATIONSHIPID") %>' Visible="false"
                                    runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
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
            var btn = document.getElementById("<%= btn_Save.ClientID  %>");
            var tat1 = document.getElementById("<%= txtrel.ClientID  %>");
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
