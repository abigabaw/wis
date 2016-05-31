<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Neighbours.aspx.cs" Inherits="WIS.Neighbours" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
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
 * @version		  Neighbour UI screen   
 * @package		  Neighbour
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  24-04-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div id="divAll">
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div><br />
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Neighbour Details</legend>
            <table border="0" cellpadding="3" id="table1">
                <tr>
                    <td style="width: 16%">
                        <asp:Label ID="lblNeighboursName" runat="server" Text="Neighbour's Name" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtNeibrName" runat="server" MaxLength="50" Width="230px" CssClass="iceTextBox" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                            ValidChars=" " TargetControlID="txtNeibrName" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="txtbxreqAppntDate" runat="server" ErrorMessage="Enter Neighbour Name"
                            Display="None" ControlToValidate="txtNeibrName" ValidationGroup="Neighbour">
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNeighbrID" Visible="false" runat="server" CssClass="iceTextBox">
                        </asp:TextBox>
                    </td>
                    <td style="width: 17%">
                        <asp:Label ID="lblDirection" runat="server" Text="Direction" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddldirectionDropDownList" CssClass="iceTextBox" runat="server"
                            AppendDataBoundItems="true">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddldirectionDropDownList"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Direction"
                            InitialValue="0" Display="None" ControlToValidate="ddldirectionDropDownList"
                            ValidationGroup="Neighbour">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblboundrConfirmed" runat="server" Text="Boundaries Confirmed" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton1" GroupName="Boundary" runat="server" Text="Yes" />
                        <asp:RadioButton ID="RadioButton2" GroupName="Boundary" Text="No" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Any Boundary Disputes?" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoBoundaryDisputesYes" GroupName="BoundaryDisputes" Text="Yes"
                            runat="server" />
                        <asp:RadioButton ID="rdoBoundaryDisputesNo" GroupName="BoundaryDisputes" Text="No"
                            Checked="true" runat="server" />
                    </td>
                </tr>
                <tr id="spnWhichBoundaryDisputes" style="visibility: hidden">
                    <td></td>
                    <td></td>
                    <td></td>
                    <td align="left">
                        <asp:TextBox ID="txtBoundaryDisputes" Text="" TextMode="MultiLine" CssClass="iceTextBox"
                            Rows="4" Width="250px" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="lnkNeighbours" runat="server" Text="Change Request" CssClass="icebutton" Width="120px" Visible="false" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="Neighbour" onclick="btnSave_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" onclick="btnClear_Click" />
                        <asp:ValidationSummary ID="VsNeighbour" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Neighbour" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-top:12px">
                        <asp:Label ID="StatusNeighbours" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <asp:GridView ID="grdNeighbor" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AllowPaging="True" ClientIDMode="Static" PageSize="10" AutoGenerateColumns="false"
            Width="100%" OnPageIndexChanging="grdNeighbor_PageIndexChanging" onrowcommand="grdNeighr_RowCommand">
            <rowstyle cssclass="gridRowStyle" />
            <alternatingrowstyle cssclass="gridAlternateRow" />
            <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" font-bold="true" forecolor="White" />
            <headerstyle cssclass="gridHeaderStyle" />
            <columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TRN_PAP_NEIGHBOURNAme1" HeaderText="Neighbour" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DIRECTION1" HeaderText="Direction" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
                 <asp:BoundField DataField="BOUNDARIESCONFIRMED1" HeaderText="Boundaries Confirmed" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
                 <asp:BoundField DataField="BOUNDARY_DISPUTE" HeaderText="Boundary Dispute" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left"/>
                 <asp:BoundField DataField="DISPUTE_DETAILS" HeaderText="Dispute Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left"/>
               
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif" CommandName="EditRow" CommandArgument='<%#Eval("PAP_NEIGHBOURID1") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PAP_NEIGHBOURID1") %>'
                            OnClientClick="return DeleteRecord();" runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("PAP_NEIGHBOURID1") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
              
            </columns>
        </asp:GridView>
    </div>
    </div>
    <script language="javascript" type="text/javascript">
        function ShowHideBoundaryDisputes(show) {
            if (show) {
                document.getElementById('spnWhichBoundaryDisputes').style.visibility = 'visible';
            }
            else {
                document.getElementById('spnWhichBoundaryDisputes').style.visibility = 'hidden';
            }
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
        document.getElementById('divAll').onclick = function () {
            isDirty = 0;
            setTimeout(function () { setDirtyText(); }, 100);
        };
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%=btnSave.ClientID%>");
            var tat1 = document.getElementById("<%= txtNeibrName.ClientID  %>");
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
</asp:Content>
