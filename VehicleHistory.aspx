﻿<%@ page title="" language="C#" masterpagefile="~/temp.master" autoeventwireup="true" inherits="GvkFMSAPP.PL.history.VehicleHistory, App_Web_ttubqba0" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset style="padding: 10px; width: 650px;">
                <legend>Vehicle History</legend>
                <table style="width: 650px">
                    <tr>
                        <td class="rowseparator" colspan="3">
                        </td>
                    </tr>
                    <tr> 
                        <td style="width: 100px">
                            <asp:Label ID="lblVehicleSelect" runat="server" Text="Select a Vehicle"></asp:Label>
                        </td>
                        <td class="columnseparator">
                        </td>
                        <td style="width: 400px" align="left">
                            <asp:DropDownList ID="ddlVehicleList" runat="server" OnSelectedIndexChanged="ddlVehicleList_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropShadowExtender ID="DropShadowExtender1" runat="server" Enabled="True" TargetControlID="ddlVehicleList">
                            </asp:DropShadowExtender>
                            <asp:DropDownExtender ID="UpdatePanel1_DropDownExtender" runat="server" DynamicServicePath=""
                                Enabled="True" TargetControlID="ddlVehicleList">
                            </asp:DropDownExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowseparator" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="Panel_Detail" runat="server" Visible="false" Width="100%">
                                <table cellpadding="2" cellspacing="2" width="600px">
                                    <tr>
                                        <td>
                                            District
                                        </td>
                                        <td class="rowseparator">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                                        </td>
                                        <td class="rowseparator">
                                        </td>
                                        <td>
                                            Petrol Card
                                        </td>
                                        <td class="rowseparator">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPetroCard" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowseparator" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="panel_vehicleDetail" runat="server" Visible="false" Width="100%">
                                <asp:Accordion ID="Accordion1" runat="server" Width="650px" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent" AutoSize="Fill" FadeTransitions="true" TransitionDuration="50"
                                    Height="300px">
                                    <Panes>
                                        <asp:AccordionPane ID="AccordionPane1" runat="server" BorderColor="Red" ForeColor="Red">
                                            <Header>
                                                Statutory Compliance</Header>
                                            <Content>
                                                <asp:Accordion ID="Accordion2" runat="server" Width="650px" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="true" TransitionDuration="50"
                                                    Height="300px">
                                                    <Panes>
                                                        <asp:AccordionPane ID="AccordionPane4" runat="server" Height="300px">
                                                            <Header>
                                                                Vehicle Insurance</Header>
                                                            <Content>
                                                                <asp:GridView ID="gvVehicleInsurance" runat="server" Width="630px" AutoGenerateColumns="False"
                                                                    AllowPaging="True" CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found"
                                                                    GridLines="None" CssClass="gridviewStyle" CellSpacing="2">
                                                                    <RowStyle CssClass="rowStyleGrid" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vehicle Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVehicleNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleNumber") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Policy Start Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPolicyStartDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PolicyStartDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Policy End Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPolicyEndDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PolicyEndDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Insurance Agency">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsuranceAgency" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InsAgency") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Insurance Policy No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsurancePolicyNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InsurancePolicyNo") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Insurance Fees Paid">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsuranceFeesPaid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InsuranceFeesPaid") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Insurance Receipt No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsuranceReceiptNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InsuranceReceiptNo") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Insurance Fees Paid Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsuranceFeesPaidDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InsuranceFeesPaidDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Insurance Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsuranceType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InsuranceTypeName") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="footerStylegrid" />
                                                                    <PagerStyle CssClass="pagerStylegrid" />
                                                                    <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                </asp:GridView>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                        <asp:AccordionPane ID="AccordionPane5" runat="server" Height="300px">
                                                            <Header>
                                                                Road Tax</Header>
                                                            <Content>
                                                                <table cellpadding="2" cellspacing="2" width="600px">
                                                                    <tr>
                                                                        <td class="rowseparator">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="gvRoadTax" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                Width="630px" ForeColor="#333333" GridLines="None" AllowPaging="True" EmptyDataText="No Records Found"
                                                                                CssClass="gridviewStyle" CellSpacing="2">
                                                                                <RowStyle CssClass="rowStyleGrid" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Vehicle Number">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblVehicleNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleNumber") %>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="RTValidityStartDate" HeaderText="RTValidity StartDate"
                                                                                        DataFormatString="{0:d}" />
                                                                                    <asp:TemplateField HeaderText="RTValidity Period">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRTValidityPeriod" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"RTValidityPeriod") %>'></asp:Label>
                                                                                            <asp:Label ID="lblRTValidityPeriodText" runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="RTValidityEndDate" HeaderText="RTValidity EndDate" DataFormatString="{0:d}" />
                                                                                    <asp:TemplateField HeaderText="Vehicle RTA Circle">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblVehicleRTACircle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleRTACircle") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="RT Receipt No">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRTReceiptNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RTReceiptNo") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Road Tax Fee">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRTFee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RTFee") %>'></asp:Label>
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
                                                                    <tr>
                                                                        <td class="rowseparator">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                        <asp:AccordionPane ID="AccordionPane6" runat="server" BorderColor="Red" ForeColor="Red">
                                                            <Header>
                                                                Pollution Under Control</Header>
                                                            <Content>
                                                                <asp:GridView ID="gvPollutionUnderControl" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" Width="630px" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Found"
                                                                    AllowPaging="True" CssClass="gridviewStyle" CellSpacing="2">
                                                                    <RowStyle CssClass="rowStyleGrid" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vehicle Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVehicleNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleNumber") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PUC Validity Start Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPUCValidityStartDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PUCValidityStartDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PUC Validity Period">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPUCValidityPeriod" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PUCValidityPeriod") %>'>
                                                                                </asp:Label>
                                                                                <asp:Label ID="lblPUCValidityPeriodText" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PUC Validity End Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPUCValidityEndDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PUCValidityEndDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PUC Receipt No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPUCReceiptNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PUCReceiptNo") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PUC Fee">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPUCFee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PUCFee") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="footerStylegrid" />
                                                                    <PagerStyle CssClass="pagerStylegrid" />
                                                                    <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                </asp:GridView>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                        <asp:AccordionPane ID="AccordionPane7" runat="server" BorderColor="Red" ForeColor="Red">
                                                            <Header>
                                                                Fitness Renewal</Header>
                                                            <Content>
                                                                <asp:GridView ID="gvFitnessRenewal" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    ForeColor="#333333" GridLines="None" Width="630px" OnRowDataBound="gvFitnessRenewal_RowDataBound"
                                                                    AllowPaging="True" EmptyDataText="No Records Found" CssClass="gridviewStyle"
                                                                    CellSpacing="2" OnPageIndexChanging="gvFitnessRenewal_PageIndexChanging">
                                                                    <RowStyle CssClass="rowStyleGrid" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vehicle Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVehicleNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleNumber") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FRValidity StartDate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFRValidityStartDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FRValidityStartDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FRValidity Period">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFRValidityPeriod" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FRValidityPeriod") %>'>
                                                                                </asp:Label>
                                                                                <asp:Label ID="lblFRValidityPeriodText" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FRValidity EndDate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFRValidityEndDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FRValidityEndDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Vehicle RTACircle">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVehicleRTACircle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VehicleRTACircle") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FRReceipt No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFRReceiptNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FRReceiptNo") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FRFee">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFRFee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FRFee") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="footerStylegrid" />
                                                                    <PagerStyle CssClass="pagerStylegrid" />
                                                                    <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                </asp:GridView>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                    </Panes>
                                                </asp:Accordion>
                                            </Content>
                                        </asp:AccordionPane>
                                        <asp:AccordionPane ID="AccordionPane3" runat="server" BorderColor="Red" ForeColor="Red"
                                            Height="300px">
                                            <Header>
                                                Fuel Management</Header>
                                            <Content>
                                                <asp:Accordion ID="Accordion4" runat="server" Width="650px" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="true" TransitionDuration="50">
                                                    <Panes>
                                                        <asp:AccordionPane ID="AccordionPane12" runat="server" BorderColor="Red" ForeColor="Red">
                                                            <Header>
                                                                Petrol Card details</Header>
                                                            <Content>
                                                                <asp:GridView ID="grdPetroCard" runat="server" Width="630px" AutoGenerateColumns="False"
                                                                    AllowPaging="True" CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found"
                                                                    GridLines="None" CssClass="gridviewStyle" CellSpacing="2" OnPageIndexChanging="grdPetroCard_PageIndexChanging">
                                                                    <RowStyle CssClass="rowStyleGrid" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="PetroCard Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPetroCardNum" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PetroCardNum") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Petrocard Issue Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPolicyStartDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IssueDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="footerStylegrid" />
                                                                    <PagerStyle CssClass="pagerStylegrid" />
                                                                    <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                </asp:GridView>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                        <asp:AccordionPane ID="AccordionPane13" runat="server" BorderColor="Red" ForeColor="Red">
                                                            <Header>
                                                                Fuel Entry Detail</Header>
                                                            <Content>
                                                                <asp:GridView ID="grdFuelEntry" runat="server" Width="630px" AutoGenerateColumns="False"
                                                                    AllowPaging="True" CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found"
                                                                    GridLines="None" CssClass="gridviewStyle" CellSpacing="2">
                                                                    <RowStyle CssClass="rowStyleGrid" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Bill Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBillNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BillNumber") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Amount") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EntryDate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EntryDate") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Quantity") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PetroCard Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInsurancePolicyNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PetroCardNum") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Agency">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAgencyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AgencyName") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="footerStylegrid" />
                                                                    <PagerStyle CssClass="pagerStylegrid" />
                                                                    <SelectedRowStyle CssClass="selectedRowStyle" />
                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                </asp:GridView>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                    </Panes>
                                                </asp:Accordion>
                                            </Content>
                                        </asp:AccordionPane>
                                    </Panes>
                                </asp:Accordion>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DynamicLayout="true">
        <ProgressTemplate>
            <img src="../images/ajax-loader.gif" alt="" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

