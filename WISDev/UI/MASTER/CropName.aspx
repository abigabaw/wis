<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CropName.aspx.cs" Inherits="WIS.CropName"
    MasterPageFile="~/Site.Master" %>

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
 * @version		 0.1 CropName Status Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Irran
 * @Created Date 25-April-203
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
        <asp:Panel ID="pnlSave" runat="server" Visible="true">
            <fieldset class="icePnlinner">
                <legend>Crop Name</legend>
                <table align="center" border="0" width="55%">
                    <tr>
                        <asp:ValidationSummary ID="valsumCropName" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="CropName" />
                        <td align="left" width="25%">
                            <asp:Label ID="lblCropName" runat="server" Text="Crop Name" CssClass="iceLable"></asp:Label>
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCropName" runat="server" Width="300" MaxLength="200" CssClass="iceTextBox"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqCropName" runat="server" ErrorMessage=" Enter Crop Name"
                                ControlToValidate="txtCropName" Display="None" ValidationGroup="CropName"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteCropDiameter" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" " TargetControlID="txtCropName" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtCropNameID" runat="server" Visible="false" CssClass="iceTextBox"></asp:TextBox>
                        </td>
                    </tr>
                  <tr>
                  <td align="left">
                      <label class="iceLable">Unit of Measure </label><span class="mandatory">*</span>
                      </td>
                      <td>
                      <asp:DropDownList ID="ddlUnitOfmeasure" runat="server" CssClass="iceDropDown" 
                              AppendDataBoundItems="True" > 
                          <asp:ListItem Selected="True">--Select--</asp:ListItem>
                       </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlUnitOfmeasure"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="--Select--"
                            ErrorMessage=" Select Unit of Measure " ControlToValidate="ddlUnitOfmeasure" ValidationGroup="CropName"
                            Display="None">
                        </asp:RequiredFieldValidator>

                      </td>
                  </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="CropName"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="gvCropName" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Width="100%" CellPadding="4" CellSpacing="1" PageSize="10"
            GridLines="None" OnPageIndexChanging="gvCropName_PageIndexChanging" OnRowCommand="gvCropName_RowCommand"
            OnRowDataBound="gvCropName_RowDataBound" 
            onselectedindexchanged="gvCropName_SelectedIndexChanged">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="SI No">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CROPNAME" HeaderText="Crop Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="UNITNAME" HeaderText="Unit of Measure" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Rate">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <a id="lnkCropRate" href="#" runat="server">View</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CROPID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                  CommandName="DeleteRow"  CommandArgument='<%#Eval("CROPID") %>' OnClientClick="return DeleteRecord();" />--%>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeletedBy").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CROPID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litCROPID" Text='<%#Eval("CROPID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <script type="text/javascript" language="javascript">
            function OpenCropRate(cropID) {
                var left = (screen.width - 800) / 2;
                var top = (screen.height - 650) / 4;
                open('CropRate.aspx?id=' + cropID, 'cropRate', 'width=800px,height=650px,top=' + top + ', left=' + left);
            }

            function DeleteRecord() {
                return confirm('Are you want to delete?');
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
                var tat1 = document.getElementById("<%= txtCropName.ClientID  %>");
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
