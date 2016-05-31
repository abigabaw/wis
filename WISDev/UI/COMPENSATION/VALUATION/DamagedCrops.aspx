<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="DamagedCrops.aspx.cs" Inherits="WIS.DamagedCrops" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="ValuationMenu.ascx" TagName="ValuationMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
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
 * @version		  DamagedCrops UI screen   
 * @package		  DamagedCrops
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  30-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server" EnablePageMethods="true">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div><br />
    <div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Damaged Crop Details</legend>
            <div style="float: right">
                <%--<a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>--%>
            </div>
            <script type="text/javascript" language="javascript">
                function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
                    open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu, 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px');
                }
            </script>
            <table border="0" id="table1">
                <tr>
                    <td>
                        <asp:TextBox ID="DAMAGED_CROPIDTextBox" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 22%">
                        <asp:Label ID="lblDamagedCropFormRefNo" runat="server" Text="Damaged Crop Form Ref. No."
                            CssClass="iceLable" /> <span class="mandatory">*</span> 
                    </td>
                    <td align="left" style="width: 28%">
                        <asp:TextBox ID="txtbxDamagedCropFormRefNo" MaxLength="100" runat="server" CssClass="iceTextBox"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers"  TargetControlID="txtbxDamagedCropFormRefNo" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ErrorMessage=" Enter Damaged Crop Form Ref.No. " ControlToValidate="txtbxDamagedCropFormRefNo"
                            ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="width: 15%">
                        <asp:Label ID="lblCropName" runat="server" Text="Crop Name" CssClass="iceLable" /> <span class="mandatory">*</span> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCropName" runat="server" CssClass="iceTextBox"
                            AppendDataBoundItems="True">
                             <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlCropName"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="--Select--"
                            ErrorMessage=" Select Crop Name " ControlToValidate="ddlCropName"
                            ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCropType" runat="server" Text="Crop Type" CssClass="iceLable"></asp:Label> <span class="mandatory">*</span> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCropType" runat="server" CssClass="iceTextBox" 
                            AppendDataBoundItems="True">
                          <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlCropType"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="--Select--"
                            ErrorMessage=" Select Crop Type " ControlToValidate="ddlCropType"
                            ValidationGroup="DamageCrop" Display="None">
                         </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblCropDescription" runat="server" Text="Crop Description" CssClass="iceLable"></asp:Label> <span class="mandatory">*</span> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCropDescription" runat="server" CssClass="iceTextBox" 
                            AppendDataBoundItems="True">
                         <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlCropDescription"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="--Select--"
                            ErrorMessage=" Select Crop Description " ControlToValidate="ddlCropDescription"
                            ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDateDamaged" runat="server" Text="Date Damaged " CssClass="iceLable"></asp:Label> <span class="mandatory">*</span> 
                    </td>
                    <td>
                    <asp:TextBox ID="DateDamaged" runat="server" CssClass="iceTextBox" Width="90px"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="calDateDamaged" CssClass="WISCalendarStyle" runat="server" TargetControlID="DateDamaged">
                     </ajaxToolkit:CalendarExtender>                      

                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="DateDamaged"
                        ClientValidationFunction="CheckDOB" ErrorMessage="Date Damaged should not be greater than Today's Date"
                        ValidationGroup="DamageCrop" Display="None">
                    </asp:CustomValidator>
                     <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="DateDamaged"
                                ClientValidationFunction="CheckConstrSatrtDate" ErrorMessage="Date Damaged  cannot be before Project Start Date."
                                ValidationGroup="DamageCrop" Display="None"></asp:CustomValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                            ErrorMessage="Select Date Damaged" ControlToValidate="DateDamaged"
                            ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hfProjStartDate" runat="server" Value="0" />
                    <asp:HiddenField ID="hfProjEndDate" runat="server" Value="0" />
                    </td>
                    <td>
                        <asp:Label ID="lblDamagedBy" runat="server" Text="Damaged By " CssClass="iceLable"></asp:Label> <span class="mandatory">*</span> 
                    </td>
                    <td>
                        <div style="float:left">
                            <asp:DropDownList ID="ddlDamagedBy" runat="server" CssClass="iceTextBox" 
                                AppendDataBoundItems="True" AutoPostBack="true"
                                onselectedindexchanged="ddlDamagedBy_SelectedIndexChanged">
                                 <asp:ListItem Selected="True">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                            TargetControlID="ddlDamagedBy"
                            PromptText="Type to search"
                            PromptCssClass="ListSearchExtenderPrompt"
                            PromptPosition="Top"
                            IsSorted="true"/>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="--Select--"
                                ErrorMessage=" Select Damaged By" ControlToValidate="ddlDamagedBy"
                                ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                        </div>
                        <div style="float:left">
                            <asp:UpdatePanel ID="updDamagedBy" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="DamagedByTextBox" MaxLength="50" runat="server" CssClass="iceTextBox" Enabled="false"></asp:TextBox>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" ," TargetControlID="DamagedByTextBox"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>        
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDamagedBy" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>       
                        </div>                 
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity " CssClass="iceLable"></asp:Label> <span class="mandatory">*</span> 
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxQuantity" runat="server"  MaxLength="4" onkeypress="return CheckDecimal(event, this)" CssClass="iceTextBox"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteQty" FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtbxQuantity"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ErrorMessage=" Enter Quantity " ControlToValidate="txtbxQuantity"
                            ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblCropRate" runat="server" Text="Crop Rate " CssClass="iceLable"></asp:Label> <span class="mandatory">*</span> 
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxCropRate" runat="server" MaxLength="15" onkeypress="return CheckDecimal(event, this)" CssClass="iceTextBox"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers,Custom" ValidChars=".,"
                            TargetControlID="txtbxCropRate" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ErrorMessage=" Enter Croprate " ControlToValidate="txtbxCropRate"
                            ValidationGroup="DamageCrop" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="AmountPaidLabel" runat="server" Text="Total Amount" CssClass="iceLable"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxAmountPaid" runat="server" ReadOnly="true" BackColor="White" CssClass="iceTextBox"></asp:TextBox>
                        <%--<asp:Label ID="AmountPaidValue"  runat="server"  CssClass="iceLable" Text=" "></asp:Label>--%>
                    </td>
                  <td>
                        <asp:Label ID="UploadPhotoLabel" runat="server" Text="Upload Photo" CssClass="iceLable" Visible="false" />
                    </td>
                    <td colspan="3">
                        <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblComments" runat="server" Text="Comments" CssClass="iceLable"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="CommentsTextBox" runat="server" CssClass="iceTextBox" TextMode="MultiLine" Rows="3" Width="500px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom" ValidChars=". ,/()" TargetControlID="CommentsTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="4">
                        <div style="margin-top: 12px;">
                          <asp:ValidationSummary ID="VsDamageCrops" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="DamageCrop"  runat="server" />
                                  <a id="lnkDamageCrops" runat="server" href="#" runat="server" class="iceLinkButton"
                                        style="text-decoration: none; color: White; font-family: Arial; font-size: 12px;
                                        font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">
                                        Change Request</a>&nbsp;
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="DamageCrop" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                  <td colspan="4">
                     <asp:Label ID="StatusDamageCrops" runat="server" Style="text-decoration: blink;
                                color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                  </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <asp:GridView ID="grdDamagedCrops" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" 
            OnRowCommand="DamagedCrops_RowCommand" 
            onpageindexchanging="ChangePage" AllowPaging="True" 
            onrowdatabound="grdDamagedCrops_RowDataBound">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" 
                ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DMGCRPFORMREFNO" HeaderText="Reference No." 
                    HeaderStyle-HorizontalAlign="Left" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Cropname" HeaderText="Name" 
                    HeaderStyle-HorizontalAlign="Left" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Croptype" HeaderText="Type" 
                    HeaderStyle-HorizontalAlign="Left" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" 
                    HeaderStyle-HorizontalAlign="Left" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Date Damaged" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Literal ID="litDateDamaged" Text="" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QUANTITY" HeaderText="Qty" 
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                </asp:BoundField>

                 <asp:TemplateField HeaderText="Crop Rate" HeaderStyle-HorizontalAlign="Center">
                  <ItemStyle HorizontalAlign="Right"/>
                     <ItemTemplate>
                      <asp:Literal ID="litCropRate" runat="server"></asp:Literal>
                      </ItemTemplate>
                  </asp:TemplateField>
               <%-- <asp:BoundField DataField="CROPRATE" HeaderText="Crop Rate" HeaderStyle-HorizontalAlign="Center" 
               DataFormatString="{0:N0}"
                    ItemStyle-HorizontalAlign="Right" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>--%>

                  <asp:TemplateField HeaderText="Amount Paid" HeaderStyle-HorizontalAlign="Center">
                  <ItemStyle HorizontalAlign="Right"/>
                     <ItemTemplate>
                      <asp:Literal ID="litAmountPaid" runat="server"></asp:Literal>
                      </ItemTemplate>
                  </asp:TemplateField>


               <%-- <asp:BoundField DataField="AMOUNTPAID" HeaderText="Amount Paid" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"
                    ItemStyle-HorizontalAlign="Right" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>--%>
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

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("DAMAGED_CROPID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("DAMAGED_CROPID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="damagedCropId" Text='<%#Eval("DAMAGED_CROPID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
    
        <script language="javascript" type="text/javascript">
            PreventDateFieldEntry(document.getElementById('<%=DateDamaged.ClientID%>'));

            spnpnldiv = document.getElementById('table1');
            if (spnpnldiv != null) {
                scrWidth = screen.availWidth;
                spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
            }
            function ddlCropName_IndexChanged(src) {
                var drpCropID = document.getElementById('<%=ddlCropName.ClientID%>');
                var drpCropDesID = document.getElementById('<%=ddlCropDescription.ClientID%>');
                document.getElementById('<%=txtbxCropRate.ClientID %>').value = '';
                document.getElementById('<%=txtbxAmountPaid.ClientID %>').value = '';
                var CropID;
                var CropDesID;
                if (drpCropID.selectedIndex > 0) {
                    CropID = drpCropID.options[drpCropID.selectedIndex].value;
                }
                else {
                    return;
                }
                if (drpCropDesID.selectedIndex > 0) {
                    CropDesID = drpCropDesID.options[drpCropDesID.selectedIndex].value;
                }
                else {
                    return;
                }
                Unit = CropID.toString() + '|' + CropDesID.toString();
                PageMethods.GetUnitName(Unit, OnWSGetUnitNameCompleteCrop);
            }

            function OnWSGetUnitNameCompleteCrop(result) {
                fldCropRate = document.getElementById('<%=txtbxCropRate.ClientID %>');
                fldCropRate.value = result[1];
                var rate = result[1];
                var quantity = document.getElementById('<%=txtbxQuantity.ClientID %>').value;
                if (!isNaN(quantity) && !isNaN(rate))
                    amount = rate * quantity;
                else
                    amount = '';
                document.getElementById('<%=txtbxCropRate.ClientID %>').value = AddComma(document.getElementById('<%=txtbxCropRate.ClientID %>').value.replace(/,/g, ''));
                document.getElementById('<%=txtbxAmountPaid.ClientID %>').value = AddComma(amount);
            }

            function CalculateAmount() {
                var quantity;
                var rate;
                var amount;

                quantity = document.getElementById('<%=txtbxQuantity.ClientID %>').value;
                rate = RemoveComma(document.getElementById('<%=txtbxCropRate.ClientID %>').value);

                if (!isNaN(quantity) && !isNaN(rate))
                    amount = rate * quantity;
                else
                    amount = '';
                document.getElementById('<%=txtbxCropRate.ClientID %>').value =  AddComma(document.getElementById('<%=txtbxCropRate.ClientID %>').value.replace(/,/g, ''));
                document.getElementById('<%=txtbxAmountPaid.ClientID %>').value = AddComma(amount);
            }
            function RemoveComma(iValue) {
                return parseFloat(iValue.toString().replace(/,?/g, ""));
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


            function CheckConstrSatrtDate(oSrc, args) {
                dtProjectStart = document.getElementById('<%=hfProjStartDate.ClientID %>').value;
                dtProjectEnd = document.getElementById('<%=hfProjEndDate.ClientID %>').value;
                dtConstrStart = GetCalDate('<%=DateDamaged.ClientID%>');

                var ArrProjSt = dtProjectStart.split("-");
                var ProjStartDate = ArrProjSt[0];
                var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
                var ProjStartYear = ArrProjSt[2];

                var ArrProjEnd = dtProjectEnd.split("-");
                var ProjEndDate = ArrProjEnd[0];
                var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
                var ProjEndYear = ArrProjEnd[2];

                var ArrConstrSt = dtConstrStart.split("-");
                var ConstrStartDate = ArrConstrSt[0];
                var ConstrStartMonth = GetMonthNumber(ArrConstrSt[1]);
                var ConstrStartYear = ArrConstrSt[2];

                if (ProjStartYear > ConstrStartYear) {
                    args.IsValid = false;
                    return;
                }
                else if (ProjEndYear < ConstrStartYear) {
                    args.IsValid = false;
                    return;
                }
                else if ((ProjStartYear == ConstrStartYear) && (ProjStartMonth > ConstrStartMonth)) {
                    args.IsValid = false;
                    return;
                }
                else if ((ProjStartYear == ConstrStartYear) && (ProjStartMonth == ConstrStartMonth) && (ProjStartDate > ConstrStartDate)) {
                    args.IsValid = false;
                    return;
                }
                else if ((ProjEndYear == ConstrStartYear) && (ProjEndMonth < ConstrStartMonth)) {
                    args.IsValid = false;
                    return;
                }
                else if ((ProjEndYear == ConstrStartYear) && (ProjEndMonth == ConstrStartMonth) && (ProjEndDate < ConstrStartDate)) {
                    args.IsValid = false;
                    return;
                }

                args.IsValid = true;
            }

            function CheckDOB(oSrc, args) {
                var now = new Date();
                dtMeeting = GetCalDate('<%=DateDamaged.ClientID%>');

                var CurrentMonth = (now.getMonth() + 1);
                var CurrentDate = now.getDate();
                var CurrentYear = now.getFullYear();

                if (CurrentMonth.length < 2) CurrentMonth = '0' + CurrentMonth;
                if (CurrentDate.length < 2) CurrentDate = '0' + CurrentDate;

                var ArrMeetingDt = dtMeeting.split("-");
                var MeetingDt = ArrMeetingDt[0];
                var MeetingMonth = GetMonthNumber(ArrMeetingDt[1]);
                var MeetingYear = ArrMeetingDt[2];

                if (CurrentYear < MeetingYear) {
                    args.IsValid = false;
                    return;
                }
                else if ((CurrentYear == MeetingYear) && (CurrentMonth < MeetingMonth)) {
                    args.IsValid = false;
                    return;
                }
                else if ((CurrentYear == MeetingYear) && (CurrentMonth == MeetingMonth) && (CurrentDate < MeetingDt)) {
                    args.IsValid = false;
                    return;
                }

                args.IsValid = true;
            }
            function AddComma(iValue) {
                return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }

            function OpenUploadPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, PagePBID) {
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../UploadPhotoDocument.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&PagePBID=' + PagePBID, 'Uploadphoto', 'resizable=1,scrollbars=1,width=600px,height=500px,top=' + top + ', left=' + left);
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
                var tat1 = document.getElementById("<%= txtbxDamagedCropFormRefNo.ClientID  %>");
                var tat2 = document.getElementById("<%= DateDamaged.ClientID  %>");
                var tat3 = document.getElementById("<%= txtbxQuantity.ClientID  %>");
                var tat4 = document.getElementById("<%= txtbxCropRate.ClientID  %>");
                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
                else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && tat4.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
