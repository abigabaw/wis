<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="PlotDependents.aspx.cs" Inherits="WIS.UI.COMPENSATION.SOCIOECONOMIC.PlotDependents" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="width:100%">
    <asp:GridView ID="grdPlotDependent" runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                    PageSize="10" OnPageIndexChanging="ChangePage" >
                    <rowstyle cssclass="gridRowStyle" />
                    <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" forecolor="White" />
                    <headerstyle cssclass="gridHeaderStyle" />
                    <columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                      
                            <asp:BoundField DataField="PAPIDPOP" HeaderText="PAP ID" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="HhId" HeaderText="HHID" HeaderStyle-HorizontalAlign="Left" />
                             <asp:BoundField DataField="PapName" HeaderText="PAP Name" HeaderStyle-HorizontalAlign="Left" />
                              <asp:BoundField DataField="PlotReference" HeaderText="Plot Reference" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="PAPDesignationPOP" HeaderText="PAP Status" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" />
                       </columns>
                </asp:GridView>
</div>
</asp:Content>
