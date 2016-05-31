<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="UploadDocumentList.aspx.cs" Inherits="WIS.UI.UploadDocumentList" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <label class="iceLable">Project Code</label>
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectCodeTextBox" Width="200px" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <label class="iceLable">HH ID</label>
                        </td>
                        <td>
                            <asp:TextBox ID="HHIDTextBox" Visible="false" runat="server">
                            </asp:TextBox>
                            <asp:TextBox ID="HHIDTextBoxDISP" Enabled="false" runat="server">
                            </asp:TextBox>
                            <asp:TextBox ID="txtDocserviceID" Visible="false" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="iceLable">Keyword</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="iceTextBox" MaxLength="50"
                              width="200px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtSearchKeyword" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                ValidChars=". " TargetControlID="txtSearchKeyword" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="SearchUplaodButton" runat="server" CssClass="icebutton" Text="Search"
                                            onclick="SearchUplaodButton_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUploadClear" runat="server" CssClass="icebutton" Text="Clear"
                                            onclick="btnUploadClear_Click" />
                                    </td>
                                     <td>
                                   <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                                      </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><asp:Panel ID="pnlgrid" runat="server" Width="720px" Height="100%" ScrollBars="Horizontal">
                <asp:GridView ID="grdUploadDocument" runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                    PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="grdUploadDocument_RowDataBound" OnRowCommand="grdUploadDocument_RowCommand">
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
                            <%--<asp:BoundField DataField="HHID" HeaderText="HH ID" Visible="false" />--%>
                          <%--  <asp:BoundField DataField="HHID" HeaderText="HH ID" HeaderStyle-HorizontalAlign="Left" />--%>
                          <%--  <asp:BoundField DataField="DocumentType" HeaderText="Document Type" HeaderStyle-HorizontalAlign="Left" />--%>
                            <asp:BoundField DataField="DocumentCode" HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left" />
                            <%--   <asp:BoundField DataField="DocumnetPath" HeaderText="Document Path" HeaderStyle-HorizontalAlign="Left" />--%>
                             <asp:BoundField DataField="Keyword" HeaderText="KeyWord" HeaderStyle-HorizontalAlign="Left" />
                              <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="UserName" HeaderText="Uploaded By" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Date" HeaderText="Uploaded Date" HeaderStyle-HorizontalAlign="Center" />
                              <asp:TemplateField HeaderText="View" Visible="false" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="5%" />
                               <ItemTemplate>
                                   <asp:ImageButton ID="imgView" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="ViewRow" CommandArgument='<%#Eval("PAPDOCUMENTID") %>'  />
                            <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("PAPDOCUMENTID") %>' Visible="false"></asp:Literal>
                                  <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("DocumentPath") %>' visible = "False" />
                               </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="5%" />
                               <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("DocumentPath") %>' ClientIDMode="Static" runat="server">
                                   <img src="../IMAGE/edit.gif" style="background-color: transparent; border: 0;" id="img1" alt=""/></asp:HyperLink>
                               </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
                </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <iframe id="contentPanel1" runat="server" visible="true" style="height:0px;width:0px;"></iframe>
   <script type="text/javascript" language="javascript">
     // ViewDocumentUploadBYHHID(21,'BIP2');
       function ViewDocumentUploadBYHHID(PAPDOCUMENTID, ProjectCode) {
         
            var left = (screen.width - 650) / 2;
            var top = (screen.height - 640) / 4;
             open('../UI/ViewUploadDoc.aspx?papDocumentID=' + PAPDOCUMENTID + '&ProjectCode=' + ProjectCode, 'ChangeRequest', 'width=650px,height=640px,resizable=1,top=' + top + ', left=' + left);
         }

         function ViewFile(path) {
             var left = (screen.width - 800) / 2;
             var top = (screen.height - 500) / 4;
             window.open(path);
         }

         function ViewUploadDocument(PAPDOCUMENTID, ProjectCode) {
             var left = (screen.width - 650) / 2;
             var top = (screen.height - 640) / 4;
             open('../UI/ViewUploadDoc.aspx?papDocumentID=' + PAPDOCUMENTID + '&ProjectCode=' + ProjectCode, 'ChangeRequest', 'width=650px,height=640px,resizable=1,top=' + top + ', left=' + left);
         }
    </script>
</asp:Content>
