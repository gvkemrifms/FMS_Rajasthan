﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.ServiceStation, App_Web_mzr41bdz" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <div style="height: 150px; margin: 0 0px 15px 0px; padding: 5px; background-color: #f7f7f7; border: 1px #E2BBA0 solid;">
        <img src="images/b1.jpg" alt="banner" width="653" height="150" />
    </div>--%>
    <asp:UpdatePanel ID="updtpnlServiceStation" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <fieldset style="padding: 10px">
                            <legend>Service Station<br />
                            </legend>
                            <table>
                                <tr>
                                    <td>Vehicle Number<span style="color: Red">*</span>
                                    </td>
                                    <td class="columnseparator"></td>
                                    <td>
                                        <cc1:ComboBox AutoCompleteMode="Append" ID="ddlVehicleNumber" runat="server" AutoPostBack="True"
                                            DropDownStyle="DropDownList"
                                            OnSelectedIndexChanged="ddlVehicleNumber_SelectedIndexChanged">
                                        </cc1:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rowseparator"></td>
                                </tr>
                                <tr>
                                    <td>Service Station Name<span style="color: Red">*</span>
                                    </td>
                                    <td class="columnseparator"></td>
                                    <td>
                                        <asp:TextBox ID="txtServiceSrationName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rowseparator"></td>
                                </tr>
                                <tr>
                                    <td>District<span style="color: Red">*</span>
                                    </td>
                                    <td class="columnseparator"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlDistricts" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <div style="width: 300px; float: left">
                                </div>
                                <div>
                                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validationFuelEntry();" />
                                    <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClientClick="return validationFuelEntry();"
                                        OnClick="btnUpdate_Click" />
                                </div>
                            </div>
                            <div>
                                <div style="width: 200px; float: left">
                                </div>
                                <div style="float: left">
                                    <asp:GridView ID="gvServiceStationDetails" Visible="false" runat="server" EmptyDataText="No records found"
                                        AllowSorting="true" AutoGenerateColumns="false" CssClass="gridviewStyle" CellSpacing="2"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="630px" AllowPaging="true"
                                        EnableSortingAndPagingCallbacks="true"
                                        OnRowCommand="gvServiceStationDetails_RowCommand"
                                        OnPageIndexChanging="gvServiceStationDetails_PageIndexChanging">
                                        <RowStyle CssClass="rowStyleGrid" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Station">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceStation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ServiceStation_Name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistricts" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ds_lname") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VehicleNumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehNum" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="MainEdit" CommandArgument=' <%# Container.DataItemIndex %>'
                                                        Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="MainDelete" CommandArgument=' <%# Container.DataItemIndex %>'
                                                        Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="footerStylegrid" />
                                        <PagerStyle CssClass="pagerStylegrid" />
                                        <SelectedRowStyle CssClass="selectedRowStyle" />
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                            </div>
                    </td>
                </tr>
            </table>

            <script type="text/javascript" language="javascript">

                function validationFuelEntry() {

                    if (document.getElementById("<%=txtServiceSrationName.ClientID %>").value == 0) {
                        alert("Service Station Cannot be Blank");
                        document.getElementById("<%=txtServiceSrationName.ClientID %>").focus();
                        return false;
                    }

                    var VehicleNumber = document.getElementById('<%= ddlVehicleNumber.ClientID %>')
                    if (VehicleNumber.selectedIndex == 0) {
                        alert("Please select the Vehicle");
                        Districts.focus();
                        return false;
                    }

                    var Districts = document.getElementById('<%= ddlDistricts.ClientID %>')

                    if (Districts.selectedIndex == 0) {
                        alert("Please select the District");
                        Districts.focus();
                        return false;
                    }
                }

                function OnlyAlphabets(myfield, e, dec) {
                    var key;
                    var keychar;
                    if (window.event)
                        key = window.event.keyCode;
                    else if (e)
                        key = e.which;
                    else
                        return true;
                    keychar = String.fromCharCode(key);
                    if ((("!@#$%^&*()_+=-';{}[]|?<>:,/\".1234567890").indexOf(keychar) > -1))
                        return false;
                    else
                        return true;
                }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

