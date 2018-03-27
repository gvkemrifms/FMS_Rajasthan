﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.TyreRequisition, App_Web_mzr41bdz" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function CheckLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);

                alert(" Only " + long + " chars");

            }
        }
        function validationInventoryBatteryVehicleType() {

            var id = document.getElementById('<%= ddlVehicles.ClientID %>');
        var inputs = id.getElementsByTagName('input');
        var i;
        for (i = 0; i < inputs.length; i++) {
            if (inputs[i].type == 'text') {
                if (inputs[i].value != "" && inputs[i].value != null)
                    if (inputs[i].value == "--Select--") {
                        alert('Select the Vehicle');
                        return false;
                    }

                break;
            }
        }
    }
    function OnlyAlphaNumeric(e) {
        var keycode;
        if (window.event) keycode = window.event.keyCode;
        else if (event) keycode = event.keyCode;
        else if (e) keycode = e.which;
        else return true; if ((keycode >= 48 && keycode <= 57) || (keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122)) {
            return true;
        }
        else {
            return false;
        }
        return true;
    }

    function remark(e) {
        var keycode;
        if (window.event) keycode = window.event.keyCode;
        else if (event) keycode = event.keyCode;
        else if (e) keycode = e.which;
        else return true; if ((keycode != 34) && (keycode != 39)) {
            return true;
        }
        else {
            return false;
        }
        return true;
    }

    </script>

   
    <asp:UpdatePanel ID="UpdPnl1" runat="server">
        <ContentTemplate>
            <fieldset style="padding: 10px">
                <legend>Tyre Requisition</legend>
                <table style="width: 600px">
                    <tr>
                        <td class="rowseparator"></td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Panel ID="pnlNewTyreRequisition" runat="server">
                                <table>
                                    <tr>
                                        <td class="rowseparator"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbVehicles" runat="server" Text="Vehicles"></asp:Label>
                                            <span style="color: Red">*</span><ajaxToolKit:ComboBox AutoCompleteMode="Append" ID="ddlVehicles" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlVehicles_SelectedIndexChanged" DropDownStyle="DropDownList">
                                            </ajaxToolKit:ComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rowseparator"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvTyreRequisition" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                EmptyDataText="Details are not available" CssClass="table table-striped table-bordered table-hover" CellSpacing="2"
                                                OnPageIndexChanging="gvTyreRequisition_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" Enabled='<%# !Convert.ToBoolean((DataBinder.Eval (Container.DataItem, "Enabled") == DBNull.Value) ? 0:1) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Tyre Position" DataField="TyrePosition" />
                                                    <asp:BoundField HeaderText="TyreNumber" DataField="TyreNumber" />
                                                    <asp:BoundField HeaderText="TyreMake" DataField="TyreMake" />
                                                    <asp:BoundField HeaderText="TyreModelSize" DataField="TyreModelSize" />
                                                    <asp:BoundField HeaderText="IssuedDate" DataField="IssueDate" />
                                                    <asp:BoundField HeaderText="Total Km Run" DataField="IssueODOReading" />
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Enabled='<%# !Convert.ToBoolean((DataBinder.Eval (Container.DataItem, "Enabled") == DBNull.Value) ? 0:1) %>'
                                                                Text='<%# DataBinder.Eval (Container.DataItem, "Enabled") %>' onkeypress="return remark(event);"
                                                                MaxLength="50" onKeyUp="CheckLength(this,50)" onChange="CheckLength(this,50)"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="rowStyleGrid" />
                                                <FooterStyle CssClass="footerStylegrid" />
                                                <PagerStyle CssClass="pagerStylegrid" />
                                                <SelectedRowStyle CssClass="selectedRowStyle" />
                                               <%-- <HeaderStyle CssClass="headerStyle" />--%>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rowseparator"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btSave" runat="server" OnClick="btSave_Click" Text="Submit" />
                                            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" />
                                            <asp:Button ID="btnTyreReqHistory" runat="server" Text="View History" OnClick="btnTyreReqHistory_Click" OnClientClick="return validationInventoryBatteryVehicleType();" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="rowseparator"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvTyrePendingForApproval" runat="server" GridLines="None" CssClass="gridviewStyle"
                                            AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvTyrePendingForApproval_PageIndexChanging"
                                            PageSize="5" OnRowCommand="gvTyrePendingForApproval_RowCommand">
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="ReqID" DataField="FleetInventoryReqID" />--%>
                                                <asp:BoundField HeaderText="Vehicle Number" DataField="VehicleNum" />
                                                <asp:BoundField HeaderText="No. of Tyres" DataField="RequestedQty" />
                                                <asp:BoundField HeaderText="Request Date" DataField="RequestedDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField HeaderText="Request By" DataField="RequestedBy" />
                                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                                <asp:TemplateField ControlStyle-Width="50px" HeaderStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnViewDetails" runat="server" Text="View" ToolTip="Click here to Approve/Reject the details"
                                                            OnClick="BtnViewDetails_Click" CssClass="button2" RowIndex='<%# Container.DisplayIndex %>'
                                                            CommandName="Show" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FleetInventoryReqID") %>' />
                                                    </ItemTemplate>
                                                    <ControlStyle Width="50px" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="rowStyleGrid" />
                                            <FooterStyle CssClass="footerStylegrid" />
                                            <PagerStyle CssClass="pagerStylegrid" />
                                            <SelectedRowStyle CssClass="selectedRowStyle" />
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rowseparator"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="RequisitionHistory" runat="server">
                                            <fieldset style="padding: 10px; width: auto">
                                                <legend>Requisition History </legend>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grvRequisitionHistory" runat="server" AutoGenerateColumns="True"
                                                                GridLines="None" CssClass="gridviewStyle" CellPadding="3" CellSpacing="2" Width="95%"
                                                                OnPageIndexChanging="grvRequisitionHistory_PageIndexChanging" PageSize="5" AllowPaging="True" EmptyDataText="No History Found">
                                                                <RowStyle CssClass="rowStyleGrid" />
                                                                <FooterStyle CssClass="footerStylegrid" />
                                                                <PagerStyle CssClass="pagerStylegrid" />
                                                                <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                <HeaderStyle CssClass="headerStyle" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Button ID="hideHistory" runat="server" Text="Hide History" Visible="false" OnClick="hideHistory_Click" />
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rowseparator"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                        <ajaxToolKit:ModalPopupExtender ID="gv_ModalPopupExtender1" BehaviorID="mdlPopup"
                                            runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
                                        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; padding: 30px" CssClass="modalPanel"
                                            Width="500px">
                                            <fieldset style="padding: 10px; width: auto">
                                                <legend>Tyre Request Details</legend>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbVehicleNo" runat="server" Text="VehicleNo"></asp:Label>
                                                        </td>
                                                        <td class="columnseparator"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtVehicleNo" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                        <td class="columnseparator"></td>
                                                        <td>
                                                            <asp:Label ID="lbInventoryReqID" runat="server" Text="InvReqID"></asp:Label>
                                                        </td>
                                                        <td class="columnseparator"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvReqID" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rowseparator"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="7">
                                                            <asp:GridView ID="gvTyreRequisitionDetails" runat="server" GridLines="None" CssClass="gridviewStyle"
                                                                CellPadding="3" CellSpacing="2" AutoGenerateColumns="true">
                                                                <RowStyle CssClass="rowStyleGrid" />
                                                                <FooterStyle CssClass="footerStylegrid" />
                                                                <PagerStyle CssClass="pagerStylegrid" />
                                                                <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                <HeaderStyle CssClass="headerStyle" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rowseparator"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" align="center">
                                                            <asp:Button ID="btnOk" runat="server" Text="Approve" OnClick="btnOk_Click" /><ajaxToolKit:ConfirmButtonExtender
                                                                ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnOk" ConfirmText="Are you sure you want to APPROVE">
                                                            </ajaxToolKit:ConfirmButtonExtender>
                                                            <asp:Button ID="btnNo" runat="server" Text="Reject" OnClick="btnNo_Click" /><ajaxToolKit:ConfirmButtonExtender
                                                                ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnNo" ConfirmText="Are you sure you want to REJECT">
                                                            </ajaxToolKit:ConfirmButtonExtender>
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                                        </td>
                                                        <%-- <td colspan="7" align="center">
                                                            
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rowseparator"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

