<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CulturPropertiesMaster.aspx.cs" Inherits="WIS.CulturPropertiesMaster" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Culture Properties</legend>
            <table border="0" align="center" width="45%">
                <tr>
                    <td align="left" nowrap>
                        <asp:Label ID="CulturPropertiesLabel" runat="server" Text="Culture Property Type" CssClass="iceLable" />
                        &nbsp;<span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="CulturPropertiesTextBox" runat="server" CssClass="iceTextBoxLarge" MaxLength="100" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="CulturPropertiesTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rqeCulturePropTextBox" ControlToValidate="CulturPropertiesTextBox"
                            ErrorMessage="Enter Culture Properties" Display="None" ValidationGroup="CultureProp" runat="server"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="CulturPropertiesIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" 
                            ValidationGroup="CultureProp" onclick="SaveButton_Click"
                             />
                        <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="CultureProp"
                            runat="server" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" 
                            onclick="ClearButton_Click"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="msgSaveLabel" runat="server" Text="" CssClass="iceLable" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdCultureProp" runat="server" CssClass="gridStyle" CellPadding="4"
             CellSpacing="1" GridLines="None" AutoGenerateColumns="false" 
            Width="100%"  AllowPaging="true" PageSize="10" 
            onpageindexchanging="grdCultureProp_PageIndexChanging" 
            onrowcommand="grdCultureProp_RowCommand">
             <RowStyle  CssClass="gridRowStyle" />
             <AlternatingRowStyle CssClass="gridAlternateRow" />
             <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
             <HeaderStyle CssClass="gridHeaderStyle" />
              <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CulturePropTypeName" HeaderText="Culture Properties" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CulturePropTypeID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("Isdeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CulturePropTypeID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litConcernID" Text='<%#Eval("CulturePropTypeID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
     </div>
       <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
