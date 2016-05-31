<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LandInfoRespondentsOff.aspx.cs" Inherits="WIS.LandInfoRespondentsOff" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
    <fieldset class="icePnlinner">
        <legend>Living off Affected Land</legend>
        <table align="center" border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td style="width: 50%">
                    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
                        <tr>
                            <td class="iceSectionHeader" colspan="2">
                                Housing
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 30%">
                                <label class="iceLable">
                                    Type of Dwelling</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" class="iceNormalText">
                                <asp:DropDownList ID="ddlDwelling" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlDwelling"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="reqDwelling" ControlToValidate="ddlDwelling" InitialValue="0"
                                    ErrorMessage="Select Dwelling" Display="None" ValidationGroup="Dwelling" runat="server">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 30%">
                                <label class="iceLable">
                                    No. of Rooms</label>
                            </td>
                            <td align="left" class="iceNormalText">
                                <asp:TextBox ID="txtNoRooms" runat="server" CssClass="iceNormalText" MaxLength="3"
                                    Width="50px">
                                </asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteNoRooms" FilterType="Numbers" TargetControlID="txtNoRooms"
                                    runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 30%">
                                <label class="iceLable">
                                    Type of Tenure</label>
                            </td>
                            <td align="left" class="iceNormalText">
                                <asp:DropDownList ID="ddlTenureType" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlTenureType"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label5" runat="server" CssClass="iceLable" Text="Tenure"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txttenure" runat="server" MaxLength="04" AutoCompleteType="Disabled"
                                    CssClass="iceTextBox" Width="123px">
                                </asp:TextBox>
                                Years
                                <ajaxToolkit:FilteredTextBoxExtender ID="TenureId" FilterType="Numbers" TargetControlID="txttenure"
                                    runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20px">
                </td>
                <td valign="top">
                    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
                        <tr>
                            <td class="iceSectionHeader" colspan="2">
                                Materials
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 30%">
                                <label class="iceLable">
                                    Roof</label>
                            </td>
                            <td align="left" class="iceNormalText">
                                <asp:DropDownList ID="ddlRoof" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlRoof"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 30%">
                                <label class="iceLable">
                                    Walls</label>
                            </td>
                            <td align="left" class="iceNormalText">
                                <asp:DropDownList ID="ddlWalls" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlWalls"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 30%">
                                <label class="iceLable">
                                    Floor</label>
                            </td>
                            <td align="left" class="iceNormalText">
                                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlFloor"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                    <asp:Button ID="lnkLandInfoResOFF" runat="server" Visible="false" Text="Change Request"
                        CssClass="icebutton" Width="120px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="Dwelling"
                        OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="StatusLandInfoResOFF" runat="server" Style="text-decoration: blink;
                        color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
            <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Dwelling" runat="server" />
        </table>
    </fieldset>
    <asp:GridView ID="grdLandInfoRespondentsOff" runat="server" CssClass="gridStyle"
        CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
        Width="100%" OnRowCommand="grdLandInfoRespondentsOff_RowCommand" AllowPaging="True"
        OnPageIndexChanging="grdLandInfoRespondentsOff_PageIndexChanging">
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
            <asp:BoundField DataField="dwellingtype" HeaderText="Type of Dwelling" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NOOFROOMS" HeaderText="No. of Rooms" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%" />
            <asp:BoundField DataField="STR_TENURE" HeaderText="Type of Tenure" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TENURE" HeaderText="Tenure Years" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="RoofType" HeaderText="Roof" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="WallType" HeaderText="Walls" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="floortype" HeaderText="Floor" HeaderStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("LIVINGOFFID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("LIVINGOFFID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litRoleID" Text='<%#Eval("LIVINGOFFID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="footer">
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete?');
            }
            function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
            }                
        </script>
    </div>
</asp:Content>
