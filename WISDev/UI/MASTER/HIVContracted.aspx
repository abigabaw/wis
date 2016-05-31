<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HIVContracted.aspx.cs" Inherits="WIS.HIVContracted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%--/**
 * 
 * @version		 0.1 HIV Contracted Master UI screen   
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
        <fieldset class="icePnlinner">
            <legend> HIV Contracted </legend>
            <table border="0" align="center" width="65%">
                <tr>
                    <td>
                        <label class="iceLable">
                            HIVContracted</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="HIVCTextBox" runat="server" CssClass="iceTextBox" MaxLength="100"
                            Width="250px" />
                        <asp:RequiredFieldValidator ID="rqeHIVCTextBox" ControlToValidate="HIVCTextBox"
                            Display="None" ErrorMessage="Enter HIV Contracted" ValidationGroup="HIVC" runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteHIVC" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="HIVCTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 6px">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="PAPStatus"
                            OnClick="SaveButton_Click" />
                        <asp:ValidationSummary ID="vsHIVC" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" HeaderText="Please enter/correct the following" ValidationGroup="HIVC"
                            runat="server" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdHIVC" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdHIVC_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" OnSelectedIndexChanged="grdHIVC_SelectedIndexChanged">
           
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
              
                <asp:BoundField DataField="ContractedThrough" HeaderText="Contracted Through" HeaderStyle-HorizontalAlign="Left" />

                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("ContractedID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                      
                        <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" OnCheckedChanged="IsObsolete_CheckedChanged"
                            Checked='<%#bool.Parse(Eval("Isdeleted").ToString())%>' OnClientClick="return ObsoleteRecord();" />
                            
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("ContractedID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litContractedID" Text='<%#Eval("ContractedID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to delete this record?');
            }
            function ObsoleteRecord() {
                return confirm('Are you sure you want to update this record?');
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
                var tat1 = document.getElementById("<%= HIVCTextBox.ClientID  %>");
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
