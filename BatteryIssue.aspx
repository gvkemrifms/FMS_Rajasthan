﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.Inventory.Battery.BatteryIssue, App_Web_ttubqba0" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
     function CheckLength(text,long) 
{
	var maxlength = new Number(long); // Change number to your max length.
	if (text.value.length > maxlength)
       {
		text.value = text.value.substring(0,maxlength);

		alert(" Only " + long + " chars");

	}
}
    function remark(e) 
             {
                var keycode; 
                if (window.event) keycode = window.event.keyCode;
                else if (event) keycode = event.keyCode; 
                else if (e) keycode = e.which;
                else return true;if((keycode != 34) && (keycode != 39) ) 
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
                 return true; 
            }
    
     function OnlyAlphabets(myfield, e, dec) 
              {
	            var keycode; 
            if (window.event) keycode = window.event.keyCode;
            else if (event) keycode = event.keyCode; 
            else if (e) keycode = e.which;
            else return true;if((keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122)|| (keycode == 32)) 
            {
                return true; 
            }
            else
            {
                return false; 
            }
            return true; 
              }
              
            function isNumberKey(evt)
              {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                else
                    return true;
                }
    
   function validation() {
            var DcNumberPopup = document.getElementById('<%=txtDcNumberPopup.ClientID %>');
            var DcDate = document.getElementById('<%=txtDcDate.ClientID %>');
            var CourierName = document.getElementById('<%=txtCourierName.ClientID %>');
            var Remarks = document.getElementById('<%=txtRemarks.ClientID %>');

          
            if (!RequiredValidation(DcNumberPopup, "DC Number cannot be Blank"))
                return false;

              if (!RequiredValidation(DcDate, "DC Date cannot be Blank"))
                return false;


            if (trim(DcDate.value) != "") {
                if (!isValidDate(DcDate.value)) {
                    alert("Enter the DC Date");
                    DcDate.focus();
                    return false;
                }
            }
            
            var now = new Date();
            if(Date.parse(DcDate.value) > Date.parse(now))
            {
                alert("DC Date should not be greater than Current Date");
                DcDate.focus();
                return false;
            }
            
             if (!RequiredValidation(CourierName, "Courier Name cannot be Blank"))
                return false;

            if (!RequiredValidation(Remarks, "Remarks cannot be Blank"))
                return false;
                
            var txtIssuedQuantity = document.getElementById(IssuedQuantity);
            if (!RequiredValidation(txtIssuedQuantity, "Issue Qty cannot be Blank"))
                return false;
        }

        function RequiredValidation(ctrl, msg) {
            if (trim(ctrl.value) == '') {
                alert(msg);
                ctrl.focus();
                return false;
            }
            else
                return true;
        }
        
          function trim(value) {
            value = value.replace(/^\s+/, '');
            value = value.replace(/\s+$/, '');
            return value;

        }

        function isValidDate(subject) {
            if (subject.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                return true;
            } else {
                return false;
            }
        }
        function isDecimalNumberKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode
          
            if (charCode == 190 || charCode == 46) {
                var txtBox = document.getElementById(event.srcElement.id);
                if (txtBox.value.indexOf('.') == -1)
                    return true;
                else
                    return false;
            }
            else if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else
                return true;
        }
        
        function ValidateIssueQty(IssQtyID, ReqQty)
        {
            var objIssQty = document.getElementById(IssQtyID);
            if (parseInt(objIssQty.value) > parseInt(ReqQty))
            {
                alert("Issued Quantity Cannot be more than Request Quantity");
                objIssQty.focus();
                return false;
            }
        }

    </script>

