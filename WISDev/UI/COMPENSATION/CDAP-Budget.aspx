<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="CDAP-Budget.aspx.cs" Inherits="WIS.CDAP_Budget" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
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
    <fieldset class="icePnlinner">
        <legend>Item Details</legend>
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
        <table border="0" width="60%" align="center">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Item
                    </label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="iceDropDown" Style="width: 190px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlItem"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlItem"
                        InitialValue="0" ErrorMessage="Select Item" Display="None" ValidationGroup="Budget"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Item Description</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlItemDesc" runat="server" CssClass="iceDropDown" Style="width: 190px">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlItemDesc"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlItemDesc"
                        InitialValue="0" ErrorMessage="Select Item Description" Display="None" ValidationGroup="Budget"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Unit</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="iceDropDown" Style="width: 190px">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlUnit"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="ddlUnit"
                        InitialValue="0" ErrorMessage="Select Unit" Display="None" ValidationGroup="Budget"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Quantity</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="iceTextBox" MaxLength = "10"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNoRooms" FilterType="Numbers" TargetControlID="txtQuantity"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtQuantity"
                        ErrorMessage="Enter Quantity" Display="None" ValidationGroup="Budget" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Rate Per Unit</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtRateperUnit" runat="server" CssClass="iceTextBox" maxlength="10"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fterateperunit" FilterType="Numbers,Custom" TargetControlID="txtRateperUnit"
                        runat="server" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRateperUnit"
                        ErrorMessage="Enter Rate Per Unit" Display="None" ValidationGroup="Budget" runat="server"></asp:RequiredFieldValidator>
                    <label class="iceLable">
                        (USH)</label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Amount</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="iceTextBox"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtAmount" FilterType="Numbers,Custom" TargetControlID="txtAmount"
                        runat="server" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <label class="iceLable">
                        (USH)</label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Budget" CssClass="icebutton"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Budget" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:GridView ID="grdCDAPBudget" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdCDAPBudget_RowCommand"
        AllowPaging="True" OnPageIndexChanging="grdCDAPBudget_PageIndexChanging"  onrowdatabound="grdCDAPBudget_RowDataBound">
        <RowStyle CssClass="gridRowStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center"  font-bold="true" forecolor="White" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CDAP_CATEGORYNAME" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="CDAP_SUBCATEGORYNAME" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="UNITNAME" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="QUANTITY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="RATEPERUNIT" HeaderText="Rate per Unit" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:N0}"/>
            <asp:BoundField DataField="FundReqStatus" HeaderText="Fund Status" HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="floortype" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" />--%>
            <%--<asp:BoundField DataField="" HeaderText="Comments" HeaderStyle-HorizontalAlign="Left" />--%>
             <asp:TemplateField HeaderText="Resolution Status" HeaderStyle-HorizontalAlign="Center" Visible="False">
                       <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemTemplate>
                                <asp:Literal ID="litFundReqStatus" Text='<%#Eval("FundReqStatus") %>' runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("CDAP_BUDGID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("CDAP_BUDGID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litRoleID" Text='<%#Eval("CDAP_BUDGID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <center><br />
        <asp:Button ID="btnApproval" runat="server" Text="Get Approval" CssClass="icebutton"
            OnClick="btnApproval_Click" />
    </center>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete?');
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }

        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }

        function CalculateAmount() {
            var Quantity;
            var RateperUnit;
            var Amount;

            Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value;
            if (isNaN(Quantity)) Quantity = 0;
            Quantity = parseFloat(Quantity);

            RateperUnit = document.getElementById('<%=txtRateperUnit.ClientID %>').value.replace(/,/g, '');
            if (RateperUnit == "") {
                RateperUnit = 0;
            }
            if (isNaN(RateperUnit)) RateperUnit = 0;
            RateperUnit = parseFloat(RemoveComma(RateperUnit));

            if (!isNaN(Quantity) && !isNaN(RateperUnit))
                Amount = Quantity * RateperUnit;
            else
                Amount = '';

            if (!isNaN(Amount)) {
                document.getElementById('<%=txtAmount.ClientID %>').value = AddComma(Amount);
            }
            document.getElementById('<%=txtRateperUnit.ClientID %>').value = document.getElementById('<%=txtRateperUnit.ClientID %>').value.replace(/,/g, '');
        }

        function RemoveComma(iValue) {
            return parseFloat(iValue.toString().replace(/,?/g, ""));
        }
        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    </script>
    
    <%--<div align="right">
        <table align="right" width="10%">
            <tr>
                <td>
                    <label class="iceLable">
                        Total</label>
                </td>
                <td>
                    <%-- <label class="iceLable">--%>
    <%--  <asp:Label ID="lblTotal" runat="server"></asp:Label>
    (USH) </td> </tr> </table> </div>--%>
</asp:Content>
