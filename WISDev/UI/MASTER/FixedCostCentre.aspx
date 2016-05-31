<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FixedCostCentre.aspx.cs" Inherits="WIS.FixedCostCentre" %>
    
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
    <asp:Panel ID="pnlFixedCostCentreDetails" runat="server">
        <fieldset class="icePnlinner">
            <legend>Fixed Cost Centre Details</legend>
            <table align="center" border="0" width="90%">
                <tr>
                    <td>
                        <label class="iceLable">
                            Fixed Cost Centre</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFixedCostCentre" runat="server" class="iceTextBox" MaxLength="10" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqFixedCostCentre" ControlToValidate="txtFixedCostCentre" ErrorMessage="Enter Fixed Cost Centre"
                            Display="None" ValidationGroup="FixedCostCentreGroup" runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="LowercaseLetters,UppercaseLetters,Numbers"
                            TargetControlID="txtFixedCostCentre" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Description</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" class="iceTextBox"
                            MaxLength="200" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton" OnClick="btn_Save_Click"
                                        ValidationGroup="FixedCostCentreGroup" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Clear" runat="server" Text="Clear" class="icebutton" OnClick="btn_Clear_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
    <asp:ValidationSummary ID="valSummaryfixedCostCentreList" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="FixedCostCentreGroup" runat="server" />
    <asp:GridView ID="grdFixedCostCentre" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdFixedCostCentre_RowCommand"
        AllowPaging="True" OnPageIndexChanging="grdFixedCostCentre_PageIndexChanging">
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
            <asp:BoundField DataField="FixedCostCentre" HeaderText="Fixed Cost Centre" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="FixedCostCentreDescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("FixedCostCentreId") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("FixedCostCentreId") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litFixedCostCentreID" Text='<%#Eval("FixedCostCentreId") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="footer">
    </div>
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
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
                var btn = document.getElementById("<%= btn_Save.ClientID  %>");
                var tat1 = document.getElementById("<%= txtFixedCostCentre.ClientID  %>");
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
