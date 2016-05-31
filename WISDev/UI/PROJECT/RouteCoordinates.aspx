<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="RouteCoordinates.aspx.cs" Inherits="WIS.RouteCoordinates" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="../../Scripts/CoordinateConversion.js"></script>
<script type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        var width = 760;
        var height = 600;
        window.resizeTo(width, height);
        window.onresize = function () { window.resizeTo(width, height); } 

        function AfterSaveCoordinates() {
            if (opener) {
                //               window.opener.document.location.reload(true);
                window.opener.location.replace(window.opener.location.pathname);
           }
           window.close();            
        }

        function UploadCoordinates() {
            open('../RouteCoordinatesPopup.aspx', 'expWin', 'width=800px,height=700px');
        }

        function CalcLatLong() {
            easting = parseFloat(document.getElementById('<%=txtX.ClientID %>').value);
            northing = parseFloat(document.getElementById('<%=txtY.ClientID %>').value);
            Zone = parseFloat(document.getElementById('<%=txtZone.ClientID %>').value);

            arrLatLong = UTMtoGeog(easting, northing, Zone);
            var lt = arrLatLong[0];
            var ln = arrLatLong[1];
            document.getElementById('<%=txtLatitude.ClientID %>').value = lt;
            document.getElementById('<%=txtLongitude.ClientID %>').value = ln;
        }

        function CheckDecimal(e, src) {
            if (e.keyCode == 46) { // Invoke when press Enter Key
                //                var char = document.getElementById('<%=txtX.ClientID %>').value;
                var char = src.value;
                if (char.indexOf(".") == -1) {
                    return true;
                }
                else if (char.indexOf(".") > -1) {
                    return false;
                }
                return true;
            }
            return true;
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100%">
        
        <span id="spnWhichParents"  style="display: none">
                <table>
                    <tr>
                        <td>
                            <b> Latitude (Degrees): </b><asp:TextBox ID="txtLatitude" runat="server" Text="" Width="80px"></asp:TextBox>
                        </td>                                
                        <td>
                            <b> Longitude (Degrees): </b><asp:TextBox ID="txtLongitude" runat="server" Text="" Width="80px" ></asp:TextBox>                                                
                        </td> 
                        <td>
                            <b> Zone: </b><asp:TextBox ID="txtZone" runat="server" ClientIDMode="Static" Text="36" Width="40px" ReadOnly="true"></asp:TextBox>
                        </td> 
                        <td>
                            South of Equator: <input type="checkbox" id="SHemBox"/>
                        </td>
                    </tr>
                </table>  
        </span>      
        <asp:Panel ID="pnlRouteCoordinatesDetails" runat="server">
            <fieldset class="icePnlinner">
                <legend>Coordinates </legend>
                <%--<asp:UpdatePanel ID="updtpnl1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <table align="center" border="0" width="60%">
                            <tr>
                                <td align="left" class="iceLable">
                                    X:
                                    <asp:TextBox ID="txtX" runat="server" MaxLength="10" Style="width: 70px" onkeypress="return CheckDecimal(event, this)" onblur="CalcLatLong();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqXCoordinate" ControlToValidate="txtX" ErrorMessage="Enter X Co-ordinate"
                                        Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteXCoordinate" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtX" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td align="left" class="iceLable">
                                    Y:
                                    <asp:TextBox ID="txtY" runat="server" MaxLength="10" Style="width: 70px" onkeypress="return CheckDecimal(event, this)" onblur="CalcLatLong();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqYCoordinate" ControlToValidate="txtY" ErrorMessage="Enter Y Co-ordinate"
                                        Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteYCoordinate" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtY" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td align="left" class="iceLable">
                                    Z:
                                    <asp:TextBox ID="txtZ" runat="server" MaxLength="10" Style="width: 70px" onkeypress="return CheckDecimal(event, this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqZCoordinate" ControlToValidate="txtZ" ErrorMessage="Enter Z Co-ordinate"
                                        Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteZCoordinate" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtZ" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr >
                                <td colspan="3" align="center">                        
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_Save" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_Clear" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>--%>
                <table align="center" border="0" width="100%">
                    <tr>
                        <td align="center" colspan="3">
                            <br />
                            <asp:Button ID="btn_Save" Text="Save" runat="server"
                                class="icebutton" OnClick="btn_Save_Click" OnClientClick="DisableOnSaveWithVal(this);" UseSubmitBehavior="false"/>&nbsp;<asp:Button ID="btn_Clear" runat="server"
                                    Text="Clear" class="icebutton" OnClick="btn_Clear_Click" />
                            &nbsp;
                            <input type="button" id="btnClose" value="Close" class="icebutton" onclick="AfterSaveCoordinates();" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        
    <asp:Panel ID="pnlFileUpload" Visible="false" runat="server">
        <table border="0" width="100%" align="center">
            <tr>
                <td>
                    <fieldset class="icePnl1">
                        <legend>Upload Coordinates</legend>
                        <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
                            <tr>
                                <td class="iceLable" style="width: 15%">
                                    Select File
                                </td>
                                <td align="left" style="width: 40%">
                                    <asp:FileUpload ID="FileUpload" class="iceTextBox" runat="server" Width="250px" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnUpload" Text="Upload" runat="server" class="icebutton" Style="width: 100px"
                                        OnClick="BtnUpload_Click"  OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />
                                    <asp:Button ID="btnCancelUpload" Text="Cancel" runat="server" class="icebutton" Style="width: 100px"
                                        OnClick="btnCancelUpload_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
        <asp:ValidationSummary ID="valSummaryCoordinate" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="CoordinateGroup" runat="server" />
        <asp:Panel ID="pnlMap" runat="server">
            <a id="lnkMap" href="#" target="_new" visible="false" runat="server">View on Map</a>
            <div style="float: right;">
                <asp:Button ID="btn_ImportExcel" Text="Import from Excel" runat="server" CssClass="icebutton"
                    Visible="true" Style="width: 140px" OnClick="btn_ImportExcel_Click" OnClientClick="DisableOnSave(this);"
                    UseSubmitBehavior="false" />
                &nbsp;
            </div>
        </asp:Panel>
        <%--<asp:UpdatePanel ID="updRoute" UpdateMode="Conditional" runat="server">
            <ContentTemplate>--%>
                <asp:GridView ID="GrdRouteCoordinates" ClientIDMode="Static" 
            runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" 
            Width="100%" AllowPaging="False"
                    OnRowCommand="GrdRouteCoordinates_RowCommand" 
            OnPageIndexChanging="GrdRouteCoordinates_PageIndexChanging" 
            onrowdatabound="GrdRouteCoordinates_RowDataBound">
                    <RowStyle CssClass="gridRowStyle" />
                    <alternatingrowstyle cssclass="gridAlternateRow" />
                    <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" font-bold="true" forecolor="White" />
                    <HeaderStyle CssClass="gridHeaderStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="X_axis" HeaderText="X" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Y_axis" HeaderText="Y" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Z_axis" HeaderText="Z" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" /> 
                        <asp:TemplateField HeaderText="LATITUDE" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtLATITUDE" onKeyDown="doCheck()" Width="100px" runat="server" Text='<%#Eval("LATITUDE") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="LONGITUDE" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"  />
                            <ItemTemplate>
                                <asp:TextBox ID="txtLONGITUDE" onKeyDown="doCheck()" Width="100px" runat="server" Text='<%#Eval("LONGITUDE") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField>                            
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                    CommandName="EditRow" CommandArgument='<%#Eval("ROUTE_COORDINATEID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                    CommandName="DeleteRow" CommandArgument='<%#Eval("ROUTE_COORDINATEID") %>' OnClientClick="return DeleteRecord();"
                                    runat="server" />
                                <asp:Literal ID="litROUTE_COORDINATEID" Text='<%#Eval("ROUTE_COORDINATEID") %>' Visible="false"
                                    runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            <%--/ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLoadCordinate" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btn_Save" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btn_Clear" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
        <center>
            <br />
            <asp:Button ID="btnLoadCordinate" runat="server" Text="Save" CssClass="icebutton" Visible="false"
                OnClick="btnLoadCordinate_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />
            <asp:Button ID="btnGridDataCancel" runat="server" Text="Cancel" CssClass="icebutton"
                OnClick="btnGridDataCancel_Click" Visible="false" />
        </center>
        <asp:HiddenField ID="hdnFilePath" ClientIDMode="Static" runat="server" />
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to delete this record?');
            }

            function SendFilePath(filePath) {
                document.getElementById('hdnFilePath').value = filePath;
                //PageMethods.GetUploadedData(filePath, OnWSGetUploadedDataComplete);
                $get('<%=btnLoadCordinate.ClientID%>').click();
            }

            function OnWSGetUploadedDataComplete(result) {
                if (result)
                    alert('Done.');
            }

            function DisableOnSaveWithVal(src) {
                if (Page_ClientValidate()) {
                    src.disabled = true;
                    src.value = 'Please Wait...';
                }
            }

            function isIE() {
                var myNav = navigator.userAgent.toLowerCase();
                return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
            }

            function CalcLatLongForGrid() {
                tbl = document.getElementById('<%=GrdRouteCoordinates.ClientID %>');
                for (rw = 1; rw < tbl.rows.length; rw++) {
                    row = tbl.rows[rw];
                    if (isIE()) {
                        easting = parseFloat(row.cells[1].innerText.toString());
                        northing = parseFloat(row.cells[2].innerText.toString());
                    } else {
                        easting = parseFloat(row.cells[1].textContent.toString());
                        northing = parseFloat(row.cells[2].textContent.toString());
                    }
                    Zone = parseFloat(36);

                    arrLatLong = UTMtoGeog(easting, northing, Zone);
                    var lt = arrLatLong[0];
                    var ln = arrLatLong[1];
                    txt1= row.cells[4].getElementsByTagName('input');
                    txt2 = row.cells[5].getElementsByTagName('input');
                    
                    elem1 = txt1[0];
                    elem2 = txt2[0];
                    elem1.value = lt;
                    elem2.value = ln;
                }
            }

            function doCheck() {
                var keyCode = (event.which) ? event.which : event.keyCode;
                if ((keyCode == 8) || (keyCode == 46))
                    event.returnValue = false;
            }

            function DisableOnSave(src) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }
        </script>
    </div>
</asp:Content>
