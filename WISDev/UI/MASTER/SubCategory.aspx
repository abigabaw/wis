<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="SubCategory.aspx.cs" Inherits="WIS.SubCategory" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"/>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Subcategory</legend>
            <table border="0" align="center" width="50%">
                <tr>
                    <td align="left" class="iceLable">
                        <label class="iceLable">
                            Subcategory</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" class="iceNormalText">
                       <asp:TextBox ID="SubcategoryTextBox" runat="server" CssClass="iceTextBox" MaxLength="100" Width="250px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="azSubcategory" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                            ValidChars=" ," TargetControlID="SubcategoryTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqsubCategory" runat="server" ErrorMessage=" Enter Subcategory "
                            Display="None" ControlToValidate="SubcategoryTextBox" ValidationGroup="subcategory"></asp:RequiredFieldValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 30%">
                        <asp:Label ID="lblAccountCode" runat="server" Text="Account Code" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccountCode" runat="server" Width="150" MaxLength="10" CssClass="iceTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqsubcategry" runat="server" ErrorMessage="Enter Crop Rate"
                            ControlToValidate="txtAccountCode" Display="None" ValidationGroup="subcategory"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtcategoryID" runat="server" Visible="false" CssClass="iceTextBox"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftecategory" FilterType="Numbers" TargetControlID="txtAccountCode" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div style="margin-top: 12px">
                            <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="subcategory"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                            &nbsp;&nbsp;
                            <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                            <asp:ValidationSummary ID="valsumsubCatgry" runat="server" ShowSummary="false" ShowMessageBox="true"
                                HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="subcategory" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdSubcategory" runat="server" AllowPaging="True" OnPageIndexChanging="ChangePage"
            AllowSorting="True" AutoGenerateColumns="False" Width="100%" CellPadding="4"
            CellSpacing="1" GridLines="None" OnRowCommand="grdSubCategory_RowCommand">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="SI No">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BGT_SUBCATEGORYNAME" HeaderText="Sub Category" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="35%" />
                <asp:BoundField DataField="ACCOUNTCODE" HeaderText="Account Code" HeaderStyle-HorizontalAlign="Center" />
                <%--  <asp:TemplateField HeaderText="Rate">
             <ItemStyle  HorizontalAlign="Center" Width="5%"/>
               <ItemTemplate>                            
                   <asp:HyperLink ID="hypLink" ImageAlign="AbsMiddle" runat="server" Target="_blank" Text="View" NavigateUrl="~/Master/CropRate.aspx"
                            CommandName="ViewRow" CommandArgument='<%#Eval("CROPRATEID") %>'>
                            </asp:HyperLink>                                                                      
               </ItemTemplate>                 
             </asp:TemplateField> --%>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("BGT_SUBCATEGORYID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" OnCheckedChanged="IsObsolete_CheckedChanged"
                            Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("BGT_SUBCATEGORYID") %>' OnClientClick="return DeleteRecord();" />
                        <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("BGT_SUBCATEGORYID") %>' Visible="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript" language="javascript">
            function DeleteRecord() {
                return confirm('Are you sure want to delete this record');
            }
            function ObsoleteRecord() {
                return confirm('Are you sure want to update this record');
            }
        </script>
    </div>
</asp:Content>
