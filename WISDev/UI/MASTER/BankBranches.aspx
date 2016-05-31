<%@ Page Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="BankBranches.aspx.cs" Inherits="WIS.BankBranches" %>
    
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Bank Branchs Master UI screen   
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
        <fieldset class="icePnlinner">
            <legend>Branch Details</legend>
            <table align="center" border="0" width="100%">
                <tr>
                    <td align="left" class="style3">
                        <label class="iceLable">
                        Branch Name</label> <span class="mandatory">*</span> </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtBranchName0" runat="server" class="iceTextBox" 
                            MaxLength="90" Width="334px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="txtBranchName0_FilteredTextBoxExtender" 
                            runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom" 
                            TargetControlID="txtBranchName0" ValidChars=" ">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtBranchName0" Display="None" ErrorMessage="Enter Branch Name" 
                            ValidationGroup="Bank"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style3">
                        <label class="iceLable">
                        City</label> <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="width: 30%">
                        <asp:TextBox ID="txtCity0" runat="server" class="iceTextBox" MaxLength="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="txtCity0_FilteredTextBoxExtender" 
                            runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom" 
                            TargetControlID="txtCity0" ValidChars=" ">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqCity0" runat="server" 
                            ControlToValidate="txtCity0" Display="None" ErrorMessage="Enter City" 
                            ValidationGroup="Bank"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Swift Code</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSwiftCode" runat="server" class="iceTextBox" MaxLength="11"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSwiftCode"
                            ErrorMessage="Enter Swift Code" Display="None" ValidationGroup="Bank" runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Numbers"
                            ValidChars=" " TargetControlID="txtSwiftCode" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Bank Code</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtBankCode" runat="server" class="iceTextBox" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtBankCode"
                            ErrorMessage="Enter Bank Code" Display="None" ValidationGroup="Bank" runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Numbers"
                            ValidChars=" " TargetControlID="txtBankCode" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="padding-top: 12px">
                        <asp:Button ID="btnSave" Text="Save" runat="server" class="icebutton" OnClick="btnSave_Click"
                            ValidationGroup="Bank" />&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="icebutton" OnClick="btnClear_Click" />
                        <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                    </td>
                </tr>
            </table>
        </fieldset>
    <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Bank" runat="server" />
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" HorizontalAlign="Center" Height="100%">
    <asp:GridView ID="grdBankBranchs" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdBankBranchs_RowCommand"
        PageSize="10" AllowPaging="True" OnPageIndexChanging="ChangePage" 
        onselectedindexchanged="grdBankBranchs_SelectedIndexChanged">
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
         
           
            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="SwiftCode" HeaderText="Swift Code" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BANKCODE" HeaderText="Bank Code" HeaderStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("BankBranchId") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("BankBranchId") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litBankBranchId" Text='<%#Eval("BankBranchId") %>' Visible="false" runat="server"></asp:Literal>
                    <asp:Literal ID="litBankID" Text='<%#Eval("BankID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </asp:Panel>
    <div class="footer">
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }

            spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
            if (spnpnl != null) {
                //scrWidth = screen.availWidth;
                spnpnl.style.width = parseInt(680).toString() + "px";
            }
            function checkswiftcode(src) {
                var msg;
                msg = src.value;
                var n = msg.length;
                if (n < 11) {
                    alert("SwiftCode must be 11 character");
                    src.value = '';
                    return;
                }
            }
                
        </script>
    </div>
</asp:Content>
