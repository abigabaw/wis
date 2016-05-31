<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PapListExport.aspx.cs" Inherits="WIS.PapListExport"
    EnableEventValidation="false" UICulture="en" Culture="en-US" %>

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
        <legend>Search PAP</legend>
        <table width="100%" align="center" border="0">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        PAP Name</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPAPName" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="200px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="fte1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" " TargetControlID="txtPAPName" runat="server" />
                </td>
                <td align="left">
                    <label class="iceLable">
                        Plot Reference</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPlotReference" runat="server" MaxLength="100" CssClass="iceTextBoxPlotRef"
                      onchange="SetUpperCase(this);"   Width="200px" />
                <ajaxToolkit:MaskedEditExtender ID="mskPlotReference" runat="server" MessageValidatorTip="true"
                    TargetControlID="txtPlotReference">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlotReference"
                 ErrorMessage="Enter Plot Reference" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        District</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlDistrict" CssClass="iceTextBox" AppendDataBoundItems="true"
                        AutoPostBack="true" Width="205px" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlDistrict"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>

                </td>
                <td align="left">
                    <label class="iceLable">
                        County</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCounty" CssClass="iceTextBox" AutoPostBack="true" Width="205px"
                                runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                        </Triggers>
                          
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Sub County</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSubCounty" CssClass="iceTextBox" AutoPostBack="true" Width="205px"
                                runat="server" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlSubCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Parish</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlParish" CssClass="iceTextBox" Width="205px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                        TargetControlID="ddlParish"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Village</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlVillage" CssClass="iceTextBox" Width="205px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender4" runat="server"
                        TargetControlID="ddlVillage"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="pnlExport" ClientIDMode="Static" style="text-align:right;display:none" runat="server">
        <asp:Button ID="btn_ExportExcel" Text="Export" runat="server" CssClass="icebutton"
            Style="width: 140px" OnClick="btn_ExportExcel_Click" />
    </asp:Panel>
    <asp:UpdatePanel ID="upnPAP" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%" >
            <asp:GridView ID="grdPAPs" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
                GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdPAPs_RowDataBound">
                <RowStyle CssClass="gridRowStyle" />
                <AlternatingRowStyle CssClass="gridAlternateRow" />
                <HeaderStyle CssClass="gridHeaderStyle" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PapName" HeaderText="Pap Name" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PlotReference" HeaderText="Plot Reference" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="District" HeaderText="District" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="County" HeaderText="County" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SubCounty" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Parish" HeaderText="Parish" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Village" HeaderText="Village" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ROW_X" HeaderText="ROW_X" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ROW_Y" HeaderText="ROW_Y" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WL_X" HeaderText="WL_X" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WL_Y" HeaderText="WL_Y" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    There are no records for the selected criteria.
                </EmptyDataTemplate>
            </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <%-- <asp:AsyncPostBackTrigger ControlID="btn_ExportExcel" EventName="Click" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript">
        function ShowExport(show) {
                show == 1 ? document.getElementById('pnlExport').style.display = '' : document.getElementById('pnlExport').style.display = 'none';
            }


            spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
            if (spnpnl != null) {
                scrWidth = screen.availWidth;
                spnpnl.style.width = parseInt(scrWidth - 140).toString() + "px";
            }
    </script>
</asp:Content>
