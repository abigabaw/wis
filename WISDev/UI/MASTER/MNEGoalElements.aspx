<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MNEGoalElements.aspx.cs" Inherits="WIS.MNEGoalElements"
    MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 MNE Goal Elements Master UI screen   
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
                <legend>Goal Element</legend>
                <table align="center" border="0" width="45%">
                    <tr>
                        <asp:ValidationSummary ID="valsumGoalElement" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="GoalElement" />
                        <td align="left" width="25%">
                            <label class="iceLable">Goal Element</label> <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGoalElement" runat="server" Width="300" MaxLength="50" CssClass="iceTextBox"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqGoalElement" runat="server" ErrorMessage="Enter Goal Element"
                                ControlToValidate="txtGoalElement" Display="None" ValidationGroup="GoalElement"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteGoalElement" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                ValidChars=" -" TargetControlID="txtGoalElement" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="GoalElement"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClr" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="gvGoalElement" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Width="100%" CellPadding="4" CellSpacing="1" PageSize="10"
            GridLines="None" OnPageIndexChanging="gvGoalElement_PageIndexChanging" OnRowCommand="gvGoalElement_RowCommand">
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
                <asp:BoundField DataField="GoalElement" HeaderText="Goal Element" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("GoalElementID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("ISDELETED").ToString())%>'
                          OnCheckedChanged="IsObsolete_CheckedChanged"  AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("GoalElementID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litGOALID" Text='<%#Eval("GoalElementID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        </div>
</div>

        <script type="text/javascript" language="javascript">
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
                var tat1 = document.getElementById("<%= txtGoalElement.ClientID  %>");
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
