<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BudgetEstCategory.aspx.cs" Inherits="WIS.Category" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Budget Est Category Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 amalesh.t
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
            <legend> Budget Estimation Category </legend>
            <table align="center" border="0" width="50%">
                <tr>
                    <td align="left" style="width:25%">
                        <label class="iceLable">Category</label> <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="CategoryTextBox" runat="server" CssClass="iceTextBox" MaxLength="100" Width="250px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="vaccin" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                            ValidChars=" ," TargetControlID="CategoryTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqCategory" runat="server" ErrorMessage=" Enter Category "
                            Display="None" ControlToValidate="CategoryTextBox" ValidationGroup="Category"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top:12px">
                        <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Category" runat="server" />
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" OnClick="SaveButton_Click"
                            ValidationGroup="Category" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdCategory" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdCategory_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" 
            onrowdatabound="grdCategory_RowDataBound">
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
              
                <asp:BoundField DataField="BGT_CATEGORYNAME" HeaderText="Category" HeaderStyle-HorizontalAlign="Left" />

                <asp:TemplateField HeaderText="Subcategory">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <a id="lnkSubCategory" href="#" runat="server">View</a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("BGT_CATEGORYID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("ISDELETED").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("BGT_CATEGORYID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litCATEGORYID" Text='<%#Eval("BGT_CATEGORYID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</div>

        <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
          <script type="text/javascript" language="javascript">

              function OpenSubCategory(BGT_CATEGORYID) 
              {
                  var left = (screen.width - 800) / 2;
                  var top = (screen.height - 650) / 4;

                  open('BudgetEstSubCategory.aspx?id=' + BGT_CATEGORYID, 'SubCategory', 'width=800px,height=650px,top=' + top + ', left=' + left);
              }

              function DeleteRecord() {
                  return confirm('Are you want to delete?');
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
                  var tat1 = document.getElementById("<%= CategoryTextBox.ClientID  %>");
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
