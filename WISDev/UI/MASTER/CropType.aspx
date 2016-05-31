<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="CropType.aspx.cs"
 Inherits="WIS.CropType" MasterPageFile="~/Site.Master" %>

 <%@ MasterType VirtualPath="~/Site.Master" %>

 <asp:Content ID ="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 </asp:Content>
 <%--/**
 * 
 * @version		 0.1 CropType Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Irran
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
 <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
 <div id="divAll">
 <div style="width:100%">            
        <asp:Panel ID="pnlSave" runat="server" Visible="true">
                <fieldset class="icePnlinner">
                <legend>Crop Type Details</legend>
                <table border="0" width="60%" align="center">
                  <tr>   
                      

                      <td align="left" width="25%">
                          <label class="iceLable">Crop Type</label> <span class="mandatory">*</span>&nbsp;                 
                      </td>
                      <td>
                         <asp:TextBox ID="txtCropType" runat="server" MaxLength="100" Width="300" CssClass="iceTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="reqCropType" runat="server" ErrorMessage="Enter Crop Type"
                          ControlToValidate="txtCropType" Display="None" ValidationGroup="CropType"></asp:RequiredFieldValidator>
                          <ajaxToolkit:FilteredTextBoxExtender ID="fteCropDiameter" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" " TargetControlID="txtCropType" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                          <asp:TextBox ID="txtCropTypeID" runat="server" Visible="false" CssClass="iceTextBox"></asp:TextBox>
                      </td>                      
                  </tr>
                  <tr>                                                
                      <td align="center" colspan="2">
                          <div style="margin-top:20px">
                              <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" 
                                  onclick="btnSave_Click"  ValidationGroup="CropType"/>
                              &nbsp;&nbsp;
                              <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" 
                                  onclick="btnClear_Click" />
                          </div>
                      </td>
                    </tr>
                </table>   
                <asp:ValidationSummary ID="valsumCropType" runat="server" ShowSummary="false" ShowMessageBox="true"
                       HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="CropType"  />                    
                </fieldset>   
                </asp:Panel>
                
                
     <asp:GridView ID="gvCropType" runat="server" AllowPaging="True" AllowSorting="True"
      AutoGenerateColumns="False" Width="100%" CellPadding="4" CellSpacing="1" PageSize="10" 
            GridLines="None" onpageindexchanging="gvCropType_PageIndexChanging" 
            onrowcommand="gvCropType_RowCommand"  >
            <HeaderStyle CssClass="gridHeaderStyle" />
             <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
         <Columns>
             <asp:TemplateField HeaderText="SI No">
               <ItemStyle HorizontalAlign="Center" Width="5%"/>
                 <ItemTemplate>
                         <%#Container.DataItemIndex+1 %>
                 </ItemTemplate>
             </asp:TemplateField>
        

             <asp:BoundField DataField="CROPTYPE" HeaderText="Crop Type"  
             HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%"/>
                <%--<asp:BoundField DataField="UNITNAME" HeaderText="Unit of Measure"  
             HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%"/>--%>

             <asp:TemplateField HeaderText="Edit">
             <ItemStyle  HorizontalAlign="Center" Width="5%"/>
               <ItemTemplate>
                            <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif" 
                            CommandName="EditRow" CommandArgument='<%#Eval("CROPTYPEID") %>'/>                
               </ItemTemplate>                 
             </asp:TemplateField> 

             <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" 
                OncheckedChanged="IsObsolete_CheckedChanged" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>' />                
                </ItemTemplate>
                </asp:TemplateField>

             <asp:TemplateField HeaderText="Delete">
             <ItemStyle  HorizontalAlign="Center" Width="5%"/>
             <ItemTemplate>
                 <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                  CommandName="DeleteRow"  CommandArgument='<%#Eval("CROPTYPEID") %>' OnClientClick="return DeleteRecord();" />
                 <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("CROPTYPEID") %>' Visible="false" ></asp:Literal>

             </ItemTemplate>
             </asp:TemplateField>
         </Columns>


     </asp:GridView>
     </div>
                
                <script type="text/javascript" language="javascript">
                    function DeleteRecord() 
                    {
                        return confirm('Are you sure you want to delete this record');
                    }

                    function ObsoleteRecord() {
                        return confirm('Are you sure you want to update this record');
                    }
                    document.getElementById('divAll').onclick = function () {
                        isDirty = 0;
                        setTimeout(function () { isDirty = 1; }, 100);
                    };

                    var isDirty = 0;
                    function setDirty() {
                        isDirty = 1;
                    }

                    window.onbeforeunload = function DoSome() {
                        if (isDirty == 1) {
                            //isDirty = 2;
                            return '';
                        }
                    }        
                </script>
                             
    </div> 
</asp:Content>         
        
    
