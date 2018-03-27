﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.InsuranceAgencies, App_Web_ttubqba0" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
              
              
               
               function  validationInsuranceDetails()
            {  
                   if(document.getElementById("<%=txtInsuranceAgency.ClientID %>").value==0)
                 {
                    alert("Please Enter Insurance Agency Name");
                    document.getElementById("<%=txtInsuranceAgency.ClientID %>").focus();
                    return false;
                 }
                  if(document.getElementById("<%=txtAddress.ClientID %>").value==0)
                 {
                    alert("Please Enter Address");
                    document.getElementById("<%=txtAddress.ClientID %>").focus();
                    return false;
                 }
                 if(document.getElementById("<%=txtContactPerson.ClientID %>").value==0)
                 {
                    alert("Please Enter ContactPerson Name");
                    document.getElementById("<%=txtContactPerson.ClientID %>").focus();
                    return false;
                 }
                  if(document.getElementById("<%=txtContactNo.ClientID %>").value==0)
                 {
                    alert("Please Enter  Contact Number");
                    document.getElementById("<%=txtContactNo.ClientID %>").focus();
                    return false;
                 }
   
                                       var phone = document.getElementById("<%=txtContactNo.ClientID %>").value;     
                                        if (isNaN(parseInt(phone))) 
                                        {
                                        alert("The phone number contains illegal characters");
                                        //phone.focus();
                                        return false;   
                                        }
                                        else{} 
                                        if (!((phone.length >= 10)&&(phone.length <= 15)))
                                        {
                                        alert("The phone number is the wrong length");
                                        //phone.focus();
                                        return false;        
                                        }
                                        else{} 
                
                   }                     
                                        
                                        
    </script>

    <%--<div style="height: 150px; margin: 0 0px 15px 0px; padding: 5px; background-color: #f7f7f7;
        border: 1px #E2BBA0 solid;">
        <img src="images/b1.jpg" alt="banner" width="653" height="150" />
    </div>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="table1" cellspacing="0" cellpadding="0" width="500px" align="center" border="0"
                style="height: 37px">
                <tr>
                    <td class="rowseparator">
                    </td>
                </tr>
                <tr>
                    <td style="height: 200px">
                        <fieldset style="padding: 10px;">
                            <legend>Insurance Agency</legend>
                            <table id="table2" class="bordergreen" width="91%" align="center">
                                <tr>
                                    <td>
                                        <table class="logtable" align="center">
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Insurance Agency <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceAgency" runat="server" CssClass="textbox180" Width="150px"
                                                        MaxLength="35"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Address <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td style="height: 23px">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox180" TextMode="MultiLine"
                                                        onKeyUp="CheckLength(this,300)" onChange="CheckLength(this,300)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Contact Person <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox180" MaxLength="35"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Contact No <span style="color: Red" class="labelErr">*</span>
                                                </td>
                                                <td class="columnseparator">
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="textbox180" MaxLength="15"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rowseparator">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 41px">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnInsuranceUpdate" Width="55px" runat="server" CssClass="button"
                                                        Text="Insert" OnClientClick="return validationInsuranceDetails();" OnClick="btnInsuranceUpdate_Click">
                                                    </asp:Button>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnInsuranceReset" Width="55px" runat="server"
                                                        CssClass="button" Text="Reset" CausesValidation="false" OnClick="btnInsuranceReset_Click">
                                                    </asp:Button>
                                                    <input type="hidden" id="hidBatText" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
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
                        <fieldset style="padding: 10px;">
                            <asp:GridView ID="grvInsuranceAgencyDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" CellSpacing="2" PageSize="5" Width="548px" OnRowCommand="grvInsuranceAgencyDetails_RowCommand"
                                OnPageIndexChanging="grvInsuranceAgencyDetails_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InsuranceAgency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInsuranceAgency" runat="server" Text='<%#Eval("InsuranceAgency")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ContactPerson">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" runat="server" Text='<%#Eval("ContactPerson")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ContactNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactNumber" runat="server" Text='<%#Eval("ContactNumber")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditAgency" Text="Edit"
                                                CommandArgument='<%#Eval("InsuranceId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="DeleteAgency" Text="Delete"
                                                CommandArgument='<%#Eval("InsuranceId")%>' />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you Sure Want to delete"
                                                TargetControlID="lnkbtnDelete">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

