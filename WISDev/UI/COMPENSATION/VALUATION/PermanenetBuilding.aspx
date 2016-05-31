<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="PermanenetBuilding.aspx.cs" Inherits="WIS.PermanenetBuilding" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy"
    TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="ValuationMenu.ascx" TagName="ValuationMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%--/**
 * 
 * @version		 PermanentBuilding UI screen   
 * @package		  PermanentBuilding
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  28-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
        <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
        <div style="width: 100%">
            <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
        </div>
        <div style="width: 100%; height: 25px; float: right">
            <table width="100%">
                <tr>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td align="right" style="width: 180px">
                        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div style="width: 100%">
            <fieldset class="icePnlinner">
                <legend>Building/Structure Details</legend>
                <div style="float: right">
                    <%-- <a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>--%>
                </div>
                <script type="text/javascript" language="javascript">
                    function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
                        var dt = new Date();
                        open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu + '&dt=' + dt.getMilliseconds().toString(), 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px');
                    }
                </script>
                <table id="table1">
                    <tr>
                        <td>
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="perstructIDTextBox" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="iceLable" nowrap>
                                        Building/Structure Type <span class="mandatory">*</span>
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:DropDownList ID="ddlStructureType" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="PER">Permanent</asp:ListItem>
                                            <asp:ListItem Value="SEPER">Semi Permanent</asp:ListItem>
                                            <asp:ListItem Value="NPER">Non-Permanent</asp:ListItem>
                                            <asp:ListItem Value="FAC">Facilitation</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlStructureType"
                                            InitialValue="0" ErrorMessage="Select Building/ Structure Type" Display="None"
                                            ValidationGroup="Permanent" runat="server"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <asp:Label ID="lblBuildingType" runat="server" Text="Building Type" CssClass="iceLable" />
                                        <span class="mandatory">*</span>
                                    </td>
                                    <td width="35%">
                                        <asp:DropDownList ID="ddlBuidingType" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true"
                                            Width="205px">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlBuidingType"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                            ErrorMessage="Select Building Type" ValidationGroup="Permanent" InitialValue="0"
                                            ControlToValidate="ddlBuidingType"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="15%">
                                        <asp:Label ID="lbloterSpecify" runat="server" Text="Other(Specify)" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="otherSpecifyTextBox" runat="server" MaxLength="80" CssClass="iceTextBox"
                                            Width="198px" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteotherSpecify" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                            ValidChars=" ," TargetControlID="otherSpecifyTextBox" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRoofmaterial" runat="server" Text="Roof Material" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoofMaterial" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlRoofMaterial"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWallsMaterial" runat="server" Text="Walls Material" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWallsMaterial" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlWallsMaterial"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFloorMaterial" runat="server" Text="Floor Material" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFloorMaterial" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlFloorMaterial"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWindowsMaterial" runat="server" Text="Windows Material" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWindowsMaterial" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender8" runat="server" TargetControlID="ddlWindowsMaterial"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRoofCondition" runat="server" Text="Roof Condition" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoofCondition" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlRoofCondition"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWallsCondition" runat="server" Text="Walls Condition" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWallsCondition" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender9" runat="server" TargetControlID="ddlWallsCondition"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFloorCondition" runat="server" Text="Floor Condition" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFloorCondition" runat="server" CssClass="iceDropDown" Width="205px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlFloorCondition"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWindowsCondition" runat="server" Text="Windows Condition" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWindowsCondition" runat="server" CssClass="iceDropDown"
                                            Width="205px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender10" runat="server" TargetControlID="ddlWindowsCondition"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOwner" runat="server" Text="Owner" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <div style="float: left; width: 120px;">
                                            <asp:RadioButton ID="RbtnSelf" GroupName="Radiobtn" runat="server" Text="Self" />&nbsp;
                                            <asp:RadioButton ID="RbtnOther" GroupName="Radiobtn" runat="server" Text="Other" />
                                        </div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtbxOther" MaxLength="60" ClientIDMode="Static" runat="server"
                                                Width="95px" CssClass="iceTextBox" disabled="disabled" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                ValidChars=" ," TargetControlID="txtbxOther" runat="server">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOccupant" runat="server" Text="Occupant" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <div style="float: left; width: 120px;">
                                            <asp:RadioButton ID="RdbtnSelfoccupied" GroupName="Radiobtn1" runat="server" Text="Self" />&nbsp;
                                            <asp:RadioButton ID="RdbtnOccupantOther" GroupName="Radiobtn1" runat="server" Text="Other" />
                                        </div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtbxOccupantOther" MaxLength="60" ClientIDMode="Static" runat="server"
                                                Width="95px" disabled="disabled"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                ValidChars=" ," TargetControlID="txtbxOccupantOther" runat="server">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOccupantStatus" runat="server" Text="Occupant Status" CssClass="iceLable" /><%--<span class="mandatory">*</span>--%>
                                    </td>
                                    <td style="width: 5%">
                                        <asp:DropDownList ID="ddlOccupantStatus" runat="server" CssClass="iceDropDown" Width="100px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="ddlOccupantStatus"
                                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                            IsSorted="true" />
                                        <%--<asp:RequiredFieldValidator ID="ddlRequiredFieldValidator3" runat="server" InitialValue="--Select--"
                            ErrorMessage=" Select Occupant Status" ControlToValidate="ddlOccupantStatus"
                            ValidationGroup="Permanent" Display="None"></asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtbxEnterStatus" ClientIDMode="Static" runat="server" MaxLength="60"
                                            Width="100px" CssClass="iceTextBox" Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                ValidChars=" ," TargetControlID="txtbxEnterStatus" runat="server">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        <br />
                                        <%--<asp:RequiredFieldValidator ID="isDirectorCompleted" Display="None" runat="server"
            ErrorMessage="Enter occupant Status" ValidationGroup="Permanent" ControlToValidate="txtbxEnterStatus"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDimensions" runat="server" Text="Dimensions(meters)" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtbxLength" runat="server" Width="45px" CssClass="iceTextBox" MaxLength="10"
                                                        onkeypress="return CheckDecimal(event, this)" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers,Custom"
                                                        ValidChars="." TargetControlID="txtbxLength" runat="server">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLength" runat="server" Text="(Length)" CssClass="iceLable" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbxWidth" runat="server" Width="45px" CssClass="iceTextBox" MaxLength="10"
                                                        onkeypress="return CheckDecimal(event, this)" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                                                        ValidChars="." TargetControlID="txtbxWidth" runat="server">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblWidth" runat="server" Text="(Width)" CssClass="iceLable" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumberofRooms" runat="server" Text="Number of Rooms" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbxNoofrooms" MaxLength="8" runat="server" CssClass="iceTextBoxLarge"
                                            Width="200px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Numbers"
                                            TargetControlID="txtbxNoofrooms" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSurfaceArea" runat="server" Text="Surface Area" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbxSurfaceArea" runat="server" MaxLength="50" ReadOnly="true"
                                            CssClass="iceTextBox" Width="128px"></asp:TextBox>
                                        <asp:Label ID="lblsqMeters" runat="server" Text="(sq meters)" CssClass="iceLable"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDepreciatedValue" runat="server" Text="Depreciated Value" CssClass="iceLable" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbxDepreciatedValue" runat="server" MaxLength="20" onkeypress="return CheckDecimal(event, this)"
                                            CssClass="iceTextBox" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDepreciatedValue" FilterType="Numbers,Custom"
                                            ValidChars=".," TargetControlID="txtbxDepreciatedValue" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReplacementValue" runat="server" Text="Replacement Value" CssClass="iceLable" /><span
                                            class="mandatory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbxReplacementValue" runat="server" MaxLength="20" onkeypress="return CheckDecimal(event, this)"
                                            CssClass="iceTextBox" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                            ErrorMessage="Enter Replacement Value" ValidationGroup="Permanent" ControlToValidate="txtbxReplacementValue"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteReplacementValue" FilterType="Numbers,Custom"
                                            ValidChars=".," TargetControlID="txtbxReplacementValue" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <%--CalculateAmount--%>
                                        <asp:CustomValidator ID="cust" runat="server" ValidationGroup="Permanent" ClientValidationFunction="CalculateAmount"
                                            ControlToValidate="txtbxReplacementValue" Display="None"></asp:CustomValidator>
                                        <%--<asp:CompareValidator ID="CompareValidator2" ControlToCompare="txtbxReplacementValue"
                                        ControlToValidate="txtbxDepreciatedValue" Type="Double" Display="None" ErrorMessage="Replacement Value cannot be lesser than Depreciated Value"
                                        ValidationGroup="Permanent" Operator="LessThan" runat="server"></asp:CompareValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReplacementUplift" runat="server" Text="Replacement Uplift" CssClass="iceLable"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbxReplacementUplift" runat="server" CssClass="iceTextBoxLarge"
                                            ReadOnly="true" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteReplacementUplift" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtbxReplacementUplift" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Upload Photo" CssClass="iceLable" Visible="false" />
                                    </td>
                                    <td colspan="3">
                                        <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblComments" runat="server" Text="Additional Comments (Other comments not related to structures' description)"
                                            CssClass="iceLable"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtbxComments" runat="server" TextMode="MultiLine" CssClass="iceTextBox"
                                            Width="98%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                            ValidChars=" ," TargetControlID="txtbxComments" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table align="center">
                                            <tr>
                                                <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Permanent" runat="server" />
                                                <td>
                                                    <a id="lnkPermBuild" runat="server" href="#" runat="server" class="iceLinkButton"
                                                        style="text-decoration: none; color: White; font-family: Arial; font-size: 12px;
                                                        font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">
                                                        Change Request</a>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                                        ValidationGroup="Permanent" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="StatusPermBuild" runat="server" Style="text-decoration: blink; color: Red;
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
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="grdPermanentBuilding" runat="server" CssClass="gridStyle" CellPadding="4"
                            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="PermanentBuilding_RowCommand"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="grdPermanentBuilding_RowDataBound">
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
                                <asp:BoundField DataField="StructureType" HeaderText="Building/ Structure Type" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="StructTypid" HeaderText="Building Type" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Depreciated Value" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Right" Width="45%" />
                                    <ItemTemplate>
                                        <asp:Literal ID="litDepreciatedValue" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Replacement Value" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                    <ItemTemplate>
                                        <asp:Literal ID="litReplacementValue" runat="server"></asp:Literal>
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
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                            CommandName="EditRow" CommandArgument='<%#Eval("PermanentStructureID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                            CommandName="DeleteRow" CommandArgument='<%#Eval("PermanentStructureID") %>'
                                            OnClientClick="return DeleteRecord();" runat="server" />
                                        <asp:Literal ID="PermanentStrucID" Text='<%#Eval("PermanentStructureID") %>' Visible="false"
                                            runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script language="javascript" type="text/javascript">

        spnpnldiv = document.getElementById('table1');
        if (spnpnldiv != null) {
            scrWidth = screen.availWidth;
            spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
        }

        function surfacearea() {
            var length;
            var width;
            var surfaceArea;
            length = document.getElementById('<%=txtbxLength.ClientID %>').value;
            width = document.getElementById('<%=txtbxWidth.ClientID %>').value;
            if (!isNaN(length) && !isNaN(width)) {
                surfaceArea = length * width;
                surfaceArea = surfaceArea.toFixed(4);
            }
            else
                surfaceArea = '';

            document.getElementById('<%=txtbxSurfaceArea.ClientID %>').value = surfaceArea;
        }
        function CalculateAmount() {
            var DepreciatedValue;
            var ReplacementValue;
            var ReplacementUplift;

            DepreciatedValue = document.getElementById('<%=txtbxDepreciatedValue.ClientID %>').value;
            ReplacementValue = document.getElementById('<%=txtbxReplacementValue.ClientID %>').value;
            DepreciatedValue = DepreciatedValue.replace(/,/g, '');
            ReplacementValue = ReplacementValue.replace(/,/g, '');
            ReplacementUplift = '';

            if (!isNaN(DepreciatedValue) && !isNaN(ReplacementValue)) {
                if (ReplacementValue > 0 && DepreciatedValue > 0) {
                    ReplacementUplift = ReplacementValue - DepreciatedValue;

                    if (ReplacementUplift < 0) {
                        alert('Replacement Value cannot be less than Depreciated Value.');
                        document.getElementById('<%=txtbxDepreciatedValue.ClientID %>').value = '';
                        document.getElementById('<%=txtbxReplacementValue.ClientID %>').value = '';
                        ReplacementUplift = '';
                    }
                }
            }
            document.getElementById('<%=txtbxDepreciatedValue.ClientID %>').value = DepreciatedValue;
            document.getElementById('<%=txtbxReplacementValue.ClientID %>').value = ReplacementValue;
            document.getElementById('<%=txtbxReplacementUplift.ClientID %>').value = ReplacementUplift;
        }

        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }

        function EnableDisableOtherOwner(enableFld) {
            if (enableFld) {
                document.getElementById('txtbxOther').disabled = '';
                document.getElementById('txtbxOther').focus();
            }
            else {
                document.getElementById('txtbxOther').disabled = 'disabled';
            }
        }

        function EnableDisableOtherOccupant(enableFld) {
            if (enableFld) {
                document.getElementById('txtbxOccupantOther').disabled = '';
                document.getElementById('txtbxOccupantOther').focus();
            }
            else {
                document.getElementById('txtbxOccupantOther').disabled = 'disabled';
            }
        }

        function EnableDisableOtherOccupantStatus(src) {
            occupantStatus = src.options[src.selectedIndex].text;

            if (occupantStatus == 'Other') {
                document.getElementById('txtbxEnterStatus').disabled = '';
                document.getElementById('txtbxEnterStatus').focus();
            }
            else {
                document.getElementById('txtbxEnterStatus').disabled = 'disabled';
                document.getElementById('txtbxEnterStatus').value = '';
            }
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
            var tat1 = document.getElementById("<%= txtbxDepreciatedValue.ClientID  %>");
            var tat2 = document.getElementById("<%= txtbxReplacementValue.ClientID  %>");
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
