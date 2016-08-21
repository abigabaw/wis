<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Crops.aspx.cs" Inherits="WIS.Crops" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy"
    TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="ValuationMenu.ascx" TagName="ValuationMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%--/**
 * 
 * @version		  Crops UI screen   
 * @package		  Crops
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  1-05-2013
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
    <div style="width: 100%; height: 25px; float: right">
        <table width="100%">
            <tr>
                <td>&nbsp;&nbsp;
                </td>
                <td align="right" style="width: 180px">
                    <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="divAll">
        <div style="width: 100%" id="div1">
            <fieldset class="icePnlinner">
                <legend>Crop Details</legend>
                <div style="float: right">
                    <%-- <a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>--%>
                </div>
                <script type="text/javascript" language="javascript">
                    function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
                        open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu, 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px');
                    }
                </script>
                <table border="0" id="table1">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtbxPAP_CROPID" runat="server" Visible="false">
                            </asp:TextBox>
                        </td>
                        <td>
                            <%--  <asp:LinkButton ID="lnkupload" runat="server">Upload Photo</asp:LinkButton>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCropName" runat="server" Text="Crop Name" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCropName" runat="server" CssClass="iceTextBox" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlCropName"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="--Select--"
                                ErrorMessage=" Select Crop Name " ControlToValidate="ddlCropName" ValidationGroup="Crops"
                                Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblCropType" runat="server" Text="Crop Type" CssClass="iceLable"></asp:Label>
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCropType" runat="server" CssClass="iceTextBox" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlCropType"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="--Select--"
                                ErrorMessage=" Select Crop Type " ControlToValidate="ddlCropType" ValidationGroup="Crops"
                                Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
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
                                ErrorMessage=" Select Crop Description " ControlToValidate="ddlCropDescription"
                                ValidationGroup="Crops" Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label class="iceLable">
                                Unit of Measure</label>
                        </td>
                        <td>
                            <asp:Label ID="lblUnitMeasure" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                            <%--<asp:DropDownList ID="ddlUnitofMeasure" runat="server" CssClass="iceTextBox" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>--%>
                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="--Select--"
                            ErrorMessage=" Select Unit Of measure " ControlToValidate="ddlUnitofMeasure"
                            ValidationGroup="Crops" Display="None">--%>
                            <%--</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity " CssClass="iceLable"></asp:Label>
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxQuantity" runat="server" MaxLength="4" CssClass="iceTextBox">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteQty" FilterType="Numbers" TargetControlID="txtbxQuantity"
                                runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage=" Enter Quantity "
                                ControlToValidate="txtbxQuantity" ValidationGroup="Crops" Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblCropRate" runat="server" Text="Crop Rate " CssClass="iceLable"></asp:Label>
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxCropRate" runat="server" MaxLength="15"
                                CssClass="iceTextBox">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"
                                TargetControlID="txtbxCropRate" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage=" Enter Crop Rate "
                                ControlToValidate="txtbxCropRate" ValidationGroup="Crops" Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="lblCropvaluation" runat="server" Text="Crop Valuation" CssClass="iceLable">
                            </asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="txtbxCropvaluation" runat="server" CssClass="iceTextBox" ReadOnly="true">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers"
                                TargetControlID="txtbxCropvaluation" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <%--<asp:Label ID="AmountPaidValue"  runat="server"  CssClass="iceLable" Text=" "></asp:Label>--%>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="lblComments" runat="server" Text="General Comments" CssClass="iceLable">
                            </asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="CommentsTextBox" runat="server" TextMode="MultiLine" Height="44px"
                                Width="295px">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" " TargetControlID="CommentsTextBox" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UploadPhotoLabel" runat="server" Text="Upload Photo" CssClass="iceLable" Visible="false" />
                        </td>
                        <td colspan="3">
                            <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td colspan="4">
                            <asp:ValidationSummary ID="VsCrops" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Crops" runat="server" />
                            <table>
                                <tr>
                                    <td>
                                        <a id="lnkValuationCrop" runat="server" href="#" runat="server" class="iceLinkButton"
                                            style="text-decoration: none; color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">Change Request</a>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                            ValidationGroup="Crops" />&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="StatusValuationCrop" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="msgsaveLabel" runat="server" CssClass="iceLable"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div>
            <asp:GridView ID="grdCrops" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
                GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdCrops_RowCommand"
                OnRowDataBound="grdCrops_RowDataBound" AllowPaging="True" OnPageIndexChanging="Changepage">
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
                    <asp:BoundField DataField="Cropname" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Croptype" HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Cropdescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="UnitName" HeaderText="Unit of Measure" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="QUANTITY" HeaderText="Qty" ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Crop Rate" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Literal ID="litCroprate" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Crop Valuation" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                        <ItemTemplate>
                            <asp:Literal ID="litDimensions" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                                CommandName="EditRow" CommandArgument='<%#Eval("PAP_CROPID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("PAP_CROPID") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                            <asp:Literal ID="PapCropId" Text='<%#Eval("PAP_CROPID") %>' Visible="false" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <script language="javascript" type="text/javascript">

        spnpnldiv = document.getElementById('table1');
        if (spnpnldiv != null) {
            scrWidth = screen.availWidth;
            spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
        }


        function ddlCropName_IndexChanged(src) {
            var drpCropID = document.getElementById('<%=ddlCropName.ClientID%>');
                var drpCropDesID = document.getElementById('<%=ddlCropDescription.ClientID%>');
                document.getElementById('<%=txtbxCropRate.ClientID %>').value = '';
                document.getElementById('<%=txtbxCropvaluation.ClientID %>').value = '';
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
                PageMethods.GetUnitName(Unit, OnWSGetUnitNameComplete);
                CalculateAmount();
            }

            function OnWSGetUnitNameComplete(result) {
                document.getElementById('lblUnitMeasure').innerText = result[0];

                fldCropRate = document.getElementById('<%=txtbxCropRate.ClientID %>');

                fldCropRate.value = result[1];
                var rate = result[1];
                var quantity = document.getElementById('<%=txtbxQuantity.ClientID %>').value;
                if (!isNaN(quantity) && !isNaN(rate))
                    amount = rate * quantity;
                else
                    amount = '';
                document.getElementById('<%=txtbxCropRate.ClientID %>').value = AddComma(document.getElementById('<%=txtbxCropRate.ClientID %>').value.replace(/,/g, ''));
                document.getElementById('<%=txtbxCropvaluation.ClientID %>').value = AddComma(amount);
            }
            function AddComma(iValue) {
                return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }

            function CalculateAmount() {
                var Quantity;
                var CropRate;
                var valuation;

                Quantity = document.getElementById('<%=txtbxQuantity.ClientID %>').value;
                if (isNaN(Quantity)) Quantity = 0;
                Quantity = parseFloat(Quantity);

                CropRate = document.getElementById('<%=txtbxCropRate.ClientID %>').value;
                if (isNaN(CropRate)) CropRate = 0;
                CropRate = parseFloat(CropRate);

                if (!isNaN(Quantity) && !isNaN(CropRate))
                    valuation = Quantity * CropRate;
                else
                    valuation = '';

                if (!isNaN(valuation)) {
                    document.getElementById('<%=txtbxCropvaluation.ClientID %>').value = valuation;
                }
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
                var tat1 = document.getElementById("<%= txtbxQuantity.ClientID  %>");
                var tat2 = document.getElementById("<%= txtbxCropRate.ClientID  %>");
                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
                else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
