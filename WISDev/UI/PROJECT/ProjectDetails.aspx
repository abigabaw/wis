<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" UICulture="en" Culture="en-US"
    AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="WIS.ProjectDetails" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="99%">
        <tr>
            <td class="iceLable" style="width: 18%">
                Project Code <span class="mandatory">*</span>
            </td>
            <td align="left" class="iceNormalText" colspan="3">
                <asp:TextBox ID="txtProjectCode" CssClass="iceTextBox" MaxLength="10" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqeProjectCode" ControlToValidate="txtProjectCode"
                    ErrorMessage="Enter Project Code" Display="None" ValidationGroup="ProjectDet"
                    runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="fteProjectCode" FilterType="UppercaseLetters,LowercaseLetters,Numbers"
                    TargetControlID="txtProjectCode" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Project Name <span class="mandatory">*</span>
            </td>
            <td align="left" class="iceNormalText" colspan="3">
                <asp:TextBox ID="txtProjectName" CssClass="iceTextBox" MaxLength="100" Width="350px"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProjectName"
                    ErrorMessage="Enter Project Name" Display="None" ValidationGroup="ProjectDet"
                    runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                    ValidChars=" .-()" TargetControlID="txtProjectName" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td class="iceLable" style="vertical-align: top">
                Objective
            </td>
            <td align="left" class="iceNormalText" colspan="3">
                <asp:TextBox ID="txtObjective" CssClass="iceTextBox" TextMode="MultiLine" Rows="3"
                    MaxLength="2000" Width="90%" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlSegments" runat="server">
        <fieldset class="icePnlinner" style="width: 96%">
            <legend>Segments</legend>
            <div id="divAll">
                <div align="center" class="CSSTableGenerator" style="width: 100%">
                    <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
                        <asp:GridView ID="grdSegmentDetails" runat="server" CssClass="gridStyle" CellPadding="4"
                            CellSpacing="1" GridLines="Both" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                            ShowFooter="false" OnPageIndexChanging="grdSegmentDetails_PageIndexChanging"
                            OnRowCommand="grdSegmentDetails_RowCommand" OnRowDataBound="grdSegmentDetails_RowDataBound">
                            <RowStyle CssClass="gridRowStyle" />
                            <AlternatingRowStyle CssClass="gridAlternateRow" />
                            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                            <HeaderStyle CssClass="gridHeaderStyle" />
                            <FooterStyle VerticalAlign="Top" />
                            <Columns>
                                <%--Serial Number--%>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--ProjectSegmentID--%>
                                <asp:TemplateField HeaderText="Project SegmentID" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectSegmentID" runat="server" Text='<%# Eval("ProjectSegmentID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Segment Name--%>
                                <asp:TemplateField HeaderText="Segment Name">
                                    <ItemStyle HorizontalAlign="Left" Width="18%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSegmentName" runat="server" Text='<%# Eval("SegmentName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Route Length--%>
                                <asp:TemplateField HeaderText="Route Length (KM)">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRouteLength" runat="server" Text='<%# Eval("RouteLength")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Type of Line(KV)--%>
                                <asp:TemplateField HeaderText="Type of Line(KV)" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLineTypeID" runat="server" Text='<%# Eval("LineTypeID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type of Line(KV)">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLineTypeName" runat="server" Text='<%# Eval("TypeofLine")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Right Of Way (Metres)--%>
                                <asp:TemplateField HeaderText="Right Of Way (Metres)">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRightofwaymeasurement" runat="server" Text='<%# Eval("RightOfWay")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Wayleave (Metres)--%>
                                <asp:TemplateField HeaderText="Wayleave">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWayleavemeasurement" runat="server" Text='<%# Eval("WayLeave")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Estimated Budget (Million USH)--%>
                                <asp:TemplateField HeaderText="Estimated Budget">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstBudget" runat="server" Text='<%# Eval("EstBudget","{0:N0}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Implementation Period--%>
                                <asp:TemplateField HeaderText="Implementation Period">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblImplementationPeriod" runat="server" Text='<%# Eval("ImplementationPeriod")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Construction Start Date--%>
                                <asp:TemplateField HeaderText="Construction Start Date">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblConstrStartDate" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Construction End Date--%>
                                <asp:TemplateField HeaderText="Construction End Date">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblConstrEndDate" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Funder" HeaderText="Funder" />
                                <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                <asp:TemplateField HeaderText="Value of House">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblValHouse" runat="server" Text='<%# Eval("Valueofhouse","{0:N0}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--Edit Section --%>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                                            CommandArgument='<%#Eval("ProjectSegmentID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <table width="100%" border="0">
                        <tr>
                            <td align="left" style="width: 18%">
                                <label class="iceLable">
                                    Segment Name</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" style="width: 30%">
                                <div>
                                    <asp:TextBox ID="txtSegmentName" runat="server" CssClass="icefrmDropDown" Width="300px"
                                        MaxLength="100" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                        ValidChars=" .-()" TargetControlID="txtSegmentName" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtSegmentName" runat="server" ErrorMessage="Enter Segment Name"
                                        Display="None" ValidationGroup="vgSegment" ControlToValidate="txtSegmentName"></asp:RequiredFieldValidator></div>
                            </td>
                            <td align="left" style="width: 20%">
                                <label class="iceLable">
                                    Route Length</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" style="width: 30%">
                                <asp:TextBox ID="txtRouteLength" runat="server" onkeypress="javascript:return CheckDecimal (event, this);"
                                    CssClass="iceTextBox" MaxLength="5" Width="70px" />
                                <label class="labelSuffix">
                                    (KM)</label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtRouteLength" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtRouteLength" runat="server" ErrorMessage="Enter Route Length"
                                    Display="None" ValidationGroup="vgSegment" ControlToValidate="txtRouteLength"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: top">
                                <label class="iceLable">
                                    Type of Line</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" style="vertical-align: top">
                                <asp:DropDownList ID="ddlTypeLine" runat="server" CssClass="icefrmDropDown" Width="100px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTypeLine_SelectedIndexChanged">
                                </asp:DropDownList>
                                <label class="labelSuffix">
                                    (KV)</label>
                                <asp:RequiredFieldValidator ID="rfvddlTypeLine" runat="server" ErrorMessage="Select Type of Line"
                                    InitialValue="0" Display="None" ValidationGroup="vgSegment" ControlToValidate="ddlTypeLine"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Right Of Way Measurement</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblRightofWayMeasurement" runat="server" Font-Bold="true" Text=""
                                    CssClass="iceLable" Width="50px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Way Leave Measurement</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblWayLeaveMeasurement" runat="server" Font-Bold="true" Text="" CssClass="iceLable"
                                    Width="50px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <label class="iceLable">
                                    Esimated Budget</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEstBudget" runat="server" CssClass="iceTextBox" MaxLength="18"
                                    Width="150px" onblur="CheckAmount(this);"></asp:TextBox>
                                <%--<label class="labelSuffix">(Million USH)</label>--%>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers,Custom"
                                    TargetControlID="txtEstBudget" ValidChars="," runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtEstBudget" runat="server" ErrorMessage="Enter Estimated Budget"
                                    Display="None" ValidationGroup="vgSegment" ControlToValidate="txtEstBudget"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlCurrencyTypeEstBudget" runat="server" Enabled="false" CssClass="icefrmDropDown"
                                    Width="70px" AppendDataBoundItems="true">
                                    <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Implementation Period</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtImplementationPeriod" runat="server" CssClass="iceTextBox" MaxLength="5"
                                    Width="70px" />
                                <label class="labelSuffix">
                                    months</label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Numbers"
                                    TargetControlID="txtImplementationPeriod" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtImplementationPeriod" runat="server" ErrorMessage="Enter Implementation Period"
                                    Display="None" ValidationGroup="vgSegment" ControlToValidate="txtImplementationPeriod"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <label class="iceLable">
                                    Construction Start Date</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="dpConstructionStartDate" runat="server" Width="90px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caldpConstructionStartDate" runat="server" CssClass="WISCalendarStyle"
                                    TargetControlID="dpConstructionStartDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtConstructionStartDate" runat="server" ErrorMessage="Enter Construction Start Date"
                                    Display="None" ValidationGroup="vgSegment" ControlToValidate="dpConstructionStartDate"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpConstructionStartDate"
                                    ClientValidationFunction="CheckConstrSatrtDate" ErrorMessage="Construction Start Date should be in between the Project Start date and End date."
                                    ValidationGroup="vgSegment" Display="None"></asp:CustomValidator>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Construction End Date</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="dpConstructionEndDate" runat="server" Width="90px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CaldpConstructionEndDate" CssClass="WISCalendarStyle"
                                    runat="server" TargetControlID="dpConstructionEndDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtConstructionEndDate" runat="server" ErrorMessage="Enter Construction End Date"
                                    Display="None" ValidationGroup="vgSegment" ControlToValidate="dpConstructionEndDate"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpConstructionEndDate"
                                    ClientValidationFunction="CheckConstrDate" ErrorMessage="Construction End Date cannot be lesser than or equal to Construction Start Date"
                                    ValidationGroup="vgSegment" Display="None"></asp:CustomValidator>
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="dpConstructionEndDate"
                                    ClientValidationFunction="CheckConstrEndDate" ErrorMessage="Construction End Date should be in between the Project Start date and End date."
                                    ValidationGroup="vgSegment" Display="None"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="12%">
                                <label class="iceLable">
                                    Funder</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" style="width: 30%">
                                <div>
                                    <asp:TextBox ID="txtfunder" runat="server" CssClass="icefrmDropDown" Width="300px"
                                        MaxLength="100" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                        ValidChars=" " TargetControlID="txtfunder" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Funder"
                                        Display="None" ValidationGroup="vgSegment" ControlToValidate="txtfunder"></asp:RequiredFieldValidator></div>
                            </td>
                            <td align="left" width="15%">
                                <label class="iceLable">
                                    Name of Bank</label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlBank" CssClass="iceTextBox" AppendDataBoundItems="true"
                                    runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
                                    ErrorMessage="Select Name of Bank" ValidationGroup="vgSegment" InitialValue="0"
                                    ControlToValidate="ddlBank"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>
                                <asp:Label ID="lblValueofHouse" runat="server" Text="Value Of House" CssClass="iceLable"></asp:Label>
                                <span class="mandatory">*</span>
                            </td>
                            <td align="left" style="width: 30%">
                                <asp:TextBox ID="txtValueofhouse" runat="server" CssClass="iceTextBox" MaxLength="18"
                                    onblur="CheckAmount(this);"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" FilterType="Numbers,Custom"
                                    TargetControlID="txtValueofhouse" ValidChars="," runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Value Of House"
                                    Display="None" ValidationGroup="vgSegment" ControlToValidate="txtValueofhouse"></asp:RequiredFieldValidator>
                                <asp:Label ID="Label3" class="iceLable" runat="server">USH</asp:Label>
                            </td>
                            <%-- <td>
                        <asp:Label ID="lblIncludeDA" Text="Include DA" CssClass="iceLable" runat="server"></asp:Label>
                         <span class="mandatory">*</span>    
                        </td>
                        <td align="left">
                        <asp:CheckBox ID="chkincludeDA" runat="server" />
                        </td>--%>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <div>
                                    <asp:Button ID="btnADDSegment" runat="server" CssClass="icebutton" OnClientClick="DisableOnSaveWithVal(this, 'vgSegment');"
                                        UseSubmitBehavior="false" Text="Add Segment" OnClick="btnADDSegment_Click" Style="width: auto">
                                    </asp:Button>
                                    <asp:Button ID="btnSegmentClear" runat="server" CssClass="icebutton" Text="Clear Segment"
                                        OnClick="btnSegmentClear_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="vsSegment" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSegment" runat="server" />
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="99%">
        <tr>
            <td class="iceLable" style="width: 18%">
                Project Start Date
            </td>
            <td align="left" style="width: 32%">
                <asp:TextBox ID="dpProjectStartDate" runat="server" Width="90px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="caldpProjectStartDate" runat="server" CssClass="WISCalendarStyle"
                    TargetControlID="dpProjectStartDate">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td class="iceLable" style="width: 15%">
                Project End Date
            </td>
            <td align="left">
                <asp:TextBox ID="dpProjectEndDate" runat="server" Width="90px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CaldpProjectEndDate" runat="server" CssClass="WISCalendarStyle"
                    TargetControlID="dpProjectEndDate">
                </ajaxToolkit:CalendarExtender>
                <asp:CustomValidator ID="CustomValidatorProjDate" runat="server" ControlToValidate="dpProjectEndDate"
                    ClientValidationFunction="CheckProjectDate" ErrorMessage="Project End Date cannot be lesser than or equal to Project Start Date"
                    ValidationGroup="ProjectDet" Display="None"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="left" class="iceLable" style="vertical-align: top">
                Total Estimated Budget (RAP)
            </td>
            <td align="left" style="vertical-align: top">
                <asp:TextBox ID="txtTotalEstBudget" CssClass="iceTextBox" MaxLength="20" Width="150px"
                    runat="server" onblur="CheckAmountComp(this);"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                    TargetControlID="txtTotalEstBudget" ValidChars="," runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:DropDownList ID="ddlCurrencyTypeTotalEstBudget" runat="server" CssClass="icefrmDropDown"
                    Width="70px" AppendDataBoundItems="true">
                </asp:DropDownList>
                <div class="labelSuffix">
                    (Excluding Construction Cost)</div>
            </td>
            <td class="iceLable" style="vertical-align: top">
                Project Status
            </td>
            <td align="left" style="vertical-align: top">
                <asp:DropDownList ID="ddlProjectStatus" CssClass="iceTextBox" Enabled="false" runat="server">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="NEW" Selected="True">New</asp:ListItem>
                    <asp:ListItem Value="IN PROGRESS">In Progress</asp:ListItem>
                    <asp:ListItem Value="COMPLETED">Completed</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="iceLable" style="vertical-align: top">
                Labour Cost <span class="mandatory">*</span>
            </td>
            <td align="left" style="vertical-align: top">
                <asp:TextBox ID="txtLabouCost" CssClass="iceTextBox" MaxLength="20" Width="150px"
                    runat="server" onblur="CheckAmountComp(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLabouCost"
                    ErrorMessage="Enter Labour Cost" Display="None" ValidationGroup="ProjectDet"
                    runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="Numbers,Custom"
                    TargetControlID="txtLabouCost" ValidChars="," runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:Label ID="Label2" class="iceLable" runat="server">USH</asp:Label>
            </td>
            <td align="left" class="iceLable" style="vertical-align: top">
                Dollar Value<span class="mandatory">*</span>
            </td>
            <td align="left" style="vertical-align: top">
                <asp:TextBox ID="txtDollervalue" CssClass="iceTextBox" onkeypress="javascript:return CheckDecimal (event, this);"
                    MaxLength="10" Width="150px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtDollervalue"
                    ErrorMessage="Enter Doller Value" Display="None" ValidationGroup="ProjectDet"
                    runat="server"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator121" ControlToValidate="txtDollervalue"
                    ErrorMessage="Enter Doller Value" Display="None" ValueToCompare="0" Operator="GreaterThan"
                    Type="Double" ValidationGroup="ProjectDet" runat="server"></asp:CompareValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" FilterType="Numbers,Custom"
                    TargetControlID="txtDollervalue" ValidChars="." runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:Label ID="lblDoller" class="iceLable" runat="server">USH</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="iceLable" style="vertical-align: top">
                Building Materials Cost <span class="mandatory">*</span>
            </td>
            <td align="left" style="vertical-align: top" colspan="0">
                <asp:TextBox ID="txtBulMatCost" CssClass="iceTextBox" MaxLength="20" Width="150px"
                    runat="server" onblur="CheckAmountComp(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtBulMatCost"
                    ErrorMessage="Enter Building Materials Cost" Display="None" ValidationGroup="ProjectDet"
                    runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="Numbers,Custom"
                    TargetControlID="txtBulMatCost" ValidChars="," runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:Label ID="Label1" class="iceLable" runat="server">USH</asp:Label>
            </td>
            <td align="left" class="iceLable" style="vertical-align: top" nowrap>
                Percentage to make PAP as PDP <span class="mandatory">*</span>
            </td>
            <td align="left" style="vertical-align: top" colspan="0">
                <asp:TextBox ID="txtpercentage" CssClass="iceTextBox" MaxLength="4" Width="150px"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtpercentage"
                    ErrorMessage="Enter Percentage Of PAP" Display="None" ValidationGroup="ProjectDet"
                    runat="server"></asp:RequiredFieldValidator>
                <br />
                <asp:RangeValidator ID="RangeValidator1" Type="Integer" MinimumValue="1" MaximumValue="100"
                    ControlToValidate="txtpercentage" runat="server" ValidationGroup="ProjectDet"
                    ForeColor="Red" ErrorMessage="Enter only numbers between 1 and 100"></asp:RangeValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" FilterType="Numbers,Custom"
                    TargetControlID="txtpercentage" ValidChars="." runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <br />
                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save Project" runat="server"
                    OnClick="btnSave_Click" OnClientClick="DisableOnSaveWithVal(this,'ProjectDet');"
                    UseSubmitBehavior="false" />&nbsp;
                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear Project" runat="server"
                    OnClick="btnClear_Click" />
                <asp:ValidationSummary ID="valSummaryProj" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="ProjectDet" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="mandatoryText" colspan="4">
                <b>Fields marked with <span class="mandatory">*</span> are mandatory</b>.
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('MainContent_dpConstructionStartDate'));
        PreventDateFieldEntry(document.getElementById('MainContent_dpConstructionEndDate'));

        PreventDateFieldEntry(document.getElementById('MainContent_dpProjectStartDate'));
        PreventDateFieldEntry(document.getElementById('MainContent_dpProjectEndDate'));
        function CheckDecimal(e, src) {
            if (e.keyCode == 46) { // Invoke when press Enter Key
                var char = src.value;
                if (char.indexOf(".") == -1) {
                    return true;
                }
                else if (char.indexOf(".") > -1) {
                    return false;
                }
                //               
                return true;
            }
            return true;
        }

        function CheckProjectDate(oSrc, args) {
            dtProjectStart = GetCalDate('<%=dpProjectStartDate.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dpProjectEndDate.ClientID%>');

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

        function DisableOnSaveWithVal(src, Vgroup) {
            if (Page_ClientValidate(Vgroup)) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 140).toString() + "px";
        }

        function DisableOnSave(src) {
            src.disabled = true;
            src.value = 'Please Wait...';
        }

        function CheckAmount(src) {
            var amount;
            var val = RemoveComma(src.value);

            if (!isNaN(val)) {
                amount = val;
            }
            else
                amount = '';
            src.value = AddComma(amount);
        }

        function CheckAmountComp(src) {
            var amount;
            var val = RemoveComma(src.value);
            if (!isNaN(val)) {
                amount = val;
                var la = RemoveComma(document.getElementById("<%= txtLabouCost.ClientID  %>").value);
                var total = RemoveComma(document.getElementById("<%= txtTotalEstBudget.ClientID  %>").value);
                if (!isNaN(la) && !isNaN(total)) {
                    if (parseFloat(la) == 0) {
                        alert('Labour Cost cannot be Zero.');
                        amount = '';
                    }
                    else if (parseFloat(la) > parseFloat(total)) {
                        alert('Labour Cost cannot be greter than Total Estimated Budget.');
                        amount = '';
                    }
                }
            }
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

        function CheckConstrDate(oSrc, args) {
            dtConstrStart = GetCalDate('<%=dpConstructionStartDate.ClientID%>');
            dtConstrEnd = GetCalDate('<%=dpConstructionEndDate.ClientID%>');

            var ArrConstrSt = dtConstrStart.split("-");
            var ConstrStartDate = ArrConstrSt[0];
            var ConstrStartMonth = GetMonthNumber(ArrConstrSt[1]);
            var ConstrStartYear = ArrConstrSt[2];

            var ArrConstrEnd = dtConstrEnd.split("-");
            var ConstrEndDate = ArrConstrEnd[0];
            var ConstrEndMonth = GetMonthNumber(ArrConstrEnd[1]);
            var ConstrEndYear = ArrConstrEnd[2];

            if (ConstrStartYear > ConstrEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((ConstrStartYear == ConstrEndYear) && (ConstrStartMonth > ConstrEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ConstrStartYear == ConstrEndYear) && (ConstrStartMonth == ConstrEndMonth) && (ConstrStartDate >= ConstrEndDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        function CheckConstrSatrtDate(oSrc, args) {
            dtProjectStart = GetCalDate('<%=dpProjectStartDate.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dpProjectEndDate.ClientID%>');
            dtConstrStart = GetCalDate('<%=dpConstructionStartDate.ClientID%>');

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

        function CheckConstrEndDate(oSrc, args) {
            dtProjectStart = GetCalDate('<%=dpProjectStartDate.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dpProjectEndDate.ClientID%>');
            dtConstrEnd = GetCalDate('<%=dpConstructionEndDate.ClientID%>');

            var ArrProjSt = dtProjectStart.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("-");
            var ProjEndDate = ArrProjEnd[0];
            var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
            var ProjEndYear = ArrProjEnd[2];

            var ArrConstrEnd = dtConstrEnd.split("-");
            var ConstrEndDate = ArrConstrEnd[0];
            var ConstrEndMonth = GetMonthNumber(ArrConstrEnd[1]);
            var ConstrEndYear = ArrConstrEnd[2];

            if (ProjStartYear > ConstrEndYear) {
                args.IsValid = false;
                return;
            }
            else if (ProjEndYear < ConstrEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ConstrEndYear) && (ProjStartMonth > ConstrEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ConstrEndYear) && (ProjStartMonth == ConstrEndMonth) && (ProjStartDate > ConstrEndDate)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjEndYear == ConstrEndYear) && (ProjEndMonth < ConstrEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjEndYear == ConstrEndYear) && (ProjEndMonth == ConstrEndMonth) && (ProjEndDate < ConstrEndDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        spnContainer = document.getElementById('<%=dpProjectStartDate.ClientID%>');
        if (spnContainer != null) {
            inputElems = spnContainer.getElementsByTagName('input');

            for (i = 0; i < inputElems.length; i++) {
                elem = inputElems[i];

                if (elem.type == 'text') {
                    elem.readOnly = true;
                    elem.onkeydown = function Check() {
                        if (event != null) {
                            var keyCode = (event.which) ? event.which : event.keyCode;
                            if ((keyCode == 8) || (keyCode == 46))
                                event.returnValue = false;
                        };
                    }
                }
            }
        }

        spnContainer123 = document.getElementById('<%=dpProjectEndDate.ClientID%>');
        if (spnContainer123 != null) {
            inputElems1 = spnContainer123.getElementsByTagName('input');

            for (i = 0; i < inputElems1.length; i++) {
                elem1 = inputElems1[i];

                if (elem1.type == 'text') {
                    elem1.readOnly = true;
                    elem1.onkeydown = function doCheck() {
                        if (event != null) {
                            var keyCode = (event.which) ? event.which : event.keyCode;
                            if ((keyCode == 8) || (keyCode == 46))
                                event.returnValue = false;
                        };
                    }
                }
            }
        }

        spnContainer2 = document.getElementById('<%=dpConstructionStartDate.ClientID%>');
        if (spnContainer2 != null) {
            inputElems2 = spnContainer2.getElementsByTagName('input');

            for (i = 0; i < inputElems2.length; i++) {
                elem2 = inputElems2[i];

                if (elem2.type == 'text') {
                    elem2.readOnly = true;
                    elem2.onkeydown = function Check2() {
                        if (event != null) {
                            var keyCode = (event.which) ? event.which : event.keyCode;
                            if ((keyCode == 8) || (keyCode == 46))
                                event.returnValue = false;
                        };
                    }
                }
            }
        }

        spnContainer3 = document.getElementById('<%=dpConstructionEndDate.ClientID%>');
        if (spnContainer3 != null) {
            inputElems3 = spnContainer3.getElementsByTagName('input');

            for (i = 0; i < inputElems3.length; i++) {
                elem3 = inputElems3[i];

                if (elem3.type == 'text') {
                    elem3.readOnly = true;
                    elem3.onkeydown = function Check3() {
                        if (event != null) {
                            var keyCode = (event.which) ? event.which : event.keyCode;
                            if ((keyCode == 8) || (keyCode == 46))
                                event.returnValue = false;
                        };
                    }
                }
            }
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
            var tat1 = document.getElementById("<%= txtProjectCode.ClientID  %>");
            var tat2 = document.getElementById("<%= txtProjectName.ClientID  %>");
            var tat3 = document.getElementById("<%= txtLabouCost.ClientID  %>");
            var tat4 = document.getElementById("<%= txtSegmentName.ClientID  %>");
            var tat5 = document.getElementById("<%= txtRouteLength.ClientID  %>");
            var tat6 = document.getElementById("<%= txtEstBudget.ClientID  %>");
            var tat7 = document.getElementById("<%= txtImplementationPeriod.ClientID  %>");
            var tat8 = document.getElementById("<%= txtfunder.ClientID  %>");

            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat4.value.toString().replace(/^\s+/, '') == ''
                && tat5.value.toString().replace(/^\s+/, '') == '' && tat6.value.toString().replace(/^\s+/, '') == ''
                && tat7.value.toString().replace(/^\s+/, '') == '' && tat8.value.toString().replace(/^\s+/, '') == ''
                && btn.value.toString() == 'Add Segment') {
                isDirty = 0;
            }
            else {
                isDirty = 1;

            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                return '';
            }
        }   
    </script>
    </table>
</asp:Content>
