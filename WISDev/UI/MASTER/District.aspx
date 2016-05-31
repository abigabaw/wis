<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="District.aspx.cs" Inherits="WIS.UI.MASTER.District" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 District Master UI screen   
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
       <div>
       <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <asp:Button ID="btnShowAdd" Text="Add New District" runat="server" CssClass="icebutton"
            OnClick="btnShowAdd_Click" Width="123px" />&nbsp;
        <asp:Button ID="btnShowSearch" Text="Search District" runat="server" CssClass="icebutton"
            OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch" >
        <fieldset class="icePnlinner">
            <legend>Search District</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td align="left" style="width: 15%">
                        <asp:Label ID="Label1" runat="server" Text="District Name" CssClass="iceLable" />
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSearchDistrictName" runat="server" CssClass="iceTextBox" 
                            MaxLength="150" Width="356px"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="reqDistrictNameSearch" runat="server" ErrorMessage=" Enter DistrictName to search"
                                ControlToValidate="txtSearchDistrictName" Display="None" ValidationGroup="District"></asp:RequiredFieldValidator>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtSearchDistrictName" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding-top: 12px">
                        <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                        <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                            OnClick="btnClearSearch_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
      <asp:Panel ID="pnlDistrictDetails" runat="server">
        <fieldset class="icePnlinner">
            <legend>District Details</legend>
   <table align="center" border="0" width="50%">
        <tr>
            <td width="30%">
                <label class="iceLable">District Name</label><span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtDistrict" runat="server" MaxLength="100" Width="300px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter District Name"
                    Display="None" ControlToValidate="txtDistrict" ValidationGroup="District"></asp:RequiredFieldValidator>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtDistrict" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="District" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="District" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
     </fieldset>
    </asp:Panel>
    <asp:GridView ID="grdDistrict" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdDistrict_RowCommand"
        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
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
            <asp:BoundField DataField="DistrictName" HeaderText="District Name" HeaderStyle-HorizontalAlign="Left" />
          
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("DISTRICTID")%>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("DISTRICTID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litDISTRICTID" Text='<%#Eval("DISTRICTID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div class="footer">
      <script language="javascript" type="text/javascript">
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
                var btn = document.getElementById("<%= btnSave.ClientID  %>");
                var tat1 = document.getElementById("<%= txtDistrict.ClientID  %>");
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
     <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
