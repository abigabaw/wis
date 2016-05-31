<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="ApprovalComments.aspx.cs" Inherits="WIS.UI.PROJECT.ApprovalComments" %>
    <%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <table width="100%">
      <tr>
         <td>
             <asp:GridView ID="grdApprovalComments" runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" >
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
                             <asp:TemplateField HeaderText="Approver Name" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="10%" />
                               <ItemTemplate>
                               <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("AppName") %>' visible = "true" />
                               </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Role Name" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="15%" />
                               <ItemTemplate>
                               <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("RoleName") %>' visible = "true" />
                               </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Approved Status" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="5%" />
                               <ItemTemplate>
                               <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("Status") %>' visible = "true" />
                               </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Comments" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="30%" />
                               <ItemTemplate>
                               <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("Comments") %>' visible = "true" />
                               </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action Date" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="8%" />
                               <ItemTemplate>
                               <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("ActioDate") %>' visible = "true" />
                               </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:BoundField DataField="RoleName" HeaderText="Role Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Status" HeaderText="Action Status" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-HorizontalAlign="Left" />
                               <asp:BoundField DataField="ActioDate" HeaderText="Action Date" HeaderStyle-HorizontalAlign="Left" />--%>
                             
                           
                        </columns>
                </asp:GridView>
         </td>
      </tr>
   </table>
</asp:Content>
