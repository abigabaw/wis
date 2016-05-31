<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="UploadDocPop.aspx.cs" Inherits="WIS.UploadDocPop" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnShowUpload" Text="Upload Document" runat="server" CssClass="icebutton"
                        OnClick="btnShowUpload_Click" Style="width: 100%" />
                </td>
                <td>
                    <asp:Button ID="btnShowSearch" Text="Search Document" runat="server" CssClass="icebutton"
                        OnClick="btnShowSearch_Click" Style="width: 100%" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlSearchDocument" Visible="false" runat="server">
        <fieldset class="icePnlinner">
            <legend>Search Document</legend>
            <table border="0" align="center" width="50%">
                <tr>
                    <td>
                        <label class="iceLable">
                            Keyword</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="iceTextBox" MaxLength="50"
                            Width="162px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                            ValidChars=". " TargetControlID="txtSearchKeyword" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="SearchUplaodButton" runat="server" CssClass="icebutton" Text="Search"
                                        OnClick="SearchUplaodButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadClear" runat="server" CssClass="icebutton" Text="Clear"
                                        OnClick="btnUploadClear_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlUploadDocuments" runat="server">
        <fieldset class="icePnlinner">
            <table align="center" width="100%">
                <tr>
                    <td>
                        <asp:TextBox ID="upProjectIDTextBox" runat="server" Visible="false" />
                        <asp:TextBox ID="userIDTextBox" runat="server" Visible="false" />
                        <asp:TextBox ID="txtDocserviceID" runat="server" CssClass="iceTextBox" Visible="false"
                            ReadOnly="true" />
                        <table align="center" width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="UpProjectIDLabel" runat="server" Text="Project Code " CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="ProjectCodeTextBox" runat="server" CssClass="iceTextBox" ReadOnly="true"
                                        Width="162px" />
                                </td>
                                <td>
                                    <asp:Label ID="upHHIDLabel" runat="server" Text="HH ID " CssClass="iceLable" Enabled="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="upHHIDTextBox" runat="server" Visible="false" CssClass="iceTextBox" ReadOnly="true"
                                        Width="232px" />
                                        <asp:TextBox ID="upHHIDTextBoxDisp" runat="server" CssClass="iceTextBox" ReadOnly="true"
                                        Width="232px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="DocTypeLabel" runat="server" Text="Document Type" CssClass="iceLable" />
                                     <span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DocTypeDropDownList" runat="server" CssClass="iceDropDown">
                                       <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                        ErrorMessage="Select a Document Type" ValidationGroup="vgfilMyFile" InitialValue="0"
                                        ControlToValidate="DocTypeDropDownList"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="DocPathLabel" runat="server" Text="Document Path" CssClass="iceLable" />
                                     <span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileMyFile" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvfilMyFile" runat="server" Display="None" ControlToValidate="fileMyFile"
                                        ValidationGroup="vgfilMyFile" Text="Mandatory!!" ErrorMessage="Select a Document"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="iceLable">
                                        Keyword</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="iceTextBox" MaxLength="50"
                                        Width="162px" /><br />
                                    <label style="color: Red;">
                                        (Max 50 characters)</label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtSearchKeyword" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                        ValidChars=". " TargetControlID="txtKeyword" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <label class="iceLable">
                                        Description</label>
                                </td>
                                <td style="vertical-align: top">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="iceTextBox" MaxLength="500"
                                        Width="96%" TextMode="MultiLine" Rows="3" /><label style="color: Red;">(Max 500 characters)</label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                        ValidChars="." TargetControlID="txtSearchKeyword" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="SaveButton" runat="server" CssClass="icebutton" ValidationGroup="vgfilMyFile"
                                        Text="Save" OnClick="SaveButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ClearButton" runat="server" CssClass="icebutton" Text="Clear" OnClick="ClearButton_Click" />
                                </td>
                                <td>
                                    <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgfilMyFile" runat="server" />
        </fieldset>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlgrid" runat="server" Width="720px" Height="100%" ScrollBars="Horizontal">
                    <asp:GridView ID="grdUploadDocument" runat="server" CssClass="gridStyle" CellPadding="4"
                        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                        PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="grdUploadDocument_RowDataBound"
                        OnRowCommand="grdUploadDocument_RowCommand">
                        <RowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                        <HeaderStyle CssClass="gridHeaderStyle" />
                        <Columns>
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
                            <asp:BoundField DataField="Keyword" ItemStyle-Wrap="true" ItemStyle-Width="100px"
                                HeaderText="KeyWord" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Description" ItemStyle-Wrap="true" ItemStyle-Width="200px"
                                HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="UserName" HeaderText="Uploaded By" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Date" HeaderText="Uploaded Date" HeaderStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgView" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                        CommandName="ViewRow" CommandArgument='<%#Eval("PAPDOCUMENTID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("DocumentPath") %>' ClientIDMode="Static"
                                        runat="server">
                                   <img id="imgViewIcon" src="~/Image/edit.gif" style="background-color: transparent; border: 0;" alt="" runat="server"/></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                        CommandName="DeleteRow" CommandArgument='<%#Eval("PAPDOCUMENTID") %>' OnClientClick="return DeleteRecord();" />
                                    <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("PAPDOCUMENTID") %>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="LtlDocumentPath" runat="Server" Text='<%#Eval("DocumentPath") %>'
                                        Visible="False" />
                                    <asp:Literal ID="LilProjectCode" runat="Server" Text='<%#Eval("Projectcode") %>'
                                        Visible="False" />
                                    <asp:Literal ID="LitHHID" runat="Server" Text='<%#Eval("HHID") %>' Visible="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <script language="javascript" type="text/javascript">
                    function DeleteRecord() {
                        return confirm('Are you sure you want to delete this record?');
                    }
                </script>
            </td>
        </tr>
    </table>
    <iframe id="contentPanel1" runat="server" visible="true" style="height: 0px; width: 0px;">
    </iframe>
    <script type="text/javascript" language="javascript">
        function ViewUploadDocument(PAPDOCUMENTID, ProjectCode) {
            var left = (screen.width - 650) / 2;
            var top = (screen.height - 640) / 4;
            open('../UI/ViewUploadDoc.aspx?papDocumentID=' + PAPDOCUMENTID + '&ProjectCode=' + ProjectCode, 'ChangeRequest', 'width=650px,height=640px,resizable=1,top=' + top + ', left=' + left);
        }

        function SetVisible(val) {
            var hf = document.getElementById("<%= hfVisible.ClientID  %>");
            hf.value = val;
        }

        function ViewFile(path) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 500) / 4;
            window.open(path);
        }

        function SeesionExperpopup() {
            alert('Session Expired. Please relogin.');

            if (opener) {
                window.opener.location.reload();
            }

            window.close();
        }
    </script>
</asp:Content>
