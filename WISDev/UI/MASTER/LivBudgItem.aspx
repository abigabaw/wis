<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="LivBudgItem.aspx.cs" Inherits="WIS.UI.MASTER.LivBudgItem" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 livBudg Item Master UI screen   
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
 <table align="center" border="0" width="70%">
       <tr>
   <td width="35%">
                <label class="iceLable">Budget Item Name</label><span class="mandatory">*</span>
            </td>
               <td>
                <asp:TextBox ID="txtBudCategory" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Budget Item Name"
                    Display="None" ControlToValidate="txtBudCategory" ValidationGroup="BudgetItem"></asp:RequiredFieldValidator>
                      <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtBudCategory" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
 </tr>
        <tr>
            <td width="35%">
                <label class="iceLable">Budget Item Description </label><span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtBudgetItem" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Budget Item Description "
                    Display="None" ControlToValidate="txtBudgetItem" ValidationGroup="BudgetItem"></asp:RequiredFieldValidator>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtBudgetItem" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="BudgetItem" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="BudgetItem" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                &nbsp;&nbsp;<input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="grdBudgetItem" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdBudgetItem_RowCommand"
        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" 
        onrowdatabound="grdBudgetItem_RowDataBound">
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
             <asp:BoundField DataField="LIV_BUD_ITEMNAME" HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LIV_BUD_ITEMDESC" HeaderText="Item Description" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("LIV_BUD_ITEMID")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("LIV_BUD_ITEMID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litCategoryID" Text='<%#Eval("LIV_BUD_ITEMID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle CssClass="iceLable" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <br />There are no records.
        </EmptyDataTemplate>
    </asp:GridView>
     <div class="footer">
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }      
        </script>
    </div>
</asp:Content>
