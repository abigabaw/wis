<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GraveFinish.aspx.cs" Inherits="WIS.GraveFinish" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Grave Finish Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Eshwar
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
    <fieldset class="icePnlinner">
        <legend>Grave Finish</legend>
        <table align="center" border="0" style="width: 50%">
            <tr>
                <td>
                    <label id="Label1" class="iceLable" cssclass="iceLable" runat="server">
                        Grave Finish</label>
                    <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtGrave" runat="server" class="iceTextBox" MaxLength="250" Width="326px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteGrave" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                        ValidChars=",- " TargetControlID="txtGrave" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="reqGrave" ControlToValidate="txtGrave" ErrorMessage="Enter Grave Finish"
                        Display="None" ValidationGroup="VsGraveFinish" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <div align="center">
                        <asp:Button ID="btn_Save" runat="server" value="Save" class="icebutton" OnClick="btn_Save_Click"
                            Text="Save" ValidationGroup="VsGraveFinish" />&nbsp;
                        <asp:ValidationSummary ID="VsGF" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsGraveFinish" runat="server" />
                        <asp:Button ID="btn_Clear" value="Clear" runat="server" class="icebutton" OnClick="btn_Clear_Click"
                            Text="Clear" /></div>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:GridView ID="grdGraveFinish" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" CellSpacing="1" GridLines="None" PageSize="10" AutoGenerateColumns="false"
        Width="100%" OnRowCommand="grdGraveFinish_RowCommand" OnPageIndexChanging="grdGraveFinish_PageIndexChanging">
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
            <asp:BoundField DataField="GraveFinishID" HeaderText="GraveFinishId" HeaderStyle-HorizontalAlign="Left"
                Visible="false" />
            <asp:BoundField DataField="GraveFinishType" HeaderText="Grave Finish" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("GraveFinishID") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("GraveFinishID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litGraveFinishID" Text='<%#Eval("GraveFinishID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        </div>
    <div class="footer">

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
                var btn = document.getElementById("<%= btn_Save.ClientID  %>");
                var tat1 = document.getElementById("<%= txtGrave.ClientID  %>");
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
    </div>
</asp:Content>
