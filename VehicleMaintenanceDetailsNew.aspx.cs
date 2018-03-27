using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
namespace GvkFMSAPP.PL.VehicleMaintenanceandRepairs
{
    public partial class VehicleMaintenanceDetailsNew : System.Web.UI.Page
    {
       GvkFMSAPP.BLL.FMSGeneral fmsgeneral = new GvkFMSAPP.BLL.FMSGeneral();
       GvkFMSAPP.BLL.VehicleMaintenance.VehicleMaintenance vehMain = new GvkFMSAPP.BLL.VehicleMaintenance.VehicleMaintenance();
       GvkFMSAPP.BLL.VAS fmsVas = new GvkFMSAPP.BLL.VAS();
       GvkFMSAPP.BLL.BaseVehicleDetails fmsobj = new GvkFMSAPP.BLL.BaseVehicleDetails();
        GvkFMSAPP.BLL.VAS_BLL.VASGeneral vehallobj = new GvkFMSAPP.BLL.VAS_BLL.VASGeneral();

        DataSet dslabourAggregates = null;
        DataSet dsLabourCategories = null;
        DataSet dsLabourSubCategories = null;
	protected void Page_PreInit(Object sender, EventArgs e)
        {

            if (Session["Role_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else {
                if (Session["Role_Id"].ToString() == "120")
                { 
                    this.MasterPageFile = "~/MasterERO.master";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return validation()");
                //GetVehicleNumber();
                GetDistrict();
                FillHoursandMins();
                //FillMaintenanceTypes();
                GetTime();
                SetInitialRowSP();
                SetInitialRowLubricant();
                SetInitialRowLabour();
                btnSave.Enabled = true;
                //GetVehicleMainDet();
            }
        }



        protected void GetTime()
        {
           // string[] timenow = DateTime.Now.ToString().Split(' ');
            string[] timenow = DateTime.Now.ToString("dd/MM/yyyy h:mm tt").Split(' ');
            txtUptime.Text = timenow[0].ToString();
            int hour = Convert.ToInt32(timenow[1].Split(':')[0].ToString());
            int minute = Convert.ToInt32(timenow[1].Split(':')[1].ToString());
            if (timenow[2] != "PM")
            {
                if (hour < 10)
                {
                    ddlUpHour.Items.FindByValue("0" + timenow[1].Split(':')[0].ToString()).Selected = true;
                }
                else
                {
                    ddlUpHour.Items.FindByValue(timenow[1].Split(':')[0].ToString()).Selected = true;
                }
            }
            else
            {
                if (hour == 12)
                    hour = 0;
                hour = hour + 12;
                ddlUpHour.Items.FindByValue(hour.ToString()).Selected = true;
            }


            if (minute < 10)
            {
                ddlUpMin.Items.FindByValue(timenow[1].Split(':')[1]).Selected = true;
            }
            else
            {
                ddlUpMin.Items.FindByValue(timenow[1].Split(':')[1]).Selected = true;
            }
        }

        public void GetVehicleNumber()
        {

            fmsVas.District = ddlDistrict.SelectedItem.Text;
            ddlVehicleNumber.DataSource = fmsVas.GetOffRoadVehicles();// .GetVehicleNumber();
            ddlVehicleNumber.DataValueField = "ORV_VehOffRoadId";
            ddlVehicleNumber.DataTextField = "ORV_VehicleNumber";
            ddlVehicleNumber.DataBind();
            ddlVehicleNumber.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void GetDistrict()
        {
            ddlDistrict.DataSource = fmsobj.GetDistricts_new();
            ddlDistrict.DataTextField = "district_name";
            ddlDistrict.DataValueField = "district_id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        //public void FillMaintenanceTypes()
        //{
        //    DataSet ds = fmsgeneral.GetMaintenanceType();
        //    ddlMaintenanceType.DataSource = ds.Tables[0];
        //    ddlMaintenanceType.DataValueField = "Maint_Type_ID";
        //    ddlMaintenanceType.DataTextField = "Maint_Desc";
        //    ddlMaintenanceType.DataBind();
        //    ddlMaintenanceType.Items.Insert(0, new ListItem("--Select--", "0"));
        //}

        private void FillHoursandMins()
        {
            int i;
            for (i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    this.ddlUpHour.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.ddlUpHour.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            for (i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    this.ddlUpMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.ddlUpMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }

        }

        protected void ddlVehicleNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVehicleNumber.SelectedIndex != 0)
            {
                fmsVas.VehicleNumber = ddlVehicleNumber.SelectedItem.Text;
                fmsVas.VehOffRoadId = Convert.ToInt64(ddlVehicleNumber.SelectedValue);
                DataSet ds = fmsVas.GetOffRoadVehicleDetails();

                txtMaintenanceType.Text = ds.Tables[0].Rows[0][0].ToString();
                txtDownTime.Text = ds.Tables[1].Rows[0][0].ToString();

                if (ds.Tables[2].Rows.Count != 0)
                {
                    txtDownOdo.Text = ds.Tables[2].Rows[0][0].ToString();
                }
                else
                {
                    txtDownOdo.Text = "";
                }
                lblBreakdownID.Text = ds.Tables[3].Rows[0][0].ToString();

                if (ds.Tables[4].Rows.Count != 0)
                {
                    lblApprovedCost.Text = ds.Tables[4].Rows[0][0].ToString();
                }
                else
                {
                    lblApprovedCost.Text = "";
                }

                GetVehicleMainDet();
            }
        }

        protected void ddlSpareItemDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            int rowIndex = row.RowIndex;
            fmsVas.SpareId = Convert.ToString(((DropDownList)grdvwSPBillDetails.Rows[rowIndex].Cells[6].FindControl("ddlSpareItemDesc")).SelectedValue);
            fmsVas.SpareItemDesc = ((DropDownList)grdvwSPBillDetails.Rows[rowIndex].Cells[6].FindControl("ddlSpareItemDesc")).SelectedItem.Text;
            fmsVas.VehicleNumber = ddlVehicleNumber.SelectedItem.Text;
            DataSet insres = fmsVas.GetSpareCode();
            if (insres.Tables[0].Rows.Count > 0)
            {

                ((TextBox)grdvwSPBillDetails.Rows[rowIndex].Cells[5].FindControl("txtSparePartCode")).Text = Convert.ToString(insres.Tables[0].Rows[0]["ManufacturerSpare_Id"]);
                ((TextBox)grdvwSPBillDetails.Rows[rowIndex].Cells[8].FindControl("txtSpareBillAmount")).Text = Convert.ToString(insres.Tables[0].Rows[0]["Cost"]);

            }

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            string spbillno = "", spbilldate = "", spbillamt = "", spvendorname = "", spemripartcode = "", sppartcode = "", spitemdesc = "", spqty = "";
            string lubribillno = "", lubribilldate = "", lubribillamt = "", lubrivendorname = "", lubriemripartcode = "", lubripartcode = "", lubriitemdesc = "", lubriqty = "";
            string labourbillno = "", labourbilldate = "", labourbillamt = "", labAggre = "", labCate = "", labsubCat = "", labItemDesc = "", labqty = "", labVendorname = "";



            fmsVas.District = ddlDistrict.SelectedItem.Text;//txtDistrict.Text;
            fmsVas.MaintenanaceType = txtMaintenanceType.Text;
try{
            fmsVas.MaintenanceDate =DateTime.ParseExact(txtMaintenanceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

 // DateTime.Parse(txtMaintenanceDate.Text);
}
catch(Exception ex)
{
Show(txtMaintenanceDate.Text);
  // Show("txtMaintenanceDate.Text");
return;
}
            fmsVas.DownTimeOdoReading = txtDownOdo.Text;
try{
            fmsVas.Downtime =  DateTime.ParseExact(txtDownTime.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
}
catch(Exception ex)
{
  Show(txtDownTime.Text + ex.ToString());
return;
}

            fmsVas.UptimeOdoReading = txtUpOdo.Text;

try{
DateTime.ParseExact(txtUptime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
}
catch(Exception ex)
{
Show("fmsVas.Uptime--txtUptime.Text");
}
try{
	string dt = DateTime.ParseExact(txtUptime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy") + " " + ddlUpHour.SelectedValue + ":" + ddlUpMin.SelectedValue + ":00";

            fmsVas.Uptime = DateTime.ParseExact(dt,"dd-MM-yyyy HH:mm:ss",CultureInfo.InvariantCulture);
}

catch(Exception ex)
{
string dt = DateTime.ParseExact(txtUptime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString() + " " + ddlUpHour.SelectedValue + ":" + ddlUpMin.SelectedValue + ":00";

Show(dt);
  Show("fmsVas.Uptime");
return;
}
            if (pnlSPBillDetails.Visible)
            {
                for (int j = 0; j < grdvwSPBillDetails.Rows.Count; j++)
                {
                    spbillno = spbillno + ((TextBox)grdvwSPBillDetails.Rows[j].Cells[2].FindControl("txtSpareBillNo")).Text + ",";
                    spbilldate = spbilldate + ((TextBox)grdvwSPBillDetails.Rows[j].Cells[3].FindControl("txtSpareBillDate")).Text + ",";
                    spbillamt = spbillamt + ((TextBox)grdvwSPBillDetails.Rows[j].Cells[8].FindControl("txtSpareBillAmount")).Text + ",";
                    spemripartcode = spemripartcode + ((TextBox)grdvwSPBillDetails.Rows[j].Cells[4].FindControl("txtSpareEMRIpc")).Text + ",";
                    spvendorname = spvendorname + ((DropDownList)grdvwSPBillDetails.Rows[j].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem + ",";
                    sppartcode = sppartcode + ((TextBox)grdvwSPBillDetails.Rows[j].Cells[5].FindControl("txtSparePartCode")).Text + ",";
                    spitemdesc = spitemdesc + ((DropDownList)grdvwSPBillDetails.Rows[j].Cells[6].FindControl("ddlSpareItemDesc")).SelectedItem + ",";
                    spqty = spqty + ((TextBox)grdvwSPBillDetails.Rows[j].Cells[7].FindControl("txtSpareQuant")).Text + ",";


                }
            }

            if (pnlLubricantBillDetails.Visible)
            {
                for (int j = 0; j < grdvwLubricantBillDetails.Rows.Count; j++)
                {
                    lubribillno = lubribillno + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[2].FindControl("txtLubricantBillNo")).Text + ",";
                    lubribilldate = lubribilldate + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[3].FindControl("txtLubricantBillDate")).Text + ",";
                    lubribillamt = lubribillamt + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[8].FindControl("txtLubricantBillAmount")).Text + ",";
                    lubriemripartcode = lubriemripartcode + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[4].FindControl("txtLubricantEMRIpc")).Text + ",";
                    lubrivendorname = lubrivendorname + ((DropDownList)grdvwLubricantBillDetails.Rows[j].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem + ",";
                    lubripartcode = lubripartcode + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[5].FindControl("txtLubricantPartCode")).Text + ",";
                    lubriitemdesc = lubriitemdesc + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[6].FindControl("txtLubricantItemDesc")).Text + ",";
                    lubriqty = lubriqty + ((TextBox)grdvwLubricantBillDetails.Rows[j].Cells[7].FindControl("txtLubricantQuant")).Text + ",";
                }
            }

            if (pnlLabourBillDetails.Visible)
            {
                for (int j = 0; j < grdvwLabourBillDetails.Rows.Count; j++)
                {
                    labourbillno = labourbillno + ((TextBox)grdvwLabourBillDetails.Rows[j].Cells[2].FindControl("txtLabourBillNo")).Text + ",";
                    labourbilldate = labourbilldate + ((TextBox)grdvwLabourBillDetails.Rows[j].Cells[3].FindControl("txtLabourBillDate")).Text + ",";
                    labourbillamt = labourbillamt + ((TextBox)grdvwLabourBillDetails.Rows[j].Cells[9].FindControl("txtLabourBillAmount")).Text + ",";
                    labVendorname = labVendorname + ((DropDownList)grdvwLabourBillDetails.Rows[j].Cells[1].FindControl("ddlLabourVendorName")).SelectedItem + ",";
                    labAggre = labAggre + ((ComboBox)grdvwLabourBillDetails.Rows[j].Cells[4].FindControl("ddlLabourAggregates")).SelectedItem + ",";
                    labCate = labCate + ((ComboBox)grdvwLabourBillDetails.Rows[j].Cells[5].FindControl("ddlLabourCategories")).SelectedItem + ",";
                    labsubCat = labsubCat + ((ComboBox)grdvwLabourBillDetails.Rows[j].Cells[6].FindControl("ddlLabourSubCategories")).SelectedItem + ",";
                    labItemDesc = labItemDesc + ((TextBox)grdvwLabourBillDetails.Rows[j].Cells[7].FindControl("txtLabourItemDesc")).Text + ",";
                    labqty = labqty + ((TextBox)grdvwLabourBillDetails.Rows[j].Cells[8].FindControl("txtLabourQuant")).Text + ",";
                }
            }

            fmsVas.SpareBillNo = spbillno;
            fmsVas.SpareBillAmount = spbillamt;
            fmsVas.SpareBillDate = spbilldate;
            fmsVas.SpareEmriPartCode = spemripartcode;
            fmsVas.SpareItemDesc = spitemdesc;
            fmsVas.SparePartCode = sppartcode;
            fmsVas.SpareVendorName = spvendorname;
            fmsVas.SpareQty = spqty;

            fmsVas.LubricantBillNo = lubribillno;
            fmsVas.LubricantBillAmount = lubribillamt;
            fmsVas.LubricantBillDate = lubribilldate;
            fmsVas.LubricantEmriPartCode = lubriemripartcode;
            fmsVas.LubricantItemDesc = lubriitemdesc;
            fmsVas.LubricantPartCode = lubripartcode;
            fmsVas.LubricantVendorName = lubrivendorname;
            fmsVas.LubricantQty = lubriqty;

            fmsVas.LabourBillNo = labourbillno;//txtLabourBill.Text == "" ? "0" : txtLabourBill.Text;
            fmsVas.LabourBillAmount = labourbillamt; //Convert.ToDecimal(txtLabourBillAmount.Text == "" ? "0" : txtLabourBillAmount.Text);
            fmsVas.LabourBillDate = labourbilldate; //DateTime.Parse(txtLabourBillDate.Text == "" ? DateTime.Now.ToString() : txtLabourBillDate.Text);
            fmsVas.LabourVendorName = labVendorname;
            fmsVas.LabourAggregates = labAggre;
            fmsVas.LabourCategories = labCate;
            fmsVas.LabourSubCategories = labsubCat;
            fmsVas.LabourItemDesc = labItemDesc;
            fmsVas.LabourQty = labqty;

try{
            DateTime MaintenanceDate = DateTime.ParseExact(txtMaintenanceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
}
catch(Exception ex)
{
 Show("txtMaintenanceDate.Text");
}
try{
            DateTime DownDate = DateTime.ParseExact(txtDownTime.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
}
catch(Exception ex)
{
 Show(txtDownTime.Text);
}

            // MaintenanceDate = DateTime.ParseExact(MaintenanceDate.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToString("HH:mm:ss"), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string[] a = txtDownTime.Text.Split(' ');

            DateTime dtDate = DateTime.ParseExact(a[0], "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime dtDate3 = DateTime.ParseExact(txtMaintenanceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (dtDate <= dtDate3)
            {

                if (btnSave.Text == "Save")
                {
                    fmsVas.VehicleNumber = ddlVehicleNumber.SelectedItem.Text;
                    DataSet insres = fmsVas.InsertOffRoadVehcileMaintenance();

                    if (Convert.ToString(insres.Tables[0].Rows[0]["Result"].ToString()) == string.Empty)
                    {
                        Show("Records Inserted Successfully");
                        ClearAll();
                    }
                    else
                    {
                        Show(Convert.ToString(insres.Tables[0].Rows[0]["Result"]));
                    }




                    //if (insres != 0)
                    //{
                    //    Show("Records Inserted Successfully");
                    //    ClearAll();
                    //}
                    //else
                    //    Show("Error!!!!");

                    GetVehicleMainDet();
                }
                else
                {
                    fmsVas.VehicleNumber = txtVehicleNumber.Text;
                    fmsVas.OffRoadId = Convert.ToInt32(ViewState["OffRoadId"]);

                    DataSet insres = fmsVas.UpdateOffRoadVehcileMaintenance();

                    if (Convert.ToString(insres.Tables[0].Rows[0]["Result"].ToString()) == string.Empty)
                    {
                        Show("Records Updated Successfully");
                        ClearAll();
                    }
                    else
                    {
                        Show(Convert.ToString(insres.Tables[0].Rows[0]["Result"]));
                    }



                    ddlVehicleNumber.Visible = true;
                    txtVehicleNumber.Visible = false;

                    //GetVehicleMainDet();
                }
            }
            else
            {
		// Show( DateTime.ParseExact(txtDownTime.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString());
Show( DateTime.ParseExact(txtMaintenanceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString());
                Show("Maintenance Date should be greater than Down Time.");
                return;
            }
        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            ddlVehicleNumber.Visible = true;
            txtVehicleNumber.Text = "";
            txtVehicleNumber.Visible = false;
            ddlVehicleNumber.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            //txtDistrict.Text = "";
            txtMaintenanceType.Text = "";
            txtMaintenanceDate.Text = "";
            txtDownOdo.Text = "";
            txtDownTime.Text = "";
            txtUpOdo.Text = "";
            txtUptime.Text = "";
            ddlUpHour.SelectedIndex = 0;
            ddlUpMin.SelectedIndex = 0;
            lblBreakdownID.Text = "";
            chkAmount.Checked = true;
            foreach (ListItem item in chkbxlistBillType.Items)
            {
                item.Selected = false;
            }
            btnSave.Enabled = true;
            chkbxlistBillType.Enabled = false;
            pnlSPBillDetails.Visible = false;
            pnlLubricantBillDetails.Visible = false;
            pnlLabourBillDetails.Visible = false;
            pnlBillSummaryDetails.Visible = false;
            pnlBillDetailsSummaryBtn.Visible = false;
            txtTotalBillAmt.Text = "";
            lblApprovedCost.Text = "";
            btnSave.Text = "Save";
            btnSave.Enabled = true;
        }

        public void GetVehicleMainDet()
        {
            var VehicleNumber = ddlVehicleNumber.SelectedItem.Text;
            ViewState["VehicleMainDet"] = fmsVas.GetVehicleMainDet();
            gvVehicleMaintenanceDetails.DataSource = (DataSet)ViewState["VehicleMainDet"];
            gvVehicleMaintenanceDetails.DataBind();
        }

        protected void gvVehicleMaintenanceDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehicleMaintenanceDetails.PageIndex = e.NewPageIndex;
            GetVehicleMainDet();
        }

        protected void gvVehicleMaintenanceDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VehMainEdit")
            {

                DataRow[] drvehoffroad = ((DataSet)ViewState["VehicleMainDet"]).Tables[0].Select("OffRoad_Id=" + Convert.ToInt32(e.CommandArgument));

                ViewState["OffRoadId"] = Convert.ToInt32(drvehoffroad[0]["OffRoad_Id"].ToString());

                foreach (ListItem item in ddlDistrict.Items)
                {
                    if (item.Selected)
                    {
                        item.Selected = false;
                    }
                }
                //string VehicleNo = gvVehicleMaintenanceDetails.SelectedRow.Cells[0].ToString();
                //Label lblVehicle_No=(Label)

                ddlDistrict.Items.FindByText(drvehoffroad[0]["District"].ToString()).Selected = true;
                ddlVehicleNumber.Visible = false;
                txtVehicleNumber.Visible = true;
                txtVehicleNumber.Text = drvehoffroad[0]["OffRoadVehicle_No"].ToString();
                //ddlVehicleNumber.SelectedValue = drvehoffroad[0]["OffRoadVehicle_No"].ToString();
                //txtDistrict.Text = drvehoffroad[0]["District"].ToString();
                //ddlMaintenanceType.SelectedIndex = Convert.ToInt32(drvehoffroad[0]["MaintenanaceType"]);//Items.FindByValue(drvehoffroad[0]["MainId"].ToString()).Selected = true;
                txtMaintenanceType.Text = drvehoffroad[0]["MaintenanaceType"].ToString();
                txtMaintenanceDate.Text = drvehoffroad[0]["MaintenanceDate"].ToString();
                txtDownOdo.Text = drvehoffroad[0]["DownTimeOdoReading"].ToString();
                txtDownTime.Text = drvehoffroad[0]["Downtime"].ToString();
                txtUpOdo.Text = drvehoffroad[0]["UptimeOdoReading"].ToString();

                string[] uptime = drvehoffroad[0]["Uptime"].ToString().Split(' ');
                txtUptime.Text = uptime[0].ToString();
                string[] hrmin = uptime[1].ToString().Split(':');

                if (uptime[2] == "PM" && hrmin[0].ToString() != "12")
                    ddlUpHour.SelectedIndex = Convert.ToInt32(hrmin[0].ToString()) + 13;
                else
                    ddlUpHour.SelectedIndex = Convert.ToInt32(hrmin[0].ToString()) + 1;

                ddlUpMin.SelectedIndex = Convert.ToInt32(hrmin[1].ToString()) + 1;


                DataSet dsbilldet = new DataSet();
                fmsVas.OffRoadId = Convert.ToInt32(ViewState["OffRoadId"]);
                dsbilldet = fmsVas.GetBillDetails();
                grdvwSPBillDetails.DataSource = dsbilldet.Tables[0];
                grdvwSPBillDetails.DataBind();
                SetInitialRowSP();

                grdvwLubricantBillDetails.DataSource = dsbilldet.Tables[1];
                grdvwLubricantBillDetails.DataBind();
                SetInitialRowLubricant();

                grdvwLabourBillDetails.DataSource = dsbilldet.Tables[2];
                grdvwLabourBillDetails.DataBind();
                SetInitialRowLabour();

                pnlSPBillDetails.Visible = false;
                pnlLubricantBillDetails.Visible = false;
                pnlLabourBillDetails.Visible = false;
                pnlBillDetailsSummaryBtn.Visible = false;
                pnlBillSummaryDetails.Visible = false;

                chkbxlistBillType.Items[0].Selected = false;
                chkbxlistBillType.Items[1].Selected = false;
                chkbxlistBillType.Items[2].Selected = false;

                if (dsbilldet.Tables[0].Rows.Count > 0)
                {
                    ViewState["SPBillDetails"] = dsbilldet.Tables[0];
                    DisplaySPBillDetails();
                    chkAmount.Checked = false;
                    chkbxlistBillType.Items[0].Selected = true;
                    pnlSPBillDetails.Visible = true;
                }

                if (dsbilldet.Tables[1].Rows.Count > 0)
                {
                    ViewState["LubriBillDetails"] = dsbilldet.Tables[1];
                    DisplayLubriBillDetails();
                    chkAmount.Checked = false;
                    chkbxlistBillType.Items[1].Selected = true;
                    pnlLubricantBillDetails.Visible = true;
                }

                if (dsbilldet.Tables[2].Rows.Count > 0)
                {
                    ViewState["LabourBillDetails"] = dsbilldet.Tables[2];
                    DisplayLabourBillDetails();
                    chkAmount.Checked = false;
                    chkbxlistBillType.Items[2].Selected = true;
                    pnlLabourBillDetails.Visible = true;
                }

                if (pnlSPBillDetails.Visible || pnlLubricantBillDetails.Visible || pnlLabourBillDetails.Visible)
                {
                    chkbxlistBillType.Enabled = true;
                    pnlBillDetailsSummaryBtn.Visible = true;
                    pnlBillSummaryDetails.Visible = true;
                    AddRowToGridSummary();
                }
                else
                {
                    chkAmount.Checked = true;
                    chkbxlistBillType.Enabled = false;
                }

                btnSave.Enabled = true;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "VehMainDelete")
            {
                fmsVas.OffRoadId = Convert.ToInt32(e.CommandArgument);
                int delres = fmsVas.DelOffRoadVehcileMaintenance();
                if (delres != 0)
                    Show("Record Deleted Successfully!!");
                else
                    Show("Error!!");

                GetVehicleMainDet();
            }
            else
            {
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetVehicleNumber();
        }

        private void SetInitialRowSP()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpVendorName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpBillDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpEMRIPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpItemDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpQuantity", typeof(string)));


            dt.Columns.Add(new DataColumn("Column3", typeof(string)));

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            //Bind the DataTable to the Grid
            grdvwSPBillDetails.DataSource = dt;
            grdvwSPBillDetails.DataBind();

            pnlBillSummaryDetails.Visible = false;
            btnSave.Enabled = false;
            txtTotalBillAmt.Text = "";
            //Extract and Fill the DropDownList with Data
            //DropDownList ddl1 = (DropDownList)grdQuotaionDetails.Rows[0].Cells[1].FindControl("ddlVendorName");
            //TextBox txtQuotationNo = (TextBox)grdQuotaionDetails.Rows[0].Cells[2].FindControl("txtQuotationNo");
            //TextBox txtQuotationDate = (TextBox)grdQuotaionDetails.Rows[0].Cells[3].FindControl("txtQuotationDate");
            //TextBox txtQuotationCost = (TextBox)grdQuotaionDetails.Rows[0].Cells[4].FindControl("txtQuotationCost");
            //LinkButton lnkAttachDoc = (LinkButton)grdQuotaionDetails.Rows[0].Cells[5].FindControl("lnkAttachDoc");
            //LinkButton lnkViewFile = (LinkButton)grdQuotaionDetails.Rows[0].Cells[6].FindControl("lnkView");
            //lnkViewFile.Enabled = false;

            //FillAgencies(ddl1);
            //// AddRowToGridSummary();
        }

        private void AddNewRowToGridSP()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                bool flag = false;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        fmsVas.SpareVendorName = ((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem.Text;
                        fmsVas.SpareBillNo = ((TextBox)grdvwSPBillDetails.Rows[i].Cells[2].FindControl("txtSpareBillNo")).Text;
                        DataSet dsSpareValidation = new DataSet();
                        dsSpareValidation = fmsVas.SpareValidation();
                        if (dsSpareValidation.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            Show(" Already Vendor Name :  " + ((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem.Text + "    and Bill Number :   " + ((TextBox)grdvwSPBillDetails.Rows[i].Cells[2].FindControl("txtSpareBillNo")).Text + "   Exists in DataBase ");
                            flag = true;
                            break;

                        }

                    }
                    if (flag)
                    {
                        return;
                    }
                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["CurrentTable"] = dtCurrentTable;
                    DataSet ds = (DataSet)ViewState["Categories"];
                    DataSet ds1 = (DataSet)ViewState["Vendor"];


                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {
                        //extract the DropDownList Selected Items

                        DropDownList ddl1 = (DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName");
                        if (((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).Text == ""
                          || ((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).Text == string.Empty)
                        {
                            ddl1.DataSource = ds1.Tables[0];
                            ddl1.DataValueField = "AgencyId";
                            ddl1.DataTextField = "AgencyName";
                            ddl1.DataBind();
                            //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));/
                        }
                        TextBox txt2 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[2].FindControl("txtSpareBillNo");
                        TextBox txt3 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[3].FindControl("txtSpareBillDate");
                        TextBox txt4 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[4].FindControl("txtSpareEMRIpc");
                        TextBox txt5 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[5].FindControl("txtSparePartCode");
                        DropDownList ddlSpareItem = (DropDownList)grdvwSPBillDetails.Rows[i].Cells[6].FindControl("ddlSpareItemDesc");
                        TextBox txt7 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[7].FindControl("txtSpareQuant");
                        TextBox txt8 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[8].FindControl("txtSpareBillAmount");

                        dtCurrentTable.Rows[i]["ColSpVendorName"] = ((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem.Text;
                        dtCurrentTable.Rows[i]["ColSpBillNo"] = txt2.Text;

                        dtCurrentTable.Rows[i]["ColSpBillDate"] = txt3.Text;
                        dtCurrentTable.Rows[i]["ColSpEMRIPartCode"] = txt4.Text;
                        dtCurrentTable.Rows[i]["ColSpPartCode"] = txt5.Text;
                        dtCurrentTable.Rows[i]["ColSpItemDescription"] = ddlSpareItem.SelectedItem.Text;
                        dtCurrentTable.Rows[i]["ColSpQuantity"] = txt7.Text;
                        dtCurrentTable.Rows[i]["Column3"] = txt8.Text;
                        //Adding first three columns default value in the new row


                        if (i == (dtCurrentTable.Rows.Count - 2))
                        {
                            DropDownList ddl2 = (DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName");
                            if (((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).Text == ""
                              || ((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).Text == string.Empty)
                            {
                                ddl2.DataSource = ds1.Tables[0];
                                ddl2.DataValueField = "AgencyId";
                                ddl2.DataTextField = "AgencyName";
                                ddl2.DataBind();
                                //ddl2.Items.Insert(0, new ListItem("--Select--", "0"));
                            }
                            //dtCurrentTable.Rows[i+1]["ColSpVendorName"] = ((DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem.Text;
                            dtCurrentTable.Rows[i + 1]["ColSpBillNo"] = txt2.Text;

                            dtCurrentTable.Rows[i + 1]["ColSpBillDate"] = txt3.Text;
                        }

                    }

                    //Rebind the Grid with the current data
                    grdvwSPBillDetails.DataSource = dtCurrentTable;
                    grdvwSPBillDetails.DataBind();
                    pnlBillSummaryDetails.Visible = false;
                    btnSave.Enabled = false;
                    txtTotalBillAmt.Text = "";
                }
                ////AddRowToGridSummary();
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousDataSP();
        }

        bool isedit = false;

        private void DisplaySPBillDetails()
        {
            DataTable dtspbilldet = (DataTable)ViewState["SPBillDetails"];
            DataTable dt = new DataTable();

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpVendorName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpBillDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpEMRIPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColSpItemDescription", typeof(string)));

            dt.Columns.Add(new DataColumn("ColSpQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            //Add Data on Load
            for (int i = 0; i < dtspbilldet.Rows.Count; i++)
            {
                DataRow dr = null;
                dr = dt.NewRow();
                dr["RowNumber"] = (i + 1);
                dt.Rows.Add(dr);

                dt.Rows[i]["ColSpBillNo"] = dtspbilldet.Rows[i]["ColSpBillNo"].ToString();
                dt.Rows[i]["ColSpBillDate"] = dtspbilldet.Rows[i]["ColSpBillDate"].ToString();
                dt.Rows[i]["ColSpVendorName"] = dtspbilldet.Rows[i]["ColSpVendorName"].ToString();
                dt.Rows[i]["ColSpEMRIPartCode"] = dtspbilldet.Rows[i]["ColSpEMRIPartCode"].ToString();
                dt.Rows[i]["ColSpPartCode"] = dtspbilldet.Rows[i]["ColSpPartCode"].ToString();
                dt.Rows[i]["ColSpItemDescription"] = dtspbilldet.Rows[i]["ColSpItemDescription"].ToString();
                dt.Rows[i]["ColSpQuantity"] = dtspbilldet.Rows[i]["ColSpQuantity"].ToString();
                dt.Rows[i]["Column3"] = dtspbilldet.Rows[i]["Column3"].ToString();
            }

            isedit = true;
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            //Bind the DataTable to the Grid
            grdvwSPBillDetails.DataSource = dt;
            grdvwSPBillDetails.DataBind();
        }

        private void SetPreviousDataSP()
        {
            int rowIndex = 0; string[] args = new string[0];
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    DataSet ds = (DataSet)ViewState["Categories"];
                    DataSet ds1 = (DataSet)ViewState["Vendor"];
                    DataSet ds2 = (DataSet)ViewState["SpareItemDesc"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Set the Previous Selected Items on Each DropDownList on Postbacks
                        DropDownList ddl1 = new DropDownList();

                        ddl1 = (DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareVendorName");
                        ddl1.DataSource = ds1.Tables[0];
                        ddl1.DataValueField = "AgencyId";
                        ddl1.DataTextField = "AgencyName";
                        ddl1.DataBind();
                        //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));
                        DropDownList ddl2 = new DropDownList();

                        ddl2 = (DropDownList)grdvwSPBillDetails.Rows[i].Cells[1].FindControl("ddlSpareItemDesc");
                        ddl2.DataSource = ds2.Tables[0];
                        ddl2.DataValueField = "ManufacturerSpare_Id";
                        ddl2.DataTextField = "SparePart_Name";
                        ddl2.DataBind();


                        TextBox txt2 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[2].FindControl("txtSpareBillNo");
                        TextBox txt3 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[3].FindControl("txtSpareBillDate");
                        TextBox txt4 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[4].FindControl("txtSpareEMRIpc");
                        TextBox txt5 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[5].FindControl("txtSparePartCode");
                        //TextBox txt6 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[6].FindControl("txtSpareItemDesc");
                        TextBox txt7 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[7].FindControl("txtSpareQuant");
                        TextBox txt8 = (TextBox)grdvwSPBillDetails.Rows[i].Cells[8].FindControl("txtSpareBillAmount");

                        if (i < dt.Rows.Count - 1)
                        {
                            DataView dvVendorName = new DataView(ds1.Tables[0]);
                            dvVendorName.RowFilter = "AgencyName = '" + dt.Rows[i][1].ToString() + "'";
                            string VendorId = dvVendorName.ToTable().Rows[0]["AgencyId"].ToString();
                            ddl1.SelectedValue = VendorId;
                            //ddl1.SelectedItem.Text = Convert.ToString(dt.Rows[i][1]);
                            txt2.Text = dt.Rows[i]["ColSpBillNo"].ToString();
                            txt3.Text = dt.Rows[i]["ColSpBillDate"].ToString();
                            txt4.Text = dt.Rows[i]["ColSpEMRIPartCode"].ToString();
                            txt5.Text = dt.Rows[i]["ColSpPartCode"].ToString();
                            //txt6.Text = dt.Rows[i]["ColSpItemDescription"].ToString();
                            DataView dvItemDesc = new DataView(ds2.Tables[0]);
                            dvItemDesc.RowFilter = "SparePart_Name = '" + dt.Rows[i][6].ToString() + "'";
                            string ItemId = dvItemDesc.ToTable().Rows[0]["ManufacturerSpare_Id"].ToString();
                            ddl2.SelectedValue = ItemId;
                            //ddl2.SelectedItem.Text = Convert.ToString(dt.Rows[i][6]);
                            txt7.Text = dt.Rows[i]["ColSpQuantity"].ToString();

                            txt8.Text = dt.Rows[i]["Column3"].ToString();
                        }

                        rowIndex++;
                    }

                }
            }

        }

        protected void btnAddNewSPRow_Click(object sender, EventArgs e)
        {
            //if ()
            AddNewRowToGridSP();
        }

        protected void btnSPReset_Click(object sender, EventArgs e)
        {
            SetInitialRowSP();
        }

        private void SetInitialRowLubricant()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumberLubri", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriVendorName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriBillDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriEMRIPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriItemDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriBillAmount", typeof(string)));

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dr["RowNumberLubri"] = 1;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["LubriCurrentTable"] = dt;
            //Bind the DataTable to the Grid
            grdvwLubricantBillDetails.DataSource = dt;
            grdvwLubricantBillDetails.DataBind();
            pnlBillSummaryDetails.Visible = false;
            btnSave.Enabled = false;
            txtTotalBillAmt.Text = "";
            ////AddRowToGridSummary();
        }

        private void AddNewRowToGridLubricant()
        {
            if (ViewState["LubriCurrentTable"] != null)
            {
                DataTable dtCurrentTableLubri = (DataTable)ViewState["LubriCurrentTable"];
                DataRow drCurrentRowLubri = null;
                bool flag = false;

                if (dtCurrentTableLubri.Rows.Count > 0)
                {
                    drCurrentRowLubri = dtCurrentTableLubri.NewRow();
                    drCurrentRowLubri["RowNumberLubri"] = dtCurrentTableLubri.Rows.Count + 1;

                    for (int i = 0; i < dtCurrentTableLubri.Rows.Count; i++)
                    {
                        fmsVas.LubricantVendorName = ((DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem.Text;
                        fmsVas.LubricantBillNo = ((TextBox)grdvwLubricantBillDetails.Rows[i].Cells[2].FindControl("txtLubricantBillNo")).Text;
                        DataSet dsLubriValidation = new DataSet();
                        dsLubriValidation = fmsVas.LubriValidation();
                        if (dsLubriValidation.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            Show(" Already Vendor Name :   " + ((DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem.Text + "    and Bill Number:   " + ((TextBox)grdvwLubricantBillDetails.Rows[i].Cells[2].FindControl("txtLubricantBillNo")).Text + "   Exists in DataBase ");
                            flag = true;
                            break;

                        }
                        if (flag)
                        {
                            return;
                        }
                    }
                    if (flag)
                    {
                        return;
                    }


                    //add new row to DataTable
                    dtCurrentTableLubri.Rows.Add(drCurrentRowLubri);
                    //Store the current data to ViewState
                    ViewState["LubriCurrentTable"] = dtCurrentTableLubri;
                    DataSet ds = (DataSet)ViewState["Categories"];
                    DataSet ds1 = (DataSet)ViewState["Vendor"];

                    for (int i = 0; i < dtCurrentTableLubri.Rows.Count - 1; i++)
                    {
                        //extract the DropDownList Selected Items
                        DropDownList ddl1 = (DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName");
                        if (((DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName")).Text == ""
                           || ((DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName")).Text == string.Empty)
                        {
                            ddl1.DataSource = ds1.Tables[0];
                            ddl1.DataValueField = "AgencyId";
                            ddl1.DataTextField = "AgencyName";
                            ddl1.DataBind();
                            //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));
                        }

                        TextBox txt2 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[2].FindControl("txtLubricantBillNo");
                        TextBox txt3 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[3].FindControl("txtLubricantBillDate");
                        TextBox txt4 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[4].FindControl("txtLubricantEMRIpc");
                        TextBox txt5 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[5].FindControl("txtLubricantPartCode");
                        TextBox txt6 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[6].FindControl("txtLubricantItemDesc");
                        TextBox txt7 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[7].FindControl("txtLubricantQuant");
                        TextBox txt8 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[8].FindControl("txtLubricantBillAmount");

                        dtCurrentTableLubri.Rows[i]["ColLubriVendorName"] = ((DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem.Text;
                        dtCurrentTableLubri.Rows[i]["ColLubriBillNo"] = txt2.Text;
                        dtCurrentTableLubri.Rows[i]["ColLubriBillDate"] = txt3.Text;
                        dtCurrentTableLubri.Rows[i]["ColLubriEMRIPartCode"] = txt4.Text;
                        dtCurrentTableLubri.Rows[i]["ColLubriPartCode"] = txt5.Text;
                        dtCurrentTableLubri.Rows[i]["ColLubriItemDescription"] = txt6.Text;
                        dtCurrentTableLubri.Rows[i]["ColLabQuantity"] = txt7.Text;
                        dtCurrentTableLubri.Rows[i]["ColLubriBillAmount"] = txt8.Text;
                        if (i == (dtCurrentTableLubri.Rows.Count - 2))
                        {
                            dtCurrentTableLubri.Rows[i + 1]["ColLubriVendorName"] = ((DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem.Text;
                            dtCurrentTableLubri.Rows[i + 1]["ColLubriBillNo"] = txt2.Text;
                            dtCurrentTableLubri.Rows[i + 1]["ColLubriBillDate"] = txt3.Text;
                        }

                    }

                    //Rebind the Grid with the current data
                    grdvwLubricantBillDetails.DataSource = dtCurrentTableLubri;
                    grdvwLubricantBillDetails.DataBind();
                    pnlBillSummaryDetails.Visible = false;
                    btnSave.Enabled = false;
                    txtTotalBillAmt.Text = "";
                }
                ////AddRowToGridSummary();
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataLubri();
        }

        private void DisplayLubriBillDetails()
        {
            DataTable dtlubribilldet = (DataTable)ViewState["LubriBillDetails"];
            DataTable dt = new DataTable();

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumberLubri", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriBillDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriBillAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriVendorName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriEMRIPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriPartCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLubriItemDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabQuantity", typeof(string)));
            //Add Data on Load
            for (int i = 0; i < dtlubribilldet.Rows.Count; i++)
            {
                DataRow dr = null;
                dr = dt.NewRow();
                dr["RowNumberLubri"] = (i + 1);
                dt.Rows.Add(dr);

                dt.Rows[i]["ColLubriBillNo"] = dtlubribilldet.Rows[i]["ColLubriBillNo"].ToString();
                dt.Rows[i]["ColLubriBillDate"] = dtlubribilldet.Rows[i]["ColLubriBillDate"].ToString();
                dt.Rows[i]["ColLubriBillAmount"] = dtlubribilldet.Rows[i]["ColLubriBillAmount"].ToString();
                dt.Rows[i]["ColLubriVendorName"] = dtlubribilldet.Rows[i]["ColLubriVendorName"].ToString();
                dt.Rows[i]["ColLubriEMRIPartCode"] = dtlubribilldet.Rows[i]["ColLubriEMRIPartCode"].ToString();
                dt.Rows[i]["ColLubriPartCode"] = dtlubribilldet.Rows[i]["ColLubriPartCode"].ToString();
                dt.Rows[i]["ColLubriItemDescription"] = dtlubribilldet.Rows[i]["ColLubriItemDescription"].ToString();
                dt.Rows[i]["ColLabQuantity"] = dtlubribilldet.Rows[i]["ColLabQuantity"].ToString();
            }
            isedit = true;
            //Store the DataTable in ViewState
            ViewState["LubriCurrentTable"] = dt;
            //Bind the DataTable to the Grid
            grdvwLubricantBillDetails.DataSource = dt;
            grdvwLubricantBillDetails.DataBind();
        }

        private void SetPreviousDataLubri()
        {

            int rowIndex = 0; string[] args = new string[0];
            if (ViewState["LubriCurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["LubriCurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    DataSet ds = (DataSet)ViewState["Categories"];
                    DataSet ds1 = (DataSet)ViewState["Vendor"];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Set the Previous Selected Items on Each DropDownList on Postbacks
                        DropDownList ddl1 = new DropDownList();
                        ddl1 = (DropDownList)grdvwLubricantBillDetails.Rows[i].Cells[1].FindControl("ddlLubricantVendorName");
                        ddl1.DataSource = ds1.Tables[0];
                        ddl1.DataValueField = "AgencyId";
                        ddl1.DataTextField = "AgencyName";
                        ddl1.DataBind();
                        //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));
                        TextBox txt2 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[2].FindControl("txtLubricantBillNo");
                        TextBox txt3 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[3].FindControl("txtLubricantBillDate");
                        TextBox txt4 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[4].FindControl("txtLubricantEMRIpc");
                        TextBox txt5 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[5].FindControl("txtLubricantPartCode");
                        TextBox txt6 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[6].FindControl("txtLubricantItemDesc");
                        TextBox txt7 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[7].FindControl("txtLubricantQuant");
                        TextBox txt8 = (TextBox)grdvwLubricantBillDetails.Rows[i].Cells[8].FindControl("txtLubricantBillAmount");

                        if (i < dt.Rows.Count - 1)
                        {
                            DataView dvVendorName = new DataView(ds1.Tables[0]);
                            dvVendorName.RowFilter = "AgencyName = '" + dt.Rows[i][1].ToString() + "'";
                            string VendorId = dvVendorName.ToTable().Rows[0]["AgencyId"].ToString();
                            ddl1.SelectedValue = VendorId;
                            //ddl1.SelectedItem.Text = Convert.ToString(dt.Rows[i][1]);
                            txt2.Text = dt.Rows[i]["ColLubriBillNo"].ToString();
                            txt3.Text = dt.Rows[i]["ColLubriBillDate"].ToString();
                            txt4.Text = dt.Rows[i]["ColLubriEMRIPartCode"].ToString();
                            txt5.Text = dt.Rows[i]["ColLubriPartCode"].ToString();
                            txt6.Text = dt.Rows[i]["ColLubriItemDescription"].ToString();
                            txt7.Text = dt.Rows[i]["ColLabQuantity"].ToString();
                            txt8.Text = dt.Rows[i]["ColLubriBillAmount"].ToString();
                        }

                        rowIndex++;
                    }

                }
            }


        }

        protected void btnAddNewLubriRow_Click(object sender, EventArgs e)
        {
            AddNewRowToGridLubricant();
        }

        protected void btnLubriReset_Click(object sender, EventArgs e)
        {
            SetInitialRowLubricant();
        }

        private void SetInitialRowLabour()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //bool flag = true;

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumberLabour", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabVendorName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabBillDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Aggregates", typeof(string)));
            dt.Columns.Add(new DataColumn("Categories", typeof(string)));
            dt.Columns.Add(new DataColumn("Sub_Categories", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabItemDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));


            DataSet dslabourFromDB = (DataSet)ViewState["Categories"];
            if (dslabourFromDB != null)
            {
                int count = 0;

                if (dslabourFromDB.Tables.Count > 0)
                {
                    if (dslabourFromDB.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drLabour in dslabourFromDB.Tables[0].Rows)
                        {
                            dr = dt.NewRow();
                            dr["Aggregates"] = Convert.ToString(drLabour["Aggregates"]);
                            dr["Categories"] = Convert.ToString(drLabour["Categories"]);
                            dr["Sub_Categories"] = Convert.ToString(drLabour["Sub_Categories"]);
                            dr["Column3"] = Convert.ToString(drLabour["Column3"]);
                            dr["RowNumberLabour"] = ++count;
                            dt.Rows.Add(dr);
                        }
                        ViewState["LabourCurrentTable"] = dt;
                        ViewState["IsGridSet"] = 1;
                        for (int i = 0; i < grdvwLabourBillDetails.Rows.Count; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                DataSet dslabourAggregatesnew = (DataSet)ViewState["LabourAggregates"];
                                DataSet dsLabourCategoriesnew = (DataSet)ViewState["LabourCategories"];
                                DataSet dsLabourSubCategories = (DataSet)ViewState["LabourSubCategories"];
                                string aggid;
                                string catid;
                                string subid;
                                ComboBox ddl4 = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourAggregates") as ComboBox;
                                ddl4.DataSource = dslabourAggregatesnew.Tables[0];
                                ddl4.DataValueField = "Aggregate_Id";
                                ddl4.DataTextField = "Aggregates";
                                ddl4.DataBind();

                                DataView dvagg = new DataView(dslabourAggregatesnew.Tables[0]);
                                dvagg.RowFilter = "Aggregates = '" + dt.Rows[i][4].ToString() + "'";
                                aggid = dvagg.ToTable().Rows[0]["Aggregate_Id"].ToString();
                                ddl4.SelectedValue = aggid;

                                ComboBox ddl5 = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourCategories") as ComboBox;
                                ddl5.DataSource = dsLabourCategoriesnew.Tables[0];
                                ddl5.DataValueField = "Category_Id";
                                ddl5.DataTextField = "Categories";
                                ddl5.DataBind();
                                DataView dvcat = new DataView(dsLabourCategoriesnew.Tables[0]);
                                dvcat.RowFilter = "Categories = '" + dt.Rows[i][5].ToString() + "'";
                                catid = dvcat.ToTable().Rows[0]["Category_Id"].ToString();
                                ddl5.SelectedValue = catid;

                                ComboBox ddl6 = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourSubCategories") as ComboBox;
                                ddl6.DataSource = dsLabourSubCategories.Tables[0];
                                ddl6.DataValueField = "SubCategory_Id";
                                ddl6.DataTextField = "SubCategories";
                                ddl6.DataBind();
                                DataView dvSub = new DataView(dsLabourSubCategories.Tables[0]);
                                dvSub.RowFilter = "SubCategories = '" + dt.Rows[i][6].ToString() + "'";
                                subid = dvSub.ToTable().Rows[0]["SubCategory_Id"].ToString();
                                ddl6.SelectedValue = subid;
                            }
                        }
                        grdvwLabourBillDetails.DataSource = dt;
                        grdvwLabourBillDetails.DataBind();
                        int count1 = 0;
                        foreach (GridViewRow item in grdvwLabourBillDetails.Rows)
                        {
                            if ((dt.Rows[count1]["Aggregates"].ToString() != null) && (dt.Rows[count1]["Aggregates"].ToString() != string.Empty))
                                (item.FindControl("ddlLabourAggregates") as ComboBox).Items.FindByText(dt.Rows[count1]["Aggregates"].ToString()).Selected = true;
                            if ((dt.Rows[count1]["Categories"].ToString() != null) && (dt.Rows[count1]["Categories"].ToString() != string.Empty))
                                (item.FindControl("ddlLabourCategories") as ComboBox).Items.FindByText(dt.Rows[count1]["Categories"].ToString()).Selected = true;
                            if ((dt.Rows[count1]["Sub_Categories"].ToString() != null) && (dt.Rows[count1]["Sub_Categories"].ToString() != string.Empty))
                                (item.FindControl("ddlLabourSubCategories") as ComboBox).Items.FindByText(dt.Rows[count1]["Sub_Categories"].ToString()).Selected = true;

                            count1 += 1;
                        }
                    }
                    else
                    {
                        //Add a Dummy Data on Initial Load
                        dr = dt.NewRow();
                        dr["RowNumberLabour"] = 1;
                        dt.Rows.Add(dr);

                        //Store the DataTable in ViewState
                        ViewState["LabourCurrentTable"] = dt;

                        if ((IsPostBack) && (dt.Rows.Count == 1))
                        {
                            grdvwLabourBillDetails.DataSource = dt;
                            grdvwLabourBillDetails.DataBind();
                            pnlBillSummaryDetails.Visible = false;
                            btnSave.Enabled = false;
                            txtTotalBillAmt.Text = "";
                        }
                    }
                }
            }
            else
            {
                //Add a Dummy Data on Initial Load
                dr = dt.NewRow();
                dr["RowNumberLabour"] = 1;
                dt.Rows.Add(dr);
                grdvwLabourBillDetails.DataSource = dt;
                grdvwLabourBillDetails.DataBind();
                pnlBillSummaryDetails.Visible = false;
                btnSave.Enabled = false;
                txtTotalBillAmt.Text = "";
                //Store the DataTable in ViewState
                ViewState["LabourCurrentTable"] = dt;
            }

            if ((IsPostBack == false) && (dt != null))
            {
                //Bind the DataTable to the Grid
                grdvwLabourBillDetails.DataSource = dt;
                grdvwLabourBillDetails.DataBind();
                pnlBillSummaryDetails.Visible = false;
                btnSave.Enabled = false;
                txtTotalBillAmt.Text = "";
            }
            ////AddRowToGridSummary();
        }

        private void AddNewRowToGridLabour()
        {
            if (ViewState["LabourCurrentTable"] != null)
            {
                DataTable dtCurrentTableLabour = (DataTable)ViewState["LabourCurrentTable"];
                DataRow drCurrentRowLabour = null;

                if (dtCurrentTableLabour.Rows.Count > 0)
                {
                    drCurrentRowLabour = dtCurrentTableLabour.NewRow();
                    drCurrentRowLabour["RowNumberLabour"] = dtCurrentTableLabour.Rows.Count + 1;
                    bool flag = false;
                    for (int i = 0; i < dtCurrentTableLabour.Rows.Count; i++)
                    {
                        fmsVas.LabourVendorName = ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName")).SelectedItem.Text;
                        fmsVas.LabourBillNo = ((TextBox)grdvwLabourBillDetails.Rows[i].Cells[2].FindControl("txtLabourBillNo")).Text;
                        DataSet dsLabValidation = new DataSet();
                        dsLabValidation = fmsVas.LabValidation();
                        if (dsLabValidation.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            Show(" Already Vendor Name :    " + ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName")).SelectedItem.Text + "and Bill Number :    " + ((TextBox)grdvwLabourBillDetails.Rows[i].Cells[2].FindControl("txtLabourBillNo")).Text + " Exists in DataBase ");
                            flag = true;
                            break;

                        }

                    }
                    if (flag)
                    {
                        return;
                    }

                    //add new row to DataTable
                    dtCurrentTableLabour.Rows.Add(drCurrentRowLabour);
                    //Store the current data to ViewState
                    ViewState["LabourCurrentTable"] = dtCurrentTableLabour;

                    DataSet ds = (DataSet)ViewState["Categories"];
                    DataSet ds1 = (DataSet)ViewState["Vendor"];
                    DataSet dslabourAggregatesnew = (DataSet)ViewState["LabourAggregates"];
                    DataSet dsLabourCategoriesnew = (DataSet)ViewState["LabourCategories"];
                    DataSet dsLabourSubCategories = (DataSet)ViewState["LabourSubCategories"];

                    for (int i = 0; i < dtCurrentTableLabour.Rows.Count - 1; i++)
                    {
                        TextBox txt2 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[2].FindControl("txtLabourBillNo");
                        TextBox txt3 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[3].FindControl("txtLabourBillDate");
                        ComboBox ddl4 = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[4].FindControl("ddlLabourAggregates");
                        //ddl4.Items.Clear();
                        //ddl4.DataSource = dslabourAggregatesnew.Tables[0];
                        //ddl4.DataValueField = "Aggregate_Id";
                        //ddl4.DataTextField = "Aggregates";
                        //ddl4.DataBind();

                        if (ds.Tables[0].Rows.Count > i)
                        {
                            ddl4.SelectedItem.Text = ds.Tables[0].Rows[i][4].ToString();
                        }
                        else
                        {
                            ddl4.SelectedItem.Text = ((ComboBox)grdvwLabourBillDetails.Rows[i].Cells[4].FindControl("ddlLabourAggregates")).SelectedItem.Text;
                        }
                        ComboBox ddl5 = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[5].FindControl("ddlLabourCategories");
                        //ddl5.Items.Clear();
                        if (ds.Tables[0].Rows.Count > i)
                            ddl5.SelectedItem.Text = ds.Tables[0].Rows[i][5].ToString();
                        else
                        {
                            ddl5.SelectedItem.Text = ((ComboBox)grdvwLabourBillDetails.Rows[i].Cells[5].FindControl("ddlLabourCategories")).SelectedItem.Text;
                        }
                        ComboBox ddl6 = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[6].FindControl("ddlLabourSubCategories");
                        //ddl6.Items.Clear();
                        if (ds.Tables[0].Rows.Count > i)
                            ddl6.SelectedItem.Text = ds.Tables[0].Rows[i][6].ToString();
                        else
                        {
                            ddl6.SelectedItem.Text = ((ComboBox)grdvwLabourBillDetails.Rows[i].Cells[6].FindControl("ddlLabourSubCategories")).SelectedItem.Text;
                        }
                        TextBox txt7 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[7].FindControl("txtLabourItemDesc");
                        TextBox txt8 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[8].FindControl("txtLabourQuant");
                        TextBox txt9 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[9].FindControl("txtLabourBillAmount");

                        dtCurrentTableLabour.Rows[i]["ColLabVendorName"] = ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName")).SelectedItem.Text;
                        dtCurrentTableLabour.Rows[i]["ColLabBillNo"] = txt2.Text;
                        dtCurrentTableLabour.Rows[i]["ColLabBillDate"] = txt3.Text;
                        dtCurrentTableLabour.Rows[i]["Aggregates"] = ((ComboBox)grdvwLabourBillDetails.Rows[i].Cells[4].FindControl("ddlLabourAggregates")).SelectedItem.Text;
                        dtCurrentTableLabour.Rows[i]["Categories"] = ((ComboBox)grdvwLabourBillDetails.Rows[i].Cells[5].FindControl("ddlLabourCategories")).SelectedItem.Text;
                        dtCurrentTableLabour.Rows[i]["Sub_Categories"] = ((ComboBox)grdvwLabourBillDetails.Rows[i].Cells[6].FindControl("ddlLabourSubCategories")).SelectedItem.Text;
                        dtCurrentTableLabour.Rows[i]["ColLabItemDescription"] = txt7.Text;
                        dtCurrentTableLabour.Rows[i]["ColLabQuantity"] = txt8.Text;
                        dtCurrentTableLabour.Rows[i]["Column3"] = txt9.Text;
                        if (i == dtCurrentTableLabour.Rows.Count - 2)
                        {
                            dtCurrentTableLabour.Rows[i + 1]["ColLabBillNo"] = txt2.Text;
                            dtCurrentTableLabour.Rows[i + 1]["ColLabBillDate"] = txt3.Text;
                        }

                        //extract the DropDownList Selected Items
                        DropDownList ddl1 = (DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName");
                        if (((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName")).Text == ""
                            || ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName")).Text == string.Empty)
                        {
                            ddl1.DataSource = ds1.Tables[0];
                            ddl1.DataValueField = "AgencyId";
                            ddl1.DataTextField = "AgencyName";
                            ddl1.DataBind();
                            //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));
                        }

                        ComboBox ddlLabourAggregates = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourAggregates");

                        ddlLabourAggregates.DataSource = dslabourAggregatesnew.Tables[0];
                        ddlLabourAggregates.DataValueField = "Aggregate_Id";
                        ddlLabourAggregates.DataTextField = "Aggregates";
                        ddlLabourAggregates.DataBind();

                        ComboBox ddlLabourCategories = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourCategories");

                        ddlLabourCategories.DataSource = dsLabourCategoriesnew.Tables[0];
                        ddlLabourCategories.DataValueField = "Category_Id";
                        ddlLabourCategories.DataTextField = "Categories";
                        ddlLabourCategories.DataBind();

                        ComboBox ddlLabourSubCategories = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourSubCategories");

                        ddlLabourSubCategories.DataSource = dsLabourSubCategories.Tables[0];
                        ddlLabourSubCategories.DataValueField = "SubCategory_Id";
                        ddlLabourSubCategories.DataTextField = "SubCategories";
                        ddlLabourSubCategories.DataBind();

                    }

                    //Rebind the Grid with the current data
                    ViewState["LabourCurrentTable"] = dtCurrentTableLabour;
                    grdvwLabourBillDetails.DataSource = dtCurrentTableLabour;
                    grdvwLabourBillDetails.DataBind();
                    pnlBillSummaryDetails.Visible = false;
                    btnSave.Enabled = false;

                    txtTotalBillAmt.Text = "";
                    int count = 0;
                    foreach (GridViewRow item in grdvwLabourBillDetails.Rows)
                    {
                        if ((dtCurrentTableLabour.Rows[count]["Aggregates"].ToString() != null) && (dtCurrentTableLabour.Rows[count]["Aggregates"].ToString() != string.Empty))
                            //((ComboBox)grdvwLabourBillDetails.FindControl["ddlLabourAggregates"].)
                            (item.FindControl("ddlLabourAggregates") as ComboBox).Items.FindByText(dtCurrentTableLabour.Rows[count]["Aggregates"].ToString()).Selected = true;
                        if ((dtCurrentTableLabour.Rows[count]["Categories"].ToString() != null) && (dtCurrentTableLabour.Rows[count]["Categories"].ToString() != string.Empty))
                            (item.FindControl("ddlLabourCategories") as ComboBox).Items.FindByText(dtCurrentTableLabour.Rows[count]["Categories"].ToString()).Selected = true;
                        if ((dtCurrentTableLabour.Rows[count]["Sub_Categories"].ToString() != null) && (dtCurrentTableLabour.Rows[count]["Sub_Categories"].ToString() != string.Empty))
                            (item.FindControl("ddlLabourSubCategories") as ComboBox).Items.FindByText(dtCurrentTableLabour.Rows[count]["Sub_Categories"].ToString()).Selected = true;

                        count += 1;
                    }

                }
                ////AddRowToGridSummary();
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataLabour();
        }

        private void DisplayLabourBillDetails()
        {
            DataTable dtlabourbilldet = (DataTable)ViewState["LabourBillDetails"];
            DataTable dt = new DataTable();

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumberLabour", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabVendorName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabBillDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Aggregates", typeof(string)));
            dt.Columns.Add(new DataColumn("Categories", typeof(string)));
            dt.Columns.Add(new DataColumn("Sub_Categories", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabItemDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("ColLabQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));

            //Add Data on Load
            for (int i = 0; i < dtlabourbilldet.Rows.Count; i++)
            {
                DataRow dr = null;
                dr = dt.NewRow();
                dr["RowNumberLabour"] = (i + 1);
                dt.Rows.Add(dr);

                dt.Rows[i]["ColLabBillNo"] = dtlabourbilldet.Rows[i]["ColLabBillNo"].ToString();
                dt.Rows[i]["ColLabBillDate"] = dtlabourbilldet.Rows[i]["ColLabBillDate"].ToString();
                dt.Rows[i]["ColLabVendorName"] = dtlabourbilldet.Rows[i]["ColLabVendorName"].ToString();
                dt.Rows[i]["Aggregates"] = dtlabourbilldet.Rows[i]["Aggregates"].ToString();
                dt.Rows[i]["Categories"] = dtlabourbilldet.Rows[i]["Categories"].ToString();
                dt.Rows[i]["Sub_Categories"] = dtlabourbilldet.Rows[i]["Sub_Categories"].ToString();
                dt.Rows[i]["ColLabItemDescription"] = dtlabourbilldet.Rows[i]["ColLabItemDescription"].ToString();
                dt.Rows[i]["ColLabQuantity"] = dtlabourbilldet.Rows[i]["ColLabQuantity"].ToString();
                dt.Rows[i]["Column3"] = dtlabourbilldet.Rows[i]["Column3"].ToString();
            }
            isedit = true;
            //Store the DataTable in ViewState
            ViewState["LabourCurrentTable"] = dt;
            //Bind the DataTable to the Grid
            grdvwLabourBillDetails.DataSource = dt;
            grdvwLabourBillDetails.DataBind();
        }

        private void SetPreviousDataLabour()
        {
            int rowIndex = 0; string[] args = new string[0];
            if (ViewState["LabourCurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["LabourCurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    DataSet ds = (DataSet)ViewState["Categories"];
                    DataSet ds1 = (DataSet)ViewState["Vendor"];
                    DataSet dslabourAggregatesnew = (DataSet)ViewState["LabourAggregates"];
                    DataSet dsLabourCategoriesnew = (DataSet)ViewState["LabourCategories"];
                    DataSet dsLabourSubCategories = (DataSet)ViewState["LabourSubCategories"];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Set the Previous Selected Items on Each DropDownList on Postbacks
                        DropDownList ddl1 = new DropDownList();

                        ddl1 = (DropDownList)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourVendorName");
                        ddl1.DataSource = ds1.Tables[0];
                        ddl1.DataValueField = "AgencyId";
                        ddl1.DataTextField = "AgencyName";
                        ddl1.DataBind();
                        //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));

                        ComboBox ddlLabourAggregates = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourAggregates");
                        ddlLabourAggregates.DataSource = dslabourAggregatesnew.Tables[0];
                        ddlLabourAggregates.DataValueField = "Aggregate_Id";
                        ddlLabourAggregates.DataTextField = "Aggregates";
                        ddlLabourAggregates.DataBind();
                        ComboBox ddlLabourCategories = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourCategories");

                        ddlLabourCategories.DataSource = dsLabourCategoriesnew.Tables[0];
                        ddlLabourCategories.DataValueField = "Category_Id";
                        ddlLabourCategories.DataTextField = "Categories";
                        ddlLabourCategories.DataBind();

                        ComboBox ddlLabourSubCategories = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[1].FindControl("ddlLabourSubCategories");
                        ddlLabourSubCategories.DataSource = dsLabourSubCategories.Tables[0];
                        ddlLabourSubCategories.DataValueField = "SubCategory_Id";
                        ddlLabourSubCategories.DataTextField = "SubCategories";
                        ddlLabourSubCategories.DataBind();

                        TextBox txt2 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[2].FindControl("txtLabourBillNo");
                        TextBox txt3 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[3].FindControl("txtLabourBillDate");
                        ComboBox ddl4 = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[4].FindControl("ddlLabourAggregates");

                        //ddl4.Items.Clear();
                        //ddl4.DataSource = dslabourAggregatesnew.Tables[0];
                        //ddl4.DataValueField = "Aggregate_Id";
                        //ddl4.DataTextField = "Aggregates";
                        //ddl4.DataBind();
                        //foreach (ListItem ltItem in ddl4.Items)
                        //{
                        //    if (ltItem.Text == dt.Rows[0][4].ToString())
                        //    {
                        //        string id = ltItem.Value;
                        //        ddl4.SelectedValue = id;
                        //        break;
                        //    }
                        //}
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl4.SelectedIndex = 0;
                            //ddl4.SelectedItem.Text = ds.Tables[0].Rows[0][4].ToString();
                        }
                        else
                        {
                            //       ddl4.SelectedItem.Text = dt.Rows[i][4].ToString();
                        }
                        ComboBox ddl5 = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[5].FindControl("ddlLabourCategories");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl5.SelectedIndex = 0;
                            //ddl5.SelectedItem.Text = ds.Tables[0].Rows[0][5].ToString();
                        }
                        else
                        {
                            //     ddl5.SelectedItem.Text = dt.Rows[i][5].ToString();
                        }
                        ComboBox ddl6 = (ComboBox)grdvwLabourBillDetails.Rows[i].Cells[5].FindControl("ddlLabourSubCategories");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl6.SelectedIndex = 0;
                            //ddl6.SelectedItem.Text = ds.Tables[0].Rows[0][6].ToString();
                        }
                        else
                        {
                            //   ddl6.SelectedItem.Text = dt.Rows[i][6].ToString();
                        }
                        TextBox txt7 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[7].FindControl("txtLabourItemDesc");
                        TextBox txt8 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[8].FindControl("txtLabourQuant");
                        TextBox txt9 = (TextBox)grdvwLabourBillDetails.Rows[i].Cells[9].FindControl("txtLabourBillAmount");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txt9.Text = "";//ds.Tables[0].Rows[0][9].ToString();
                        }

                        if (i < dt.Rows.Count - 1)
                        {
                            DataView dvVendorName = new DataView(ds1.Tables[0]);
                            dvVendorName.RowFilter = "AgencyName = '" + dt.Rows[i][1].ToString() + "'";
                            string VendorId = dvVendorName.ToTable().Rows[0]["AgencyId"].ToString();
                            ddl1.SelectedValue = VendorId;
                            //ddl1.SelectedItem.Text = Convert.ToString(dt.Rows[i][1]);

                            txt2.Text = dt.Rows[i]["ColLabBillNo"].ToString();
                            txt3.Text = dt.Rows[i]["ColLabBillDate"].ToString();
                            DataTable dt1 = dslabourAggregatesnew.Tables[0];
                            DataView dvAggregateName = new DataView(dt1);
                            dvAggregateName.RowFilter = "Aggregates = '" + dt.Rows[i][4].ToString() + "'";
                            string AggregateId = dvAggregateName.ToTable().Rows[0]["Aggregate_Id"].ToString();
                            ddl4.Items.Clear();
                            ddl4.DataSource = dslabourAggregatesnew.Tables[0];
                            ddl4.DataValueField = "Aggregate_Id";
                            ddl4.DataTextField = "Aggregates";
                            ddl4.DataBind();
                            foreach (ListItem ltItem in ddl4.Items)
                            {
                                if (ltItem.Text == dvAggregateName.ToTable().Rows[0]["Aggregates"].ToString())
                                {
                                    ddl4.SelectedValue = AggregateId;
                                    //ddl4.SelectedItem.Text = dvAggregateName.ToTable().Rows[0]["Aggregates"].ToString();
                                    break;
                                }
                            }
                            //ddl4.SelectedValue = AggregateId;
                            //ddl4.SelectedItem.Text = ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[4].FindControl("ddlLabourAggregates")).SelectedItem.Text;

                            DataView dvCategoryName = new DataView(dsLabourCategoriesnew.Tables[0]);
                            dvCategoryName.RowFilter = "Categories = '" + dt.Rows[i][5].ToString() + "'";
                            string CategoryId = dvCategoryName.ToTable().Rows[0]["Category_Id"].ToString();
                            ddl5.SelectedValue = CategoryId;

                            //ddl5.SelectedItem.Text = ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[5].FindControl("ddlLabourCategories")).SelectedItem.Text;

                            txt7.Text = dt.Rows[i]["ColLabItemDescription"].ToString();
                            txt8.Text = dt.Rows[i]["ColLabQuantity"].ToString();

                            DataView dvSubCategoryName = new DataView(dsLabourSubCategories.Tables[0]);
                            dvSubCategoryName.RowFilter = "SubCategories = '" + dt.Rows[i][6].ToString() + "'";
                            string SubCategoryId = dvSubCategoryName.ToTable().Rows[0]["SubCategory_Id"].ToString();
                            ddl6.SelectedValue = SubCategoryId;

                            //ddl6.SelectedItem.Text = ((DropDownList)grdvwLabourBillDetails.Rows[i].Cells[6].FindControl("ddlLabourSubCategories")).SelectedItem.Text;

                            txt9.Text = dt.Rows[i]["Column3"].ToString();

                        }

                        rowIndex++;
                    }

                }
            }
        }

        protected void btnAddNewLabourRow_Click(object sender, EventArgs e)
        {
            AddNewRowToGridLabour();
        }

        protected void btnLabourReset_Click(object sender, EventArgs e)
        {
            SetInitialRowLabour();
        }

        private void AddRowToGridSummary()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //Define the Columns
            dt.Columns.Add(new DataColumn("TypeBillDetails", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalBillNumbers", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalBillAmount", typeof(string)));

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dr["TypeBillDetails"] = "Spare Parts";

            double totalamtsp = 0;
            for (int i = 0; i < grdvwSPBillDetails.Rows.Count; i++)
            {
                TextBox txtbxspamt = grdvwSPBillDetails.Rows[i].FindControl("txtSpareBillAmount") as TextBox;
                if (txtbxspamt.Text != "")
                    totalamtsp = totalamtsp + Convert.ToDouble(txtbxspamt.Text);
            }
            dr["TotalBillNumbers"] = (totalamtsp == 0 ? 0 : grdvwSPBillDetails.Rows.Count);
            dr["TotalBillAmount"] = totalamtsp;
            dt.Rows.Add(dr);

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dr["TypeBillDetails"] = "Lubricant";
            double totalamtlubri = 0;
            for (int i = 0; i < grdvwLubricantBillDetails.Rows.Count; i++)
            {
                TextBox txtbxlubriamt = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantBillAmount") as TextBox;
                if (txtbxlubriamt.Text != "")
                    totalamtlubri = totalamtlubri + Convert.ToDouble(txtbxlubriamt.Text);
            }
            dr["TotalBillNumbers"] = (totalamtlubri == 0 ? 0 : grdvwLubricantBillDetails.Rows.Count);
            dr["TotalBillAmount"] = totalamtlubri;
            dt.Rows.Add(dr);

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dr["TypeBillDetails"] = "Labour Charges";

            double totalamtlabour = 0;
            for (int i = 0; i < grdvwLabourBillDetails.Rows.Count; i++)
            {
                TextBox txtbxlabour = grdvwLabourBillDetails.Rows[i].FindControl("txtLabourBillAmount") as TextBox;
                if (txtbxlabour.Text != "")
                    totalamtlabour = totalamtlabour + Convert.ToDouble(txtbxlabour.Text);
            }
            dr["TotalBillNumbers"] = (totalamtlabour == 0 ? 0 : grdvwLabourBillDetails.Rows.Count);
            dr["TotalBillAmount"] = totalamtlabour;
            dt.Rows.Add(dr);

            //Bind the DataTable to the Grid
            grdvwBillDetailsSummary.DataSource = dt;
            grdvwBillDetailsSummary.DataBind();
            txtTotalBillAmt.Text = (totalamtsp + totalamtlubri + totalamtlabour).ToString();
        }

        protected void btnBillDetailsSummary_Click(object sender, EventArgs e)
        {
            if (chkbxlistBillType.Items[0].Selected)
            {
                fmsVas.SpareVendorName = ((DropDownList)grdvwSPBillDetails.Rows[0].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem.Text;
                fmsVas.SpareBillNo = ((TextBox)grdvwSPBillDetails.Rows[0].Cells[2].FindControl("txtSpareBillNo")).Text;
                DataSet dsSpareValidation = new DataSet();
                dsSpareValidation = fmsVas.SpareValidation();
                if (dsSpareValidation.Tables[0].Rows[0][0].ToString() == "1")
                {
                    Show(" Already Vendor Name :  " + ((DropDownList)grdvwSPBillDetails.Rows[0].Cells[1].FindControl("ddlSpareVendorName")).SelectedItem.Text + "    and Bill Number :   " + ((TextBox)grdvwSPBillDetails.Rows[0].Cells[2].FindControl("txtSpareBillNo")).Text + "   Exists in DataBase ");
                    return;
                }
            }
            if (chkbxlistBillType.Items[1].Selected)
            {
                fmsVas.LubricantVendorName = ((DropDownList)grdvwLubricantBillDetails.Rows[0].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem.Text;
                fmsVas.LubricantBillNo = ((TextBox)grdvwLubricantBillDetails.Rows[0].Cells[2].FindControl("txtLubricantBillNo")).Text;
                DataSet dsLubriValidation = new DataSet();
                dsLubriValidation = fmsVas.LubriValidation();
                if (dsLubriValidation.Tables[0].Rows[0][0].ToString() == "1")
                {
                    Show(" Already Vendor Name :   " + ((DropDownList)grdvwLubricantBillDetails.Rows[0].Cells[1].FindControl("ddlLubricantVendorName")).SelectedItem.Text + "    and Bill Number:   " + ((TextBox)grdvwLubricantBillDetails.Rows[0].Cells[2].FindControl("txtLubricantBillNo")).Text + "   Exists in DataBase ");
                    return;
                }
            }
            if (chkbxlistBillType.Items[2].Selected)
            {
                fmsVas.LabourVendorName = ((DropDownList)grdvwLabourBillDetails.Rows[0].Cells[1].FindControl("ddlLabourVendorName")).SelectedItem.Text;
                fmsVas.LabourBillNo = ((TextBox)grdvwLabourBillDetails.Rows[0].Cells[2].FindControl("txtLabourBillNo")).Text;
                DataSet dsLabValidation = new DataSet();
                dsLabValidation = fmsVas.LabValidation();
                if (dsLabValidation.Tables[0].Rows[0][0].ToString() == "1")
                {
                    Show(" Already Vendor Name :    " + ((DropDownList)grdvwLabourBillDetails.Rows[0].Cells[1].FindControl("ddlLabourVendorName")).SelectedItem.Text + "and Bill Number :    " + ((TextBox)grdvwLabourBillDetails.Rows[0].Cells[2].FindControl("txtLabourBillNo")).Text + " Exists in DataBase ");
                    return;

                }
            }
            if (ValidateSPGrid() && ValidateLubriGrid() && ValidateLabourGrid())
            {
                pnlBillSummaryDetails.Visible = true;
                AddRowToGridSummary();
                btnSave.Enabled = true;
            }
        }

        private bool Checkboxcount = false;

        protected void chkbxlistBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            isedit = false;
            string VehicleNo;
            pnlSPBillDetails.Visible = false;
            pnlLubricantBillDetails.Visible = false;
            pnlLabourBillDetails.Visible = false;
            pnlBillSummaryDetails.Visible = false;
            pnlBillDetailsSummaryBtn.Visible = false;


            if (chkbxlistBillType.Items[0].Selected)
            {
                pnlSPBillDetails.Visible = true;
                DropDownList ddlSpareItemDesc = (grdvwSPBillDetails.Rows[0].FindControl("ddlSpareItemDesc") as DropDownList);
                if (ddlSpareItemDesc.SelectedIndex == 0 && ddlSpareItemDesc.Items.Count == 1)
                {
                    DataSet ds = vehMain.IFillVendorsMaintenance();
                    string VehicleNumber = ddlVehicleNumber.SelectedItem.ToString();
                    DataSet ds2 = vehMain.GetSpareParts(VehicleNumber);
                    if (ds2 != null)
                    {
                        ddlSpareItemDesc.DataSource = ds2.Tables[0];
                        ddlSpareItemDesc.DataValueField = "ManufacturerSpare_Id";
                        ddlSpareItemDesc.DataTextField = "SparePart_Name";
                        ddlSpareItemDesc.DataBind();
                    }
                }


                //SetInitialRowSP();
            }
            else
                SetInitialRowSP();

            if (chkbxlistBillType.Items[1].Selected)
            {
                pnlLubricantBillDetails.Visible = true;
            }
            else
                SetInitialRowLubricant();

            if (chkbxlistBillType.Items[2].Selected && !(pnlLabourBillDetails.Visible))
            {
                Checkboxcount = true;
                pnlLabourBillDetails.Visible = true;
                if (ddlDistrict.SelectedIndex != 0)
                {
                    VehicleNo = Convert.ToString(ddlVehicleNumber.SelectedItem.Text);
                    DataSet ds = vehMain.getcatedetails(VehicleNo);
                    grdvwLabourBillDetails.DataSource = ds.Tables[0];
                    grdvwLabourBillDetails.DataBind();
                    ViewState["Categories"] = ds;
                }

                SetInitialRowLabour();
            }
            else if (chkbxlistBillType.Items[2].Selected && !(pnlLabourBillDetails.Visible))
            {
                pnlLabourBillDetails.Visible = true;
            }
            //else if (!chkbxlistBillType.Items[2].Selected)
            //    Checkboxcount = false;
            //else
            //    SetInitialRowLabour();

            if ((chkbxlistBillType.Items[0].Selected) || (chkbxlistBillType.Items[1].Selected) || (chkbxlistBillType.Items[2].Selected))
            {
                pnlBillDetailsSummaryBtn.Visible = true;
            }

            txtTotalBillAmt.Text = "";
        }


        public void GetVendors()
        {
            DataSet ds = vehMain.IFillVendorsMaintenance();

            if (ds != null)
            {



            }
        }

        protected void chkAmount_CheckedChanged(object sender, EventArgs e)
        {
            SetInitialRowSP();
            SetInitialRowLubricant();
            SetInitialRowLabour();

            if (chkAmount.Checked)
            {
                foreach (ListItem item in chkbxlistBillType.Items)
                {
                    item.Selected = false;
                }
                btnSave.Enabled = true;
                chkbxlistBillType.Enabled = false;
                pnlSPBillDetails.Visible = false;
                pnlLubricantBillDetails.Visible = false;
                pnlLabourBillDetails.Visible = false;
                pnlBillSummaryDetails.Visible = false;
                pnlBillDetailsSummaryBtn.Visible = false;
            }
            else
            {
                btnSave.Enabled = false;
                chkbxlistBillType.Enabled = true;
            }


            if (btnSave.Text == "Update" && chkAmount.Checked)
                btnSave.Enabled = true;
        }
        private bool ValidateSPGrid()
        {
            if (pnlSPBillDetails.Visible == true)
            {
                for (int i = 0; i < grdvwSPBillDetails.Rows.Count; i++)
                {
                    DropDownList ddlVendorName = grdvwSPBillDetails.Rows[i].FindControl("ddlSpareVendorName") as DropDownList;
                    if (ddlVendorName.SelectedIndex == 0)
                    {
                        Show("Please Select Spare Parts Vendor Name");
                        return false;
                    }
                    TextBox txtbxspbillno = grdvwSPBillDetails.Rows[i].FindControl("txtSpareBillNo") as TextBox;
                    if (txtbxspbillno.Text == "")
                    {
                        Show("Please enter Spare Parts Bill Number");
                        return false;
                    }
                    TextBox txtbxspdate = grdvwSPBillDetails.Rows[i].FindControl("txtSpareBillDate") as TextBox;
                    if (txtbxspdate.Text == "")
                    {
                        Show("Please enter Spare Parts Bill Date");
                        return false;
                    }
                    else if (Convert.ToDateTime(txtbxspdate.Text) > DateTime.Now)
                    {
                        Show("Spare Parts Bill Date should be less than Current Date");
                        return false;
                    }
                    TextBox txtSpareEMRIpc = grdvwSPBillDetails.Rows[i].FindControl("txtSpareEMRIpc") as TextBox;
                    if (txtSpareEMRIpc.Text == "")
                    {
                        Show("Please enter Spare Parts EMRI Part Code");
                        return false;
                    }
                    TextBox txtSparePartCode = grdvwSPBillDetails.Rows[i].FindControl("txtSparePartCode") as TextBox;
                    if (txtSparePartCode.Text == "")
                    {
                        Show("Please enter Spare Parts Part Code");
                        return false;
                    }
                    DropDownList ddlSpareItemDesc = grdvwSPBillDetails.Rows[i].FindControl("ddlSpareItemDesc") as DropDownList;
                    if (ddlSpareItemDesc.SelectedIndex == 0)
                    {
                        Show("Please Select Spare Item Description");
                        return false;
                    }
                    TextBox txtbxspamt = grdvwSPBillDetails.Rows[i].FindControl("txtSpareBillAmount") as TextBox;
                    if (txtbxspamt.Text == "")
                    {
                        Show("Please enter Spare Parts Bill Amount");
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ValidateLubriGrid()
        {
            if (pnlLubricantBillDetails.Visible == true)
            {
                for (int i = 0; i < grdvwLubricantBillDetails.Rows.Count; i++)
                {
                    DropDownList ddlLubricantVendorName = grdvwLubricantBillDetails.Rows[i].FindControl("ddlLubricantVendorName") as DropDownList;
                    if (ddlLubricantVendorName.SelectedIndex == 0)
                    {
                        Show("Please Select Lubricant Vendor Name");
                        return false;
                    }
                    TextBox txtbxlubribillno = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantBillNo") as TextBox;
                    if (txtbxlubribillno.Text == "")
                    {
                        Show("Please enter Lubricant Bill Number");
                        return false;
                    }
                    TextBox txtbxlubridate = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantBillDate") as TextBox;
                    if (txtbxlubridate.Text == "")
                    {
                        Show("Please enter Lubricant Bill Date");
                        return false;
                    }
                    else if (Convert.ToDateTime(txtbxlubridate.Text) > DateTime.Now)
                    {
                        Show("Lubricant Bill Date should be less than Current Date");
                        return false;
                    }
                    TextBox txtLubricantEMRIpc = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantEMRIpc") as TextBox;
                    if (txtLubricantEMRIpc.Text == "")
                    {
                        Show("Please enter Lubricant EMRI Part Code");
                        return false;
                    }
                    TextBox txtLubricantPartCode = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantPartCode") as TextBox;
                    if (txtLubricantPartCode.Text == "")
                    {
                        Show("Please enter Lubricant Part Code");
                        return false;
                    }
                    TextBox txtLubricantItemDesc = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantItemDesc") as TextBox;
                    if (txtLubricantItemDesc.Text == "")
                    {
                        Show("Please enter Lubricant Item Description");
                        return false;
                    }
                    TextBox txtbxlubriamt = grdvwLubricantBillDetails.Rows[i].FindControl("txtLubricantBillAmount") as TextBox;
                    if (txtbxlubriamt.Text == "")
                    {
                        Show("Please enter Lubricant Bill Amount");
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ValidateLabourGrid()
        {
            if (pnlLabourBillDetails.Visible == true)
            {
                for (int i = 0; i < grdvwLabourBillDetails.Rows.Count; i++)
                {
                    DropDownList ddlLabourVendorName = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourVendorName") as DropDownList;
                    if (ddlLabourVendorName.SelectedIndex == 0)
                    {
                        Show("Please Select Labour Vendor Name");
                        return false;
                    }
                    TextBox txtbxlabourbillno = grdvwLabourBillDetails.Rows[i].FindControl("txtLabourBillNo") as TextBox;
                    if (txtbxlabourbillno.Text == "")
                    {
                        Show("Please enter Labour Bill Number");
                        return false;
                    }
                    TextBox txtbxlabourdate = grdvwLabourBillDetails.Rows[i].FindControl("txtLabourBillDate") as TextBox;
                    if (txtbxlabourdate.Text == "")
                    {
                        Show("Please enter Labour Bill Date");
                        return false;
                    }
                    else if (Convert.ToDateTime(txtbxlabourdate.Text) > DateTime.Now)
                    {
                        Show("Labour Bill Date should be less than Current Date");
                        return false;
                    }
                    ComboBox ddlLabourAggregates = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourAggregates") as ComboBox;
                    if (ddlLabourAggregates.SelectedIndex == 0)
                    {
                        Show("Please Select Labour Aggregate");
                        return false;
                    }
                    ComboBox ddlLabourCategories = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourCategories") as ComboBox;
                    if (ddlLabourCategories.SelectedIndex == 0)
                    {
                        Show("Please Select Labour Categories");
                        return false;
                    }
                    ComboBox ddlLabourSubCategories = grdvwLabourBillDetails.Rows[i].FindControl("ddlLabourSubCategories") as ComboBox;
                    if (ddlLabourSubCategories.SelectedIndex == 0)
                    {
                        Show("Please Select Labour SubCategories");
                        return false;
                    }
                    TextBox txtLabourItemDesc = grdvwLabourBillDetails.Rows[i].FindControl("txtLabourItemDesc") as TextBox;
                    if (txtLabourItemDesc.Text == "")
                    {
                        Show("Please enter Labour Item Description");
                        return false;
                    }
                    TextBox txtbxlabouramt = grdvwLabourBillDetails.Rows[i].FindControl("txtLabourBillAmount") as TextBox;
                    if (txtbxlabouramt.Text == "")
                    {
                        Show("Please enter Labour Bill Amount");
                        return false;
                    }
                }
            }
            return true;
        }


        protected void grdvwSPBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSpareVendorName = (e.Row.FindControl("ddlSpareVendorName") as DropDownList);
                DropDownList ddlSpareItemDesc = (e.Row.FindControl("ddlSpareItemDesc") as DropDownList);

                DataSet ds = vehMain.IFillVendorsMaintenance();
                string VehicleNumber = ddlVehicleNumber.SelectedItem.ToString();
                DataSet ds2 = vehMain.GetSpareParts(VehicleNumber);
                if (ds != null)
                {

                    ddlSpareVendorName.DataSource = ds.Tables[0];
                    ddlSpareVendorName.DataValueField = "AgencyId";
                    ddlSpareVendorName.DataTextField = "AgencyName";
                    ddlSpareVendorName.DataBind();
                    //ddlSpareVendorName.Items.Insert(0, new ListItem("--Select--", "0"));
                    ViewState["Vendor"] = ds;

                }
                if (ds2 != null)
                {
                    ddlSpareItemDesc.DataSource = ds2.Tables[0];
                    ddlSpareItemDesc.DataValueField = "ManufacturerSpare_Id";
                    ddlSpareItemDesc.DataTextField = "SparePart_Name";
                    ddlSpareItemDesc.DataBind();

                    ViewState["SpareItemDesc"] = ds2;

                }

                if (isedit)
                {
                    //DropDownList ddlVehicleNumber1 = (e.Row.FindControl("ddlVehicleNumber") as DropDownList);
                    //string VehicleNumber1 = gvVehicleMaintenanceDetails.SelectedRow.Cells[0].ToString();
                    DataSet dsVehNo = vehMain.GetSpareParts(txtVehicleNumber.Text);
                    if (dsVehNo != null)
                    {
                        ddlSpareItemDesc.DataSource = dsVehNo.Tables[0];
                        ddlSpareItemDesc.DataValueField = "ManufacturerSpare_Id";
                        ddlSpareItemDesc.DataTextField = "SparePart_Name";
                        ddlSpareItemDesc.DataBind();
                    }

                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "AgencyName='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1]) + "'";
                    string selectedID = Convert.ToString(dv.ToTable().Rows[0]["AgencyId"]);
                    ((DropDownList)e.Row.FindControl("ddlSpareVendorName")).SelectedValue = selectedID;
                    DataView dv1 = dsVehNo.Tables[0].DefaultView;
                    dv1.RowFilter = "SparePart_Name='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[6]) + "'";
                    string SparePartID = Convert.ToString(dv1.ToTable().Rows[0]["ManufacturerSpare_Id"]);
                    ((DropDownList)e.Row.FindControl("ddlSpareItemDesc")).SelectedValue = SparePartID;
                }
            }
        }

        protected void grdvwLubricantBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlLubricantVendorName = (e.Row.FindControl("ddlLubricantVendorName") as DropDownList);
                    DataSet ds = vehMain.IFillVendorsMaintenance();

                    if (ds != null && ddlLubricantVendorName != null)
                    {

                        ddlLubricantVendorName.DataSource = ds.Tables[0];
                        ddlLubricantVendorName.DataValueField = "AgencyId";
                        ddlLubricantVendorName.DataTextField = "AgencyName";
                        ddlLubricantVendorName.DataBind();
                        //ddlLubricantVendorName.Items.Insert(0, new ListItem("--Select--", "0"));
                        ViewState["Vendor"] = ds;

                    }

                    if (isedit)
                    {

                        DataView dv = ds.Tables[0].DefaultView;
                        dv.RowFilter = "AgencyName='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4]) + "'";
                        string selectedID = Convert.ToString(dv.ToTable().Rows[0]["AgencyId"]);
                        ((DropDownList)e.Row.FindControl("ddlLubricantVendorName")).SelectedValue = selectedID;

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void grdvwLabourBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdvwLabourBillDetails.Rows.Count != -1)
                {
                    DropDownList ddlLabourVendorName = (e.Row.FindControl("ddlLabourVendorName") as DropDownList);
                    DataSet ds = vehMain.IFillVendorsMaintenance();

                    if (ds != null)
                    {

                        ddlLabourVendorName.DataSource = ds.Tables[0];
                        ddlLabourVendorName.DataValueField = "AgencyId";
                        ddlLabourVendorName.DataTextField = "AgencyName";
                        ddlLabourVendorName.DataBind();
                        //ddlLabourVendorName.Items.Insert(0, new ListItem("--Select--", "0"));
                        ViewState["Vendor"] = ds;
                    }

                    dslabourAggregates = new DataSet();
                    dslabourAggregates = vehallobj.GetAggregates();
                    ComboBox ddlLabourAggregates = (e.Row.FindControl("ddlLabourAggregates") as ComboBox);
                    if (dslabourAggregates != null)
                    {
                        ddlLabourAggregates.DataSource = dslabourAggregates.Tables[0];
                        ddlLabourAggregates.DataValueField = "Aggregate_Id";
                        ddlLabourAggregates.DataTextField = "Aggregates";
                        ddlLabourAggregates.DataBind();
                        ViewState["LabourAggregates"] = dslabourAggregates;
                    }
                    dsLabourCategories = new DataSet();
                    dsLabourCategories = vehallobj.GetCategoriesMaintenance();
                    ComboBox ddlLabourCategories = (e.Row.FindControl("ddlLabourCategories") as ComboBox);
                    if (dsLabourCategories != null)
                    {
                        ddlLabourCategories.DataSource = dsLabourCategories.Tables[0];
                        ddlLabourCategories.DataValueField = "Category_Id";
                        ddlLabourCategories.DataTextField = "Categories";
                        ddlLabourCategories.DataBind();
                        ViewState["LabourCategories"] = dsLabourCategories;
                    }
                    dsLabourSubCategories = new DataSet();
                    dsLabourSubCategories = vehallobj.GetSubcategoriesMaintenance();
                    ComboBox ddlLabourSubCategories = (e.Row.FindControl("ddlLabourSubCategories") as ComboBox);
                    if (dsLabourSubCategories != null)
                    {
                        ddlLabourSubCategories.DataSource = dsLabourSubCategories.Tables[0];
                        ddlLabourSubCategories.DataValueField = "SubCategory_Id";
                        ddlLabourSubCategories.DataTextField = "SubCategories";
                        ddlLabourSubCategories.DataBind();
                        ViewState["LabourSubCategories"] = dsLabourSubCategories;
                    }
                }
                if (isedit)
                {
                    DropDownList ddlLabourVendorName = (e.Row.FindControl("ddlLabourVendorName") as DropDownList);
                    DataSet ds = vehMain.IFillVendorsMaintenance();

                    if (ds != null)
                    {

                        ddlLabourVendorName.DataSource = ds.Tables[0];
                        ddlLabourVendorName.DataValueField = "AgencyId";
                        ddlLabourVendorName.DataTextField = "AgencyName";
                        ddlLabourVendorName.DataBind();
                        //ddlLabourVendorName.Items.Insert(0, new ListItem("--Select--", "0"));
                        // ViewState["Vendor"] = ds;
                    }

                    DataView dv = new DataView(ds.Tables[0]);

                    dv.RowFilter = "AgencyName='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1]) + "'";
                    if (dv.Count > 0)
                    {
                        string selectedID = Convert.ToString(dv.ToTable().Rows[0]["AgencyId"]);
                        ((DropDownList)e.Row.FindControl("ddlLabourVendorName")).SelectedValue = selectedID;

                    }
                    else
                    {
                        return;
                    }


                    dslabourAggregates = new DataSet();
                    dslabourAggregates = vehallobj.GetAggregates();
                    ComboBox ddlLabourAggregates = (e.Row.FindControl("ddlLabourAggregates") as ComboBox);
                    if (dslabourAggregates != null)
                    {
                        ddlLabourAggregates.DataSource = dslabourAggregates.Tables[0];
                        ddlLabourAggregates.DataValueField = "Aggregate_Id";
                        ddlLabourAggregates.DataTextField = "Aggregates";
                        ddlLabourAggregates.DataBind();
                        //ViewState["LabourAggregates"] = dslabourAggregates;
                    }
                    DataView dv1 = new DataView(dslabourAggregates.Tables[0]);
                    dv1.RowFilter = "Aggregates='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4]) + "'";
                    if (dv1.Count > 0)
                    {
                        string selectedID1 = Convert.ToString(dv1.ToTable().Rows[0]["Aggregate_Id"]);
                        ((ComboBox)e.Row.FindControl("ddlLabourAggregates")).SelectedValue = selectedID1;
                    }
                    else
                    {
                        return;
                    }

                    dsLabourCategories = new DataSet();
                    dsLabourCategories = vehallobj.GetCategoriesMaintenance();
                    ComboBox ddlLabourCategories = (e.Row.FindControl("ddlLabourCategories") as ComboBox);
                    if (dsLabourCategories != null)
                    {
                        ddlLabourCategories.DataSource = dsLabourCategories.Tables[0];
                        ddlLabourCategories.DataValueField = "Category_Id";
                        ddlLabourCategories.DataTextField = "Categories";
                        ddlLabourCategories.DataBind();
                        //ViewState["LabourCategories"] = dsLabourCategories;
                    }
                    DataView dv2 = new DataView(dsLabourCategories.Tables[0]);
                    dv2.RowFilter = "Categories='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5]) + "'";
                    if (dv2.Count > 0)
                    {
                        string selectedID2 = Convert.ToString(dv2.ToTable().Rows[0]["Category_Id"]);
                        ((ComboBox)e.Row.FindControl("ddlLabourCategories")).SelectedValue = selectedID2;
                    }
                    else
                    {
                        return;
                    }




                    dsLabourSubCategories = new DataSet();
                    dsLabourSubCategories = vehallobj.GetSubcategoriesMaintenance();
                    ComboBox ddlLabourSubCategories = (e.Row.FindControl("ddlLabourSubCategories") as ComboBox);
                    if (dsLabourSubCategories != null)
                    {
                        ddlLabourSubCategories.DataSource = dsLabourSubCategories.Tables[0];
                        ddlLabourSubCategories.DataValueField = "SubCategory_Id";
                        ddlLabourSubCategories.DataTextField = "SubCategories";
                        ddlLabourSubCategories.DataBind();
                        ViewState["LabourSubCategories"] = dsLabourSubCategories;
                    }
                    DataView dv3 = dsLabourSubCategories.Tables[0].DefaultView;
                    dv3.RowFilter = "SubCategories='" + Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[6]) + "'";
                    if (dv3.Count > 0)
                    {
                        string selectedID3 = Convert.ToString(dv3.ToTable().Rows[0]["SubCategory_Id"]);
                        ((ComboBox)e.Row.FindControl("ddlLabourSubCategories")).SelectedValue = selectedID3;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        protected void gvVehicleMaintenanceDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}