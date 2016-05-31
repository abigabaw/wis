<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HouseholdRelation.aspx.cs" Inherits="WIS.HouseholdRelation" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc4" %>
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
    <script language="javascript" type="text/javascript">

        function OpenHolderTypeDetails(id) {
            var left = (screen.width - 1200) / 2;
            var top = (screen.height - 700) / 4;
            window.open('HolderTypeDetails.aspx?id=' + id, "winHolder", 'width=760px,height=600px,resizable=0,scrollbars=1,toolbar=no,menubar=no,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc4:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    
    <fieldset class="icePnlinner" style="width: 96%;">
        <legend>Household Relations</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
        <br />
        <br />
        <asp:UpdatePanel ID="updRelations" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdRelations" runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdRelations_RowDataBound">
                    <RowStyle CssClass="gridRowStyle" />
                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="gridHeaderStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Holder Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <a id="lnkHolderTypeDetails" href="#" runat="server">
                                    <%#Eval("HolderTypeName")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HolderCount" HeaderText="No. of people in Household" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25%" />
                        <asp:BoundField DataField="AffectedHolderCount" HeaderText="No. of people on the Affected Land"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25%" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLoadRelations" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="btnLoadRelations" Style="display: none" runat="server" OnClick="btnLoadRelations_Click" />
    </fieldset>
    <fieldset class="icePnlinner" style="margin-top: 15px; width: 96%">
        <legend>Services on Affected Plot</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy2" runat="server" /></td></tr></table>
    </div>
        <table width="100%">
            <tr>
                <td>
                    <label class="labelSuffix" style="text-align: right">
                        (Check if YES)</label>
                </td>
            </tr>
        </table>
        <asp:DataList ID="lstServices" ItemStyle-Width="50%" CellPadding="4" CellSpacing="1"
            RepeatDirection="Horizontal" RepeatColumns="2" runat="server" Width="98%" OnItemDataBound="lstServices_ItemDataBound">
            <ItemStyle CssClass="iceLable" />
            <ItemTemplate>
                <asp:Literal ID="litServiceID" Text='<%#Eval("ServiceID")%>' Visible="false" runat="server"></asp:Literal>
                <asp:Label ID="lblServiceName" Text='<%#Eval("ServiceName")%>' Width="300px" runat="server"></asp:Label>
                <asp:CheckBox ID="chkServiceType" runat="server" />
                <asp:TextBox ID="txtServiceType" Visible="false" MaxLength="100" runat="server" ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                    ValidChars=", " TargetControlID="txtServiceType" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </ItemTemplate>
        </asp:DataList>
        <table style="margin-top: 10px" width="98%">
            <tr>
                <td align="center">
                    <asp:Button ID="lnkServiceAffected" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSaveService" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSaveService_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearService" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClearService_Click" />
                    &nbsp;&nbsp;
                    <asp:Label ID="StatusService" runat="server" Style="text-decoration: blink; color: Red;
                        font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset class="icePnlinner" style="margin-top: 15px; width: 96%">
        <legend>Affected Land users on the Affected Plot of Land</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy3" runat="server" /></td></tr></table>
    </div>
     <div id="divAll">
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="90%">
            <tr>
                <td align="left" style="width: 22%">
                    <label class="iceLable">
                        Affected Land User's Name</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" style="width: 30%">
                    <asp:TextBox ID="txtAffecLandUserName" CssClass="iceTextBox" MaxLength="250" runat="server">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteAffecLandUserName" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" '" TargetControlID="txtAffecLandUserName" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAffecLandUserName"
                        Display="None" ErrorMessage="Enter Affected Land User Name" ValidationGroup="LandUser"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="left" style="width: 15%">
                    <label class="iceLable">
                        Status</label><span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlStatus" CssClass="iceTextBox" AppendDataBoundItems="true"
                        runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlStatus"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="reqStatus" ControlToValidate="ddlStatus" InitialValue="0"
                        Display="None" ErrorMessage="Select a Status" ValidationGroup="LandUser" runat="server">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Related To</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtRelatedTo" CssClass="iceTextBox" MaxLength="250" runat="server">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteRelatedTo" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" '" TargetControlID="txtRelatedTo" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Time on Land</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtTimeOnLand" CssClass="iceTextBox" MaxLength="3" Width="90px"
                        runat="server">
                    </asp:TextBox>
                    Years
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteTimeOnLand" FilterType="Numbers" TargetControlID="txtTimeOnLand"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="lnkChangeRequest" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSaveLandUser" CssClass="icebutton" Text="Save" ValidationGroup="LandUser"
                        runat="server" OnClick="btnSaveLandUser_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearLandUser" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClearLandUser_Click" />

                     &nbsp;&nbsp;  
                    <asp:Label ID="StatusLandUser" runat="server" Style="text-decoration: blink; color: Red;
                        font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="vsLandUser" DisplayMode="BulletList" ShowMessageBox="true"
            ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="LandUser"
            runat="server" />
        <br />
         
        <div align="left" class="CSSTableGenerator">
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
            <asp:GridView ID="grdAffectedLandUsers" runat="server" CssClass="gridStyle" CellPadding="4"
                AllowPaging="true" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
                Width="100%" OnPageIndexChanging="grdAffectedLandUsers_PageIndexChanging" OnRowCommand="grdAffectedLandUsers_RowCommand"
              >
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
                    <asp:BoundField DataField="LandUserName" HeaderText="Affected Land User's Name" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="StatusName" HeaderText="Status" HeaderStyle-Width="20%"
                        HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="RelatedTo" HeaderText="Related To" HeaderStyle-Width="20%"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="TimeOnLand" HeaderText="Time on Land (months)" HeaderStyle-Width="15%"
                        HeaderStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("LandUserID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("LandUserID") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        </div>
        
          </div>        

    </fieldset>
  
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this record?');
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 130).toString() + "px";
        }

        function RefreshHouseholdRelationsList() {
            $get('<%=btnLoadRelations.ClientID%>').click();
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
            var btn = document.getElementById("<%= btnSaveLandUser.ClientID  %>");
            var tat1 = document.getElementById("<%= txtAffecLandUserName.ClientID  %>");
           
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == ''  && btn.value.toString() == 'Save') {
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
