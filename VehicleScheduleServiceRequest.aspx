﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.VehicleMaintenanceandRepairs.VehicleScheduleServiceRequest, App_Web_ttubqba0" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height: 150px; margin: 0 0px 15px 0px; padding: 5px; background-color: #f7f7f7;
        border: 1px #E2BBA0 solid;">
        <img src="images/b1.jpg" alt="banner" width="653" height="150" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--<span style="color:Red";class="labelErr">*</span>--%>
            <table id="table1" cellspacing="0" cellpadding="0" width="500px" align="center" border="0"
                style="height: 37px">
                <tr>
                    <td class="rowseparator">
                    </td>
                </tr>
                <tr>
                    <td style="height: 200px">
                        <fieldset style="padding: 10px;">
                            <legend>Schedule Service Request</legend>
                            <table id="table2" class="bordergreen" width="91%" align="center">
                                <tr>
                                    <td>
                                        <table class="logtable" align="center">
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Vehicle Number <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVehicleNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicleNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Schedule Category <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlScheduleCat" runat="server" AppendDataBoundItems="true"
                                                        Width="150px">
                                                        <%--<asp:ListItem Value="-1">Select</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Schedule Plan Date <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSchedulePlanDate" runat="server" CssClass="textbox180" onpaste="return false" oncopy="return false" oncut="return false" onkeypress="return false"></asp:TextBox>
                                                    
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtSchedulePlanDate"
                                                        TargetControlID="txtSchedulePlanDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 41px" align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" Width="65px"
                                            OnClick="btnSubmit_Click" OnClientClick="return validationForServiceReq();" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" Width="65px"
                                            OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlDisplayDetails" runat="server" Width="100%" Visible="false">
                                <asp:GridView ID="grvScheduleServiceRequest" runat="server" AllowPaging="True" PageSize="5"
                                    AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" CellSpacing="2" Width="660px" OnRowEditing="grvScheduleServiceRequest_RowEditing"
                                    OnRowDeleting="grvScheduleServiceRequest_RowDeleting" OnPageIndexChanging="grvScheduleServiceRequest_PageIndexChanging"
                                    CssClass="gridviewStyle">
                                    <RowStyle CssClass="rowStyleGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSlNo" runat="server" Text='<%#Eval("Sl_No")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vehicle ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleID" runat="server" Text='<%#Eval("Vehicle_Id")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vehicle Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleNo" runat="server" Text='<%#Eval("VehicleNumber")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrict" runat="server" Text='<%#Eval("District_Name")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Schedule Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScheduleCategory" runat="server" Text='<%#Eval("Schedule_Category")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Schedule Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScheduleDate" runat="server" Text='<%#Eval("Scheduled_Date")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("Sl_No")%>'
                                                    CommandName="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span onclick="return confirm('Are you sure to Delete the record?')">
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="Delete"
                                                        CommandArgument='<%# Eval("Sl_No") + "," + Eval("Vehicle_Id")%>' OnCommand="lnkDelete_Click"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="footerStylegrid" />
                                    <PagerStyle CssClass="pagerStylegrid" />
                                    <SelectedRowStyle CssClass="selectedRowStyle" />
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </asp:Panel>
                        </fieldset>
                    </td>
                </tr>
            </table>

            <script type="text/javascript" language="javascript">
         function  validationForServiceReq()
           {  
           //alert(document.getElementById("<%=ddlScheduleCat.ClientID %>").value);
                 if(document.getElementById("<%=ddlVehicleNo.ClientID %>").value==0)
                 {
                    alert("Please Select Vehicle Number");
                    document.getElementById("<%=ddlVehicleNo.ClientID %>").focus();
                    return false;
                 }
                 if(document.getElementById("<%=ddlScheduleCat.ClientID %>").value==0)
                 {
                    alert("Please Select Scheduled Category");
                    document.getElementById("<%=ddlScheduleCat.ClientID %>").focus();
                    return false;
                 }
                 var SPDate = document.getElementById('<%=txtSchedulePlanDate.ClientID %>');
                   if (!RequiredValidation(SPDate, "Scheduled Plan date cannot be Blank"))
                return false;
                
                if (trim(SPDate.value) != "") {
                if (!isValidDate(SPDate.value)) {
                    alert("Enter the correct format (mm/dd/yyyy)");
                    SPDate.focus();
                    return false;
                }
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
            if (subject.match(/^(?:(0[1-9]|1[012])[\- \/.](0[1-9]|[12][0-9]|3[01])[\- \/.](19|20)[0-9]{2})$/)) {
                return true;
            } else {
                return false;
            }
        }
        
}         
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

