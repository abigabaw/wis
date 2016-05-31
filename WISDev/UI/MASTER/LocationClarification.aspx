<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocationClarification.aspx.cs" Inherits="WIS.UI.MASTER.LocationClarification" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>

<fieldset class = "icePnlinner">
<legend> Location Classification </legend>
<table align="center" border="0" width="90%">
   <tr>
    <td>
         <label class = "iceLable"> Location Classification</label><span class = "mandatory">*</span>
     </td>
     <td>
     <asp:TextBox ID="txtLocation" runat="server" MaxLength="100" Width="200px" CssClass="iceTextBox"></asp:TextBox>
      <asp:RequiredFieldValidator ID = "reqLocation"   runat = "server" ErrorMessage = "Enter the Location" Display = "None"
      ControlToValidate = "txtLocation" ValidationGroup = "Location"></asp:RequiredFieldValidator>
     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
        ValidChars=" " TargetControlID="txtLocation" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
     </td>

         <td>
         <label class = "iceLable"> Land Code</label><span class = "mandatory">*</span>
     </td>
     <td>
     <asp:TextBox ID="txtcompland" runat="server" MaxLength="50" Width="150px" CssClass="iceTextBox"></asp:TextBox>
      <asp:RequiredFieldValidator ID = "reqland"   runat = "server" ErrorMessage = "Enter the Compensation Land" Display = "None"
      ControlToValidate = "txtcompland" ValidationGroup = "Location"></asp:RequiredFieldValidator>
     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,LowercaseLetters,Numbers"
        ValidChars=" " TargetControlID="txtcompland" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
     </td>
    </tr>
    <tr>
        <td colspan="4">
            <table align="center" border="0" width="50%">
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                            ValidationGroup="Location" />
                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Location" runat="server" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</fieldset>

<asp:GridView ID="grdLocation" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" 
        AllowPaging="true" PageSize="10">
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
            <asp:BoundField DataField="LocClassification" HeaderText="Location Classification" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Location" HeaderText="Land Code" HeaderStyle-HorizontalAlign="Left" />

          
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("LOCATIONID")%>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("LOCATIONID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litLOCATIONID" Text='<%#Eval("LOCATIONID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


</asp:Content>