<%--    <div style="height: 150px; margin: 0 0px 15px 0px; padding: 5px; background-color: #f7f7f7;
        border: 1px #E2BBA0 solid;">
        <img src="images/b1.jpg" alt="banner" width="653" height="150" />
    </div>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div_BatteryIssue" runat="server">
                <fieldset style="padding: 10px">
                    <legend>Battery Issue</legend>
                    <div id="Div2" runat="server" align="center">
                        Vehicle Number<span style="color: Red">*</span>
                        <ajaxToolkit:ComboBox AutoCompleteMode="Append" ID="ddlInventoryBatteryIssueVehicles" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlInventoryBatteryIssueVehicles_SelectedIndexChanged" DropDownStyle="DropDownList">
                        </ajaxToolkit:ComboBox>
                    </div>
                    <br />
                    <div id="Div5" runat="server">
                        Pending for Issue
                    </div>
                    <div id="Div6" runat="server" align="center">
                        <asp:GridView ID="grvBatteryPendingForIssue" runat="server" CellPadding="3" CellSpacing="2"
                            EmptyDataText="Details are not available" AutoGenerateColumns="False" GridLines="None"
                            CssClass="gridviewStyle" AllowPaging="True" DataKeyNames="FleetInventoryReqID"
                            PageSize="5" OnPageIndexChanging="grvBatteryPendingForIssue_PageIndexChanging"
                            OnRowCommand="grvBatteryPendingForIssue_RowCommand">
                            <Columns>
                                <%--<asp:BoundField HeaderText="S.No" DataField="FleetInventoryReqID" />--%>
                                <asp:BoundField HeaderText="Vehicle Number" DataField="VehicleNum" />
                                <asp:BoundField HeaderText="No. of Batteries" DataField="RequestedQty" />
                                <asp:BoundField HeaderText="Request Date" DataField="RequestedDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Request By" DataField="RequestedBy" />
                                <asp:TemplateField ControlStyle-Width="50px" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewDetails" runat="server" Text="Issue" ToolTip="Click here to Issue the details"
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
                    </div>
                    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="gv_ModalPopupExtenderBatteryIssue" BehaviorID="mdlPopup"
                        runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPanel">
                        <asp:UpdatePanel ID="updPnlReqDetail" runat="server">
                            <ContentTemplate>
                                <fieldset style="padding: 10px">
                                    <legend>Battery Issue Detail</legend>
                                    <table class="style1">
                                        <tr>
                                            <td align="left">
                                                Vehicle ID
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtBatIssVehicleID" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Inventory Request ID
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInvReqIdPopUp" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                DC Number
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDcNumberPopup" runat="server" MaxLength="5"></asp:TextBox>
                                        </tr>
                                        <tr align="center">
                                            <td align="left">
                                                DC Date
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDcDate" runat="server" onkeypress="return false" MaxLength="20"
                                                    oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDcDate"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImageButton1">
                                                </ajaxToolkit:CalendarExtender><asp:ImageButton ID="ImageButton1" runat="server" alt="" src="images/Calendar.gif" Style="vertical-align: top" />
                                            </td>
                                            <td align="left">
                                                Courier Name
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCourierName" runat="server" MaxLength="20"></asp:TextBox>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:GridView ID="grvBatteryIssueDetailsPopup" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" CssClass="gridviewStyle" CellPadding="3" CellSpacing="2" Width="95%"
                                        OnRowDataBound="grvBatteryIssueDetailsPopup_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="ReqID" DataField="FleetInventoryReqID" />--%>
                                            <asp:BoundField HeaderText="Battery Position ID" DataField="BatteryPositionID" />
                                            <asp:BoundField HeaderText="Old Battery Number" DataField="BatteryNumber" />
                                            <asp:TemplateField HeaderText="New Battery Number">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlNewBatteryNumber" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="RequestedQty" DataField="RequestedQty" />
                                            <%-- <asp:BoundField HeaderText="Make" DataField="Make" />
                                <asp:BoundField HeaderText="Model" DataField="Model" ItemStyle-Wrap="false">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Capacity" DataField="Capacity" />--%>
                                            <asp:TemplateField HeaderText="IssuedQty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBatteryIssuedQty" runat="server" onkeypress="return isNumberKey(event);"
                                                        MaxLength="20"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="rowStyleGrid" />
                                        <FooterStyle CssClass="footerStylegrid" />
                                        <PagerStyle CssClass="pagerStylegrid" />
                                        <SelectedRowStyle CssClass="selectedRowStyle" />
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                    <div id="Div7" align="center" style="width: 95%; background-color: white">
                                        Remarks<asp:TextBox ID="txtRemarks" runat="server" MaxLength="20" TextMode="MultiLine"
                                            onKeyUp="CheckLength(this,50)" onChange="CheckLength(this,50)"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnOk" runat="server" Text="Issue" Width="50px" OnClick="btnOk_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnNo" runat="server" Text="Cancel" Width="50px" OnClick="btnNo_Click" />
                                    </div>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

