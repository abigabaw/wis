<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SharedAuthorization.aspx.cs" Inherits="WIS.UI.MYACTIVITIES.SharedAuthorization" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Panel ID="pnlMyAuthorizationApprovel" runat="server">
        <fieldset class="icePnlinner">
            <legend>Shared Authorization</legend>
            <table border="0" width="80%" align="center">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Assign To
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="iceDropDown" Style="width: 190px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqddlAssignTo" runat="server" ErrorMessage="Select Assign To User"
                            InitialValue="0" ControlToValidate="ddlAssignTo" Display="None" ValidationGroup="vgShared"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Module
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlModuleDropDownList" runat="server" CssClass="iceDropDown"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlModuleDropDownList_SelectedIndexChanged">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvModuleDropDownList" runat="server" ControlToValidate="ddlModuleDropDownList"
                            ValidationGroup="vgShared" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Module"
                            Display="None">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            WorkFlow Item
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlWorkflowItemDropDownList" runat="server" CssClass="iceDropDown"
                            AppendDataBoundItems="true" Width="210px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvWorkflowItemDropDownList" runat="server" ControlToValidate="ddlWorkflowItemDropDownList"
                            ValidationGroup="vgShared" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Workflow"
                            Display="None">
                        </asp:RequiredFieldValidator>
                        <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dptxtTo"
                            ClientValidationFunction="CheckDate" ErrorMessage="To Date cannot be lesser than From Date"
                            ValidationGroup="VsTempAuth" Display="None"></asp:CustomValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Remarks</label>
                    </td>
                    <td align="left" style="vertical-align: top" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                            MaxLength="700" Style="width: 400px">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="4">
                        <div style="margin-top: 12px;">
                            <asp:ValidationSummary ID="valsumTempAuth" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgShared" runat="server" />
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                ValidationGroup="vgShared" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            <asp:Label ID="msgsaveLabel" runat="server" CssClass="iceLable"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="icePnlinner">
            <legend>Shared Authorizations</legend>
            <asp:GridView ID="grdTempAuth" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdTempAuth_RowCommand"
                PageSize="10" AllowPaging="True" OnPageIndexChanging="grdTempAuth_PageIndexChanging"
                OnRowDataBound="grdTempAuth_RowDataBound">
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
                    <asp:BoundField DataField="AuthoriserName" HeaderText="Authoriser" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="15%">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Assigned To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="15%" DataField="AssignedTo" />
                    <%--<asp:TemplateField HeaderText="Module" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="lblModuleName" runat="server" Text="'<%#Eval("ModuleName") %>'"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField HeaderText="Module" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="15%" DataField="WorkFlow" />
                    <asp:BoundField HeaderText="WorkFlow" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="15%" DataField="ModuleName" />
                    <%-- <asp:TemplateField HeaderText="Work Flow" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="lblWorkFlow" runat="server" Text='<%#Eval("WorkFlow") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        DataField="Remarks" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                                CommandArgument='<%#Eval("WorkFlowSharedId") %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="5%"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%--<asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("Isdeleted").ToString())%>'
                                OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />--%>
                            <asp:ImageButton ImageUrl="~/Image/delete.gif" runat="server" ID="ImgDelete" CommandName="DeleteRow"
                                CommandArgument='<%#Eval("WorkFlowSharedId") %>' OnClientClick="return DeleteRecord();" />
                            <asp:Literal ID="litLineTypeID" Text='<%#Eval("WorkFlowSharedId") %>' Visible="false"
                                runat="server"></asp:Literal>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
       

      
    </script>
</asp:Content>
