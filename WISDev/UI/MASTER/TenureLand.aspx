<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TenureLand.aspx.cs" Inherits="WIS.TenureLand" %>
    <%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 TenureLand Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Iranna
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
    <div>
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <asp:Button ID="btnShowAdd" Text="Add Land Tenure" CssClass="icebutton" Width="120px"
            runat="server" OnClick="btnShowAdd_Click" />
        <asp:Button ID="btnShowSearch" Text="Search Land Tenure" CssClass="icebutton" Width="130px"
            runat="server" OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch">
        <fieldset class="icePnlinner">
            <legend>Search Land Tenure</legend>
            <table align="center" border="0" width="47%">
                <tr>
                    <td align="left" style="width: 30%">
                        <asp:Label ID="lblLandtenure" runat="server" Text="Land Tenure" CssClass="iceLable" />
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="iceTextBox" MaxLength="250"
                            Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                            <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlLandTenureDetails" runat="server" DefaultButton="btn_Save">
        <fieldset class="icePnlinner">
            <legend>Land Tenure Details</legend>
            <table align="center" border="0" width="47%">
                <tr>
                    <td align="left" style="width: 30%">
                        <label class="iceLable">
                            Land Tenure</label>
                        <span class="mandatory">*</span> &nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLandtenure" runat="server" class="iceTextBox" MaxLength="250"
                            Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqLandTenure" ControlToValidate="txtLandtenure"
                            ErrorMessage="Enter Land Tenure" Display="None" runat="server" ValidationGroup="LandTenureGroup"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteLandtenure" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtLandtenure" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding-top: 12px">
                        <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton" ValidationGroup="LandTenureGroup"
                            OnClick="btn_Save_Click" />&nbsp;<asp:Button ID="btn_Clear" runat="server" Text="Clear"
                                class="icebutton" OnClick="btn_Clear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:ValidationSummary ID="valSummaryLandTenure" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="LandTenureGroup" runat="server" />
    <asp:GridView ID="GrdLandTenure" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="True"
        OnRowCommand="GrdLandTenure_RowCommand" OnPageIndexChanging="GrdLandTenure_PageIndexChanging">
        <RowStyle CssClass="gridRowStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LND_TENURE" HeaderText="Land Tenure" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("LND_TENUREID") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("LND_TENUREID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litTenureLandID" Text='<%#Eval("LND_TENUREID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <EmptyDataTemplate>
            There are no records for the selected criteria.
        </EmptyDataTemplate>
    </asp:GridView>
    </div>
    <div class="footer">
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
                var btn = document.getElementById("<%= btn_Save.ClientID  %>");
                var tat1 = document.getElementById("<%= txtLandtenure.ClientID  %>");
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
