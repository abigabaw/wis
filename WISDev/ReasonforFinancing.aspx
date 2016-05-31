<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReasonforFinancing.aspx.cs" 
Inherits="WIS.ReasonforFinancing" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
 <%--/**
 * 
 * @version		 Reason For Financing UI  screen   
 * @package		  Reason For Financing
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  15-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <table width="100%">
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td >
                                   <label class="iceLable" >Reason for Financing </label><span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="iceTextBoxLarge" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="Reason" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars = " ," TargetControlID="ReasonTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="reqReason" runat="server" ErrorMessage="Enter Reason for Financing "
                                        Display="None" ControlToValidate="ReasonTextBox" ValidationGroup="Reason"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="ReasonIDTextBox" runat="server" CssClass="iceTextBoxLarge"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="errMsgReasonLabel" runat="server" Text="" Visible="false" CssClass="iceLable" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center">
                                        <tr >
                                            <td>
                                                <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" OnClick="SaveButton_Click"
                                                    ValidationGroup="Reason" />
                                            </td>
                                            <asp:ValidationSummary ID="VsReason" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Reason" runat="server" />
                                            <td>
                                                <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                                            <%-- <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                        ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ValSummary"
                                        runat="server" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdReason" runat="server" CssClass="gridStyle" CellPadding="4"
                            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdReason_RowCommand"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
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
                           
                                <asp:BoundField DataField="FINANCEREASON" HeaderText="Reason For Financing" HeaderStyle-HorizontalAlign="Left" />
                             
                              
                               <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("FINANCEREASONID") %>' runat="server" />
                            </ItemTemplate>
                                </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" Width="7%" />
            <ItemTemplate>
            <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
             OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />            
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("FINANCEREASONID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litReasonID" Text='<%#Eval("FINANCEREASONID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
