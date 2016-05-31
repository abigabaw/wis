<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Consultant.aspx.cs" Inherits="WIS.Consultant" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Consultant Details</legend>
        <table align="center" border="0" style="width: 100%">
            <tr>
                <td align="left">
                    <label id="Label1" class="iceLable" cssclass="iceLable" runat="server">
                        Consultant Name</label> <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtConsultantname" runat="server" class="iceTextBox" MaxLength="200" Width="250px" />
                    <asp:RequiredFieldValidator ID="reqConsultantName" ControlToValidate="txtConsultantname" ErrorMessage="Enter Consultant Name"
                        Display="None" ValidationGroup="Consultant" runat="server"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteConsultantName" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=".,(),& " TargetControlID="txtConsultantname" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <label id="Label6" class="iceLable" cssclass="iceLable" runat="server">
                        Consultant Type</label> <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DrpConsultantType" runat="server" Width="250px" CssClass="iceDropDown" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DrpConsultantType" ErrorMessage="Select Consultant Type" InitialValue="0"
                        Display="None" ValidationGroup="Consultant" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" rowspan="3" style="vertical-align:top">
                    <label id="Label2" class="iceLable" cssclass="iceLable" runat="server">
                        Address</label>
                </td>
                <td align="left" rowspan="3">
                    <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" class="iceTextBox" Rows="3" MaxLength="400" Width="250px" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label id="Label3" class="iceLable" cssclass="iceLable" runat="server">
                        Contact Person</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtContactPerson" runat="server" class="iceTextBox" MaxLength="200" Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" " TargetControlID="TxtContactPerson" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label id="Label4" class="iceLable" cssclass="iceLable" runat="server">
                        Contact Number</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtContactNum" runat="server" class="iceTextBox" MaxLength="15" Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom" ValidChars="-" TargetControlID="TxtContactNum" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label id="Label5" class="iceLable" cssclass="iceLable" runat="server">
                        Email Address</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtEmailaddress" runat="server" class="iceTextBox" MaxLength="50" Width="250px" />
                    <asp:RegularExpressionValidator ID="REVtxtEmailID" runat="server" ControlToValidate="TxtEmailaddress"
                        ErrorMessage="Invalid Email" Text="Invalid Email"
                        Display="None" ValidationGroup="Consultant"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <br />
                    <asp:Button ID="btn_Save" runat="server" value="Save" class="icebutton" OnClick="btn_Save_Click"
                        Text="Save" OnClientClick="DisableOnSaveWithVal(this);" UseSubmitBehavior="false"/>&nbsp;
                    <asp:Button ID="btn_Clear" value="Clear" runat="server" class="icebutton" OnClick="btn_Clear_Click"
                        Text="Clear" />
                    <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Consultant" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
    <asp:GridView ID="grdConsultant" runat="server" CssClass="gridStyle" CellPadding="4"
        PageSize="10" CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%"
        OnRowCommand="grdConsultant_RowCommand">
        <RowStyle CssClass="gridRowStyle" />
         <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl . No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ConsultName" HeaderText="Consultant Name" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ConsultType" HeaderText="Consultant Type" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ConPerson" HeaderText="Contact Person" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ConNumber" HeaderText="Contact Number" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" HeaderStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("ConsultID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />  
                    <asp:Literal ID="litConsultantID" Text='<%#Eval("ConsultID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("ConsultID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </asp:Panel>

    <div class="footer">
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }

            spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
            if (spnpnl != null) {
                scrWidth = screen.availWidth;
                spnpnl.style.width = parseInt(scrWidth - 80).toString() + "px";
            }

            function DisableOnSaveWithVal(src) {
                if (Page_ClientValidate()) {
                    src.disabled = true;
                    src.value = 'Please Wait...';
                }
            } 
            /*
            function MakeObsolete(src, id) {
                if (src.checked) {
                    PageMethods.ObsoleteConsultant(id, OnWSObsoleteConsultantComplete);                    
                }
                else {
                    PageMethods.RevokeObsoleteConsultant(id, OnWSRevokeObsoleteConsultantComplete);
                }
            }

            function OnWSObsoleteConsultantComplete(result) {
                if (result)
                    alert('Record is made obsolete.');
            }
            
            function OnWSRevokeObsoleteConsultantComplete(result) {
                if (result)
                    alert('Record is restored.');
            }
            */
            var isDirty = 0;
            function setDirty() {
                isDirty = 1;
            }

            function setDirtyText() {
                var btn = document.getElementById("<%= btn_Save.ClientID  %>");
                var tat1 = document.getElementById("<%= txtConsultantname.ClientID  %>");
                

                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
                else if (tat1.value.toString().replace(/^\s+/, '') == ''       
             && btn.value.toString() == 'Save') {
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
    </div>
</asp:Content>
