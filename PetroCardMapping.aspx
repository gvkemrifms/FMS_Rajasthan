﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.PetroCardMapping, App_Web_ttubqba0" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        
        
        function isMandatory(evt)
        {
         
            var id = document.getElementById('<%=ddlVehicleNumber.ClientID %>');
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
           if(document.getElementById("<%=ddlPetroCardNumber.ClientID %>").selectedIndex==0)
                 {
                    alert("Please Select PetroCardNumber");
                    document.getElementById("<%=ddlPetroCardNumber.ClientID %>").focus();
                    return false;
                 }
         
           if(document.getElementById("<%=txtIssDate.ClientID %>").value==0)
                 {
                    alert("Please enter Issued Date");
                    document.getElementById("<%=txtIssDate.ClientID %>").focus();
                    return false;
                 }  
              var IssuedDate = document.getElementById('<%=txtIssDate.ClientID %>');   
                 var now = new Date();
            if(Date.parse(IssuedDate.value) > Date.parse(now))
            {
                alert("Issued Date should not be greater than Current Date");
                IssuedDate.focus();
                return false;
            } 
           
          } 
          
          function isNumberKey(evt)
              {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                else
                    return true;
                }
                
                
                function OnlyAlphabets(myfield, e, dec) 
              {
	            var key;
	            var keychar;
	            if (window.event)
		            key = window.event.keyCode;
	            else if ( e )
		            key = e.which;
	            else
		            return true;
	            keychar = String.fromCharCode(key);
	            if (((" !@#$%^&*()_+=-';{}[]|?<>:,/\".1234567890").indexOf(keychar) > -1))
		        return false;
	            else
		        return true;
              }   
          
    </script>

  <%--  <div style="height: 150px; margin: 0 0px 15px 0px; padding: 5px; background-color: #f7f7f7;
        border: 1px #E2BBA0 solid;">
        <img src="images/b1.jpg" alt="banner" width="653" height="150" />
    </div>--%>
    <asp:UpdatePanel ID="UpdPanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td class="rowseparator">
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="padding: 10px">
                            <legend>Petro Card Mapping</legend>
                            <table align="center">
                                <tr>
                                    <td class="rowseparator">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:GridView ID="gvPetroCardMapping" runat="server" Width="459px" CellPadding="3"
                                            CellSpacing="2" GridLines="None" class="table table-striped table-bordered table-hover" OnPageIndexChanging="gvPetroCardMapping_PageIndexChanging"
                                            OnRowEditing="gvPetroCardMapping_RowEditing" AutoGenerateColumns="False" OnRowCommand="gvPetroCardMapping_RowCommand" EmptyDataText="No Records Found">
                                            <RowStyle CssClass="rowStyleGrid" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Vehicle" DataField="Vehicle" />
                                                <asp:BoundField HeaderText="CardNo" DataField="CardNo" />
                                                <asp:BoundField HeaderText="IssuedDate" DataField="IssuedToAmbyDate" />
                                                <asp:BoundField HeaderText="Agency" DataField="Agency" />
                                                <asp:BoundField HeaderText="Card" DataField="Card" />
                                                <asp:BoundField HeaderText="Validity" DataField="Validity" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"MapID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="footerStylegrid" />
                                            <PagerStyle CssClass="pagerStylegrid" />
                                            <SelectedRowStyle CssClass="selectedRowStyle" />
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td class="rowseparator">
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="padding: 10px">
                            <table style="width: 463px" align="center">
                                <tr>
                                    <td align="left" style="width: 155px; height: 19px">
                                        Vehicle Number<span style="color: Red">*</span>
                                    </td>
                                    <td style="width: 109px; height: 19px">
                                        <cc1:ComboBox AutoCompleteMode="Append" ID="ddlVehicleNumber" runat="server" AutoPostBack="True" DropDownStyle="DropDownList" Width="154px"
                                            OnSelectedIndexChanged="ddlVehicleNumber_SelectedIndexChanged">
                                        </cc1:ComboBox>
                                    </td>
                                    <td style="width: 100px; height: 19px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        Petro Card Number<span style="color: Red">*</span>
                                    </td>
                                    <td style="width: 109px">
                                        <asp:DropDownList ID="ddlPetroCardNumber" runat="server" AutoPostBack="True" Width="154px"
                                            OnSelectedIndexChanged="ddlPetroCardNumber_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        Card Validity<span style="color: Red">*</span>
                                    </td>
                                    <td style="width: 109px">
                                        <asp:TextBox ID="txtCardValidity" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        Agency<span style="color: Red">*</span>
                                    </td>
                                    <td style="width: 109px">
                                        <asp:TextBox ID="txtAgency" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        Card Type<span style="color: Red">*</span>
                                    </td>
                                    <td style="width: 109px">
                                        <asp:TextBox ID="txtCardType" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtEdit" runat="server" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        Issue To Ambulance Date<span style="color: Red">*</span>
                                    </td>
                                    <td align="left" style="width: 109px">
                                        <asp:TextBox ID="txtIssDate" runat="server" oncut="return false;" onpaste="return false;"
                                            oncopy="return false;" onkeypress="return false" MaxLength="15"></asp:TextBox>
                                        <cc1:CalendarExtender ID="IssDate" runat="server" TargetControlID="txtIssDate" Format="dd/MM/yyyy" PopupButtonID="ImageButton1">
                                        </cc1:CalendarExtender><asp:ImageButton ID="ImageButton1" runat="server" alt="" src="images/Calendar.gif" Style="vertical-align: top" />
                                    </td>
                                    <td nowrap="nowrap" style="width: 51px">
                                                    
                                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 155px">
                                        <asp:RadioButton ID="Deactivate" runat="server" Text="Deactivate" OnCheckedChanged="Deactivate_CheckedChanged"
                                            AutoPostBack="true" Visible="false" GroupName="kk" />
                                    </td>
                                    <td style="width: 109px">
                                        <asp:RadioButton ID="TransfertoNewVehicle" runat="server" OnCheckedChanged="TransfertoNewVehicle_CheckedChanged"
                                            Text="Transfer" AutoPostBack="true" Visible="false" GroupName="kk" />
                                    </td>
                                    <td style="width: 100px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px; height: 19px">
                                        <asp:Label ID="lbReason" runat="server" Text="Reason" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 109px; height: 19px">
                                        <asp:DropDownList ID="ddlReason" runat="server" AutoPostBack="True" Width="154px"
                                            Visible="false" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; height: 19px">
                                        <asp:TextBox ID="txtRemarks" runat="server" Visible="False" onkeypress="return OnlyAlphabets(event);"
                                            MaxLength="15"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        <asp:Label ID="lbTransfer" runat="server" Text="Transfer To New Vehicle" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 109px">
                                        <asp:DropDownList ID="ddlNewVehicleNumber" runat="server" AutoPostBack="True" Width="154px"
                                            Visible="False" OnSelectedIndexChanged="ddlNewVehicleNumber_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 100px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 155px">
                                        <asp:Label ID="lbPetroCard" runat="server" Text="Petro Card Mapped" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 109px">
                                        <asp:TextBox ID="txtMappedCardNum" runat="server" Visible="False" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rowseparator">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; height: 19px">
                                        <asp:Button ID="Save" runat="server" Text="Save" OnClientClick="return isMandatory();"
                                            OnClick="Save_Click" />&nbsp;
                                    </td>
                                    <td align="left" style="width: 99px; height: 19px">
                                        &nbsp;<asp:Button ID="Reset" runat="server" Text="Reset" OnClick="Reset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

