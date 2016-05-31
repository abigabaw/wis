<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OptionParameterMapping.aspx.cs" Inherits="WIS.UI.MASTER.OptionParameterMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
        <asp:Panel ID="pnlOptionParameterDetails" runat="server">
            <fieldset class="icePnlinner">
                <legend>Option Parameter Mapping Details</legend>
                <table align="center" border="0" width="90%">
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                Option Group</label><span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOptionGroup" CssClass="iceTextBox" AppendDataBoundItems="true"
                                Width="250px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                ErrorMessage=" Select Option Group" ControlToValidate="ddlOptionGroup" ValidationGroup="OptionMapping"
                                Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <label class="iceLable">
                                Option Available</label><span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOptionAvailable" CssClass="iceTextBox" AutoPostBack="true"
                                Width="250px" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlOptionAvailable_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                ErrorMessage=" Select Option Available " ControlToValidate="ddlOptionAvailable"
                                ValidationGroup="OptionMapping" Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                Parameter</label><span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlParameter" CssClass="iceTextBox" Width="250px" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlParameter_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                ErrorMessage="Select Parameter" ControlToValidate="ddlParameter" ValidationGroup="OptionMapping"
                                Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <label class="iceLable">
                                Description</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDescription" CssClass="iceTextBox" Width="250px" runat="server"
                                >
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Select Description" InitialValue="0"
                                Display="None" ControlToValidate="ddlDescription" ValidationGroup="OptionMapping"></asp:RequiredFieldValidator>
                           <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                ValidChars=",- " TargetControlID="txtParish" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" 
                                ValidationGroup="OptionMapping" onclick="btnSave_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="OptionMapping" runat="server" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" 
                                onclick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdOptionMapping" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true" 
            PageSize="10" OnPageIndexChanging="ChangePage" 
            onrowcommand="grdOptionMapping_RowCommand">
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
                <asp:BoundField DataField="OptionGroup" HeaderText="OptionGroup" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="OptionAvailable" HeaderText="OptionAvailable" HeaderStyle-HorizontalAlign="Left" />
                <%--<asp:BoundField DataField="subcountyname" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Left" />--%>
                <asp:BoundField DataField="ParameterName" HeaderText="ParameterName" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("OptParID")%>' runat="server" />
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("OptParID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litPARISHID" Text='<%#Eval("OptParID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
