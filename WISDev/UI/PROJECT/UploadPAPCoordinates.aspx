<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SitePopup.Master" CodeBehind="UploadPAPCoordinates.aspx.cs" 
Inherits="WIS.UploadPAPCoordinates" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="../../Scripts/CoordinateConversion.js"></script>
<script type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     <script type="text/javascript">
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

         function CheckDecimal(e, src) {
             if (e.keyCode == 46) { 
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

         function CalcLatLong() {
             easting = parseFloat(document.getElementById('<%=txtROWX.ClientID %>').value);
             northing = parseFloat(document.getElementById('<%=txtROWY.ClientID %>').value);
             Zone = parseFloat(document.getElementById('<%=txtROWZone.ClientID %>').value);

             arrLatLong = UTMtoGeog(easting, northing, Zone);
             var lt = arrLatLong[0];
             var ln = arrLatLong[1];
             document.getElementById('<%=txtROWLatitude.ClientID %>').value = lt;
             document.getElementById('<%=txtROWLongitude.ClientID %>').value = ln;
         }

         function CalcLatLongWL() {
             easting = parseFloat(document.getElementById('<%=txtWLX.ClientID %>').value);
             northing = parseFloat(document.getElementById('<%=txtWLY.ClientID %>').value);
             Zone = parseFloat(document.getElementById('<%=txtWLZone.ClientID %>').value);

             arrLatLong = UTMtoGeog(easting, northing, Zone);
             var lt = arrLatLong[0];
             var ln = arrLatLong[1];
             document.getElementById('<%=txtWLLatitude.ClientID %>').value = lt;
             document.getElementById('<%=txtWLLongitude.ClientID %>').value = ln;
         }
    </script>
    <span id="spnWhichParents" style="display: none">
        <table>
            <tr>
                <td>
                    <b>Latitude (Degrees): </b>
                    <asp:TextBox ID="txtROWLatitude" runat="server" Text="" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <b>Longitude (Degrees): </b>
                    <asp:TextBox ID="txtROWLongitude" runat="server" Text="" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <b>Zone: </b>
                    <asp:TextBox ID="txtROWZone" runat="server" ClientIDMode="Static" Text="36" Width="40px"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    South of Equator:
                    <input type="checkbox" id="Checkbox1" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Latitude (Degrees): </b>
                    <asp:TextBox ID="txtWLLatitude" runat="server" Text="" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <b>Longitude (Degrees): </b>
                    <asp:TextBox ID="txtWLLongitude" runat="server" Text="" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <b>Zone: </b>
                    <asp:TextBox ID="txtWLZone" runat="server" ClientIDMode="Static" Text="36" Width="40px"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    South of Equator:
                    <input type="checkbox" id="Checkbox2" />
                </td>
            </tr>
        </table>
    </span>
    <asp:Panel ID="pnlFileUpload" Visible="false" runat="server">
        <table border="0" width="100%" align="center">
            <tr>
                <td>
                    <fieldset class="icePnlinner">
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
                                    <%--<asp:Button ID="btnCancelUpload" Text="Close" runat="server" class="icebutton" Style="width: 100px"
                                         OnClientClick="AfterSaveCoordinates();"/>--%>
                                    <asp:Button ID="btn_Cancel" Text="Cancel" runat="server" class="icebutton" Style="width: 100px"
                                        OnClick="btn_Cancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlRouteCoordinatesDetails" runat="server">
        <fieldset class="icePnlinner">
            <legend>Coordinates </legend>
            <table align="center" border="0" width="100%">
                <tr>
                    <td align="left" class="iceLable">
                        Right Of Way X:</td><td>
                        <asp:TextBox ID="txtROWX" runat="server" MaxLength="10" Style="width: 120px" onkeypress="return CheckDecimal(event, this)"
                            onblur="CalcLatLong();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqXCoordinate" ControlToValidate="txtROWX" ErrorMessage="Enter Right Of Way X Co-ordinate"
                            Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteXCoordinate" FilterType="Numbers,Custom"
                            ValidChars="." TargetControlID="txtROWX" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left" class="iceLable">
                        Right Of Way Y:</td><td>
                        <asp:TextBox ID="txtROWY" runat="server" MaxLength="10" Style="width: 120px" onkeypress="return CheckDecimal(event, this)"
                            onblur="CalcLatLong();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqYCoordinate" ControlToValidate="txtROWY" ErrorMessage="Enter Right Of Way Y Co-ordinate"
                            Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteYCoordinate" FilterType="Numbers,Custom"
                            ValidChars="." TargetControlID="txtROWY" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="iceLable">
                        Wayleave X:</td><td>
                        <asp:TextBox ID="txtWLX" runat="server" MaxLength="10" Style="width: 120px" onkeypress="return CheckDecimal(event, this)"
                            onblur="CalcLatLongWL();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtWLX" ErrorMessage="Enter Wayleave X Co-ordinate"
                            Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers,Custom"
                            ValidChars="." TargetControlID="txtWLX" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left" class="iceLable">
                        Wayleave Y:
                        </td><td>
                        <asp:TextBox ID="txtWLY" runat="server" MaxLength="10" Style="width: 120px" onkeypress="return CheckDecimal(event, this)"
                            onblur="CalcLatLongWL();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtWLY" ErrorMessage="Enter Wayleave Y Co-ordinate"
                            Display="None" runat="server" ValidationGroup="CoordinateGroup"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                            ValidChars="." TargetControlID="txtWLY" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                    </td>
                </tr>
            </table>
            <table align="center" border="0" width="100%">
                <tr>
                    <td align="center" colspan="3">
                        <br />
                        <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton" OnClientClick="DisableOnSaveWithVal(this);"
                            UseSubmitBehavior="false" onclick="btn_Save_Click" />&nbsp;<asp:Button 
                            ID="btn_Clear" runat="server" Text="Clear"
                                class="icebutton" onclick="btn_Clear_Click" />
                        &nbsp;
                        <input type="button" id="btnClose" value="Close" class="icebutton" onclick="AfterSaveCoordinates();" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlMap" runat="server">
       <%-- <a id="lnkMap" href="#" target="_new" visible="false" runat="server">View on Map</a>--%>
        <div style="float: right;">
            <asp:Button ID="btn_ImportExcel" Text="Import from Excel" runat="server" CssClass="icebutton"
                Visible="true" Style="width: 140px" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false"
                OnClick="btn_ImportExcel_Click" />
            &nbsp;
        </div>
    </asp:Panel>
    <fieldset class="icePnlinner">
        <legend id="lgndTitle" runat="server">PAP Coordinates</legend>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%" >
            <asp:GridView ID="GrdPAPCoordinates" ClientIDMode="Static" 
            runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" 
            Width="100%" AllowPaging="False"
                    OnRowCommand="GrdPAPCoordinates_RowCommand" 
            onrowdatabound="GrdPAPCoordinates_RowDataBound">
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
                        <asp:BoundField DataField="HHID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ROW_X" HeaderText="ROW_X" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ROW_Y" HeaderText="ROW_Y" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" /> 
                        <asp:TemplateField HeaderText="ROW_LATITUDE" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtLATITUDE" onKeyDown="doCheck()" Width="100px" runat="server" Text='<%#Eval("ROW_LATITUDE") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="ROW_LONGITUDE" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"  />
                            <ItemTemplate>
                                <asp:TextBox ID="txtLONGITUDE" onKeyDown="doCheck()" Width="100px" runat="server" Text='<%#Eval("ROW_LONGITUDE") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField>                            
                        <asp:BoundField DataField="WL_X" HeaderText="WL_X" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="WL_Y" HeaderText="WL_Y" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" /> 
                        <asp:TemplateField HeaderText="WL_LATITUDE" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtWLLATITUDE" onKeyDown="doCheck()" Width="100px" runat="server" Text='<%#Eval("WL_LATITUDE") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="WL_LONGITUDE" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"  />
                            <ItemTemplate>
                                <asp:TextBox ID="txtWLLONGITUDE" onKeyDown="doCheck()" Width="100px" runat="server" Text='<%#Eval("WL_LONGITUDE") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                    CommandName="EditRow" CommandArgument='<%#Eval("Id") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                    CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' OnClientClick="return DeleteRecord();"
                                    runat="server" />
                                <asp:Literal ID="litPapCoorID" Text='<%#Eval("Id") %>' Visible="false"
                                    runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <center>
            <br />
            <asp:Button ID="btnLoadCordinate" runat="server" Text="Save" CssClass="icebutton" Visible="false"
                OnClick="btnLoadCordinate_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />
            <asp:Button ID="btnGridDataCancel" runat="server" Text="Cancel" CssClass="icebutton"
                OnClick="btnGridDataCancel_Click" Visible="false" /><input type="checkbox" style="display: none;" id="SHemBox"/>
        </center>
        </asp:Panel>
    </fieldset><asp:HiddenField ID="hdnFilePath" ClientIDMode="Static" runat="server" />
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to delete this record?');
            }

            spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
            if (spnpnl != null) {
                scrWidth = screen.availWidth;
                spnpnl.style.width = parseInt(650).toString() + "px";
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
                //                alert('hi1');
                tbl = document.getElementById('<%=GrdPAPCoordinates.ClientID %>');
                for (rw = 1; rw < tbl.rows.length; rw++) {
                    row = tbl.rows[rw];
                    //for ROW
                    //                    alert('hi2');
                    //                    alert(row.cells[2].innerText.toString());
                    if (isIE()) {
                        easting = parseFloat(row.cells[2].innerText.toString());
                        northing = parseFloat(row.cells[3].innerText.toString());
                    } else {
                        easting = parseFloat(row.cells[2].textContent.toString());
                        northing = parseFloat(row.cells[3].textContent.toString());
                    }
                    Zone = parseFloat(36);

                    arrLatLong = UTMtoGeog(easting, northing, Zone);
                    var lt = arrLatLong[0];
                    var ln = arrLatLong[1];
                    txt1 = row.cells[4].getElementsByTagName('input');
                    txt2 = row.cells[5].getElementsByTagName('input');

                    elem1 = txt1[0];
                    elem2 = txt2[0];
                    if (!isNaN(lt)) {
                        elem1.value = lt;
                    }
                    if (!isNaN(ln)) {
                        elem2.value = ln;
                    }

                    //For WL
                    if (isIE()) {
                        easting = parseFloat(row.cells[6].innerText.toString());
                        northing = parseFloat(row.cells[7].innerText.toString());
                    } else {
                        easting = parseFloat(row.cells[6].textContent.toString());
                        northing = parseFloat(row.cells[7].textContent.toString());
                    }
                    //                    easting = parseFloat(row.cells[6].textContent.toString());
                    //                    northing = parseFloat(row.cells[7].textContent.toString());
                    Zone = parseFloat(36);

                    arrLatLongWL = UTMtoGeog(easting, northing, Zone);
                    var lt1 = arrLatLongWL[0];
                    var ln1 = arrLatLongWL[1];
                    txt1 = row.cells[8].getElementsByTagName('input');
                    txt2 = row.cells[9].getElementsByTagName('input');

                    elem3 = txt1[0];
                    elem4 = txt2[0];
                    if (!isNaN(lt1)) {
                        elem3.value = lt1;
                    }
                    if (!isNaN(ln1)) {
                        elem4.value = ln1;
                    }
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
</asp:Content>
