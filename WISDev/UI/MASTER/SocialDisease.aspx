<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SocialDisease.aspx.cs" Inherits="WIS.SocialDisease" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Social Disease Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Amalesh.T
 * @Created Date 24-April-203
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
        <asp:Button ID="btnShowAdd" Text="Add New Disease" CssClass="icebutton" Width="120px"
            runat="server" OnClick="btnShowAdd_Click" />
        <asp:Button ID="btnShowSearch" Text="Search Disease" CssClass="icebutton" Width="110px"
            runat="server" OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch">
        <fieldset class="icePnlinner">
            <legend>Search Disease</legend>
            <table align="center" border="0" width="45%">
                <tr>
                    <td align="left" style="width: 25%">
                        <asp:Label ID="lblDisease" runat="server" Text="Disease" CssClass="iceLable" />
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="iceTextBox" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="padding-top: 12px;">
                            <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlDiseaseDetails" runat="server" DefaultButton="btn_Save">
        <fieldset class="icePnlinner">
            <legend>Disease Details</legend>
            <table align="center" border="0" width="45%">
                <tr>
                    <td align="left" style="width: 25%">
                        <label class="iceLable">
                            Disease</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDisease" runat="server" class="iceTextBox" MaxLength="100" Width="300px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers"
                            ValidChars=" ," TargetControlID="txtDisease" runat="server" >
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="ReqDisease" ControlToValidate="txtDisease" ErrorMessage="Enter Disease Name"
                            Display="None" runat="server" ValidationGroup="DiseaseNameGroup"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding-top: 12px;">
                        <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton" ValidationGroup="DiseaseNameGroup"
                            OnClick="btn_Save_Click" />&nbsp;<asp:Button ID="btn_Clear" runat="server" Text="Clear"
                                class="icebutton" OnClick="btn_Clear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:ValidationSummary ID="valSummaryDisease" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="DiseaseNameGroup"
        runat="server" />
    <asp:GridView ID="GrdSocialdisease" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
        PageSize="10" OnPageIndexChanging="GrdSocialdisease_PageIndexChanging" OnRowCommand="GrdSocialdisease_RowCommand">
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
            <asp:BoundField DataField="DiseaseName" HeaderText="Disease" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("DiseaseId") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("isdeleted ").ToString())%>'
                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("DiseaseId") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litDiseaseId" Text='<%#Eval("DiseaseId") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
     <script language="javascript" type="text/javascript">
         function SetVisible(val) {
             var hf = document.getElementById("<%= hfVisible.ClientID  %>");
             hf.value = val;

             };

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
                 var tat1 = document.getElementById("<%= txtDisease.ClientID  %>");
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
</asp:Content>
