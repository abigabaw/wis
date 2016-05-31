<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Description.aspx.cs" Inherits="WIS.UI.MASTER.Description" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
        <asp:Panel ID="pnlSubCountyDetails" runat="server">
            <fieldset class="icePnlinner">
                <legend>Description Details</legend>
                <table align="center" border="0" width="90%">
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                Option Available</label><span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddloptionAvailable" CssClass="iceTextBox" AutoPostBack="true" AppendDataBoundItems="true"
                                Width="250px" runat="server" OnSelectedIndexChanged="ddloptionAvailable_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <label class="iceLable">
                                Parameter Name</label><span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlparam" CssClass="iceTextBox" AppendDataBoundItems="true"
                               Width="250px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                Description</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Width="250px" CssClass="iceTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Description"
                                Display="None" ControlToValidate="txtDescription" ValidationGroup="Sub County"></asp:RequiredFieldValidator>
                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                ValidChars=",- ()/.<>" TargetControlID="txtDescription" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                ValidationGroup="Sub County" />
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Sub County" runat="server" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdDescription" runat="server" CssClass="gridStyle" 
        CellPadding="4" CellSpacing="1" 
        GridLines="None" AutoGenerateColumns="false" Width="100%" 
        AllowPaging="true" PageSize="10" onrowcommand="grdDescription_RowCommand" onpageindexchanging="grdDescription_PageIndexChanging1" 
            >
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
             <asp:BoundField DataField="OptionAvailablename" HeaderText="Option Available" HeaderStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="ParameterName" HeaderText="Parameter Name" HeaderStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
          
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("DescriptionID")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                       OnCheckedChanged="IsObsolete_CheckedChanged"  AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("DescriptionID")%>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litSUBCOUNTYID" Text='<%#Eval("DescriptionID")%>' Visible="false" runat="server"></asp:Literal>
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
                var tat1 = document.getElementById("<%= txtDescription.ClientID  %>");
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
