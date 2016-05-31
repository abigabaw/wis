<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="County.aspx.cs" Inherits="WIS.UI.MASTER.County" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <%-- <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>--%>
</asp:Content>
<%--/**
 * 
 * @version		  County UI screen   
 * @package		  County
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  20-08-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="divAll">
 <div style="width: 100%">
        <div>
        <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnShowAdd" Text="Add New County" runat="server" OnClick="btnShowAdd_Click"
                            CssClass="icebutton" style="width:100%"/>
                    </td>
                    <td>
                        <asp:Button ID="btnShowSearch" Text="Search County" runat="server" OnClick="btnShowSearch_Click"
                            CssClass="icebutton" />
                    </td>
                </tr>
            </table>
        </div>

         <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnCountySearch" >
            <fieldset class="icePnlinner" style="margin-top: -06px;">
                <legend>Search County</legend>
                <table align="center" border="0" width="86%">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                         <td >
                <label class="iceLable">County</label>
            </td>
            <td>
                <asp:TextBox ID="txtSearchCounty" runat="server" MaxLength="100" Width="300px" CssClass="iceTextBox"></asp:TextBox>
               
             <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtSearchCounty" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtSearchCounty" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
                        </tr>
                         <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnCountySearch" CssClass="icebutton" Text="Search" runat="server"
                                OnClick="btnUserSearch_Click" />
                            <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="btnClearSearch_Click" />
                        </td>
                    </tr>
                    </table>
                </fieldset>
        </asp:Panel>

 <asp:Panel ID="pnlCountyDetails" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner" style="margin-top: -06px;">
                <legend>ADD New County</legend>
   <table align="center" border="0" width="100%">
        <tr>
        <td >
        <label class="iceLable">District Name</label><span class="mandatory">*</span>
        </td>
        <td>
         <asp:DropDownList ID="ddlDistrictName" runat="server" CssClass="iceDropDown" Width="300px" 
                              AppendDataBoundItems="True" AutoPostBack="True" 
                onselectedindexchanged="ddlDistrictName_SelectedIndexChanged" > 
                          <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                       </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                            ErrorMessage=" Select District Name " ControlToValidate="ddlDistrictName" ValidationGroup="County"
                            Display="None">
                        </asp:RequiredFieldValidator>
        </td>
            <td >
                <label class="iceLable">County</label><span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtCounty" runat="server" MaxLength="100" Width="300px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter County"
                    Display="None" ControlToValidate="txtCounty" ValidationGroup="County"></asp:RequiredFieldValidator>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtCounty" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        </table>
        <table align="center">
        <tr>
            <td align="center" colspan="2" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="County" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="County" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
    </fieldset>
    </asp:Panel>
    <asp:GridView ID="grdCounty" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdCounty_RowCommand"
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
              <asp:BoundField DataField="CountyName" HeaderText="County" HeaderStyle-HorizontalAlign="Left" />
          
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("COUNTYID")%>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("COUNTYID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litCOUNTYID" Text='<%#Eval("COUNTYID") %>' Visible="false" runat="server"></asp:Literal>
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
                var tat1 = document.getElementById("<%= txtCounty.ClientID  %>");
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
 </div>
</asp:Content>
