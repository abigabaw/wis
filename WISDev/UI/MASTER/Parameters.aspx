﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parameters.aspx.cs" Inherits="WIS.UI.MASTER.Parameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div id="divAll">
        <div style="width: 100%">
            <asp:Panel ID="pnlParameterDetails" runat="server">
                <ajaxtoolkit:ToolkitScriptManager ID="tsManager" runat="server">
                </ajaxtoolkit:ToolkitScriptManager>
                <fieldset class="icePnlinner" style="margin-top: -06px;">
                    <legend>ADD New Parameter</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td>
                                <label class="iceLable">
                                    Available Options</label><span class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAvailableOptions" runat="server" CssClass="iceDropDown"
                                    Width="300px" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                    ErrorMessage=" Select District Name " ControlToValidate="ddlAvailableOptions"
                                    ValidationGroup="Parameter" Display="None">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label class="iceLable">
                                    Parameter</label><span class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtParameter" runat="server" MaxLength="100" Width="300px" CssClass="iceTextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Parameter"
                                    Display="None" ControlToValidate="txtParameter" ValidationGroup="Parameter"></asp:RequiredFieldValidator>
                                <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                    ValidChars=",- ()/" TargetControlID="txtParameter" runat="server">
                                </ajaxtoolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td align="center" colspan="2" style="padding-top: 12px">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                    ValidationGroup="Parameter" />
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Parameter" runat="server" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:GridView ID="grdParameter" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdParameter_RowCommand"
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
                    <asp:BoundField DataField="AvailableOptions" HeaderText="Available Options" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="PARAMETERNAME" HeaderText="Parameter" HeaderStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("PARAMETERID")%>' runat="server" />
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
                                CommandName="DeleteRow" CommandArgument='<%#Eval("PARAMETERID") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                            <asp:Literal ID="litPARAMETERID" Text='<%#Eval("PARAMETERID") %>' Visible="false"
                                runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="footer">
            <script language="javascript" type="text/javascript">

                var isDirty = 0;
                function setDirty() {
                    isDirty = 1;
                }

                function setDirtyText() {
                    var btn = document.getElementById("<%= btnSave.ClientID  %>");
                    var tat1 = document.getElementById("<%= txtParameter.ClientID  %>");
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
    </div>

</asp:Content>
