<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="PaymentExport.aspx.cs" Inherits="WIS.PaymentExport" %>

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
                        onchange="SetUpperCase(this);" Width="200px" />
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
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlDistrict"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
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
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlCounty"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
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
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlSubCounty"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
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
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlParish"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
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
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlVillage"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
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
    <br />
    <asp:Panel ID="pnlExport" ClientIDMode="Static" style="text-align:right;display:none" runat="server">
        <asp:Button ID="btn_ExportExcel" Text="Export" runat="server" CssClass="icebutton"
            Style="width: 140px" OnClick="btn_ExportExcel_Click" />
    </asp:Panel>
    <asp:UpdatePanel ID="upnPAP" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <div>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%" >
        <asp:GridView ID="grdPAPs" runat="server" AllowPaging="false" AllowSorting="false"
            CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
            Width="100%" 
            ShowFooter="false">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <%--<EmptyDataTemplate>
                                No Records Found
                            </EmptyDataTemplate>--%>
            <Columns>
                <asp:TemplateField HeaderText="SI No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="LI" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left"  />
                <asp:TemplateField HeaderText="PERIOD">
                    <ItemStyle HorizontalAlign="Center"/>
                    <ItemTemplate>
                        <asp:Label ID="lblCompPaymentId" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TRANSACTION DATE" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblCompensationType" runat="server" Text='<%#Eval("DeliveredDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TRANSACTION REFERENCE" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblModeOfPayment" Text='<%#Eval("TReference") %>' runat="server"
                            />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Left"  />
                    <ItemTemplate>
                        <asp:Literal ID="ltrlInKindType" runat="server" Text='<%#Eval("Papname") %>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="AMOUNT" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Left"  />
                    <ItemTemplate>
                        <asp:Literal ID="ltrlInKindType" runat="server" Text='<%#Eval("CompensationAmount") %>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:TemplateField HeaderText="Dr/Cr" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Left"  />
                    <ItemTemplate>
                        <asp:Literal ID="ltrlInKindType" runat="server" Text="D"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:BoundField DataField="FixedCostCentre" HeaderText="C/CENTRE" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />   
                <asp:BoundField DataField="BankCode" HeaderText="GL CODE" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                <asp:TemplateField HeaderText="STAFF NUMBER" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"/>
                    <ItemTemplate>
                        <asp:Label ID="lblDeliveredToStakeHolder" Text='<%#Eval("CompPaymentId") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="PROJECTNAME" HeaderText="PROJECT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="SEGMENTNAME" HeaderText="ANAL 3" HeaderStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        </asp:Panel>
    </div>
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
            spnpnl.style.width = parseInt(scrWidth - 120).toString() + "px";
        }
    </script>
</asp:Content>
