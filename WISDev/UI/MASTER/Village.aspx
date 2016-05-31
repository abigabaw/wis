<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Village.aspx.cs" Inherits="WIS.UI.MASTER.Village" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Village Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Iranna
 * @Created Date 17-April-203
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
        <asp:Button ID="btnShowAdd" Text="Add New Village" runat="server" CssClass="icebutton"
            OnClick="btnShowAdd_Click" Width="133px" />&nbsp;
        <asp:Button ID="btnShowSearch" Text="Search Village" runat="server" CssClass="icebutton"
            OnClick="btnShowSearch_Click" Width="149px" />
         </div>
         <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch" >
        <fieldset class="icePnlinner">
            <legend>Search Village</legend>
            <table align="center" border="0" style="width: 100%">
                <tr>
                    <td align="right" style="width: 40%">
                        <label class="iceLable">
                            Village</label><span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtVillagename" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"
                            OnTextChanged="txtVillagename_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Village"
                            Display="None" ControlToValidate="txtVillagename" ValidationGroup="Village"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtVillagename" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="icebutton" OnClick="btnSearch_Click"
                            Text="Search" />
                        <asp:Button ID="btnClearSearch" runat="server" CssClass="icebutton" OnClick="btnClearSearch_Click"
                            Text="Clear" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
       <asp:Panel ID="pnlAddVillage" Visible="true" runat="server">
        <fieldset class="icePnlinner">
            <legend>Add Village</legend>
      <table align="center" border="0" width="90%">
        <tr>
          <td align="left">
                    <label class="iceLable">
                        District</label><span class="mandatory">*</span>
                </td>
            <td align="left">
                    <asp:DropDownList ID="ddlDistrict" CssClass="iceTextBox" AppendDataBoundItems="true"
                        AutoPostBack="true" Width="250px" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                            ErrorMessage=" Select District Name " ControlToValidate="ddlDistrict" ValidationGroup="Village"
                            Display="None">
                        </asp:RequiredFieldValidator>
                </td>

                    <td align="left">
                    <label class="iceLable">
                        County</label><span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCounty" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
                                runat="server" onselectedindexchanged="ddlCounty_SelectedIndexChanged1">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                            ErrorMessage=" Select County " ControlToValidate="ddlCounty" ValidationGroup="Village"
                            Display="None">
                        </asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                </tr>
                <tr>
                
           <td align="left">
                    <label class="iceLable">
                        Sub County</label><span class="mandatory">*</span>
                </td>
 <td align="left">
                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSubcounty" CssClass="iceTextBox"  Width="250px"
                                runat="server" onselectedindexchanged="ddlSubcounty_SelectedIndexChanged" 
                                AppendDataBoundItems="True" AutoPostBack="True"  >
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                            ErrorMessage=" Select Sub County " ControlToValidate="ddlSubcounty" ValidationGroup="Village"
                            Display="None">
                        </asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubcounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                
          <td align="left">
                    <label class="iceLable">
                        Village</label><span class="mandatory">*</span>
                </td>
                    <td>
                <asp:TextBox ID="txtVillage" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Village"
                    Display="None" ControlToValidate="txtVillage" ValidationGroup="Village"></asp:RequiredFieldValidator>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtVillage" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
            </tr>
   
        <tr>
            <td align="center" colspan="4" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="Village" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Village" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
    </fieldset>
    </asp:Panel>
   
    <asp:GridView ID="grdvillage" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdvillage_RowCommand"
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
              <asp:BoundField DataField="SubCountyName" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Left" />
            
              <asp:BoundField DataField="VillageName" HeaderText="Village" HeaderStyle-HorizontalAlign="Left" />
          
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("VILLAGEID")%>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("VILLAGEID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litVILLAGEID" Text='<%#Eval("VILLAGEID") %>' Visible="false" runat="server"></asp:Literal>
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
                var tat1 = document.getElementById("<%= txtVillage.ClientID  %>");
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
