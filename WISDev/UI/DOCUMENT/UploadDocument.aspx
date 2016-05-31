<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UploadDocument.aspx.cs" Inherits="WIS.UploadDocument" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Upload Document Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Ramu.S
 * @Created Date 30-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnShowUpload" Text="Upload Document" runat="server"
                            CssClass="icebutton" onclick="btnShowUpload_Click" style="Width:100%"/>
                    </td>
                    <td>
                        <asp:Button ID="btnShowSearch" Text="Search Document" runat="server" 
                            CssClass="icebutton" onclick="btnShowSearch_Click" style="Width:100%"/>
                    </td>
                </tr>
            </table>
        </div>
 <asp:Panel ID="pnlSearchDocument" Visible="false" runat="server" DefaultButton="SearchUplaodButton">
    <fieldset class="icePnlinner">
        <legend>Search Document</legend>
        <table border="0" align="center" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Project" CssClass="iceLable" />
                </td>
                <td>
                    <asp:DropDownList ID="drpProjectsearch" runat="server" CssClass="iceDropDown" Width="300px"
                        AppendDataBoundItems="True" AutoPostBack="true"
                        onselectedindexchanged="drpProjectsearch_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="drpProjectsearch"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                   
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="HH ID " CssClass="iceLable" Enabled="false" />
                </td>
                <td>
                    <asp:TextBox ID="txtHHID" runat="server" Visible="false" CssClass="iceTextBox" MaxLength="8" ReadOnly="true" onKeyDown="doCheck()"/>
                    <asp:TextBox ID="txtHHIDDISP" runat="server" CssClass="iceTextBox" MaxLength="8" ReadOnly="true" onKeyDown="doCheck()"/>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"
                                TargetControlID="txtHHID" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                             <asp:ImageButton ID="imgSearchHHID" runat="server" Enabled="false" ImageAlign="Bottom" ToolTip="Click here to change PAP"
        ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
                </td>
                <td>
                    <label class="iceLable">
                        Keyword</label>
                </td>
                <td>
                    <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="iceTextBox" MaxLength="50"
                        Width="162px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtSearchKeyword" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=". " TargetControlID="txtSearchKeyword" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td colspan="6">
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
<asp:Panel ID="pnlUploadDocuments" runat="server" DefaultButton="SaveButton">
    <fieldset class="icePnlinner">
        <legend>Upload Document</legend>
        <table border="0" align="center" width="100%">
            <tr>
                <td >
                    <label class="iceLable">Project Name</label> <span class="mandatory">*</span>
                </td>
                <td >
                    <asp:DropDownList ID="drpProject" runat="server" CssClass="iceDropDown" Width="300px"
                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="drpProject_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                       <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                        TargetControlID="drpProject"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="drpProject"
                        InitialValue="0" ErrorMessage="Select Project" Display="None" ValidationGroup="vgfilMyFile"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
                <td >
                    <asp:Label ID="HHIDLabel" runat="server" Text="HH ID " CssClass="iceLable" Enabled="false" />
                </td>
                <td>
                  <table>
                     <tr>
                        <td>
                        <asp:TextBox ID="HHIDTextBox" runat="server" Visible="false" CssClass="iceTextBox" ReadOnly="true" />
                        <asp:TextBox ID="HHIDTextBoxDISP" runat="server" CssClass="iceTextBox" ReadOnly="true" />
                        </td>
                        <td>
                          <asp:ImageButton ID="imgSearch" runat="server" Enabled="false" ImageAlign="Bottom" ToolTip="Click here to change PAP" ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
                        </td>
                     </tr>
                  </table>                    
                </td>
            </tr>
            <tr>
                <td>
                    <label class="iceLable">Document Type</label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="DocTypeDropDownList" runat="server" CssClass="iceDropDownlarge"
                        Width="300px" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                     <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="DocTypeDropDownList"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                        ErrorMessage="Select a Document Type" ValidationGroup="vgfilMyFile" InitialValue="0"
                        ControlToValidate="DocTypeDropDownList"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <label class="iceLable">Document Path</label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:FileUpload ID="fileMyFile" Style="width: 96%" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvfilMyFile" runat="server" Display="None" ControlToValidate="fileMyFile"
                        ValidationGroup="vgfilMyFile" Text="Mandatory!!" ErrorMessage="Select a Document"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-top:8px">
                    <label class="iceLable">
                        Keyword</label>
                </td>
                 <td style="vertical-align:top;padding-top:8px">
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="iceTextBox" MaxLength="50"
                      width="162px"/><br /><label style="color:Red;">(Max 50 characters)</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtKeyword1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                ValidChars=". " TargetControlID="txtKeyword" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="vertical-align:top;padding-top:8px">
                    <label class="iceLable">
                        Description</label>
                </td>
                <td style="vertical-align: top;padding-top:8px">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="iceTextBox" MaxLength="500"
                        Width="96%" TextMode="MultiLine" Rows="3"  onkeydown="return checkMaxLength(this,500)"/><label style="color:Red;">(Max 500 characters)</label>
                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                ValidChars="." TargetControlID="txtSearchKeyword" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                </td> 
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                    <asp:Button ID="SaveButton" ValidationGroup="vgfilMyFile" runat="server" CssClass="icebutton"
                        Text="Save" OnClick="ButtonUpload_Click" />
                    <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgfilMyFile" runat="server" />
                  <%--  <asp:Button ID="ButtonUpload" runat="server" Text="Upload" 
                        onclick="ButtonUpload_Click" />--%>
                </td>
                <td>
                    <asp:Button ID="ClearButton" runat="server" CssClass="icebutton" Text="Clear" 
                        onclick="btnUploadClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    </asp:Panel>
       <asp:GridView ID="grdUploadDocument" runat="server" CssClass="gridStyle" CellPadding="4"
                        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                        PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="grdUploadDocument_RowDataBound" OnRowCommand="grdUploadDocument_RowCommand">
                        <RowStyle CssClass="gridRowStyle" />
                        <AlternatingRowStyle CssClass="gridAlternateRow" />
                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                        <HeaderStyle CssClass="gridHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DocumentCode" HeaderText="Document Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                             <asp:BoundField DataField="KeyWord" HeaderText="Keyword" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                              <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="UserName" HeaderText="Uploaded By" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="Date" HeaderText="Uploaded Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                              <asp:TemplateField HeaderText="View" Visible="false" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="5%" />
                               <ItemTemplate>
                                   <asp:ImageButton ID="imgView" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="ViewRow" CommandArgument='<%#Eval("PAPDOCUMENTID") %>'  />
                                  <asp:Literal ID="LtlProjectCode" runat="Server" Text = '<%#Eval("Projectcode") %>' visible = "False" />
                               </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" Width="5%" />
                               <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("DocumentPath") %>' ClientIDMode="Static" runat="server">
                                    <img src="../../IMAGE/edit.gif" style="background-color: transparent; border: 0;" id="img1" alt=""/></asp:HyperLink>
                               </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                             <ItemStyle HorizontalAlign="Center" Width="5%" />
                               <ItemTemplate>
                                   <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PAPDOCUMENTID") %>' OnClientClick="return DeleteRecord();" />
                                  <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("PAPDOCUMENTID") %>' Visible="false"></asp:Literal>
                                  <asp:Literal ID="LtlDocumentPath" runat="Server" Text = '<%#Eval("DocumentPath") %>' visible = "False" />
                                  <asp:Literal ID="LilProjectCode" runat="Server" Text = '<%#Eval("Projectcode") %>' visible = "False" />
                                  <asp:Literal ID="LitHHID" runat="Server" Text = '<%#Eval("HHID") %>' visible = "False" />
                               </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                     <script language="javascript" type="text/javascript">
                         function DeleteRecord() {
                             return confirm('Are you sure you want to delete this record?');
                         }
            </script>

              <script language="javascript" type="text/javascript">
