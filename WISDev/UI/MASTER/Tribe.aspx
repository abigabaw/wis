<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Tribe.aspx.cs" Inherits="WIS.Tribe" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Tribe Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Iranna
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
    <asp:Panel ID="pnlSave" Visible="true" runat="server">
            <fieldset class="icePnlinner">
                <legend>Tribe</legend>
        <table width="100%">
            <tr>
                <td>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="lblTribe" runat="server" Text="Tribe" CssClass="iceLable"></asp:Label><span
                                    class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTribe" runat="server" CssClass="iceTextBoxLarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Tribe"
                                    Display="None" ControlToValidate="txtTribe" ValidationGroup="Tribe"></asp:RequiredFieldValidator>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="ftetribe" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtTribe" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table align="center">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                                ValidationGroup="Tribe" />
                                            <asp:ValidationSummary ID="VsTribe" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Tribe" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
              </table>
              </fieldset>
              </asp:Panel>
                <!--Data Grid Start Point -->
                    <asp:GridView ID="gvTribe" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
                        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvTribe_RowCommand"
                        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="gvTribe_RowDataBound">
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
                            <asp:BoundField DataField="TRIBENAME" HeaderText="Tribe Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="Clans">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <a id="lnkClan" href="#" runat="server">View</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Clans" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Button ID="ViewPage" ImageAlign="AbsMiddle" Text="View" CommandName="ViewROW" CommandArgument='<%#Eval("TRIBEID")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                        CommandName="EditRow" CommandArgument='<%#Eval("TRIBEID")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted ").ToString())%>'
                                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                        CommandName="DeleteRow" CommandArgument='<%#Eval("TRIBEID") %>' OnClientClick="return DeleteRecord();"
                                        runat="server" />
                                    <asp:Literal ID="litTRIBEID" Text='<%#Eval("TRIBEID") %>' Visible="false" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <!-- Data Gid End point -->
                    <!--JavaScript Code -->
                    <script language="javascript" type="text/javascript">
                        function OpenClans(TRIBEID) {
                            var left = (screen.width - 800) / 2;
                            var top = (screen.height - 650) / 4;
                            open('Clans.aspx?id=' + TRIBEID, 'Clans', 'width=800px,height=650px,top=' + top + ', left=' + left);
                        }
                    </script>
                    <!--End of Javascript -->
          
      
    </div>
</div>
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
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
            var tat1 = document.getElementById("<%= txtTribe.ClientID  %>");
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
