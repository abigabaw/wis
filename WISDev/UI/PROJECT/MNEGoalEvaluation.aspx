<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MNEGoalEvaluation.aspx.cs" Inherits="WIS.UI.PROJECT.MNEGoalEvaluation" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <asp:Panel ID="pnlSave" runat="server">
            <fieldset class="icePnlinner">
                <legend>M &amp; E Goal Evaluation Details</legend>
                <table align="center" border="0" width="45%">
                    <tr>
                        <asp:ValidationSummary ID="valsummneGoal" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="GoalEval" />
                        <td align="left" width="25%">
                            <label class="iceLable">
                                Goal</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtCropName" runat="server" Width="300" MaxLength="200" CssClass="iceTextBox"
                                AutoCompleteType="Disabled"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlGoal" CssClass="iceTextBox" Width="201px" runat="server"
                                AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlGoal"
                                InitialValue="0" ErrorMessage="Select a Goal" Display="None" ValidationGroup="GoalEval"
                                runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label class="iceLable">
                                Description</label><span class="mandatory">*</span>
                        </td>
                        <td align="left" class="iceNormalText">
                            <asp:TextBox ID="txtDescription" CssClass="iceTextBox" Text="" runat="server" Width="400px" MaxLength="199"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqdesc" runat="server" ErrorMessage="Enter Description"
                                ControlToValidate="txtDescription" Display="None" ValidationGroup="GoalEval"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: top">
                            <label class="iceLable">
                                Narrative</label>
                        </td>
                        <td align="left" style="vertical-align: top">
                            <asp:TextBox ID="txtNarrative" CssClass="iceTextBox" runat="server" TextMode="MultiLine" MaxLength="1999" 
                               Width="750px" Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 12px">
                            <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="GoalEval"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="gvGoalEval" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Width="100%" CellPadding="4" CellSpacing="1" PageSize="10"
            GridLines="None" OnPageIndexChanging="gvGoalEval_PageIndexChanging" OnRowCommand="gvGoalEval_RowCommand"
            OnRowDataBound="gvGoalEval_RowDataBound">
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
                <asp:BoundField DataField="Goalname" HeaderText="Goal" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoalDescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Evaluation">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <a id="lnkEvalElement" href="#" runat="server">View</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("EvaluationID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("EvaluationID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litEvalID" Text='<%#Eval("EvaluationID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript" language="javascript">



            function OpenEvalElements(EvaluationID, GoalName) {
                var left = (screen.width - 800) / 2;
                var top = (screen.height - 650) / 4;
                window.open('MNEGoalEvalElements.aspx?EvalID=' + EvaluationID + '&goalName=' + GoalName, "", 'cropRate', 'width=800px,height=650px,top=' + top + ', left=' + left);
            }

            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }
            var isDirty = 0;
            function setDirty() {
                isDirty = 1;
            }

            function setDirtyText() {
                var btn = document.getElementById("<%= btnSave.ClientID  %>");
                var tat1 = document.getElementById("<%= txtDescription.ClientID  %>");
               
                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
                else if (tat1.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
