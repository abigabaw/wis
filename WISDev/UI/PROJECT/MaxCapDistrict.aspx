<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MaxCapDistrict.aspx.cs" Inherits="WIS.MaxCapDistrict" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <div id="divAll">
            <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
            </ajaxToolkit:ToolkitScriptManager>
            <fieldset class="icePnlinner">
                <legend>ADD New MaxCap</legend>
                <table align="center" border="0" width="100%">
                    <tr>
                        <td>
                            <label class="iceLable">
                                District Name</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrictName" runat="server" CssClass="iceDropDown" Width="300px"
                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictName_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                ErrorMessage=" Select District Name " ControlToValidate="ddlDistrictName" ValidationGroup="MaxCap"
                                Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label class="iceLable">
                                MaxCap</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMaxCap" runat="server" MaxLength="19" Width="300px" CssClass="iceTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter MaxCap Value"
                                Display="None" ControlToValidate="txtMaxCap" ValidationGroup="MaxCap"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                                ValidChars="" TargetControlID="txtMaxCap" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                ValidationGroup="MaxCap" />
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="MaxCap" runat="server" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        <asp:GridView ID="grdDistrict" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdDistrict_RowCommand"
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
                <asp:BoundField DataField="MaxCapVal" HeaderText="Max Cap Val" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("MaxCapID")%>' runat="server" />
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("MaxCapID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litDistrictID" Text='<%#Eval("MaxCapID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="footer">
        <script language="javascript" type="text/javascript">
            
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
                var tat1 = document.getElementById("<%= txtMaxCap.ClientID  %>");
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
