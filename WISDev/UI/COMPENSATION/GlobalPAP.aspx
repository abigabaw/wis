<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US" CodeBehind="GlobalPAP.aspx.cs" Inherits="WIS.UI.COMPENSATION.GlobalPAP" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
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
                        HHID</label>
                </td>
                  <td align="left">
                    <asp:TextBox ID="txtHHID" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="200px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"
                        ValidChars=" " TargetControlID="txtHHID" runat="server" />
                </td>
                      <td align="left">
                    <label class="iceLable">
                        PAP UID</label>
                </td>
                  <td align="left">
                    <asp:TextBox ID="txtPAPUID" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="200px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,LowercaseLetters,UppercaseLetters"
                         TargetControlID="txtPAPUID" runat="server" />
                </td>
            </tr>
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
                       onchange="SetUpperCase(this);"  Width="200px" />
              <ajaxToolkit:MaskedEditExtender ID="mskPlotReference" runat="server" MessageValidatorTip="true" ClearMaskOnLostFocus="false"
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
                    <asp:DropDownList ID="ddlDistrict" CssClass="iceTextBox" AppendDataBoundItems="true" AutoPostBack="true" Width="205px"
                        runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
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
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" 
                        runat="server" onclick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:UpdatePanel ID="upnPAP" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:GridView ID="grdPAPs" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
                GridLines="None" AutoGenerateColumns="False" Width="100%" 
                AllowPaging="True" onpageindexchanging="grdPAPs_PageIndexChanging" 
                onrowcommand="grdPAPs_RowCommand">
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
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="PAP Name">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPAPName" CommandName="SetPAP" CommandArgument='<%#Eval("HHID") %>' Text='<%#Eval("PapName") %>' runat="server"></asp:LinkButton>
                            <asp:Label ID="lblPaptype" Text='<%#Eval("Paptype") %>' runat="server"  Visible="false" ></asp:Label>
                            <asp:Label ID="lblPlotreference" Text='<%#Eval("PlotReference") %>' runat="server"  Visible="false" ></asp:Label>
                             <asp:Label ID="lblProjectCode" Text='<%#Eval("ProjectCode") %>' runat="server"  Visible="false" ></asp:Label>
                             <asp:Label ID="lblProjectedId" Text='<%#Eval("ProjectedId") %>' runat="server"  Visible="false" ></asp:Label>
                              <asp:Label ID="lblviewstatus" Text='<%#Eval("Viewstatus") %>' runat="server"  Visible="false" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:BoundField DataField="hhid" HeaderText="HHID" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PapUid" HeaderText="PAP UID" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                 
                    <asp:TemplateField HeaderText="Plot Reference">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                        <asp:LinkButton ID="lnkPlotReference" CommandName="ViewMap" CommandArgument='<%#Eval("HHID") %>' Text='<%#Eval("PlotReference") %>' runat="server"></asp:LinkButton>
                            <%--<a href="#" id='<%# Eval("PlotReference") %>' class="labelPlotRef" onclick="ViewMap(<%# Eval("HHID") %>);" ><%# Eval("PlotReference")%></a>--%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                     <asp:BoundField DataField="OptionGroup" HeaderText="Option Group" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="District" HeaderText="District" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="County" HeaderText="County" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SubCounty" HeaderText="Sub County" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Parish" HeaderText="Parish" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Village" HeaderText="Village" 
                        HeaderStyle-HorizontalAlign="Center" >
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
            <EmptyDataTemplate>
                There are no records for the selected criteria.
            </EmptyDataTemplate>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function ViewMap(HHID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 500) / 4;
            open('../PROJECT/ViewPAPMap.aspx?HHID=' + HHID, 'routeMapWin', 'width=800px,height=500px,top=' + top + ', left=' + left + ',scrollbars=1');
        }


        function ViewMapadad(Plotlatitude, Plotlongitude) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 500) / 4;
            open('../PROJECT/ViewPAPMap.aspx?Plotlatitude=' + Plotlatitude + '&Plotlongitude=' + Plotlongitude, 'routeMapWin', 'width=800px,height=500px,top=' + top + ', left=' + left + ',scrollbars=1');
        }
    </script>
      <script type="text/javascript">

          (function () { try { var n = Sys.Extended.UI.MaskedEditBehavior.prototype, t = n._ExecuteNav; n._ExecuteNav = function (n) { var i = n.type; i == "keydown" && (n.type = "keypress"), t.apply(this, arguments), n.type = i } } catch (i) { return } })()

          // Fixes issue with delete key not working on Ipad browsers.

    </script>
</asp:Content>