//                  function textboxMultilineMaxNumber(txt, maxLen) {
//                      try {
//                          if (txt.value.length > (maxLen - 1)) return false;
//                      } catch (e) {
//                      }
                  //                  }

                  function SetVisible(val) {
                      var hf = document.getElementById("<%= hfVisible.ClientID  %>");
                      hf.value = val;
                  }

                  function ViewFile(path) {
                      var left = (screen.width - 800) / 2;
                      var top = (screen.height - 500) / 4;
                      window.open(path);
                  }

                  function ViewUploadDocument(PAPDOCUMENTID, ProjectCode) {
                      var left = (screen.width - 650) / 2;
                      var top = (screen.height - 640) / 4;
                      open('../ViewUploadDoc.aspx?papDocumentID=' + PAPDOCUMENTID + '&ProjectCode=' + ProjectCode, 'ChangeRequest', 'width=650px,height=640px,resizable=1,top=' + top + ', left=' + left);
                  }
                  function checkMaxLength(e, maxLength) {
                      //Calling will be lyk this
                      //onkeydown="return checkMaxLength(this,1000)"
                      if (e.value.length > parseInt(maxLength)) {
                          //               alert('no no no');
                          // Set value back to the first 6 characters 
                          e.value = e.value.substring(0, parseInt(maxLength));
                      }
                      return true;
                  }
</script>
<iframe id="contentPanel1" runat="server" visible="true" style="height:0px;width:0px;"></iframe>
<script type ="text/javascript">
    function doCheck() {
        var keyCode = (event.which) ? event.which : event.keyCode;
        if ((keyCode == 8) || (keyCode == 46))
            event.returnValue = false;
    }
</script>

</asp:Content>
