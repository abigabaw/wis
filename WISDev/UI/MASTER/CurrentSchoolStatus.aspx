<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="CurrentSchoolStatus.aspx.cs" Inherits="WIS.CurrentSchoolStatus" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 CurrentSchool Status Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Irran
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
        <asp:Panel ID="pnlSave" Visible="true" runat="server">
            <fieldset class="icePnlinner">
                <legend>Current School Status Details</legend>
                <table border="0" width="100%">
                    <tr>
                        <asp:ValidationSummary ID="ValSumCurrSchlStatus" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="CurrSchlStatus" runat="server" />
                        <td style="width: 20%" align="center">
                            <asp:Label ID="lblCurrSchlStatus" runat="server" Text="Current School Status" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtCurrSchlStatus" runat="server" CssClass="iceTextBox" Width="200"
                                AutoCompleteType="Disabled" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="rqeCurrSchlStatus" runat="server" ErrorMessage="Enter Current School Status"
                                ControlToValidate="txtCurrSchlStatus" Display="None" ValidationGroup="CurrSchlStatus"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteCurrSchlStatus" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" " TargetControlID="txtCurrSchlStatus" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="iceLable" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                                MaxLength="185" Height="50" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCurrSchlStatusID" runat="server" CssClass="iceTextBoxLarge" Visible="false"
                                Width="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <div style="margin-top: 15px;">
                                <asp:Button ID="SaveButton" CssClass="icebutton" Text="Save" runat="server" OnClick="SaveButton_Click"
                                    ValidationGroup="CurrSchlStatus" />&nbsp;
                                <asp:Button ID="ClearButton" CssClass="icebutton" Text="Clear" runat="server" OnClick="ClearButton_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSaveMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
         <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
        <asp:GridView ID="gvCurSchlStatus" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" AllowPaging="True"
            AllowSorting="True" Width="100%" OnPageIndexChanging="gvCurSchlStatus_PageIndexChanging"
            OnRowCommand="gvCurSchlStatus_RowCommand">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <%-- <PagerSettings FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<"
                Mode="NumericFirstLast" Position="Bottom" />--%>
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <FooterStyle CssClass="gridFooterStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Current School Status" HeaderStyle-HorizontalAlign="Center"
                    DataField="CurrentSchoolStatus" HeaderStyle-Width="25%">
                    <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" DataField="Description">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CurrentSchoolStatusID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CurrentSchoolStatusID") %>'
                            OnClientClick="return DeleteRecord();" runat="server" />
                        <asp:Literal ID="litSchoolStatusID" Text='<%#Eval("CurrentSchoolStatusID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
          </asp:Panel>
        </div>
        <script language="javascript" type="text/javascript">
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
                var btn = document.getElementById("<%= SaveButton.ClientID  %>");
                var tat1 = document.getElementById("<%= txtCurrSchlStatus.ClientID  %>");
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
