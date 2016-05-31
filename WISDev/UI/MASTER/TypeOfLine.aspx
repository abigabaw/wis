<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TypeOfLine.aspx.cs" Inherits="WIS.TypeOfLine" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 TypeofLine Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Iranna
 * @Created Date 18-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Type of Line Details</legend>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" align="center">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblLineType" runat="server" Text="Type Of Line" CssClass="iceLable"></asp:Label> <span class="mandatory">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLineType" runat="server" CssClass="iceTextBox" MaxLength="5" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqeLineType" ControlToValidate="txtLineType"
                                        Display="None" ErrorMessage="Enter Type of Line" ValidationGroup="ValSummary" runat="server"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteLineType" FilterType="Numbers" TargetControlID="txtLineType" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:TextBox ID="ConcernIDTextBox" runat="server" CssClass="iceTextBox" Visible="false"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="lblKV" runat="server" Text="kv"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblRightOfWay" runat="server" Text="Right Of Way" CssClass="iceLable"></asp:Label> <span class="mandatory">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRightOfWay" runat="server" CssClass="iceTextBox" MaxLength="5" Width="100px"></asp:TextBox>
                                    <asp:Label ID="lblmeters" runat="server" Text="meters"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rqeRightOfWay" ControlToValidate="txtRightOfWay"
                                        Display="None" ErrorMessage="Enter Right of Way" ValidationGroup="ValSummary" runat="server"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers" TargetControlID="txtRightOfWay" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblWayleave" runat="server" Text="Wayleave" CssClass="iceLable"></asp:Label> <span class="mandatory">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtWayleave" runat="server" CssClass="iceTextBox" MaxLength="5" Width="100px"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server" Text="meters"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rqeWayleave" ControlToValidate="txtWayleave"
                                        Display="None" ErrorMessage="Enter Wayleave" ValidationGroup="ValSummary" runat="server"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers" TargetControlID="txtWayleave" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="padding-top: 6px">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="ValSummary" OnClick="btnSave_Click" />
                        <asp:ValidationSummary ID="vsPAPStatus" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ValSummary"
                            runat="server" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" 
                            onclick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="dv_Details" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" CellSpacing="1" GridLines="None"
            Height="50px" PageSize="10" Width="100%"
            OnRowCommand="dv_Details_RowCommand" 
            onpageindexchanging="dv_Details_PageIndexChanging1">
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TypeOfLine" HeaderText="Type Of Line (kV)" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="15%">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Right Of Way (meters)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="15%" DataField="Rightofwaymeasurement" />
                <asp:BoundField HeaderText="Wayleave (meters)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="15%" DataField="Wayleavemeasurement" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                            CommandArgument='<%#Eval("LineTypeId") %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />            
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Image/delete.gif" runat="server" ID="ImgDelete" CommandName="DeleteRow"
                            CommandArgument='<%#Eval("LineTypeId") %>' OnClientClick="return DeleteRecord();" />
                        <asp:Literal ID="litLineTypeID" Text='<%#Eval("LineTypeId") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="gridHeaderStyle" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
        </asp:GridView>
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
                var btn = document.getElementById("<%= btnSave.ClientID  %>");
                var tat1 = document.getElementById("<%= txtLineType.ClientID  %>");
                var tat2 = document.getElementById("<%= txtRightOfWay.ClientID  %>");
                var tat3 = document.getElementById("<%= txtWayleave.ClientID  %>");
                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
       else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') { 
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
