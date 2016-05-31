<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="WelfareIndicator.aspx.cs" Inherits="WIS.WelfareIndicator" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

   <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>
</asp:Content>
<%--/**
 * 
 * @version		 0.1 WelfareIndicators Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Iranna
 * @Created Date 20-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>.
    <div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Welfare Indicator</legend>
            <table border="0" align="center" width="70%">
                <tr>
                    <td width="30%" align="left">
                        <asp:Label ID="WelfareIndicatorLabel" runat="server" Text="Welfare Indicator Name"
                            CssClass="iceLable" />
                        <span class="mandatory">*</span>&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtWelfareIndicator" runat="server" CssClass="iceTextBox" Width="250px" MaxLength="250"/>
                        <asp:RequiredFieldValidator ID="reqWelfareIndicator" runat="server" ErrorMessage="Enter Welfare Indicator"
                            ControlToValidate="txtWelfareIndicator" Display="None" ValidationGroup="WelfareIndicator"></asp:RequiredFieldValidator>
                          <ajaxToolkit:FilteredTextBoxExtender ID="fteWelfareIndicator" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtWelfareIndicator" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td width="30%" align="left">
                        <asp:Label ID="FieldLabel" runat="server" Text="Field Type" CssClass="iceLable" />
                        <%-- <span class="mandatory">*</span>--%>&nbsp;&nbsp;
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:RadioButtonList ID="rbtnfield" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">CHECKBOX</asp:ListItem>
                            <asp:ListItem Value="1">TEXTBOX</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                            <label class="iceLable">Associated With</label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlAssociatedWith" CssClass="iceTextBox" runat="server">
                                <asp:ListItem Value="0">--None--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlAssociatedWith"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </td>
                    </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="WelfareIndicator"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                        <asp:ValidationSummary ID="valsumWelfareIndicator" runat="server" ShowSummary="false"
                            ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                            ValidationGroup="WelfareIndicator" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdWelfareIndicator" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdWelfareIndicator_RowCommand"
            AllowPaging="True" OnPageIndexChanging="ChangePage">
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
                <asp:BoundField DataField="Wlf_indicatorname" HeaderText="Welfare Indicator Name"
                    HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Fieldtype" HeaderText="Field Type" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("Wlf_indicatorID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" OnCheckedChanged="IsObsolete_CheckedChanged"
                            Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("Wlf_indicatorID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("Wlf_indicatorID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to delete this record?');
            }

            function ObsoleteRecord() {
                return confirm('Are you sure you want to update this record?');
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
                var tat1 = document.getElementById("<%= txtWelfareIndicator.ClientID  %>");
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
