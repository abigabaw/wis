<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="EmailPopUpwindow.aspx.cs" Inherits="WIS.EmailPopUpwindow" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script language="javascript" type="text/javascript">
      function Aftersend() {
          window.opener.location.replace(window.opener.location.pathname)
//          window.opener.location.reload();
            window.close();
        }

        function AfterNogAmount() {
            // window.opener.location.reload();
//            if (opener) {
//                opener.refreshValue();
//             }
            //            window.close();
            window.opener.location.replace(window.opener.location.pathname)
            //          window.opener.location.reload();
            window.close();
        }

        window.onbeforeunload = function doCleanup() {
            var hf = document.getElementById("<%= hf1.ClientID  %>");
            if (hf != 'undefined' && hf.value == "1") {
                window.opener.location.replace(window.opener.location.pathname);
            }
        }

 </script>
   <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:HiddenField ID="hf1" runat="server" Value="0" />
   <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgEmailPOPUP" runat="server" />
    <fieldset>
        <legend>Email</legend>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="EmailToLabel" runat="server" Text="Email To" CssClass="iceLable" />
                            </td>
                            <td>
                                <asp:TextBox ID="EmailToTextBox" runat="server" CssClass="iceTextBoxLarge" Readonly="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="EmailSubjectLabel" runat="server" Text="Subject" CssClass="iceLable" />
                            </td>
                            <td>
                                <asp:TextBox ID="EmailSubjectTextBox" runat="server" CssClass="iceTextBoxLarge" />
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="EmailAttachmentLabel" runat="server" Text="Attachment" CssClass="iceLable" />
                            </td>
                            <td>
                               <asp:FileUpload ID="EmailAttachmentFileUpload" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="EmailBodyTextBox" runat="server" TextMode="MultiLine" CssClass="iceTextAeralarge"
                        Height="150px" Width="500px" />
                          <asp:RequiredFieldValidator ID="RVEmailBodyTextBox" ControlToValidate="EmailBodyTextBox"
                    ErrorMessage="Enter Email Body" Display="None" ValidationGroup="vgEmailPOPUP"
                    runat="server"></asp:RequiredFieldValidator>
  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom" ValidChars=". -,() :
  " TargetControlID="EmailBodyTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>

                </td>
            </tr>
            <asp:TextBox ID="WorkFlowApproverIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
            <asp:TextBox ID="StatusIDTextBox" runat="server" CssClass="iceTextBoxLarge"  Visible="false"/>
            
            <asp:TextBox ID="ApproverUserIdTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
            <asp:TextBox ID="WorkFlowDefinitionIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
             <asp:TextBox ID="ProjectCodeTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
              <asp:TextBox ID="ProjectNameTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
              <asp:TextBox ID="HHIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
              <asp:TextBox ID="PageCodeTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false"/>
            <tr>
                <td width="100%">
                    <table width="100%">
                        <tr>
                            <td align = "center">
                                <asp:Button ID="SendButton" runat="server" CssClass="icebutton" Text="Send" 
                                    onclick="SendButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
