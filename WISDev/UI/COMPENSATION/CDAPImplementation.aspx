<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="CDAPImplementation.aspx.cs" Inherits="WIS.CDAPImplementation" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
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
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Phase</legend>
        <table align="center" border="0" cellpadding="1" cellspacing="1" style="margin-top: 10px;
            width: 100%">
            <tr>
                <td class="iceNormalText">
                    <div style="float: right">
                        <a id="lnkUPloadDoc" href="#" runat="server"><b>Upload Document</b></a> &nbsp;|&nbsp;
                        <a id="lnkUPloadDoclist" href="#" runat="server"><b>View Document</b></a>
                    </div>
                    <script type="text/javascript" language="javascript">
                        function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                            var left = (screen.width - 800) / 2;
                            var top = (screen.height - 650) / 4;
                            open('../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPop', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                        }

                        function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                            var left = (screen.width - 800) / 2;
                            var top = (screen.height - 650) / 4;
                            open('../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPoplist', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                        }                  
                    </script>
                </td>
            </tr>
        </table>
        <table border="0" width="90%" align="left" style="margin-top: 4px">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Phase</label><span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlPhase" runat="server" CssClass="iceTextBox">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                    </asp:DropDownList>
                                                      
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlPhase"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="reqddlPhase" runat="server" ErrorMessage="Select Phase"
                        InitialValue="0" ControlToValidate="ddlPhase" Display="None" ValidationGroup="CDAPImplPhase"></asp:RequiredFieldValidator>
                </td>
                <td>
                <label class="iceLable">Expenditure</label>
                </td>
                <td>
                 <asp:TextBox ID="txtExpenditure" CssClass="iceTextBox" MaxLength="20" Width="100px"
                    runat="server" onblur="CheckAmount(this);"></asp:TextBox>
                <label class="labelSuffix">
                    (Million USH)</label>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                    TargetControlID="txtExpenditure" ValidChars="," runat="server"/>
                </td>
                </tr>
                <tr>
                <td align="left">
                    <label class="iceLable">
                        Period From</label><span class="mandatory">*</span>
                </td>
                <td align="left">
                <asp:TextBox ID="dptxtPeriodFrom" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldptxtPeriodFrom" CssClass="WISCalendarStyle" runat="server" TargetControlID="dptxtPeriodFrom"></ajaxToolkit:CalendarExtender>
             
                    <asp:RequiredFieldValidator ID="reqdptxtPeriodFrom" runat="server" ErrorMessage="Enter Period From"
                        ControlToValidate="dptxtPeriodFrom" Display="None" ValidationGroup="CDAPImplPhase"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Period To</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                <asp:TextBox ID="dptxtPeriodTo" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldptxtPeriodTo"  CssClass="WISCalendarStyle" runat="server" TargetControlID="dptxtPeriodTo"></ajaxToolkit:CalendarExtender>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Period To"
                        ControlToValidate="dptxtPeriodTo" Display="None" ValidationGroup="CDAPImplPhase"></asp:RequiredFieldValidator>
   <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dptxtPeriodTo"
                        ClientValidationFunction="CheckPhaseDate" ErrorMessage="Period End Date cannot be lesser than or equal to Period Start Date"
                        ValidationGroup="CDAPImplPhase" Display="None">
                    </asp:CustomValidator>


              
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnSavePhase" runat="server" Text="Save Phase" CssClass="icebutton"
                        OnClick="btnSavePhase_Click" ValidationGroup="CDAPImplPhase" />
                    &nbsp;
                    <asp:Button ID="btnClearPhaseData" runat="server" Text="Clear Phase" OnClick="btnClearPhase_Click"
                        CssClass="icebutton" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                        ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                        ValidationGroup="CDAPImplPhase" />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:GridView ID="grdPhases" runat="server" CssClass="gridStyle" CellPadding="4"
                        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdPhases_RowCommand"
                        AllowPaging="True" OnPageIndexChanging="grdPhases_PageIndexChanging" OnRowDataBound="grdPhases_RowDataBound">
                        <RowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                        <HeaderStyle CssClass="gridHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Phase" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkProjectCode" CommandName="EditPhase" CommandArgument='<%#Eval("Cdap_phaseid") %>'
                                        Text='<%#Eval("Cdap_phaseno") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EXPENDITURE" HeaderText="Expenditure" DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center" >
                             <ItemStyle HorizontalAlign="Right" Width="7%" />
                            </asp:BoundField >
                           
                            <asp:TemplateField HeaderText="Period From" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Literal ID="litPeriodFrom" Text="" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Period To" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Literal ID="litPeriodTo" Text="" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                        CommandName="EditRow" CommandArgument='<%#Eval("Cdap_phaseid") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                        CommandName="DeleteRow" CommandArgument='<%#Eval("Cdap_phaseid") %>' OnClientClick="return DeleteRecord();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            There are no Phases available for the selected Project.
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="pnlActivity" Visible="false" runat="server">
        <fieldset class="icePnlinner">
            <legend>
                <asp:Label ID="lblLegendActivity" Text="Activity Planning" runat="server"></asp:Label></legend>
            <table width="100%" border="0">
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="lnkbtnshow" Text="Add New Activity" OnClick="lnkbtnshow_click"
                            runat="server"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phActivity" runat="server" Visible="false">
                <table align="center" border="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left" width="17%">
                            <label class="iceLable">
                                Activity</label>
                        </td>
                        <td align="left" width="38%">
                            <asp:DropDownList ID="ddlActivity" runat="server" CssClass="iceTextBox" Width="96%">
                            </asp:DropDownList>
                             <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlActivity"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                            <asp:TextBox ID="txtCurrentPhase" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqddlActivity" runat="server" ErrorMessage="Select Activity"
                                ControlToValidate="ddlActivity" Display="None" InitialValue="0" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left" width="10%">
                            <label class="iceLable">
                                District</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left" class="iceNormalText">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="iceTextBox" Width="200px"
                                AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">                    
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                        TargetControlID="ddlDistrict"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                   
                            <asp:RequiredFieldValidator ID="reqddlDistrict" runat="server" ErrorMessage="Select District"
                                ControlToValidate="ddlDistrict" Display="None" InitialValue="0" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                County</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left" class="iceNormalText">
                            <asp:DropDownList ID="ddlCounty" runat="server" AppendDataBoundItems="true" CssClass="iceTextBox"
                                Width="200px" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                                     <ajaxToolkit:ListSearchExtender id="ListSearchExtender4" runat="server"
                        TargetControlID="ddlCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </td>
                        <td align="left" style="vertical-align: top">
                            <label class="iceLable">
                                Sub County</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left" class="iceNormalText" style="vertical-align: top">
                            <asp:DropDownList ID="ddlSubCounty" runat="server" CssClass="iceTextBox" AppendDataBoundItems="true"
                                Width="200px" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: top" colspan="4">
                            <label class="iceLable">
                                Villages:</label> &nbsp;&nbsp; <asp:CheckBox ID="chkAllVillages" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkAllVillages_CheckedChanged" /><br />
                            <asp:Panel ID="pnlchkboxVillage" runat="server" Height="200px" ScrollBars="Auto"
                                Width="96%">
                                <asp:CheckBoxList ID="chkboxVillage" runat="server" CssClass="iceTextBox" RepeatColumns="4"
                                    RepeatDirection="Horizontal" Height="50px" Width="100%" OnSelectedIndexChanged="chkboxVillage_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: top">
                            <label class="iceLable">
                                Activity Details</label>
                        </td>
                        <td align="left" style="vertical-align: top" colspan="3">
                            <asp:TextBox ID="txtActivityDetails" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                                Style="width: 600px">
                            </asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="reqtxtActivityDetails" runat="server" ErrorMessage="Enter Activity Details"
                                ControlToValidate="txtActivityDetails" Display="None" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: top">
                            <label class="iceLable">
                                Mode of Implementation</label>
                        </td>
                        <td align="left" style="vertical-align: top" colspan="3">
                            <asp:TextBox ID="txtModeImplementation" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                                Style="width: 600px">
                            </asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="reqtxtModeImplementation" runat="server" ErrorMessage="Enter Mode of Implementation"
                                ControlToValidate="txtModeImplementation" Display="None" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: top">
                            <label class="iceLable">
                                Challenges</label>
                        </td>
                        <td align="left" style="vertical-align: top" colspan="3">
                            <asp:TextBox ID="txtChallenges" runat="server" CssClass="iceTextBox" Style="width: 600px"
                                TextMode="MultiLine">
                            </asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="reqtxtChallenges" runat="server" ErrorMessage="Enter Challenges"
                                ControlToValidate="txtChallenges" Display="None" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                Date From</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                        <asp:TextBox ID="dpDateFrom" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpDateFrom" CssClass="WISCalendarStyle" runat="server" TargetControlID="dpDateFrom"></ajaxToolkit:CalendarExtender>
                        
                            <asp:RequiredFieldValidator ID="reqdpDateFrom" runat="server" ErrorMessage="Enter Date From"
                                ControlToValidate="dpDateFrom" Display="None" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <label class="iceLable">
                                Date To</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                        <asp:TextBox ID="dpDateTo" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpDateTo" CssClass="WISCalendarStyle" runat="server" TargetControlID="dpDateTo"></ajaxToolkit:CalendarExtender>
                       
                            <asp:RequiredFieldValidator ID="reqdpDateTo" runat="server" ErrorMessage="Enter Date To"
                                ControlToValidate="dpDateTo" Display="None" ValidationGroup="CDAPImplementation"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpDateTo"
                                ClientValidationFunction="CheckActivityPeriod" ErrorMessage="Phase Activity Planning To Date cannot be lesser than or equal to Phase Activity Planning From Date"
                                ValidationGroup="CDAPImplementation" Display="None"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="lblComments" runat="server" Text="Comments" CssClass="iceLable">
                            </asp:Label>
                        </td>
                        <td style="vertical-align: top" colspan="3">
                            <asp:TextBox ID="CommentsTextBox" runat="server" CssClass="iceTextBox" TextMode="MultiLine" Height="44px"
                                Style="width: 600px">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" " TargetControlID="CommentsTextBox" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <div class="iceLable">
                                <b>PAPs Involved:</b> <asp:Label ID="lblNoofPAPS" runat="server" Font-Bold="false" Text="" ></asp:Label></div>
                            <br />
                            <div align="center" class="CSSTableGenerator">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" class="gridHeaderStyle" style="width: 100px;" >
                                            Sl. No.
                                        </td>
                                        <td align="center" class="gridHeaderStyle" style="width: 100px;">
                                            Select
                                        </td>
                                        <td align="center" class="gridHeaderStyle" style="width: 120px;">
                                            HHID
                                        </td>
                                        <td align="center" class="gridHeaderStyle" style="width: 250px;">
                                            Name
                                        </td>
                                        <td style="width: 20px; background-color:transparent">
                                            <asp:Label ID="lbl" runat="server" ForeColor="Transparent" >&nbsp;</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Panel ID="pnlPaps" runat="server" Width="590px" ScrollBars="Vertical">
                                                <asp:GridView ID="dgPapsInvolved" runat="server" CssClass="gridRowStyle" AllowPaging="false"
                                                    ShowHeader="false" Width="570px" DataKeyNames="HHID" AllowSorting="true" AutoGenerateColumns="false"
                                                    OnPageIndexChanging="dgPapsInvolved_PageIndexChanging">
                                                    <RowStyle CssClass="gridRowStyle" />
                                                    <PagerStyle CssClass="gridPagerStyle" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="gridHeaderStyle" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No." HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                            <ItemStyle HorizontalAlign="Center"  />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="HHID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-Width="120px" />
                                                        <asp:BoundField DataField="PAPNAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="250px" ItemStyle-Width="250px" />
                                                    </Columns>
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        No PAPS available in the selected villages
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                ValidationGroup="CDAPImplementation" />
                            &nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                            <asp:ValidationSummary ID="valsumCDAPImplementation" runat="server" ShowSummary="false"
                                ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                                ValidationGroup="CDAPImplementation" />
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
            <br />
            <br />
            <div align="center" class="CSSTableGenerator">
                <asp:Repeater ID="rptDetails" runat="server" OnItemCommand="rptDetails_ItemCommand"
                    OnItemDataBound="rptDetails_ItemDataBound">
                    <HeaderTemplate>
                        <table border="0" cellspacing="0" cellpadding="3" style="border-color: #336699" width="100%">
                            <tr>
                                <%--<td width="4%" rowspan="2" style="background-color: #336699; color: #FFFFFF;">
                                    Phase
                                </td>--%>
                                <%--<td colspan="2" style="background-color: #336699; color: #FFFFFF;">
                                    Period
                                </td>--%>
                                <td rowspan="2" style="background-color: #336699; color: #FFFFFF;">
                                    Activity
                                </td>
                                <td colspan="3" style="background-color: #336699; color: #FFFFFF;">
                                    Area
                                </td>
                                <td rowspan="2" style="background-color: #336699; color: #FFFFFF;" width="4%">
                                    <asp:Literal ID="ltEdit" runat="server" Text="Edit"></asp:Literal>
                                </td>
                                <td rowspan="2" style="background-color: #336699; color: #FFFFFF;" width="5%">
                                    Details
                                </td>
                            </tr>
                            <tr>
                                <%--<td width="7%">
                                    From
                                </td>--%>
                                <%--<td width="7%">
                                    To
                                </td>--%>
                                <td width="20%">
                                    District
                                </td>
                                <td width="20%">
                                    County
                                </td>
                                <td width="20%">
                                    Sub County
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="gridRowStyle">
                            <%--<td>
                                <%#DataBinder.Eval(Container, "DataItem.CDAP_PHASENO")%>
                            </td>--%>
                            <%--<td>
                                <%#DataBinder.Eval(Container, "DataItem.PeriodFrom", "{0:MM/dd/yyyy}")%>
                            </td>--%>
                            <%--<td>
                                <%#DataBinder.Eval(Container, "DataItem.PeriodTo", "{0:MM/dd/yyyy}")%>
                            </td>--%>
                            <td align="left">
                                <%#DataBinder.Eval(Container, "DataItem.CDAP_ACTIVITYNAME")%>
                            </td>
                            <td align="left">
                                <%#DataBinder.Eval(Container, "DataItem.DISTRICT")%>
                            </td>
                            <td align="left">
                                <%#DataBinder.Eval(Container, "DataItem.COUNTY")%>
                            </td>
                            <td align="left">
                                <%#DataBinder.Eval(Container, "DataItem.SUBCOUNTY")%>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                    CommandName="EditRow" CommandArgument='<%#Eval("CDAP_PHASEACTIVITYID") %>' runat="server" />
                            </td>
                            <td>
                                <a href="#" onclick="ShowHideDetails('trDetails<%# Eval("CDAP_PHASEACTIVITYID") %>');">
                                    View</a>
                            </td>
                        </tr>
                        <tr id="trDetails<%# Eval("CDAP_PHASEACTIVITYID") %>" class="gridRowStyle" style="display: none">
                            <td colspan="10" align="left">
                                <label class="iceLable">
                                    Date From:
                                </label>
                                <asp:Literal ID="litActivityDateFrom" Text="" runat="server"></asp:Literal>
                                &nbsp; &nbsp;
                                <label class="iceLable">
                                    Date To:
                                </label>
                                <asp:Literal ID="litActivityDateTo" Text="" runat="server"></asp:Literal>
                                <br />
                                <br />
                                <label class="iceLable">
                                    Villages:
                                </label>
                                <%#Eval("Village")%>
                                <br />
                                <br />
                                <label class="iceLable">
                                    Persons Involved:
                                </label>
                                <asp:Literal ID="ltrPapName" runat="server" Text=''>
                                </asp:Literal>
                                
                                <br />
                                <br />
                                <label class="iceLable">
                                    Activity Details:
                                </label>
                                <%#Eval("Activitydetails")%><br />
                                <br />
                                <label class="iceLable">
                                    Mode of Implementation:
                                </label>
                                <%#Eval("Modeofimplementation")%><br />
                                <br />
                                <label class="iceLable">
                                    Challenges:
                                </label>
                                <%#Eval("Challenges")%>
                                <br /><br />
                                <label class="iceLable">
                                    Comments:
                                </label>
                                <%#Eval("Comments")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </fieldset>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=dptxtPeriodFrom.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=dptxtPeriodTo.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=dpDateFrom.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=dpDateTo.ClientID%>'));

        function CheckPhasePeriod(oSrc, args) {
            dtProjectStart = GetCalDate('<%=dptxtPeriodFrom.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dptxtPeriodTo.ClientID%>');

            var ArrProjSt = dtProjectStart.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("-");
            var ProjEndDate = ArrProjEnd[0];
            var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
            var ProjEndYear = ArrProjEnd[2];

            if (ProjStartYear > ProjEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth > ProjEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth == ProjEndMonth) && (ProjStartDate >= ProjEndDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        function CheckActivityPeriod(oSrc, args) {
            dtProjectStart = GetCalDate('<%=dpDateFrom.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dpDateTo.ClientID%>');

            var ArrProjSt = dtProjectStart.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("-");
            var ProjEndDate = ArrProjEnd[0];
            var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
            var ProjEndYear = ArrProjEnd[2];

            if (ProjStartYear > ProjEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth > ProjEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth == ProjEndMonth) && (ProjStartDate >= ProjEndDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        function ShowHideDetails(targt) {
            targetElem = document.getElementById(targt);

            targetElem.style.display == 'none' ? targetElem.style.display = '' : targetElem.style.display = 'none';
        }

        function ShowHideActivities() {
            var lnk = document.getElementById('lnkbtnshow');
            if (lnk.value == 'Add New Activity') {
                document.getElementById('spnActivity').style.display = '';
                lnk.value = 'Hide Activity';
            }
            else {
                document.getElementById('spnActivity').style.display = 'none';
                lnk.value == 'Add New Activity';
            }
        }

        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Phase?');
        }
        function CheckPhaseDate(oSrc, args) {
            dtPhaseStart = GetCalDate('<%=dptxtPeriodFrom.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dptxtPeriodTo.ClientID%>');

            var ArrPhaseStartDt = dtPhaseStart.split("-");
            var PhaseStartDt = ArrPhaseStartDt[0];
            var PhaseStartMonth = GetMonthNumber(ArrPhaseStartDt[1]);
            var PhaseStartYear = ArrPhaseStartDt[2];

            var ArrPhaseEndDt = dtProjectEnd.split("-");
            var PhaseEndDt = ArrPhaseEndDt[0];
            var PhaseEndMonth = GetMonthNumber(ArrPhaseEndDt[1]);
            var PhaseEndYear = ArrPhaseEndDt[2];

            if (PhaseStartYear > PhaseEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((PhaseStartYear == PhaseEndYear) && (PhaseStartMonth > PhaseEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((PhaseStartYear == PhaseEndYear) && (PhaseStartMonth == PhaseEndMonth) && (PhaseStartDt >= PhaseEndDt)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
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
</asp:Content>
