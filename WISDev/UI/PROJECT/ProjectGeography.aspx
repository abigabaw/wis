<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectGeography.aspx.cs" Inherits="WIS.ProjectGeography" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 96%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<fieldset class="icePnlinner">
    <legend>Geographical Details</legend>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
    <tr>
        <td class="style1">
            <label class="iceLable">General direction</label>
                <asp:TextBox ID="txtGeneralDirection" CssClass="iceTextBox" MaxLength="500" 
                runat="server" Width="551px"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" -" TargetControlID="txtGeneralDirection" runat="server" />
            <%--<ajaxToolkit:BalloonPopupExtender ID="PopupControlExtender2" runat="server"
        TargetControlID="txtGeneralDirection"
        BalloonPopupControlID="pnlGeneralDirectionMsg"
        Position="BottomRight" 
        BalloonStyle="Rectangle"
        BalloonSize="Small"
        CustomCssUrl=""
        CustomClassName=""
        UseShadow="true" 
        ScrollBars="Auto"
        DisplayOnMouseOver="true"         
        DisplayOnFocus="false"
        DisplayOnClick="false" />--%>
        <asp:Panel ID="pnlGeneralDirectionMsg" runat="server" Visible="false">
            <asp:Label ID="lblDirectionMessage" runat="server"></asp:Label>
        </asp:Panel>
        </td>
    </tr>
    <tr>    
        <td colspan="2">
            <label class="iceLable">Key geographical features traversed and points of intersection</label><br />
            <asp:TextBox ID="txtKeyGeoFeatures" CssClass="iceTextBox" TextMode="MultiLine" Rows="5" MaxLength="1000" Width="90%" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <br />
            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" 
                onclick="btnSave_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false"/>&nbsp;
                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" 
                onclick="btnClear_Click" />
        </td>
    </tr>
    <tr>
    <td colspan ="3">
    
    <asp:GridView ID="grdProjectGeo" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdProjectGeo_RowCommand"
        AllowPaging="True" OnPageIndexChanging="grdProjectGeo_PageIndexChanging">
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
            <asp:BoundField DataField="GeneralDirection" HeaderText="General Direction" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="KeyFeatures" HeaderText="Key Features" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("GeographicalID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("GeographicalID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litRoleID" Text='<%#Eval("GeographicalID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </td>
    </tr>
</table>
</fieldset>
<script type="text/javascript">

    function DisableOnSave(src) {
        src.disabled = true;
        src.value = 'Please Wait...';
    }

</script>
</asp:Content>
