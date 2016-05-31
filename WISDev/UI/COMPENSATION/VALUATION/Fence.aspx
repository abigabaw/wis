﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Fence.aspx.cs" Inherits="WIS.Fence" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register src="ValuationMenu.ascx" tagname="ValuationMenu" tagprefix="uc1" %>
<%@ Register src="~/UI/COMPENSATION/HouseholdSummary.ascx" tagname="HouseholdSummary" tagprefix="uc2" %>
<%@ Register src="OtherFixturesMenu.ascx" tagname="OtherFixturesMenu" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%--/**
 * 
 * @version		 Fence UI screen   
 * @package		  Fence
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Mahalakshmi
 * @Created Date  30-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
    <div style="width: 100%">
        <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
        <uc3:OtherFixturesMenu ID="OtherFixturesMenu1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div><br />
        <fieldset class="icePnlinner">
            <legend>Fence Details </legend>
            <div style="float: right">
               <%-- <a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>--%>
            </div>
            <script type="text/javascript" language="javascript">
                function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
                    open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu, 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px');
                }
            </script>
            <table align="center" border="0" cellpadding="0" cellspacing="2" id="table1">
                <tr>
                    <td>
                        <asp:Label ID="fenceLabel" runat="server" Text="Fence Description" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="fenceDropDownList" runat="server" CssClass="iceDropDown">
                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="fenceDropDownList"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
            ErrorMessage="Select Fence Description" ValidationGroup="Fence"  InitialValue="--Select--" ControlToValidate="fenceDropDownList"></asp:RequiredFieldValidator>
              
                    </td>
                    <td>
                        <asp:Label ID="fencedimensionLabel" runat="server" Text="Fence Dimensions (meters)"
                            CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:Label ID="lengthLabel" runat="server" Text="Length" CssClass="iceLable" /><span class="mandatory">*</span>
                        <asp:TextBox ID="lengthTextBox" runat="server" CssClass="iceTextBox" MaxLength="10" Width="60px" onkeypress="return CheckDecimal(event, this)" />
       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="Numbers,Custom" ValidChars="." TargetControlID="lengthTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
            ErrorMessage="Enter Length" ValidationGroup="Fence" ControlToValidate="lengthTextBox"></asp:RequiredFieldValidator>
              


                        <asp:Label ID="heightLabel" runat="server" Text="Height" CssClass="iceLable"/><span class="mandatory">*</span>
                        <asp:TextBox ID="heightTextBox" runat="server" CssClass="iceTextBox" MaxLength="10" Width="60px" onkeypress="return CheckDecimal(event, this)" />
       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"  FilterType="Numbers,Custom" ValidChars="." TargetControlID="heightTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
            ErrorMessage="Enter Height" ValidationGroup="Fence" ControlToValidate="heightTextBox"></asp:RequiredFieldValidator>
              

                    </td>
                </tr>
                &nbsp;
                <tr>
                    <td>
                        <asp:Label ID="surfaceareaLabel" runat="server" Text="Surface Area" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:TextBox ID="surfaceareaTextBox" ReadOnly="true" runat="server" CssClass="iceTextBox" />
      
                        (SQM)
                    </td>
                    <td>
                        <asp:Label ID="depreciatedvalueLabel" runat="server" Text="Depreciated Value" CssClass="iceLable"/>
                        <span class = "mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="depreciatedvalueTextBox" runat="server" MaxLength="20" onblur="CheckAmount(this);" onkeypress="return CheckDecimal(event, this)" CssClass="iceTextBox" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"  FilterType="Numbers,Custom" ValidChars=",." TargetControlID="depreciatedvalueTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
            ErrorMessage="Select Depreciated Value" ValidationGroup="Fence" ControlToValidate="depreciatedvalueTextBox"></asp:RequiredFieldValidator>
             
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="UploadPhotoLabel" runat="server" Text="Upload Photo" CssClass="iceLable" Visible="false"/>
                       
                    </td>
                    <td colspan="3">
                        <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                          <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Fence" runat="server" />
                            <%--   <asp:TextBox ID="IDTextBox1" runat="server" Visible = "false" CssClass = "iceTextBox"/>--%>
                           <%-- <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Fence" runat="server" />--%>
     <%--    <a id="lnkFence" runat="server" href="#" class="iceLinkButton"
                                        style="text-decoration: none; color: White; font-family: Arial; font-size: 12px;
                                        font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">
                                        Change Request</a>&nbsp;--%>
                                        <asp:Button ID="lnkFence" runat="server" Text="Change Request" CssClass="icebutton" Width="120px" />
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="Fence" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            <asp:Label ID="msgsaveLabel" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                   <td colspan="4" align = "center">
                       <asp:Label ID="StatusFence" runat="server" Style="text-decoration: blink;
                                color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                   </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <asp:GridView ID="grdFence" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
            GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdUsers_RowCommand"
            OnRowDataBound="grdFence_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdFence_PageIndexChanging">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White"  />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Fencedescription" HeaderText="Fence Description" HeaderStyle-HorizontalAlign="Left" />
                <%--                <asp:BoundField DataField="Dimensions(sq meters)" HeaderText="Dimensions(sq meters)" HeaderStyle-HorizontalAlign="Left" />--%>
                <asp:TemplateField HeaderText="Dimensions (SQM)" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                    <ItemTemplate>
                        <asp:Literal ID="litDimensions" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>

                     <asp:TemplateField HeaderText="Depreciated Value" HeaderStyle-HorizontalAlign="Center">
                  <ItemStyle HorizontalAlign="Right"/>
                     <ItemTemplate>
                      <asp:Literal ID="litDepreciatedValue" runat="server"></asp:Literal>
                      </ItemTemplate>
                  </asp:TemplateField>
               <%-- <asp:BoundField DataField="Depreciatedvalue" HeaderText="Depreciated Value" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />--%>
                     <asp:TemplateField HeaderText="Upload Photo" HeaderStyle-HorizontalAlign="Center">
                   <ItemStyle HorizontalAlign="Center" Width="12%" />
                      <ItemTemplate>
                               <a id="lnkUPloadPhoto" href="#" runat="server"><b>Upload Photo</b></a>
                       </ItemTemplate>
                 </asp:TemplateField>  
                 <asp:TemplateField HeaderText="View Photo" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                        <ItemTemplate>
                            <a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>
                    </ItemTemplate>
                 </asp:TemplateField>  

                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("PAP_FENCEID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PAP_FENCEID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("PAP_FENCEID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <script language="javascript" type="text/javascript">
             function surfacearea() {
                 var length;
                 var height;
                 var surfaceArea;
                 length = document.getElementById('<%=lengthTextBox.ClientID %>').value;
                 height = document.getElementById('<%=heightTextBox.ClientID %>').value;
                 if (!isNaN(length) && !isNaN(height))
                     surfaceArea = length * height;
                 else
                     surfaceArea = '';

                 document.getElementById('<%=surfaceareaTextBox.ClientID %>').value = surfaceArea;
             }

             spnpnldiv = document.getElementById('table1');
             if (spnpnldiv != null) {
                 scrWidth = screen.availWidth;
                 spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
             }

             function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
                 var left = (screen.width - 600) / 2;
                 var top = (screen.height - 500) / 4;
                 open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
             }

             function doCheck() {
                 var keyCode = (event.which) ? event.which : event.keyCode;
                 if ((keyCode == 8) || (keyCode == 46))
                     event.returnValue = false;
             }

             function CheckDecimal(e, src) {
                 if (e.keyCode == 46) { // Invoke when press Enter Key
                     var char = src.value;
                     if (char.indexOf(".") == -1) {
                         return true;
                     }
                     else if (char.indexOf(".") > -1) {
                         return false;
                     }
                     return true;
                 }
                 return true;
             }

             function OpenUploadPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, PagePBID) {
                 var left = (screen.width - 600) / 2;
                 var top = (screen.height - 500) / 4;
                 open('../../UploadPhotoDocument.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&PagePBID=' + PagePBID, 'Uploadphoto', 'resizable=1,scrollbars=1,width=600px,height=500px,top=' + top + ', left=' + left);
             }

             function CheckAmount(src) {
                 var amount;
                 var val = RemoveComma(src.value);

                 if (!isNaN(val))
                     amount = val;
                 else
                     amount = '';
                 src.value = AddComma(amount);
             }

             function AddComma(iValue) {
                 return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
             }

             function RemoveComma(iValue) {
                 return iValue.toString().replace(/,?/g, "");
             }
               
        </script>
    </div>
</asp:Content>
