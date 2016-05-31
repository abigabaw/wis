<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="MNEGoalEvalElements.aspx.cs" Inherits="WIS.MNEGoalEvalElements" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="icePnlinner">
        <legend>M &amp; E Goal Evaluation Elements</legend>
        <table border="0" align="center" width="90%">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Goal</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:Label ID="lblGoal" runat="server" CssClass="iceLable"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Element</label>
                    <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlElement" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqelement" runat="server" ErrorMessage="Select Element"
                        InitialValue="0" ControlToValidate="ddlElement" Display="None" ValidationGroup="GoalElement"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Description</label>
                    <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Width="400px" CssClass="iceTextBox" MaxLength="999"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Description"
                        ControlToValidate="txtDescription" Display="None" ValidationGroup="GoalElement"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <div style="margin-top: 12px">
                        <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="GoalElement"
                            OnClick="btnSave_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                        &nbsp;&nbsp;
                        <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                        <asp:ValidationSummary ID="valsumGoalElement" runat="server" ShowSummary="false"
                            ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                            ValidationGroup="GoalElement" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:GridView ID="gvGoalEvalElement" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" Width="100%" CellPadding="4" CellSpacing="1" PageSize="10"
        GridLines="None" OnPageIndexChanging="gvGoalEvalElement_PageIndexChanging" OnRowCommand="gvGoalEvalElement_RowCommand">
        <HeaderStyle CssClass="gridHeaderStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
        <FooterStyle CssClass="gridFooterStyle" />
        <RowStyle CssClass="gridRowStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Goal_elementname" HeaderText="Element" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Evalelementdescriptionn" HeaderText="Description" HeaderStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Edit">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("EvalelementID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("EvalelementID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litEvalID" Text='<%#Eval("EvalelementID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <script type="text/javascript" language="javascript">

        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }
    </script>
</asp:Content>
