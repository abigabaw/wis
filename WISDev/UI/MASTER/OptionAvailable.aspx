<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OptionAvailable.aspx.cs" Inherits="WIS.UI.MASTER.OptionAvailable" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Option Creation</legend>
            <table border="0" align="center" width="45%">
                <tr>
                    <td width="25%">
                        <asp:Label ID="OptionAvailableLabel" runat="server" Text="Create Option" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="OptionAvailableTextBox" runat="server" CssClass="iceTextBoxLarge" MaxLength="100" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteOptionAvailable" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="OptionAvailableTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rqeOptionAvailableTextBox" ControlToValidate="OptionAvailableTextBox"
                            ErrorMessage="Enter OptionAvailable" Display="None" ValidationGroup="OptionAvailable" runat="server"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="OptionAvailableIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="OptionAvailable"
                            OnClick="SaveButton_Click" />
                        <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="OptionAvailable"
                            runat="server" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="msgSaveLabel" runat="server" Text="" CssClass="iceLable" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdOptionAvailable" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdOptionAvailable_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" >
            <RowStyle CssClass="gridRowStyle" />
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
                <asp:BoundField DataField="OptionAvailable" HeaderText="Options Available" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("ID") %>' runat="server" />
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("ID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litOptionAvailableID" Text='<%#Eval("ID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
    <script type="text/javascript">
        //        document.getElementById('divAll').onclick = function () {
        //            isDirty = 0;
        //            setTimeout(function () { isDirty = 1; }, 100);
        //        };

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
            var tat1 = document.getElementById("<%= OptionAvailableTextBox.ClientID  %>");
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
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"/>
</asp:Content>
