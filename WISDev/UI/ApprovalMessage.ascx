<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalMessage.ascx.cs" Inherits="WIS.ApprovalMessage" %>
<%--<a id="lnkApprovalComments" href="#" runat="server"><asp:Label ID="lblApprovalMessage" runat="server" Style="text-decoration: blink;color:Red;font-family:Arial; font-size: 16px; font-weight: bold" /></a>--%>

<asp:Label ID="lblApprovalMessage" runat="server" Style="text-decoration: blink;color:Red;font-family:Arial; font-size: 16px; font-weight: bold" />

<%--<script type="text/javascript">
  function OpenApproverDocumnet(ProjectID, WorkFlowCode, pageCode) {
            var left = (screen.width - 850) / 2;
            var top = (screen.height - 350) / 4;
            open('../COMPENSATION/ConversationLog.aspx?ProjectID=' + ProjectID + '&WorkFlowCode=' + WorkFlowCode + '&pageCode=' + pageCode, 'ChangeRequest', 'width=850px,height=350px,scrollbars=1,top=' + top + ', left=' + left);
        }

        </script>--%>