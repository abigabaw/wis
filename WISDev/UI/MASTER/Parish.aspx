<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parish.aspx.cs" Inherits="WIS.UI.MASTER.Parish" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		  Parish UI screen   
 * @package		  Parish
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  21-08-2013
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
        <asp:Button ID="btnShowAdd" Text="Add New Parish" runat="server" CssClass="icebutton"
            OnClick="btnShowAdd_Click" Width="123px" />&nbsp;
        <asp:Button ID="btnShowSearch" Text="Search Parish" runat="server" CssClass="icebutton"
            OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch" >
        <fieldset class="icePnlinner">
            <legend>Search Parish</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td align="left" style="width: 15%">
                        <asp:Label ID="llbParish" runat="server" Text="Parish" CssClass="iceLable" />
                    </td>
                    <td align="left">
                       <asp:TextBox ID="TxtSearchParish" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FTETxtSearchParish" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="TxtSearchParish" runat="server">
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
            <legend>Parish Details</legend>
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
                            ErrorMessage=" Select District Name " ControlToValidate="ddlDistrict" ValidationGroup="Parish"
                            Display="None">
                     </asp:RequiredFieldValidator>
                </td>

                    <td align="left">
                    <label class="iceLable">
                        County</label><span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplCounty" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCounty" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
                                runat="server" onselectedindexchanged="ddlCounty_SelectedIndexChanged" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                            ErrorMessage=" Select County " ControlToValidate="ddlCounty" ValidationGroup="Parish"
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
                            <asp:DropDownList ID="ddlsubcounty" CssClass="iceTextBox"  Width="250px"
                                runat="server" onselectedindexchanged="ddlsubcounty_SelectedIndexChanged" 
                                AppendDataBoundItems="True" AutoPostBack="True"  >
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                            ErrorMessage=" Select Sub County " ControlToValidate="ddlsubcounty" ValidationGroup="Parish"
                            Display="None">
                        </asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                  <td align="left">
                    <label class="iceLable">
                        Parish</label><span class="mandatory">*</span>
                </td>
                <td>
                <asp:TextBox ID="txtParish" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Parish"
                    Display="None" ControlToValidate="txtParish" ValidationGroup="Parish"></asp:RequiredFieldValidator>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtParish" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
</td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="Parish" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Parish" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
    </fieldset>
   </asp:Panel> 
    <asp:GridView ID="grdParish" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdParish_RowCommand"
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
           
            <asp:BoundField DataField="districtname" HeaderText="District Name" HeaderStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="countyname" HeaderText="County" HeaderStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="subcountyname" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Left" />
               <asp:BoundField DataField="parishname" HeaderText="Parsih" HeaderStyle-HorizontalAlign="Left" />
          
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("PARISHID")%>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("PARISHID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litPARISHID" Text='<%#Eval("PARISHID") %>' Visible="false" runat="server"></asp:Literal>
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
                var tat1 = document.getElementById("<%= txtParish.ClientID  %>");
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
