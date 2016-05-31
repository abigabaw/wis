<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OptionGroupParameters.aspx.cs" Inherits="WIS.UI.MASTER.OptionGroupParameters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Panel ID="pnlOptionGrp" runat="server" DefaultButton="btnAdd">
        <fieldset class="icePnlinner">
            <legend>Option Group Parameters</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td>
                        <asp:Label ID="lblOption" CssClass="iceLable" Text="Option Group" runat="server"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddloptionGroup" runat="server" Width="260px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddloptionGroup"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddloptionGroup"
                            InitialValue="0" ErrorMessage="Select Option Group" Display="None" ValidationGroup="HHDetails"
                            runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatus" CssClass="iceLable" Text="Land Status" runat="server"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="OccupationStatus" runat="server" AppendDataBoundItems="true"
                            Width="200px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="OccupationStatus"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="OccupationStatus"
                            InitialValue="0" ErrorMessage="Select Status" Display="None" ValidationGroup="HHDetails"
                            runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblResident" Text="Is Resident?" CssClass="iceLable" runat="server"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:RadioButton ID="RdbtnYES" Text="Yes" GroupName="Rd3" runat="server" />
                        <asp:RadioButton ID="RdbtnNO" Text="No" GroupName="Rd3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLandCompn" Text="Land Compensation" runat="server" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:RadioButton ID="RdbtnCash" Text="Cash" GroupName="Rd2" runat="server" />
                        <asp:RadioButton ID="Rdbtninkind" Text="In Kind" GroupName="Rd2" runat="server" />
                        <asp:RadioButton ID="RdbtnBoth" Text="Both" GroupName="Rd2" runat="server" />
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblHouseCompn" Text="House Compensation" runat="server" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:RadioButton ID="RdbtnHcash" Text="Cash" GroupName="Rd1" runat="server" />
                        <asp:RadioButton ID="RdbtnHbtninkind" Text="In Kind" GroupName="Rd1" runat="server" />
                        <asp:RadioButton ID="RdbtnHBoth" Text="Both" GroupName="Rd1" runat="server" />
                        
                    </td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="2">
                        <asp:Button ID="btnAdd" Text="Save" ValidationGroup="HHDetails" CssClass="icebutton"
                            runat="server" OnClick="btnAdd_Click" />
                              <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                              ShowMessageBox="true" ShowSummary="false" ValidationGroup="HHDetails" runat="server" />                              
                        <asp:Button ID="btnCncl" Text="Canel" CssClass="icebutton" runat="server" OnClick="btnCncl_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    
    <table>
        <tr>
            <asp:GridView ID="grdOptionGrp"  runat="server" CssClass="gridStyle" 
                CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" 
        PageSize="10" AllowPaging="True" onrowcommand="grdOptionGrp_RowCommand" 
               >
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
                    <asp:BoundField DataField="Optiongrpname" HeaderText="Option Group Name" />
                     <asp:BoundField DataField="Optiongrpstatusname" HeaderText="Land Status Name" />
                      <asp:BoundField DataField="IsResident" HeaderText="Is Resident" />
                       <asp:BoundField DataField="LandCompensation" HeaderText="Land Compensation" />
                       <asp:BoundField DataField="HouseCompensation" HeaderText="House Compensation" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("ParamID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("ParamID") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                            <asp:Literal ID="litBankID" Text='<%#Eval("ParamID") %>' Visible="false" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </tr>
    </table>
     <script language="javascript" type="text/javascript">
      function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }
            function CustomValidator1_ClientValidate(source, args) {
                if (document.getElementById("<%= RdbtnYES.ClientID %>").checked || document.getElementById("<%= RdbtnNO.ClientID %>").checked) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }

            }
              </script>
</asp:Content>
