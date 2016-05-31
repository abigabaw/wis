<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DefineWorkflow.aspx.cs" Inherits="WIS.DefineWorkflow" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div width="100%">
        <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
        <fieldset class="icePnl">
            <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgWorkFlow" runat="server" />
            <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Please enter/correct the following:"
                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgWorkFlow1" runat="server" />
            <legend>Define Workflow</legend>
            <table width="100%">
                <tr>
                    <td>
                        <table align="left" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="WORKFLOWDEFINITIONIDTextBox" runat="server" CssClass="iceTextBox"
                                        Visible="false" />
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="ModuleLabel" runat="server" Text="Module" CssClass="iceLable" /><span
                                                        class="mandatory">*</span></div>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ModuleDropDownList" runat="server" CssClass="iceDropDown" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ModuleDropDownList_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator ID="rfvModuleDropDownList" runat="server" ControlToValidate="ModuleDropDownList"
                                                    ValidationGroup="vgWorkFlow1" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Module"
                                                    Display="None">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td nowrap>
                                                <div>
                                                    <asp:Label ID="WorkflowItemLabel" runat="server" Text="Workflow Item" CssClass="iceLable" /><span
                                                        class="mandatory">*</span></div>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="WorkflowItemDropDownList" runat="server" CssClass="iceDropDown"
                                                    AppendDataBoundItems="true" Width="210px">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvWorkflowItemDropDownList" runat="server" ControlToValidate="WorkflowItemDropDownList" ValidationGroup="vgWorkFlow1" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Workflow"
                                                    Display="None">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="HightAuthLabel" runat="server" Text="Higher Authority" CssClass="iceLable" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="HightAuthDropDownList" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true" Width="173px"
                                                AutoPostBack="true" OnSelectedIndexChanged="HightAuthDropDownList_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="TriggerLabel" runat="server" Text="Trigger" CssClass="iceLable" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="TriggerDropDownList" runat="server" CssClass="iceDropDown"
                                                    Width="160px" Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="AfterLabel" runat="server" Text="After" CssClass="iceLable" />
                                            </td>
                                            <td style="color: Black" nowrap>
                                                <asp:DropDownList ID="AfterDropDownList" runat="server" CssClass="iceDropDown" 
                                                    Enabled="False" />
                                                &nbsp;<b>Days</b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="SaveButton" runat="server" CssClass="icebutton" Text="Save" OnClick="SaveButton_Click"
                                                    OnClientClick="DisableOnSaveWithVal(this, 'vgWorkFlow1');" UseSubmitBehavior="false" />
                                            </td>
                                            <td>
                                                <asp:Button ID="ClearButton" runat="server" CssClass="icebutton" Text="Clear" OnClick="ClearButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdWorkflowItem" runat="server" CssClass="gridStyle" CellPadding="4"
                                                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdWorkflowItem_RowCommand"
                                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="grdWorkflowChangePage"
                                                    OnRowDataBound="grdWorkflowItem_RowDataBound">
                                                    <rowstyle cssclass="gridRowStyle" />
                                                    <alternatingrowstyle cssclass="gridAlternateRow" />
                                                    <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" forecolor="White" />
                                                    <headerstyle cssclass="gridHeaderStyle" />
                                                    <columns>
                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Module Name" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="18%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkAddApproved" runat="server" Text='<%#Eval("ModuleName") %>'
                                                                    CommandArgument='<%#Eval("ModuleName") %>' CommandName="ClickAddApproved">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="WorkflowName"  HeaderText="WorkFlow Name" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="UserName" HeaderText="Higher Authority" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="18%" />
                                                        <asp:BoundField DataField="Trigger" HeaderText="Trigger" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="After Days" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                            <ItemTemplate>
                                                                <asp:Literal ID="litAfterDays" Text="" runat="server"></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                                                                    CommandName="EditRow" CommandArgument='<%#Eval("WorkFlowDefID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                                    CommandName="DeleteRow" CommandArgument='<%#Eval("WorkFlowDefID") %>' OnClientClick="return DeleteRecord();"
                                                                    runat="server" />
                                                                <asp:Literal ID="litWorkFlowDefID" Text='<%#Eval("WorkFlowDefID") %>' Visible="false" runat="server"></asp:Literal>
                                                                 <asp:Literal ID="LitModuleName" Text='<%#Eval("ModuleName") %>' Visible="false" runat="server"></asp:Literal>
                                                                   <asp:Literal ID="LitWorkflowName" Text='<%#Eval("WorkflowName") %>' Visible="false" runat="server"></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </columns>
                                                </asp:GridView>
                                                <script language="javascript" type="text/javascript">
                                                    function DeleteRecord() {
                                                        return confirm('Are you sure you want to Delete this Record?');
                                                    }
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="ApprovalTable" runat="server" width="100%">
                            <tr>
                                <td>
                                    <fieldset class="icePnlinner">
                                        <legend id="ApproverTab" runat="server"></legend>
                                        <table border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="ApproverRoleNameLabel" runat="server" Text="Role Name" CssClass="iceLable" />
                                                    <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ApproverRoleNameDropDownList" runat="server" CssClass="iceDropDown"
                                                        AppendDataBoundItems="true" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ApproverRoleNameDropDownList_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvApproverRoleNameDropDownList" runat="server" ControlToValidate="ApproverRoleNameDropDownList"
                                                        ValidationGroup="vgWorkFlow" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Role Name"
                                                        Display="None">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="WorkApproverIDTextBox" runat="server" CssClass="iceTextBox" Visible="false" />
                                                    <asp:TextBox ID="WorkDefinationTextBox" runat="server" CssClass="iceTextBox" Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="ApproverNameLabel" runat="server" Text="User Name" CssClass="iceLable" />
                                                    <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ApproverNameDropDownList" runat="server" CssClass="iceDropDown"
                                                        AppendDataBoundItems="true" Width="250px">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvApproverNameDropDownList" runat="server" ControlToValidate="ApproverNameDropDownList"
                                                        ValidationGroup="vgWorkFlow" Text="Mandatory" InitialValue="0" ErrorMessage="Select a User Name"
                                                        Display="None">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <div>
                                                        <asp:Label ID="ApproverLevelLabel1" runat="server" Text="Level" CssClass="iceLable" />
                                                        <span class="mandatory">*</span></div>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ApproverLevelDropDownList" runat="server" CssClass="iceDropDown"
                                                        AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvApproverLevelDropDownList" runat="server" ControlToValidate="ApproverLevelDropDownList"
                                                        ValidationGroup="vgWorkFlow" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Level"
                                                        Display="None">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="ADDButton" runat="server" CssClass="icebutton" Text="Add" OnClick="ADDButton_Click"
                                                        OnClientClick="DisableOnSaveWithVal(this, 'vgWorkFlow');" UseSubmitBehavior="false" />
                                                    <asp:Button ID="UpDateButton" runat="server" CssClass="icebutton" Text="Update" ValidationGroup="vgWorkFlow"
                                                        OnClick="ADDButton_Click" />
                                                    <asp:Button ID="CancelButton" runat="server" CssClass="icebutton" Text="Cancel" OnClick="CancelButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdApprover" runat="server" CssClass="gridStyle" CellPadding="4"
                                                        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdApprover_RowCommand"
                                                        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
                                                        <rowstyle cssclass="gridRowStyle" />
                                                        <alternatingrowstyle cssclass="gridAlternateRow" />
                                                        <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" forecolor="White" />
                                                        <headerstyle cssclass="gridHeaderStyle" />
                                                        <columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RoleName" HeaderText="Role Name" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="ApproverUserName" HeaderText="Approver User Name" HeaderStyle-HorizontalAlign="Left" />
                                                 <asp:BoundField DataField="LEVEL" HeaderText="Level" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                                            CommandName="EditRow" CommandArgument='<%#Eval("WorkApprovalID") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                            BorderColor="Transparent" CommandName="DeleteRow" CommandArgument='<%#Eval("WorkApprovalID") %>'
                                                            OnClientClick="return DeleteRecord();" runat="server" />
                                                           <asp:Literal ID="litUserID" Text='<%#Eval("WorkApprovalID") %>' Visible="false" runat="server"></asp:Literal>
                                                            <asp:Literal ID="litWorkFlowDefID" Text='<%#Eval("WorkFlowDefID") %>' Visible="false" runat="server"></asp:Literal>
                                                             <asp:Literal ID="litWorkApprovallevel" Text='<%#Eval("LEVEL") %>' Visible="false" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }

        function DisableOnSaveWithVal(src, Vgroup) {
            if (Page_ClientValidate(Vgroup)) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }
        }
    </script>
</asp:Content>
