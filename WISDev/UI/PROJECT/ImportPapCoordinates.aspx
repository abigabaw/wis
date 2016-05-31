<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
 CodeBehind="ImportPapCoordinates.aspx.cs" Inherits="WIS.ImportPapCoordinates" %>


<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="../../Scripts/CoordinateConversion.js"></script>
<script type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     <script type="text/javascript">
         function UploadCoordinates() {
             open('../RouteCoordinatesPopup.aspx', 'expWin', 'width=800px,height=700px');
         }
    </script>
    <asp:Panel ID="pnlFileUpload" Visible="true" runat="server">
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
                                    <asp:Button ID="btnCancelUpload" Text="Close" Visible="false" runat="server" class="icebutton" Style="width: 100px"
                                         OnClientClick="AfterSaveCoordinates();"/>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <fieldset class="icePnlinner">
        <legend id="lgndTitle" runat="server">PAP Coordinates</legend>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Auto" Height="350px">
            <asp:GridView ID="GrdPAPCoordinates" ClientIDMode="Static" runat="server" CssClass="gridStyle"
                CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
                Width="100%" AllowPaging="False" OnRowCommand="GrdPAPCoordinates_RowCommand"
                OnRowDataBound="GrdPAPCoordinates_RowDataBound">
                <RowStyle CssClass="gridRowStyle" />
                <AlternatingRowStyle CssClass="gridAlternateRow" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
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
                            <asp:TextBox ID="txtLATITUDE" onKeyDown="doCheck()" Width="100px" runat="server"
                                Text='<%#Eval("ROW_LATITUDE") %>' ClientIDMode="Static" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ROW_LONGITUDE" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtLONGITUDE" onKeyDown="doCheck()" Width="100px" runat="server"
                                Text='<%#Eval("ROW_LONGITUDE") %>' ClientIDMode="Static" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WL_X" HeaderText="WL_X" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="WL_Y" HeaderText="WL_Y" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="WL_LATITUDE" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtWLLATITUDE" onKeyDown="doCheck()" Width="100px" runat="server"
                                Text='<%#Eval("WL_LATITUDE") %>' ClientIDMode="Static" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WL_LONGITUDE" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtWLLONGITUDE" onKeyDown="doCheck()" Width="100px" runat="server"
                                Text='<%#Eval("WL_LONGITUDE") %>' ClientIDMode="Static" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                    CommandName="DeleteRow" CommandArgument='<%#Eval("ROUTE_COORDINATEID") %>' OnClientClick="return DeleteRecord();"
                                    runat="server" />
                                <asp:Literal ID="litROUTE_COORDINATEID" Text='<%#Eval("ROUTE_COORDINATEID") %>' Visible="false"
                                    runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <center>
            <br />
            <asp:Button ID="btnLoadCordinate" runat="server" Text="Save" CssClass="icebutton"
                Visible="false" OnClick="btnLoadCordinate_Click" OnClientClick="DisableOnSave(this);"
                UseSubmitBehavior="false" />
            <asp:Button ID="btnGridDataCancel" runat="server" Text="Cancel" CssClass="icebutton"
                OnClick="btnGridDataCancel_Click" Visible="false" /><input type="checkbox" style="display: none;"
                    id="SHemBox" />
        </center>
    </fieldset>
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
