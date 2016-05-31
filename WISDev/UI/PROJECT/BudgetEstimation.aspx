<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="BudgetEstimation.aspx.cs" Inherits="WIS.BudgetEstimation" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width:100%">
        <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
        <fieldset class="icePnl">
            <legend>Budget Estimation</legend>
            <table border="0" align="center" width="70%">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Total Budget Estimation
                        </label>
                    </td>
                    <td align="left" style="vertical-align: middle">
                        <div style="float:left">
                            <asp:TextBox ID="TBSTextBox" runat="server" Style="display: block"  ReadOnly="True"
                                CssClass="iceTextBox" Width="293px" onKeyDown="doCheck()"/>
                        </div>
                        <div style="float:left">
                            &nbsp;<asp:Label ID="lblTotalBudgEstCurr" runat="server" CssClass="labelSuffix" Font-Bold="true" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Category</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="vertical-align: middle">
                        <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="iceDropDownlarge"
                            AutoPostBack="true" OnSelectedIndexChanged="CategoryDropDownList_SelectedIndexChanged" />
                        <asp:TextBox ID="BudgetEstimationIDTextBox" runat="server" Visible="false" />
                        <asp:RequiredFieldValidator ID="reqCategory" ControlToValidate="CategoryDropDownList"
                            InitialValue="0" ErrorMessage="Select a Category" Display="None" ValidationGroup="BdgEst"
                            runat="server" />
                        &nbsp;&nbsp;<input type="button" id="btnAddNewCategory" runat="server" class="icebutton" value="Add New Category"
                            style="width: 150px" onclick="document.getElementById('light1').style.display='block';document.getElementById('light').style.display='block';document.getElementById('fade1').style.display='block';" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Sub Category</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="vertical-align: middle">
                        <asp:DropDownList ID="SubCategoryDropDownList" runat="server" CssClass="iceDropDownlarge"
                            AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="SubCategoryDropDownList"
                            InitialValue="0" ErrorMessage="Select a Sub Category" Display="None" ValidationGroup="BdgEst"
                            runat="server" />
                        &nbsp;&nbsp;<input type="button" id="btnAddNewSubCategory" runat="server" class="icebutton" value="Add New Sub Category"
                            style="width: 150px" onclick="document.getElementById('light2').style.display='block';document.getElementById('light').style.display='block';document.getElementById('fade').style.display='block';" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Value</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="vertical-align: middle">
                        <asp:TextBox ID="ValueTextBox" runat="server" MaxLength="18" CssClass="iceTextBox" onblur="CalculateBGTPRECT();" />&nbsp;
                         <asp:Label ID="MillionUSH" runat="server" CssClass="iceLable" />
                       <%-- <asp:DropDownList ID="ddlCurrencyBudget" runat="server" CssClass="icefrmDropDown"
                            Width="70px" AppendDataBoundItems="true">
                           <asp:ListItem>--Select--</asp:ListItem>
                        </asp:DropDownList>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers,Custom" ValidChars=","
                            TargetControlID="ValueTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ValueTextBox"
                            ErrorMessage="Enter Budget Value" Display="None" ValidationGroup="BdgEst" runat="server" />
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="ValueTextBox"
                            ClientValidationFunction="CalculateBGTPRECTFinal" ErrorMessage="The Total Amount Value should be less than Total Budget Estimation."
                            ValidationGroup="BdgEst" Display="None"></asp:CustomValidator>
                        <asp:HiddenField ID="HiddenFieldTotalValue" runat="server" />
                 </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Percentage Value</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="vertical-align: middle">
                   
                        <asp:TextBox ID="ValuePerTextBox" runat="server" MaxLength="5" Width="50px" CssClass="iceTextBox"
                         onKeyDown="doCheck()" />&nbsp;<label class="iceLable">%</label>
                         &nbsp;&nbsp;<label class="labelSuffix">(rounded to 2 decimals)</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom,Numbers"
                            TargetControlID="ValuePerTextBox" ValidChars="." runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ValuePerTextBox"
                            ErrorMessage="Enter Budget Value %" Display="None" ValidationGroup="BdgEst" runat="server" />
                        <asp:CompareValidator ID="cvValuerPer" ControlToValidate="ValuePerTextBox" Operator="LessThanEqual"
                            ValueToCompare="100.00" Type="Double" ErrorMessage="Percentage cannot be more than 100"
                            Display="None" ValidationGroup="BdgEst" runat="server">
                        </asp:CompareValidator>
                          
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Account Code
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="CurrenceID" runat="server" CssClass="iceTextBox" MaxLength="10" />
                        <asp:TextBox ID="AcountNumberTextBox" runat="server" CssClass="iceTextBox" onblur="CheckAcountNumber();"  MaxLength="10" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredAcountNumberTextBox" FilterType="Numbers"
                            TargetControlID="AcountNumberTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredAcountNumberTextBox" ControlToValidate="AcountNumberTextBox"
                            ErrorMessage="Enter Account Number" Display="None" ValidationGroup="BdgEst" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="SaveButton" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="BdgEst"
                            OnClick="SaveButton_Click" />
                        <asp:Button ID="ClearButton" runat="server" CssClass="icebutton" Text="Clear" OnClick="ClearButton_Click" />
                        <asp:ValidationSummary ID="vsBdg" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="BdgEst" runat="server" />
                    </td>
                </tr>
            </table>
            <br />

            <table class="gridStyle" cellspacing="1" width="100%">
                <tr class="gridHeaderStyle">
                    <td align="center" style="height:20px;width:5%">
                        Sl. No.
                    </td>
                    <td>
                        &nbsp;&nbsp;Sub Category Name
                    </td>
                    <td align="center" style="width:12%">
                        Value
                    </td>
                    <td align="center" style="width:7%">
                        Value in %
                    </td>               
                    <asp:PlaceHolder runat="server" ID="plsedit" >
                    <td id="edittd" runat="server" align="center" style="width:6%">
                        Edit
                    </td>
                    <td  id="Deletetd" runat="server" align="center" style="width:7%">
                        Delete
                    </td></asp:PlaceHolder>
                </tr>
            </table>

            <table width="100%">                
            <asp:Repeater ID="rptBudgetEstCategory" runat="server" onitemdatabound="rptBudgetEstCategory_ItemDataBound">
                <ItemTemplate>                    
                        <tr>
                            <td style="background-color: #ffffff;border-top:1px solid #000000;">
                                &nbsp;&nbsp;<asp:Literal ID="lblEstCategoryName" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Literal>
                                <asp:HiddenField ID="hdnEstCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color:#ffffff">
                                <asp:GridView ID="grdBudgetEstimation" runat="server" CssClass="gridStyle" ShowHeader="false" ShowFooter="true"
                                    CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
                                    Width="100%" OnRowCommand="grdBudgetEstimation_RowCommand" OnRowDataBound="grdBudgetEstimation_RowDataBound">
                                    <FooterStyle CssClass="gridRowStyle" Font-Bold="true" HorizontalAlign="Center" />
                                    <RowStyle CssClass="gridRowStyle" />
                                    <AlternatingRowStyle CssClass="gridAlternateRow" />
                                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                                    <HeaderStyle CssClass="gridHeaderStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <%# Eval("CategoryName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Category Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("SubCategoryName")%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="TotalAmount" runat="server" Text="SUB TOTAL" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAmountDisplay" Text='<%#Eval("ValueAmount") %>' runat="server"></asp:Literal>
                                                <asp:Literal ID="litValueAmount" Text='<%#Eval("ValueAmount") %>' Visible="false"
                                                    runat="server"></asp:Literal>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="TotalAmount_cal" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value in %">
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            <ItemTemplate>
                                                <%# Eval("ValueAmountper", "{0:c}")%>
                                                &#37;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/Image/edit.gif"
                                                    CommandName="EditRow" CommandArgument='<%#Eval("BudgetEstimationID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                    CommandName="DeleteRow" CommandArgument='<%#Eval("BudgetEstimationID") %>' OnClientClick="return DeleteRecord();"
                                                    runat="server" />
                                                <asp:Literal ID="litBudgetEstimationID" Text='<%#Eval("BudgetEstimationID") %>' Visible="false"
                                                    runat="server"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    
                </ItemTemplate>
            </asp:Repeater>
            </table>
            
            <table class="gridStyle" cellspacing="1" width="100%">
                <tr class="gridHeaderStyle">
                    <td align="center" style="height:20px;width:5%">
                        
                    </td>
                    <td align="right">
                        <b>GRAND TOTAL</b>
                    </td>
                    <td align="right" style="width:12%">
                        <asp:Label ID="lblGrandTotal" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                    </td>
                    <td align="center" style="width:7%">
                        
                    </td>                      
                    <asp:PlaceHolder runat="server" ID="plsedit1" >
                    <td align="center" style="width:6%">  
                    </td>
                    <td align="center" style="width:7%">                        
                    </td></asp:PlaceHolder>
                </tr>
            </table>

            <script type="text/javascript" language="javascript">
                function DeleteRecord() {
                    return confirm('Are you sure want to delete this record');
                }
               
            </script>
        </fieldset>
    </div>
    <!-- Popup window-->
    <div style="width: 100%; height: 120%;">
        <div id="light1" class="white_content">
            <div style="clear: both">
            </div>
            <div style="margin-top: 60px; margin-left: 200px;">
                <div style="width: 50%">
                    <div style="clear: both;">
                    </div>
                    <fieldset class="icePnl" style="background-color: #eee;">
                        <table class="frmpopTable">
                            <tr class="frmMenu">
                                <td class="textboldform">
                                    &nbsp; Add New Category
                                </td>
                            </tr>
                        </table>
                        <hr class="icepophrtag" />
                        <table width="100%">
                            <tr>
                                <td align="left" width="22%">
                                    <asp:Label ID="AddCategoryLabel" runat="server" Text="Category" CssClass="iceLable" />
                                    <span class="mandatory">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="AddCategoryTextBox" runat="server" MaxLength="50" Class="iceTextBox"
                                        Width="220px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="AddCategoryTextBox"
                                        ErrorMessage="Enter a Category" Display="None" ValidationGroup="BdgEstCat" runat="server" />
                                        <ajaxToolkit:FilteredTextBoxExtender
                                            ID="vaccin" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,"
                                            TargetControlID="AddCategoryTextBox" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="padding-top: 10px">
                                    <asp:Button ID="Category_SaveButton" runat="server" Text="Save" CssClass="icebutton"
                                        ValidationGroup="BdgEstCat" OnClick="Category_SaveButton_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="BdgEstCat" runat="server" />
                                    <input type="button" id="Button1" class="icebutton" value="Close" onclick="document.getElementById('light1').style.display='none';document.getElementById('light').style.display='none';document.getElementById('fade1').style.display='none';" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
        </div>
        <div id="fade1" class="black_overlay">
            <%--background, used to hide the master page--%>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <!-- Popup window-->
    <div style="width: 100%; height: 120%;">
        <div id="light2" class="white_content">
            <div style="clear: both">
            </div>
            <div style="margin-top: 60px; margin-left: 200px;">
                <div style="width: 80%">
                    <div style="clear: both;">
                    </div>
                    <fieldset class="icePnl" style="background-color: #eee;">
                        <table class="frmpopTable">
                            <tr class="frmMenu">
                                <td class="textboldform">
                                    &nbsp; Add New Sub Category
                                </td>
                            </tr>
                        </table>
                        <hr class="icepophrtag" />
                        <table align="center" width="100%">
                            <tr>
                                <td align="left" style="width: 20%">
                                    <asp:Label ID="AddNewCategoryLabel1" runat="server" Text="Category" CssClass="iceLable" />
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="AddNewCategoryDropDownList" runat="server" CssClass="iceDropDownlarge"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="AddNewCategoryDropDownList"
                                        ErrorMessage="Select a Category" InitialValue="0" Display="None" ValidationGroup="BdgEstSubCat"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="AddSubCategoryLabel" runat="server" Text="Sub Category" CssClass="iceLable" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="AddSubCategoryTextBox" runat="server" MaxLength="100" Class="iceTextBox"
                                        Style="width: 295px;" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="azSubcategory" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars="- ,/\" TargetControlID="AddSubCategoryTextBox" runat="server">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="AddSubCategoryTextBox"
                                        ErrorMessage="Enter a Sub Category" Display="None" ValidationGroup="BdgEstSubCat"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="Sub_Category_SaveButton" runat="server" Text="Save" CssClass="icebutton"
                                        ValidationGroup="BdgEstSubCat" OnClick="Sub_Category_SaveButton_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="BdgEstSubCat" runat="server" />
                                    <input type="button" id="Button2" class="icebutton" value="Close" onclick="document.getElementById('light2').style.display='none';document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none';" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
        </div>
        <div id="fade" class="black_overlay">
            <%--background, used to hide the master page--%>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <script type="text/javascript">
        function CalculateBGTPRECT() {
            var Percentage;
            var TotalBGT = document.getElementById('<%=TBSTextBox.ClientID %>').value;
            var UserEnterBGT = document.getElementById('<%=ValueTextBox.ClientID %>').value;

            TotalBGT = TotalBGT.replace(/,/g, '');
            UserEnterBGT = UserEnterBGT.replace(/,/g, '');

            if (parseFloat(UserEnterBGT) == 0 || parseFloat(UserEnterBGT).toString() == 'NaN') {
                alert("Amount Value can not Be Zero or Empty");
                document.getElementById('<%=ValueTextBox.ClientID %>').value = "";
                return;
            }
            if (parseFloat(TotalBGT) >= parseFloat(UserEnterBGT)) {
                if (!isNaN(TotalBGT) && !isNaN(UserEnterBGT))
                    Percentage = ((UserEnterBGT * 100) / TotalBGT);
                else
                    Percentage = '';
                var totalPercent = Percentage.toFixed(2);
                document.getElementById('<%=ValuePerTextBox.ClientID %>').value = totalPercent;
                
            } else {
                alert("Amount Value should be less than Total Budget Estimation");
                document.getElementById('<%=ValuePerTextBox.ClientID %>').innerText = "";
            }
        }

        function CheckAcountNumber() {
            var AcountNumber = document.getElementById('<%=AcountNumberTextBox.ClientID %>').value;
            if (parseFloat(AcountNumber) == 0) {
                alert("Invalid Acount Number.Enter Valid Account Number.");
                document.getElementById('<%=AcountNumberTextBox.ClientID %>').value = "";
                return;
            }
        }

        function CalculateBGTPRECTFinal(oSrc, args) {
            var TotalBGT = document.getElementById('<%=TBSTextBox.ClientID %>').value;
            var UserEnterBGT = document.getElementById('<%=ValueTextBox.ClientID %>').value;
            var TotalHaving = document.getElementById('<%=HiddenFieldTotalValue.ClientID %>').value;

            TotalBGT = TotalBGT.replace(/,/g, '');
            UserEnterBGT = UserEnterBGT.replace(/,/g, '');

            var total = parseFloat(UserEnterBGT) + parseFloat(TotalHaving);
            if (parseFloat(total) > parseFloat(TotalBGT)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }

//        function CalculateBGTValue() {
//            var ValueAmount;
//            var TotalBGT = document.getElementById('<%=TBSTextBox.ClientID %>').value;
//            var UserEnterBgtPercent = document.getElementById('<%=ValuePerTextBox.ClientID %>').value;
//            var endTotal = "100";
//            if (parseFloat(UserEnterBgtPercent) == 0 || parseFloat(UserEnterBgtPercent).toString() == 'NaN') {
//                alert("Percentage Value can not Be Zero or Empty");
//                document.getElementById('<%=ValuePerTextBox.ClientID %>').value = "";
//                return;
//            }
//            if (parseFloat(UserEnterBgtPercent) > parseFloat(endTotal)) {
//                alert("Percentage Value should be less than Total Budget Estimation");
//            }
//            else {
//                if (!isNaN(TotalBGT) && !isNaN(UserEnterBgtPercent))

//                    ValueAmount = ((TotalBGT * UserEnterBgtPercent) / 100);
//                var total = Math.round(ValueAmount);
//                document.getElementById('<%=ValueTextBox.ClientID %>').innerText = total;
//            }
//            ValueAmount = "";
        //        }
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= SaveButton.ClientID  %>");
            var tat1 = document.getElementById("<%= ValueTextBox.ClientID  %>");
            var tat2 = document.getElementById("<%= ValuePerTextBox.ClientID  %>");
            var tat3 = document.getElementById("<%= CurrenceID.ClientID  %>");
           
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == ''
             && tat2.value.toString().replace(/^\s+/, '') == '' &&
             tat3.value.toString().replace(/^\s+/, '') == ''
             && btn.value.toString() == 'Save') {
                isDirty = 0;
            }
            else {
                isDirty = 1;

            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                return '';
            }
        }                

    </script>
</asp:Content>
