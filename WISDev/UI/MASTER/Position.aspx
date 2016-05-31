<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Position.aspx.cs" Inherits="WIS.UI.MASTER.Position" %>
    <%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 position Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Victory
 * @Created Date 21-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
      <div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Position</legend>
            <table align="center" border="0" width="45%">
                <tr>
                    <asp:ValidationSummary ID="valsumPosition" runat="server" ShowSummary="false" ShowMessageBox="true"
                        HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="Position" />
                    <td align="left" width="25%">
                        <asp:Label ID="lblPosition" runat="server" Text="Position" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPosition" runat="server" CssClass="iceTextBox" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqPosition" runat="server" ErrorMessage="Enter Position Name"
                            ControlToValidate="txtPosition" Display="None" ValidationGroup="Position"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftePosition" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtPosition" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:TextBox ID="txtPositionID" runat="server" Visible="false" CssClass="iceTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" 
                                        ValidationGroup="Position" onclick="btnSave_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" 
                                        onclick="btnClear_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
         <asp:GridView ID="grdPosition" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1" GridLines="None"
            AutoGenerateColumns="false" Width="100%" onrowcommand="grdPosition_RowCommand" 
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage"> 
            <RowStyle CssClass="gridRowStyle" />
             <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White"/>
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PositionName" HeaderText="Position Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif" CommandName="EditRow" CommandArgument='<%#Eval("PositionID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" Width="7%" />
            <ItemTemplate>
             <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("PositionIsDeleted").ToString())%>'
             OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />            
            </ItemTemplate>
            </asp:TemplateField>
             
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("PositionID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litPositionID" Text='<%#Eval("PositionID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete the Record?');
            }
    </script>       
    </div>
      </div>
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
                    var tat1 = document.getElementById("<%= txtPosition.ClientID  %>");
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
</asp:Content>
