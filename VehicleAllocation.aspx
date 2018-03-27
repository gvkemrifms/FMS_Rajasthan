﻿<%@ Page Title="" Language="C#" MasterPageFile="~/temp.master" AutoEventWireup="true" CodeFile="VehicleAllocation.aspx.cs" Inherits="GvkFMSAPP.PL.VAS.VehicleAllocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="css/newStyles.css" rel="stylesheet" />--%>
    <script type="text/javascript" language="javascript">

        function validation() {

            var fld_District = document.getElementById('<%= ddlDistrict.ClientID %>');
            var fld_VehicleNumber = document.getElementById('<%= ddlVehicleNumber.ClientID %>');
            var fld_ReasonForDown = document.getElementById('<%= txtReasonforDown.ClientID %>');
            var fld_DownTime = document.getElementById('<%= txtDownTime.ClientID %>');
            var fld_Odo = document.getElementById('<%= txtOdo.ClientID %>');
            var fld_Uptime = document.getElementById('<%= txtUptimeDate.ClientID %>');
            var fld_hrs = document.getElementById('<%= ddlUPHour.ClientID %>');
            var fld_mins = document.getElementById('<%= ddlUPMin.ClientID %>');
            var fld_ContactNumber = document.getElementById('<%= txtContactNumber.ClientID %>');
            var fld_PailotName = document.getElementById('<%= txtPailotName.ClientID %>');
            var fld_pailotGid = document.getElementById('<%= txtpailotGid.ClientID %>');


            var today = new Date();
            var dd = addZero(today.getDate());
            var mm = addZero(today.getMonth() + 1); //January is 0!
            var h = addZero(today.getHours());
            var m = addZero(today.getMinutes());
            var s = addZero(today.getSeconds());
            var yyyy = today.getFullYear();

            var now = dd + '/' + mm + '/' + yyyy + " " + h + ":" + m + ":" + s;


            if (fld_District)
                if (fld_District.selectedIndex == 0) {
                    alert("Please Select District");
                    fld_District.focus();
                    return false;
                }

            if (fld_VehicleNumber)
                if (fld_VehicleNumber.selectedIndex == 0) {
                    alert("Please select Vehicle Number");
                    fld_VehicleNumber.focus();
                    return false;
                }

            if (!RequiredValidation(fld_Odo, "Odometer cannot be blank"))
                return false;

            if (!RequiredValidation(fld_Uptime, "Uptime cannot be blank"))
                return false;

            if (fld_hrs)
                if (fld_hrs.selectedIndex == 0) {
                    alert("Please select Hrs");
                    fld_hrs.focus();
                    return false;
                }

            if (fld_mins)
                if (fld_mins.selectedIndex == 0) {
                    alert("Please select Mins");
                    fld_mins.focus();
                    return false;
                }

            if (fld_Segment)
                if (fld_Segment.selectedIndex == 0) {
                    alert("Please select Segment");
                    fld_Segment.focus();
                    return false;
                }

            if (fld_SegmentName)
                if (!RequiredValidation(fld_SegmentName, "Segment cannot be blank"))
                    return false;

            if (fld_Mandals.selectedIndex == 0) {
                alert("Please select Mandal");
                fld_Mandals.focus();
                return false;
            }

            if (fld_City.selectedIndex == 0) {
                alert("Please select City");
                fld_City.focus();
                return false;
            }

            if (fld_BaseLocation)
                if (fld_BaseLocation.selectedIndex == 0) {
                    alert("Please select Base Location");
                    fld_BaseLocation.focus();
                    return false;
                }

            if (fld_TxtBaseLoc)
                if (!RequiredValidation(fld_TxtBaseLoc, "Base Location cannot be blank"))
                    return false;

            if (!RequiredValidation(fld_ContactNumber, "Contact Number cannot be blank"))
                return false;
            
            if (!RequiredValidation(fld_PailotName, "Pailot name cannot be blank"))
                return false;
            if (!RequiredValidation(fld_pailotGid, "Pailot GID cannot be blank"))
                return false;

            document.getElementById("loaderButton").style.display = '';
            document.all('<%= pnlButton.ClientID %>').style.display = "none"
        }



        function ChkDistrict() {
            var fld_District = document.getElementById('<%= ddlDistrict.ClientID %>');
            if (fld_District)
                if (fld_District.selectedIndex == 0) {
                    alert("Please Select District");
                    fld_District.focus();
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

        function numeric(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            //debugger;
            if (charCode == 190) {
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

        function OnlyNumPeriod(evt) {
            var bool = false;
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (evt.shiftKey == 1) {
                bool = false;
                alert('Shift Key is not allowed');
            }
            else if ((charCode >= 48 && charCode <= 57) || (charCode > 95 && charCode < 106))
                bool = true;
            else if (charCode == 8 || charCode == 46 || charCode == 190 || charCode == 9)
                bool = true;
            else if (charCode == 110)
                bool = true;
            return bool;
        }

        function isDecimal(control) {
            var input = control.value;
            if (input != '') {
                var arr = input.toString().split('.');
                var id = document.getElementById(control.id);
                if (arr.length != 2) {
                    alert('InCorrect Format');
                    control.value = "";
                    id.focus();
                }
                else if (arr.length == 2 && arr[0].length != 2) {
                    alert('InCorrect Format;ex 12.2345');
                    control.value = "";
                    id.focus();
                }
                else if (arr.length == 2 && arr[1].length < 1) {
                    alert('InCorrect Format;ex 12.2345');
                    control.value = "";
                    id.focus();
                }
                else if (arr.length == 2 && arr[1].length > 5) {
                    alert('InCorrect Format;ex 12.2345');
                    control.value = "";
                    id.focus();
                }
            }
        }
        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }
    </script>

    <%--   <div style="height: 150px; margin: 0 0px 15px 0px; padding: 5px; background-color: #f7f7f7;
        border: 1px #E2BBA0 solid;">
        <img src="images/b1.jpg" alt="banner" width="653" height="150" />
    </div>--%>
    <%--<asp:UpdatePanel ID="updtpnlVehAllocation" runat="server">
        <ContentTemplate>	--%>
    <fieldset style="padding: 10px">
        <legend>Vehicle Allocation</legend>
        <table style="width: 600px;">
            <tr>
                <td colspan="7"></td>
            </tr>
            <tr>
                <td>District<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="5">
                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                        AutoPostBack="True">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <tr>
                <td>Vehicle Number<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td>
                    <asp:DropDownList ID="ddlVehicleNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicleNumber_SelectedIndexChanged">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <tr>
                <td>Reason for Down<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td>
                    <asp:TextBox ID="txtReasonforDown" runat="server" TextMode="MultiLine" onkeypress="return false;"></asp:TextBox>
                </td>
                <td class="columnseparator"></td>
                <td>DownTime<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="2">
                    <asp:TextBox ID="txtDownTime" runat="server" onkeypress="return false;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="7"></td>
            </tr>
            <tr>
                <td>Odometer<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="2">
                    <asp:TextBox ID="txtOdo" runat="server" onkeypress="return numeric(event)" MaxLength="6"
                        Width="80%"></asp:TextBox>
                </td>
                <td>PreviousODO<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td>
                    <asp:Label ID="lblpvODO" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7"></td>
            </tr>
            <tr>
                <td>Requested By<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="5">
                    <%--<asp:DropDownList ID="ddlistRequestedBy" runat="server">
                                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                            </asp:DropDownList>--%>
                    <asp:TextBox ID="txtReqBy" runat="server" onkeypress="return false;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <tr>
                <td>Expected Date of Recovery<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="5">
                    <asp:TextBox ID="txtExpDateOfRec" runat="server" onkeypress="return false;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <tr>
                <td>Uptime <span style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td nowrap="nowrap" colspan="6">
                    <table style="width: 100%">
                        <tr>
                            <td nowrap="nowrap" style="width: 20%">
                                <asp:TextBox ID="txtUptimeDate" runat="server" Width="120px" onkeypress="return false;"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnUptimeDate" runat="server" Style="vertical-align: top"
                                    alt="" src="images/Calendar.gif" />
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtUptimeDate"
                                    PopupButtonID="imgBtnUptimeDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="width: 80%">
                                <asp:DropDownList ID="ddlUPHour" runat="server" Width="55px">
                                    <asp:ListItem Value="-1">--hh--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlUPMin" runat="server" Width="60px">
                                    <asp:ListItem Value="-1">--mm--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <%-- <tr>
                        <td colspan="3">
                            <asp:CheckBox ID="chkbxinactivesegment" runat="server" Text="Show InActive Segments"
                                AutoPostBack="True" OnCheckedChanged="chkbxinactivesegment_CheckedChanged" />
                        </td>
                    </tr>--%>

            <tr>
                <td class="rowseparator"></td>
            </tr>

            <tr>
                <td>Contact Number<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="5">
                    <asp:TextBox ID="txtContactNumber" runat="server" onkeypress="return numeric(event)"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLatitude" runat="server" Text="Latitude" Visible="false"></asp:Label>
                    <asp:Label ID="lblMandatory1" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                </td>
                <td class="columnseparator"></td>
                <td>
                    <asp:TextBox ID="txtLatitude" runat="server" Visible="false" onblur="isDecimal(this);"
                        onkeydown="return OnlyNumPeriod(event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="rowseparator"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLongitude" runat="server" Text="Longitude" Visible="false"></asp:Label>
                    <asp:Label ID="lblMandatory2" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                </td>
                <td class="columnseparator"></td>
                <td>
                    <asp:TextBox ID="txtLongitude" runat="server" Visible="false" onblur="isDecimal(this);"
                        onkeydown="return OnlyNumPeriod(event);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>Pailot Name:<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="5"> 
                    <asp:TextBox ID="txtPailotName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Pailot Gid:<span class="labelErr" style="color: Red">*</span>
                </td>
                <td class="columnseparator"></td>
                <td colspan="5"> 
                    <asp:TextBox ID="txtpailotGid" onkeypress="return numeric(event)"  runat="server"></asp:TextBox></td>
            </tr>


            <tr>
                <div style="top: 0px; width: 68px;">
            </tr>
            <caption>
                <img id="loaderButton" alt="" src="../images/savingimage.gif"
                    style="display: none" />
                <tr>
                    <td align="center" colspan="7" style="">
                        <asp:Panel ID="pnlButton" runat="server">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                Text="Submit" />
                            &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click"
                                        Text="Reset" />
                        </asp:Panel>
                    </td>
                </tr>
            </caption>
        </table>
    </fieldset>
    <%--</ContentTemplate>
    </asp:UpdatePanel> --%>
</asp:Content>

