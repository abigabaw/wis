<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Grave.aspx.cs" Inherits="WIS.other_fixtures" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="ValuationMenu.ascx" TagName="ValuationMenu" TagPrefix="uc1" %>
<%@ Register Src="OtherFixturesMenu.ascx" TagName="OtherFixturesMenu" TagPrefix="uc3" %>
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
    <script type="text/javascript" language="javascript">
        function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu) {
            open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule + '&perStu=' + perStu, 'Uploadphoto', 'resizable=1,scrollbars=1,width=700px,height=500px');
        }
    </script>
</asp:Content>
<%--/**
 * 
 * @version		 Grave UI screen   
 * @package		  Grave
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Mahalakshmi
 * @Created Date  30-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
    <div style="width: 100%">
        <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
        <uc3:OtherFixturesMenu ID="OtherFixturesMenu1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
    <br />
    <div id="divAll">
        <fieldset class="icePnlinner">
            <legend>Grave Details </legend>
            <div style="float: right">
                <%--<a id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>--%>
            </div>
            <table align="center" border="0" cellpadding="0" cellspacing="2" id="table1">
                <tr>
                    <td>
                        <asp:Label ID="gravefinishLabel" runat="server" Text="Grave Finish" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="gravefinishDropDownList" runat="server" CssClass="iceDropDown">
                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                         <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="gravefinishDropDownList"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                            ErrorMessage="Select Grave" ValidationGroup="Grave" InitialValue="--Select--"
                            ControlToValidate="gravefinishDropDownList"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="otherfinishLabel" runat="server" Text="If other Finish, specify" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:TextBox ID="otherfinishTextBox" runat="server" MaxLength="100" CssClass="iceTextBox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="gravedimensionLabel" runat="server" Text="Grave Dimensions (meters)"
                            CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:Label ID="lengthLabel" runat="server" Text="Length" CssClass="iceLable" /><span
                            class="mandatory"> * </span>
                        <asp:TextBox ID="lengthTextBox" runat="server" CssClass="iceTextBox" MaxLength="10" onkeypress="return CheckDecimal(event, this)"
                            Width="60px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteLen"  FilterType="Numbers,Custom" ValidChars="." TargetControlID="lengthTextBox"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                            ErrorMessage="Select Length" ValidationGroup="Grave" ControlToValidate="lengthTextBox"></asp:RequiredFieldValidator>
                        <asp:Label ID="widthLabel" runat="server" Text="Width" CssClass="iceLable" /><span
                            class="mandatory"> * </span>
                        <asp:TextBox ID="widthTextBox" runat="server" CssClass="iceTextBox" MaxLength="10" onkeypress="return CheckDecimal(event, this)"
                            Width="60px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="Numbers,Custom" ValidChars="."
                            TargetControlID="widthTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                            ErrorMessage="Select Width" ValidationGroup="Grave" ControlToValidate="widthTextBox"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="depreciatedvalueLabel" runat="server" Text="Depreciated Value" CssClass="iceLable" />
                        <span class="mandatory">* </span>
                    </td>
                    <td>
                        <asp:TextBox ID="depreciatedvalueTextBox" runat="server" MaxLength="20" CssClass="iceTextBox" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers"
                            TargetControlID="depreciatedvalueTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
                            ErrorMessage="Select Depreciated Value" ValidationGroup="Grave" ControlToValidate="depreciatedvalueTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="surfaceareaLabel" runat="server" Text="Surface Area" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:TextBox ID="surfaceareaTextBox" runat="server" ReadOnly="true" CssClass="iceTextBox" />(SQM)
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="UploadPhotoLabel" runat="server" Text="Upload Photo" CssClass="iceLable" Visible="false"/>
                    </td>
                    <td colspan="3">
                        <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                            <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Grave" runat="server" />
                            <%--   <asp:TextBox ID="IDTextBox1" runat="server" Visible = "false" CssClass = "iceTextBox"/>--%>
                          <%--  <a id="lnkGrave" runat="server" href="#" class="iceLinkButton" style="text-decoration: none;
                                color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px;
                                height: 17px; margin-top: -0.5px; vertical-align: middle;">Change Request</a>&nbsp;--%>
                                <asp:Button ID="lnkGrave" runat="server" Text="Change Request" Width="120px" CssClass="icebutton" />
                             <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                ValidationGroup="Grave" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            <asp:Label ID="msgsaveLabel" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Label ID="StatusGrave" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
        </fieldset>


    <div>
        <asp:GridView ID="grdGrave" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
            GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdUsers_RowCommand"
            OnRowDataBound="grdGrave_RowDataBound" AllowPaging="True" PageSize="10" OnPageIndexChanging="grdGrave_PageIndexChanging">
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
                <asp:BoundField DataField="Grv_finishtype" HeaderText="Grave Finish" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Dimensions (SQM)" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                    <ItemTemplate>
                        <asp:Literal ID="litDimensions" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:BoundField DataField="Depreciatedvalue" HeaderText="Depreciated Value" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />--%>
                       <asp:TemplateField HeaderText="Depreciated Value" HeaderStyle-HorizontalAlign="Center">
                  <ItemStyle HorizontalAlign="Right"/>
                     <ItemTemplate>
                      <asp:Literal ID="litDepreciatedValue" runat="server"></asp:Literal>
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
                            CommandName="EditRow" CommandArgument='<%#Eval("PAP_GRAVEID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PAP_GRAVEID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("PAP_GRAVEID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>

</div>

        <script language="javascript" type="text/javascript">
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
                var tat1 = document.getElementById("<%= lengthTextBox.ClientID  %>");
                var tat2 = document.getElementById("<%= widthTextBox.ClientID  %>");
                var tat3 = document.getElementById("<%= depreciatedvalueTextBox.ClientID  %>");
                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
                else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
