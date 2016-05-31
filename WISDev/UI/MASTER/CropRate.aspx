<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CropRate.aspx.cs" Inherits="WIS.CropRate"
    MasterPageFile="~/SitePopup.Master" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>

    <%--/**
 * 
 * @version		 0.1 Croprate Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Irran
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"/>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Crop Rate Details </legend>
            <table border="0" align="center" width="80%">
                <tr>
                    <td align="left" class="iceLable">
                        <label class="iceLable">
                            District</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:DropDownList ID="ddlDistrict" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                           <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlDistrict"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        <asp:RequiredFieldValidator ID="reqDistrict" runat="server" ErrorMessage="Select a District"
                            ControlToValidate="ddlDistrict" InitialValue="0" Display="None" ValidationGroup="CropRate"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblCropDescription" runat="server" Text="Crop Description" CssClass="iceLable">
                        </asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCropDescription" runat="server" CssClass="iceTextBox" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlCropDescription"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="--Select--"
                            ErrorMessage="Select Crop Description " ControlToValidate="ddlCropDescription"
                            ValidationGroup="CropRate" Display="None">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 40%" colspan="2">
                        <asp:Label ID="lblCropRate" runat="server" Text="Crop Rate" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCropRate" runat="server" Width="150" MaxLength="10" CssClass="iceTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCropRate" runat="server" ErrorMessage="Enter Crop Rate"
                            ControlToValidate="txtCropRate" Display="None" ValidationGroup="CropRate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtCropRateID" runat="server" Visible="false" CssClass="iceTextBox"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCropRate" FilterType="Numbers" TargetControlID="txtCropRate" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <div style="margin-top: 12px">
                            <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="CropRate"
                                OnClick="btnSave_Click1" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                            &nbsp;&nbsp;
                            <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                            <asp:ValidationSummary ID="valsumCropRate" runat="server" ShowSummary="false" ShowMessageBox="true"
                                HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="CropRate" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdCropRate" runat="server" AllowPaging="True" OnPageIndexChanging="ChangePage"
            AllowSorting="True" AutoGenerateColumns="False" Width="100%" CellPadding="4"
            CellSpacing="1" GridLines="None" OnRowCommand="grdCropRate_RowCommand">
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
                <asp:BoundField DataField="DistrictName" HeaderText="District" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="35%" />
                <asp:BoundField DataField="CropDescription" HeaderText="Crop Description" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CROPRATE" HeaderText="Crop Rate" HeaderStyle-HorizontalAlign="Center" />
                <%--  <asp:TemplateField HeaderText="Rate">
             <ItemStyle  HorizontalAlign="Center" Width="5%"/>
               <ItemTemplate>                            
                   <asp:HyperLink ID="hypLink" ImageAlign="AbsMiddle" runat="server" Target="_blank" Text="View" NavigateUrl="~/Master/CropRate.aspx"
                            CommandName="ViewRow" CommandArgument='<%#Eval("CROPRATEID") %>'>
                            </asp:HyperLink>                                                                      
               </ItemTemplate>                 
             </asp:TemplateField> --%>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CROPRATEID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" OnCheckedChanged="IsObsolete_CheckedChanged"
                            Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CROPRATEID") %>' OnClientClick="return DeleteRecord();" />
                        <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("CROPRATEID") %>' Visible="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript" language="javascript">
            function DeleteRecord() {
                return confirm('Are you sure want to delete this record');
            }
            function ObsoleteRecord() {
                return confirm('Are you sure want to update this record');
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
                var tat1 = document.getElementById("<%= txtCropRate.ClientID  %>");
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
