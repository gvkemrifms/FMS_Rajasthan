using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GvkFMSAPP.PL
{
    public partial class NonOffRoadPhysicalBills : System.Web.UI.Page
    {
        DataSet dsVehicle;
        DataSet dsDistricts, dsBillNo;
        GvkFMSAPP.BLL.BaseVehicleDetails fmsobj = new GvkFMSAPP.BLL.BaseVehicleDetails();
        GvkFMSAPP.BLL.VAS_BLL.VASGeneral obj = new GvkFMSAPP.BLL.VAS_BLL.VASGeneral();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] == null)
            {
                Response.Redirect("Error.aspx");
            }
            if (!IsPostBack)
            {
                ddlVehicleno.Enabled = false;
                ddlBillNo.Enabled = false;
                bindData();
                BindGridView();

            }

        }
        public void bindData()
        {
            dsDistricts = new DataSet();
            dsDistricts = fmsobj.GetDistrict();
            ddlDistricts.DataSource = dsDistricts.Tables[0];
            ddlDistricts.DataTextField = "ds_lname";
            ddlDistricts.DataValueField = "ds_dsid";
            ddlDistricts.DataBind();
            ddlDistricts.Items.Insert(0, new ListItem("--Select--", "0"));
            ViewState["dsDistricts"] = dsDistricts;
        }

        protected void ddlDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistricts.SelectedIndex != 0)
            {
                dsVehicle = new DataSet();
                ddlVehicleno.Enabled = true;
                obj.DistrictId = Convert.ToInt16(ddlDistricts.SelectedValue);
                dsVehicle = obj.GetNonOffVehFoBilling();
                ddlVehicleno.DataSource = dsVehicle.Tables[0];
                ddlVehicleno.DataTextField = "Vehicleno";
                ddlVehicleno.DataBind();
                ddlVehicleno.Items.Insert(0, new ListItem("--Select--", "0"));
                ViewState["dsVehicle"] = dsVehicle;
            }
            else
            {
                ddlVehicleno.Items.Clear();
                ddlVehicleno.Enabled = false;
                ddlBillNo.Items.Clear();
                ddlBillNo.Enabled = false;
                txtBillAmount.Text = "";
            }
        }


        protected void ddlVehicleno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlVehicleno.SelectedIndex != 0)
            {
                dsBillNo = new DataSet();
                ddlBillNo.Enabled = true;
                obj.SrcVehNo = ddlVehicleno.SelectedItem.ToString();
                dsBillNo = obj.GetBillNo();
                ddlBillNo.DataSource = dsBillNo.Tables[0];
                ddlBillNo.DataTextField = "Billno";
                ddlBillNo.DataBind();
                ddlBillNo.Items.Insert(0, new ListItem("--Select--", "0"));
                ViewState["dsBillNo"] = dsBillNo;
            }
            else
            {

                ddlBillNo.Items.Clear();
                ddlBillNo.Enabled = false;
                txtBillAmount.Text = "";
            }
        }





        protected void btnSave_Click(object sender, EventArgs e)
        {
            obj.District = ddlDistricts.SelectedItem.ToString();
            obj.SrcVehNo = ddlVehicleno.SelectedItem.ToString();
            obj.NonOffBillNo = ddlBillNo.SelectedItem.ToString();
            obj.ReceiptDate = Convert.ToDateTime(txtReceiptDate.Text);
            obj.CourierName = txtCourierName.Text;
            obj.DocketNo = txtDocketNo.Text;
            obj.NonOffAmount = txtBillAmount.Text;
            obj.MandalId = int.Parse(lblBrkdwn.Text);
            obj.VenName = HiddenField1.Value;
            int i = obj.InsertNonoffroadPhysicalBills();

            if (i != 0)
            {

                Show("Records inserted successfully");
                BindGridView();
                ddlDistricts.SelectedIndex = 0;
                ddlVehicleno.Items.Clear();
                ddlBillNo.Items.Clear();
                txtReceiptDate.Text = "";
                txtCourierName.Text = "";
                txtDocketNo.Text = "";
                txtBillAmount.Text = "";
                lblBrkdwn.Text = "";

            }
            else
            {
                Show("Records not inserted successfully");
            }
        }
        public void BindGridView()
        {
            DataSet ds = new DataSet();
            ds = obj.GetNonOffPhysicalBills();
            ViewState["VehiclePhyBill"] = ds.Tables[0];

            gvVehiclePhysicalBillDetails.DataSource = ds.Tables[0];
            gvVehiclePhysicalBillDetails.DataBind();
        }
        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }

        protected void gvVehiclePhysicalBillDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehiclePhysicalBillDetails.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void gvVehiclePhysicalBillDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper() == "VEHMAINEDIT")
            {
                GridViewRow row = (GridViewRow)((WebControl)e.CommandSource).Parent.Parent;
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                //int index = Convert.ToInt32(e.CommandArgument.ToString());
                DateTime dt = Convert.ToDateTime(((Label)((row.FindControl("lblReceiptDate")))).Text);
                txtReceiptDate.Text = dt.ToString("dd/MM/yyyy");
                txtCourierName.Text = ((Label)((row.FindControl("lblCourier_Name")))).Text;
                txtDocketNo.Text = ((Label)((row.FindControl("lblDocketNo")))).Text;
                lblBrkdwn.Text = ((Label)((row.FindControl("lblBrkDwnID")))).Text;
                DataSet dsDist = (DataSet)ViewState["dsDistricts"];
                DataView dvDistrict = dsDist.Tables[0].DefaultView;
                dvDistrict.RowFilter = "ds_lname='" + ((Label)((row.FindControl("lblDistrict")))).Text + "'";
                ddlDistricts.SelectedValue = Convert.ToString(dvDistrict.ToTable().Rows[0]["ds_dsid"]);
                ddlDistricts.Enabled = false;
                ddlDistricts_SelectedIndexChanged(this, null);
                ddlVehicleno.Items.Insert(1, new ListItem((((Label)((row.FindControl("lblVehicle_No")))).Text), "1"));
                ddlVehicleno.SelectedIndex = 1;

                ddlVehicleno.Enabled = false;
                //DataSet dsVeh = (DataSet)ViewState["dsVehicle"];
                //DataView dvVeh = dsVeh.Tables[0].DefaultView;
                //dvVeh.RowFilter = "Vehicleno='" + ((Label)((gvVehiclePhysicalBillDetails.Rows[index].FindControl("lblVehicle_No")))).Text + "'";
                //ddlVehicleno.SelectedValue = Convert.ToString(dvVeh.ToTable().Rows[0]["Vehicleno"]);
                ddlVehicleno_SelectedIndexChanged1(this, null);
                ddlBillNo.Items.Insert(1, new ListItem((((Label)((row.FindControl("lblBillNo")))).Text), "1"));
                ddlBillNo.SelectedIndex = 1;
                ddlBillNo.Enabled = false;
                txtBillAmount.Text = ((Label)((row.FindControl("lblBillAmount")))).Text;
                //ddlDistricts_SelectedIndexChanged(ddlDistricts,e);

                //ddlDistricts.SelectedItem.Text = ((Label)((gvVehiclePhysicalBillDetails.Rows[index].FindControl("lblDistrict")))).Text;
                //ddlVehicleno.SelectedItem.Text = ((Label)((gvVehiclePhysicalBillDetails.Rows[index].FindControl("lblVehicle_No")))).Text;
                //ddlBillNo.SelectedItem.Text = ((Label)((gvVehiclePhysicalBillDetails.Rows[index].FindControl("lblBillNo")))).Text;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            obj.District = ddlDistricts.SelectedItem.ToString();
            obj.SrcVehNo = ddlVehicleno.SelectedItem.ToString();
            obj.NonOffBillNo = ddlBillNo.SelectedItem.ToString();
            obj.ReceiptDate = Convert.ToDateTime(txtReceiptDate.Text);
            obj.CourierName = txtCourierName.Text;
            obj.DocketNo = txtDocketNo.Text;
            obj.NonOffAmount = txtBillAmount.Text;

            int i = obj.UpdateNonoffroadPhysicalBills();

            if (i != 0)
            {

                Show("Records Updated successfully");
                BindGridView();
                ddlDistricts.SelectedIndex = 0;
                ddlDistricts.Enabled = true;
                ddlVehicleno.Items.Clear();
                ddlVehicleno.Enabled = true;
                ddlBillNo.Items.Clear();
                ddlBillNo.Enabled = true;
                txtReceiptDate.Text = "";
                txtCourierName.Text = "";
                txtDocketNo.Text = "";
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                txtBillAmount.Text = "";
                lblBrkdwn.Text = "";
            }
            else
            {
                Show("Records not Updated successfully");
            }
        }

        protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBillNo.SelectedIndex != 0)
            {
                obj.District = ddlDistricts.SelectedItem.ToString();
                obj.VehNumforNonOff = ddlVehicleno.SelectedItem.ToString();
                obj.NonOffBillNo = ddlBillNo.SelectedItem.ToString();
                DataSet ds = obj.GetNonOffroadBillAmt();
                txtBillAmount.Text = ds.Tables[0].Rows[0][0].ToString();
                lblBrkdwn.Text = ds.Tables[1].Rows[0][0].ToString();
                HiddenField1.Value = ds.Tables[1].Rows[0][1].ToString();
            }
            else
            {
                txtBillAmount.Text = "";
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlDistricts.SelectedIndex = 0;
            ddlDistricts_SelectedIndexChanged(this, null);
            ddlVehicleno.Items.Clear();
            ddlVehicleno.Enabled = true;
            ddlBillNo.Items.Clear();
            ddlBillNo.Enabled = true;
            txtReceiptDate.Text = "";
            txtCourierName.Text = "";
            txtDocketNo.Text = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            txtBillAmount.Text = "";
            lblBrkdwn.Text = "";
        }

        //protected void gvVehiclePhysicalBillDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    if (e.CommandName == "VehMainEdit")
        //    {


        //    }
        //}

        //protected void gvVehiclePhysicalBillDetails_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string x = gvVehiclePhysicalBillDetails.SelectedRow.Cells[0].Text;
        //}


    }
}