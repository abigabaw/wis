<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MNEGoalName.aspx.cs" Inherits="WIS.GoalName"
    MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 MNE Goal Name Master UI screen   
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
    <div style="width: 100%">
        <asp:Panel ID="pnlSave" runat="server" Visible="true">
            <fieldset class="icePnlinner">
                <legend>Goal Name</legend>
                <table align="center" border="0" width="45%">
                    <tr>
                        <asp:ValidationSummary ID="valsumGoalName" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="GoalName" />
                        <td align="left" width="25%">
                            <label class="iceLable">Goal Name</label> <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGoalName" runat="server" Width="300" MaxLength="50" CssClass="iceTextBox"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqGoalName" runat="server" ErrorMessage="Enter Goal Name"
                                ControlToValidate="txtGoalName" Display="None" ValidationGroup="GoalName"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteGoalName" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                ValidChars=" -" TargetControlID="txtGoalName" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="GoalName"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClr" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="gvGoalName" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Width="100%" CellPadding="4" CellSpacing="1" PageSize="10"
            GridLines="None" OnPageIndexChanging="gvGoalName_PageIndexChanging" OnRowCommand="gvGoalName_RowCommand">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="SI No">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GoalName" HeaderText="Goal Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("GoalID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("GoalID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litGOALID" Text='<%#Eval("GoalID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <script type="text/javascript" language="javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to delete this record?');
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
                var tat1 = document.getElementById("<%= txtGoalName.ClientID  %>");
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
