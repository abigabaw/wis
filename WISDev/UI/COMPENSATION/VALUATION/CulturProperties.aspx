
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="CulturProperties.aspx.cs" Inherits="WIS.CulturProperties" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc1" %>
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
 * @version		 CulturalProperties UI screen   
 * @package		  CulturalProperties
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  07-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div><br />
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Culture Property Type </legend>
            <asp:Panel ID="Linkpanel" runat="server" >
            <div style="float: right">  
                <%--<a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>&nbsp;&nbsp;|&nbsp;&nbsp;--%>
                <a id="lnkUPloadDoc" href="#" runat="server"><b>Upload Document</b></a> &nbsp;&nbsp;|&nbsp;&nbsp;
                <a id="lnkUPloadDoclist" href="#" runat="server"><b>View Upload Document</b></a>
            </div>
            </asp:Panel>
            <script type="text/javascript" language="javascript">
                function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode, DocumentID) {
                    var left = (screen.width - 800) / 2;
                    var top = (screen.height - 700) / 4;
                    open('../../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode + '&DOCSERVICEID=' + DocumentID, 'UploadDocPop', 'width=800px,height=700px,top=' + top + ', left=' + left);
                }

                function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode, DocumentID) {
                    var left = (screen.width - 800) / 2;
                    var top = (screen.height - 700) / 4;
                    open('../../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode + '&DOCSERVICEID=' + DocumentID, 'UploadDocPoplist', 'width=800px,height=700px,top=' + top + ', left=' + left);
                }

                function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
                    var left = (screen.width - 800) / 2;
                    var top = (screen.height - 700) / 4;
                    open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu, 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px,top=' + top + ', left=' + left);
                }
            </script>
            <table border="0" cellpadding="3" id="table1">
                <tr>
                    <td>
                        <asp:TextBox ID="CULTURALPROPIDtxtbx" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 16%; vertical-align: top">
                        <label class="iceLable">
                            Culture Property Type
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td style="width: 34%; vertical-align: top">
                        <asp:DropDownList ID="ddlCulturePropertyType" runat="server" CssClass="iceTextBox"
                            AppendDataBoundItems="true" Width="150px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                          <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlCulturePropertyType"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                            ErrorMessage=" Select Cultural Property Type" ControlToValidate="ddlCulturePropertyType"
                            ValidationGroup="CulturalProp" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 14%; vertical-align: top">
                        <label class="iceLable">
                            Description</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxDescription" runat="server" TextMode="MultiLine" MaxLength="300" Rows="3" Width="96%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                            ValidChars=" ," TargetControlID="txtbxDescription" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Dimensions (meters)</label>
                    </td>
                    <td>
                        <label class="iceLable">
                            Length</label>
                        <asp:TextBox ID="txtbxlength" runat="server" MaxLength="10" Width="80px" onkeypress="return CheckDecimal(event, this)"></asp:TextBox>                       
                        <ajaxToolkit:FilteredTextBoxExtender ID="azLength"  FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtbxlength"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <label class="iceLable">
                            Width</label>
                        <asp:TextBox ID="txtbxwidth" runat="server" MaxLength="10" Width="80px" onkeypress="return CheckDecimal(event, this)"></asp:TextBox>                        
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="Numbers,Custom" ValidChars="."
                            TargetControlID="txtbxwidth" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <label class="iceLable">
                            Depreciated Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxDepreciatedValue" runat="server" MaxLength="20"></asp:TextBox>                       
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers"
                            TargetControlID="txtbxDepreciatedValue" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Valuation Amount</label>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxValuationAmount" runat="server" MaxLength="20" CssClass="iceTextBox"></asp:TextBox>                       
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers"
                            TargetControlID="txtbxValuationAmount" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="4">
                        <div style="margin-top: 12px;">
                            <asp:ValidationSummary ID="VsTribe" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="CulturalProp" runat="server" />
                                   <a id="lnkValuationCulturProperties" runat="server" href="#" runat="server" class="iceLinkButton"
                                        style="text-decoration: none; color: White; font-family: Arial; font-size: 12px;
                                        font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">
                                        Change Request</a>&nbsp;
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                ValidationGroup="CulturalProp" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                  <td colspan="4">
                     <asp:Label ID="StatusCulturProperties" runat="server" Style="text-decoration: blink;
                                color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                  </td>
                </tr>
            </table>
        </fieldset>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" HorizontalAlign="Center" Height="100%">
        <asp:GridView ID="grdCultureProperties" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="CultureProperties_RowCommand"
            OnRowDataBound="grdCultureProperties_RowDataBound" OnPageIndexChanging="ChangePage"
            AllowPaging="True">
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
                <asp:BoundField DataField="CULTUREPROPTYP" HeaderText="Culture Property Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                </asp:BoundField>
                <asp:BoundField DataField="CULTUREPROPDESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="Center" >
                 <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                     <asp:TemplateField HeaderText="Valuation Amount" HeaderStyle-HorizontalAlign="Center">
                  <ItemStyle HorizontalAlign="Right" Width="15%"/>
                     <ItemTemplate>
                      <asp:Literal ID="litValuationAmount" runat="server"></asp:Literal>
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
                            CommandName="EditRow" CommandArgument='<%#Eval("CULTURALPROPID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Meeting">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <a id="lnkMeeting" href="#" runat="server">Meeting</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel>
        <script type="text/javascript" language="javascript">

            spnpnldiv = document.getElementById('table1');
            if (spnpnldiv != null) {
                scrWidth = screen.availWidth;
                spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
            }

            function OpenMeeting(CULTURALPROPID) {
                var left = (screen.width - 940) / 2;
                var top = (screen.height - 600) / 4;
                open('ScheduleMeeting.aspx?CULTURALPROPID=' + CULTURALPROPID, 'Meeting', 'width=940px,height=600px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);

            }

            spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
            if (spnpnl != null) {
                scrWidth = screen.availWidth;
                spnpnl.style.width = parseInt(scrWidth - 80).toString() + "px";
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

            function OpenUploadPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, PagePBID) {
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../UploadPhotoDocument.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&PagePBID=' + PagePBID, 'Uploadphoto', 'resizable=1,scrollbars=1,width=600px,height=500px,top=' + top + ', left=' + left);
            }    
        </script>
    </div>
</asp:Content>
