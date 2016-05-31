<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Non-perm_structure.aspx.cs" Inherits="WIS.Non_perm_structure" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="ValuationMenu.ascx" TagName="ValuationMenu" TagPrefix="uc1" %>
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
 * @version		 Non permanent Structure UI screen   
 * @package		   Non permanent Structure 
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Mahalakshmi
 * @Created Date  28-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
    <div style="width: 100%">
        <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
        <fieldset class="icePnlinner" style="margin-top: -10px;">
            <legend>Non Permanent Structure Details </legend>
            <div style="float: right">
               <%-- <a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>--%>
            </div>
            <script type="text/javascript" language="javascript">
                function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
                    open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu, 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px');
                }

                function CheckDecimal(e, src) {
                    if (e.keyCode == 46) { // Invoke when press Enter Key
                        var char = src.value;
                        if (char.indexOf(".") == -1) {
                            return true;
                        }
                        else if (char.indexOf(".") > -1) {
                        e.keyCode = 0;
                            return false;
                        }
                        return true;
                    }
                    return true;
                }
            </script>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="buildingtypeLabel" runat="server" Text="Building Type" CssClass="iceLable" />
                                    <span class="mandatory">* </span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="buildingtypeDropDownList" runat="server" CssClass="iceDropDown"
                                        Width="210px">
                                    </asp:DropDownList>
                                     <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                                         TargetControlID="buildingtypeDropDownList"
                                         PromptText="Type to search"
                                         PromptCssClass="ListSearchExtenderPrompt"
                                         PromptPosition="Top"
                                         IsSorted="true"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                        ErrorMessage="Select Building Type" ValidationGroup="NonPermanent" InitialValue="0"
                                        ControlToValidate="buildingtypeDropDownList">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="otherspecifyLabel" runat="server" Text=" Other specify" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="otherspecifyTextBox" runat="server" CssClass="iceTextBox" Width="205px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="categoryLabel" runat="server" Text="Category" CssClass="iceLable" />
                                    <span class="mandatory">* </span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="categoryDropDownList" runat="server" CssClass="iceDropDown"
                                        Width="210px">
                                    </asp:DropDownList>
                                     <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                                       TargetControlID="categoryDropDownList"
                                       PromptText="Type to search"
                                       PromptCssClass="ListSearchExtenderPrompt"
                                       PromptPosition="Top"
                                       IsSorted="true"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                        ErrorMessage="Select Category" ValidationGroup="NonPermanent" InitialValue="0"
                                        ControlToValidate="categoryDropDownList">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="conditionLabel" runat="server" Text="Condition" CssClass="iceLable" />
                                    <span class="mandatory">* </span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="conditionDropDownList" runat="server" CssClass="iceDropDown"
                                        Width="210px">
                                    </asp:DropDownList>
                                     <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                                      TargetControlID="conditionDropDownList"
                                      PromptText="Type to search"
                                      PromptCssClass="ListSearchExtenderPrompt"
                                      PromptPosition="Top"
                                      IsSorted="true"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                        ErrorMessage="Select Condition" ValidationGroup="NonPermanent" InitialValue="0"
                                        ControlToValidate="conditionDropDownList">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="ownerLabel" runat="server" Text="Owner" CssClass="iceLable" />
                                    <%-- <span class = "mandatory">*</span>--%>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="RadioButton1" GroupName="Radiobtn" runat="server" CssClass="Logincheckbox"
                                                    Style="margin-right: -10px;" />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="Label1" runat="server" Text="Self" CssClass="iceLable" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RadioButton2" GroupName="Radiobtn" runat="server" CssClass="Logincheckbox"
                                                    Style="margin-right: -10px;" />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="Label2" runat="server" Text="Other" CssClass="iceLable" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="otherTextBox" ClientIDMode="Static" MaxLength="60" runat="server"
                                                    Enabled="false" Width="100px" CssClass="iceTextBox">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="occupantLabel" runat="server" Text=" Occupant " CssClass="iceLable" />
                                    <%--  <span class = "mandatory">*</span>--%>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="RadioButton3" GroupName="Boundaryoccupant" runat="server" CssClass="Logincheckbox"
                                                    Style="margin-right: -10px;" />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="Label3" runat="server" Text="Self" CssClass="iceLable" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RadioButton4" GroupName="Boundaryoccupant" runat="server" CssClass="Logincheckbox"
                                                    Style="margin-right: -10px;" />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="Label4" runat="server" Text="Other" CssClass="iceLable" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="otherselfoccupantTextBox" CssClass="iceTextBox" MaxLength="60" ClientIDMode="Static"
                                                    Enabled="false" runat="server" Width="100px">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="occupantstatusLabel" runat="server" Text=" Occupant Status " CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="occupantstatusDropDownList" runat="server" CssClass="iceDropDown"
                                        Width="110px">
                                    </asp:DropDownList>
                                     <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                                      TargetControlID="occupantstatusDropDownList"
                                       PromptText="Type to search"
                                      PromptCssClass="ListSearchExtenderPrompt"
                                        PromptPosition="Top"
                                      IsSorted="true"/>
                                    <asp:TextBox ID="occupantstatusTextBox" CssClass="iceTextBox" ClientIDMode="Static"
                                        MaxLength="60" Enabled="false" runat="server" Width="95px">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="dimensionLabel" runat="server" Text=" Dimensions (meters) " CssClass="iceLable"
                                        Width="120px" />
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="lengthTextBox" runat="server" Width="50px" CssClass="iceTextBox"
                                                    MaxLength="10" onkeypress="return CheckDecimal(event, this)" />
                                                <label class="labelSuffix">
                                                    (Length)</label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6"  FilterType="Numbers,Custom" ValidChars="."
                                                     TargetControlID="lengthTextBox" runat="server">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="widthTextBox" runat="server" Width="50px" CssClass="iceTextBox"
                                                    MaxLength="10" onkeypress="return CheckDecimal(event, this)" />
                                                <label class="labelSuffix">
                                                    (Width)</label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="Numbers,Custom" ValidChars="."
                                                     TargetControlID="widthTextBox" runat="server">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="noofroomsLabel" runat="server" Text="Number of Rooms" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="noofroomsTextBox" runat="server" MaxLength="8" CssClass="iceTextBox"
                                        Width="205px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers"
                                        TargetControlID="noofroomsTextBox" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:Label ID="surfaceareaLabel" runat="server" Text="Surface Area" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="surfaceareaTextBox" ReadOnly="true" runat="server" CssClass="iceTextBox" MaxLength="50"
                                        Width="135px" />
                                    
                                    <label class="labelSuffix">
                                        (Sq meters)</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="UploadPhotoLabel" runat="server" Text="Upload Photo" CssClass="iceLable" Visible="false"/>
                                    <%-- <span class = "mandatory">*</span>--%>
                                </td>
                                <td colspan="3">
                                    <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="IDTextBox1" runat="server" Visible="false" CssClass="iceTextBox" />
                                            </td>
                                            <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="NonPermanent" runat="server" />
                                            <td>
                                                <a id="lnkNonPermStr" runat="server" href="#" runat="server" class="iceLinkButton"
                                                    style="text-decoration: none; color: White; font-family: Arial; font-size: 12px;
                                                    font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">
                                                    Change Request</a>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                                    ValidationGroup="NonPermanent" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                                <asp:Label ID="msgsaveLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="StatusNonPermStr" runat="server" Style="text-decoration: blink; color: Red;
                                                    font-family: Arial; font-size: 18px; font-weight: bold" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdNPS" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
            GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdUsers_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage"
            onrowdatabound="grdNPS_RowDataBound"
            >
            <rowstyle cssclass="gridRowStyle" />
            <alternatingrowstyle cssclass="gridAlternateRow" />
            <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" forecolor="White" />
            <headerstyle cssclass="gridHeaderStyle" />
            <columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="STR_TYPE" HeaderText="Building Type" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="STR_CATEGORYNAME" HeaderText="Category" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="STR_CONDITION" HeaderText="Condition" HeaderStyle-HorizontalAlign="Left" />
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
                            CommandName="EditRow" CommandArgument='<%#Eval("NonPermanentStructureID") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("NonPermanentStructureID") %>'
                            OnClientClick="return DeleteRecord();" runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("NonPermanentStructureID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
        </asp:GridView>
        <script language="javascript" type="text/javascript">
            function EnableDisableOtherOwner(enableFld) {
                if (enableFld) {
                    document.getElementById('otherTextBox').disabled = '';
                    document.getElementById('otherTextBox').focus();
                }
                else {
                    document.getElementById('otherTextBox').disabled = 'disabled';
                    document.getElementById('otherTextBox').value = '';
                }
            }

            function EnableDisableOtherOccupant(enableFld) {
                if (enableFld) {
                    document.getElementById('otherselfoccupantTextBox').disabled = '';
                    document.getElementById('otherselfoccupantTextBox').focus();
                }
                else {
                    document.getElementById('otherselfoccupantTextBox').disabled = 'disabled';
                    document.getElementById('otherselfoccupantTextBox').value = '';
                }
            }

            function EnableDisableOccupantStatus(src) {
                occupantStatus = src.options[src.selectedIndex].text;

                if (occupantStatus == 'Other') {
                    document.getElementById('occupantstatusTextBox').disabled = '';
                    document.getElementById('occupantstatusTextBox').focus();
                }
                else {
                    document.getElementById('occupantstatusTextBox').disabled = 'disabled';
                    document.getElementById('occupantstatusTextBox').value = '';
                }
            }

            function surfacearea() {
                var length;
                var width;
                var surfaceArea;
                length = document.getElementById('<%=lengthTextBox.ClientID %>').value;
                width = document.getElementById('<%=widthTextBox.ClientID %>').value;
                if (!isNaN(length) && !isNaN(width))
                    surfaceArea = length * width;
                else
                    surfaceArea = '';

                document.getElementById('<%=surfaceareaTextBox.ClientID %>').value = surfaceArea;
            }
            function doCheck() {
                var keyCode = (event.which) ? event.which : event.keyCode;
                if ((keyCode == 8) || (keyCode == 46))
                    event.returnValue = false;
            }

            function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
            }

            function OpenUploadPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, PagePBID) {
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../UploadPhotoDocument.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&PagePBID=' + PagePBID, 'Uploadphoto', 'resizable=1,scrollbars=1,width=600px,height=500px,top=' + top + ', left=' + left);
            }    

        </script>
    </div>
</asp:Content>
