<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="PersonalIdentification.aspx.cs" Inherits="WIS.PersonalIdentification" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class = "icePnlinner">
      <legend> Report Criteria </legend>
        <table width = "100%" align = "center" border = "0">
          <tr>
           <td align="left">
                    <label class="iceLable">
                        Project Name
                    </label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" Width="250px" AppendDataBoundItems="true"
                        runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlProject" runat="server" ControlToValidate="ddlProject"
                        ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Project to search Paps"
                        Display="None">
                    </asp:RequiredFieldValidator>
                </td>
           <%-- <td align="left">
                    <label class="iceLable">
                        HHID
                    </label>
                </td>
                 <td align="left">
                    <asp:TextBox ID="txthhid" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="100px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="fte1" FilterType="Numbers"
                        ValidChars=" " TargetControlID="txthhid" runat="server" />
                </td>--%>
         
          
          </tr>

          <tr>
          <td align = "left">
           <label class = "iceLable">
                PAP Name
           </label>
          </td>
          <td align = "left">
            <asp:TextBox ID="txtname" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" " TargetControlID="txtname" runat="server" />
          </td>


             <td align = "left">
           <label class = "iceLable">
                Gender
           </label>
          </td>
          <td align = "left">
           <asp:DropDownList ID="ddlgender" CssClass="iceTextBox" AppendDataBoundItems="true"
              AutoPostBack="true" Width="100px" runat="server" >
            <asp:ListItem Value="0">--Select--</asp:ListItem>
            <asp:ListItem Value="1"> Male </asp:ListItem>
            <asp:ListItem Value="2">Female</asp:ListItem>
             </asp:DropDownList>
                             
          </td>
          </tr>

          <tr>
          <td align = "left">
          <label class = "iceLable">
          Tribe
          </label>
          </td>
          <td align = "left">
          <asp:DropDownList ID="ddlTribe" CssClass="iceTextBox" AppendDataBoundItems="true"
              AutoPostBack="true" Width="250px" runat="server" 
                  onselectedindexchanged="ddlTribe_SelectedIndexChanged" >
            <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
          </td>
           <td align = "left">
          <label class = "iceLable">
          Clan
          </label>
          </td>
          <td align = "left">
           <%--<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">--%>
          <asp:DropDownList ID="ddlClan" CssClass="iceTextBox" AppendDataBoundItems="true"
              AutoPostBack="true" Width="250px" runat="server" >
            <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
          </td>
          </tr>

           <tr>
          <td align = "left">
          <label class = "iceLable">
          Religion
          </label>
          </td>
          <td align = "left">
          <asp:DropDownList ID="ddlReligion" CssClass="iceTextBox" AppendDataBoundItems="true"
              AutoPostBack="true" Width="250px" runat="server" >
            <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
          </td>
           <td align = "left">
          <label class = "iceLable">
          Option Group
          </label>
          </td>
          <td align = "left">
          <asp:DropDownList ID="ddloptiongroup" CssClass="iceTextBox" AppendDataBoundItems="true"
              AutoPostBack="true" Width="250px" runat="server" >
            <asp:ListItem Value="0">--Select--</asp:ListItem>

                    </asp:DropDownList>
          </td>
          </tr>

            <tr>
                <td colspan="4" align="center">
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
        
           <script language="javascript" type="text/javascript">
               function OpenReport() {
                   var fldTribe = document.getElementById('<%=ddlTribe.ClientID%>');
                   var fldClan = document.getElementById('<%=ddlClan.ClientID%>');
                   var fldReligion = document.getElementById('<%=ddlReligion.ClientID%>');
                   var fldOptionGroup = document.getElementById('<%=ddloptiongroup.ClientID%>');
               
                   var fldPAPName = document.getElementById('<%=txtname.ClientID%>');
                   var fldGender = document.getElementById('<%=ddlgender.ClientID%>');
                   var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');


                   var Tribe;
                   var Clan;
                   var Religion;
                   var religionName;
                   var OptionGroup;
                   var optionGroupName;

                   var PAPName;
                   var Gender;
                   var projectID;

                  
                   if (fldprojectID.selectedIndex > 0) {
                       projectID = fldprojectID.options[fldprojectID.selectedIndex].value;
                   }
                   else {
                       alert('Please select a Project Name');
                       return;
                   }

                   if (fldTribe.selectedIndex > 0)
                       Tribe = fldTribe.options[fldTribe.selectedIndex].text;
                   else
                       Tribe = '';

                   if (fldClan.selectedIndex > 0)
                       Clan = fldClan.options[fldClan.selectedIndex].text;
                   else
                       Clan = '';

                   if (fldReligion.selectedIndex > 0) {
                       Religion = fldReligion.options[fldReligion.selectedIndex].value;
                       religionName = fldReligion.options[fldReligion.selectedIndex].text;
                   }
                   else {
                       Religion = 0;
                       religionName = '';
                   }

                   if (fldOptionGroup.selectedIndex > 0) {
                       OptionGroup = fldOptionGroup.options[fldOptionGroup.selectedIndex].value;
                       optionGroupName = fldOptionGroup.options[fldOptionGroup.selectedIndex].text;
                   }
                   else {
                       OptionGroup = 0;
                       optionGroupName = '';
                   }

                   PAPName = fldPAPName.value;
               

                   if (fldGender.selectedIndex > 0)
                       Gender = fldGender.options[fldGender.selectedIndex].text;
                   else
                       Gender = '';

                   var left = (screen.width - 960) / 2;
                   var top = (screen.height - 650) / 4;

                   var param = 'rptCode=PIR' +
                        '&Tribe=' + Tribe +
                        '&Clan=' + Clan +
                        '&Religion=' + Religion +
                        '&OptionGroup=' + OptionGroup +                 
                        '&PAPName=' + PAPName +
                        '&Gender=' + Gender +
                        '&religionName=' + religionName +
                        '&optionGroupName=' + optionGroupName + 
                         '&ProjectID=' + projectID;

                   open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
               }
    </script>
</asp:Content>
