<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="OptionSummary.aspx.cs" Inherits="WIS.OptionSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner" style="width: 850px">
        <legend align="left">Summary Reports</legend>
        <table width="820px" align="center" border="0" style="height: 52px">
            <tr>
                <td align="left" style="width: 500px;">
                    <label class="iceLable">
                        Select Project Name
                    </label>
                </td>
                <td style="width: 300px;">
                    <label class="iceLable">
                        Select Legacy Report Type
                    </label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" AppendDataBoundItems="true"
                        Width="500px" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlProject" runat="server" ControlToValidate="ddlProject"
                        ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Project to search Paps"
                        Display="None">
                    </asp:RequiredFieldValidator>
                </td>
                
                <td>
                    <asp:DropDownList ID="ddlReportType" CssClass="iceTextBox" AppendDataBoundItems="true"
                        Width="300px" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <%-- Edwin Baguma: Static List Items removed   --%>
                        
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="ddlReportTypeValidator" runat="server" ControlToValidate="ddlReportType"
                        ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Report to Retrieve"
                        Display="None">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <%--
            <tr>
            <td colspan="4">
            <div id="divDates" runat="server">
            <table width="100%"><tr>
                <td align="left" style="width: 100px;">
                    <label class="iceLable">
                        From Date</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="opsStartDate" runat="server" Width="90px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calopsStartDate" runat="server" CssClass="WISCalendarStyle"
                        TargetControlID="opsStartDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td style="width: 100px;">
                    <label class="iceLable">
                        To Date</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="opsEndDate" runat="server" Width="90px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalopsEndDate" runat="server" CssClass="WISCalendarStyle"
                        TargetControlID="opsEndDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                </tr></table>
            </div>
            </td>
            </tr>

            
            <tr>
                <td align="center" colspan="4">
                    <span id="spBatch" runat="server" style="display: none;">
                        <asp:CheckBox Checked="true" runat="server" ID="chkHide" Text="Summary" />&nbsp;&nbsp;
                        <asp:CheckBox Checked="false" runat="server" ID="chkShow" Text="Detail" />
                        &nbsp;&nbsp; <span id="SpBachDetails" runat="server" style="display: none;">
                            <label class="iceLable">
                                Batch No</label>
                            <asp:TextBox ID="txtBatchNo" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                FilterType="Numbers" ValidChars="" TargetControlID="txtBatchNo" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="iceLable">
                                HHID</label>
                            <asp:TextBox ID="txtHHID" runat="server" MaxLength="10">
                            </asp:TextBox>&nbsp;<asp:ImageButton ID="imgSearch"  runat="server" ValidationGroup="vgSearch" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                    ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
                    <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSearch" runat="server" /><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"
                                FilterType="Numbers" ValidChars="" TargetControlID="txtHHID" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp;&nbsp;</span> </span>
                    <span id="spClosing" runat="server" style="display: none;">
                        <asp:RadioButtonList ID="rblClosing" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static">
                            <asp:ListItem Text="Option Group" Selected="True" Value="OPT" ></asp:ListItem>
                            <asp:ListItem Text="Amount" Value="AMT" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </span>
                </td>
            </tr>
            --%>

            <tr>
                <td colspan="4" align="left">
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('MainContent_opsStartDate'));
        PreventDateFieldEntry(document.getElementById('MainContent_opsEndDate'));
        function OpenReport() {
            var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');
            var ReportType = document.getElementById('<%=ddlReportType.ClientID%>');

            var ProjectID;
            var ProjectName;
            var reportCode;


            if (fldprojectID.selectedIndex > 0) {
                ProjectID = fldprojectID.options[fldprojectID.selectedIndex].value;
                ProjectName = fldprojectID.options[fldprojectID.selectedIndex].text.toString();
            }
            else {
                alert('Please select a Project Name');
                return;
            }

            if (ReportType.selectedIndex > 0) {
                reportCode = ReportType.options[ReportType.selectedIndex].value;
            }
            else {
                alert('Please select a Report Type');
                return;
            }

            <%--
            startdate = GetCalDate('<%=opsStartDate.ClientID%>');
            Enddate = GetCalDate('<%=opsEndDate.ClientID%>');
            var BatchNo ='';
            var Hhid = '';
            var rptStatus = '';
            status = document.getElementById('<%=chkHide.ClientID%>').checked;
            if (status.toString() == 'true') {
                rptStatus = 'summary';
            }
            else {
                rptStatus = 'Detail';
                BatchNo = document.getElementById('<%=txtBatchNo.ClientID%>').value;
                Hhid = document.getElementById('<%=txtHHID.ClientID%>').value;
            }
            if (reportCode == 'PC') {
                var list = document.getElementById("<% =rblClosing.ClientID %>"); //Cleint ID of RadioButtonList
                var rdbtnLstValues = list.getElementsByTagName("input");
                var Checkdvalue;
                for (var i = 0; i < rdbtnLstValues.length; i++) {
                    if (rdbtnLstValues[i].checked) {
                        Checkdvalue = rdbtnLstValues[i];
                        break;
                    }
                }
//                if (Checkdvalue) {
//                    alert(Checkdvalue.value);
//                }
                rptStatus = Checkdvalue.value;
            }--%>

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'workflowCode=Legacy&reportID=' + reportCode +
            '&ProjectID=' + ProjectID;

            //'&ProjectNmae=' + ProjectNmae +
            //'&opsStartDate=' + startdate +
            //'&opsEndDate=' + Enddate +
            //'&BatchNo=' + BatchNo +
            //'&Hhid=' + Hhid +
            //'&Status=' + rptStatus;

            window.open('RptViewer.aspx?' + param, 'WIS Report Viewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left );
        }

        
        <%--function ddlReportType_IndexChanged(src) {
            rpttype = src.options[src.selectedIndex].value;
            document.getElementById('<%=divDates.ClientID%>').style.display = '';
            if (rpttype.toString() == 'CFBS') {
                document.getElementById('<%=spBatch.ClientID%>').style.display = '';
                //document.getElementById('spBatch').style.display = '';
                document.getElementById('<%=chkShow.ClientID%>').checked = false;
                document.getElementById('<%=chkHide.ClientID%>').checked = true;
                document.getElementById('<%=spClosing.ClientID%>').style.display = 'none';
            }
            else if (rpttype.toString() == 'PC') {
                document.getElementById('<%=divDates.ClientID%>').style.display = 'none';
                document.getElementById('<%=spBatch.ClientID%>').style.display = 'none';
                document.getElementById('<%=spClosing.ClientID%>').style.display = '';
            }
            else {
                document.getElementById('<%=spBatch.ClientID%>').style.display = 'none';
                document.getElementById('<%=spClosing.ClientID%>').style.display = 'none';
                //document.getElementById('spBatch').style.display = 'none';
                document.getElementById('<%=chkShow.ClientID%>').checked = false;
                document.getElementById('<%=chkHide.ClientID%>').checked = false;
                if (rpttype.toString() == 'LOSNEW' || rpttype.toString() == 'NOPNEW' || rpttype.toString() == 'NOCNEW' ||
                rpttype.toString() == 'ILDNEW' || rpttype.toString() == 'SOPNEW' || rpttype.toString() == 'PDPNEW' ||
                rpttype.toString() == 'IHPDPNEW' || rpttype.toString() == 'ALSNEW' || rpttype.toString() == 'LAPNEW') {
                    document.getElementById('<%=divDates.ClientID%>').style.display = 'none';                    
                }
            }
        }--%>

        <%--
              function ShowHide(src) {
            document.getElementById('<%=chkHide.ClientID%>').checked = false;
            if (src.checked) {
                //document.getElementById('SpBachDetails').style.display = '';
                document.getElementById('<%=SpBachDetails.ClientID%>').style.display = '';
                document.getElementById('<%=txtBatchNo.ClientID%>').value = '';
                document.getElementById('<%=txtHHID.ClientID%>').value = '';
            }
            else {
                document.getElementById('<%=SpBachDetails.ClientID%>').style.display = 'none';
                //document.getElementById('SpBachDetails').style.display = 'none';
                document.getElementById('<%=txtBatchNo.ClientID%>').value = '';
                document.getElementById('<%=txtHHID.ClientID%>').value = '';
            }
        } --%>

        <%--
        function SetDefault(src) {
            if (src.checked) {
                document.getElementById('<%=SpBachDetails.ClientID%>').style.display = 'none';
                //document.getElementById('SpBachDetails').style.display = 'none';
                document.getElementById('<%=chkShow.ClientID%>').checked = false;
            }
        }--%>
    </script>
</asp:Content>
