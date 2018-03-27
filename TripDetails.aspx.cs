using GvkFMSAPP.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GvkFMSAPP.PL
{
    public partial class TripDetails : System.Web.UI.Page
    {
        public IFuelManagement objFuelEntry = new FuelManagement();

        GvkFMSAPP.BLL.FMSGeneral fmsg = new GvkFMSAPP.BLL.FMSGeneral();
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
            if (Session["User_Name"] == null)
            {
                Response.Redirect("Error.aspx");
            }

            if (!IsPostBack)
            {

                if (Session["UserdistrictId"] != null)
                {
                    fmsg.UserDistrictId = Convert.ToInt32(Session["UserdistrictId"].ToString());
                }


                FillVehicles();
                FillTrips();
                FillHoursandMins();

                txtDestinationLocation.Attributes.Add("onkeypress", "javascript:return OnlyAlphabets(this,event)");

                txtRemarks.Attributes.Add("onkeypress", "javascript:return OnlyAlphabets(this,event)");
                txtStartOdo.Attributes.Add("onkeypress", "javascript:return isNumberKey(this,event)");
                txtEndOdo.Attributes.Add("onkeypress", "javascript:return isNumberKey(this,event)");


            }

        }

        private void FillTrips()
        {
            DataSet ds = objFuelEntry.IGetTripTypes();
            ddlTripType.DataSource = ds.Tables[0];
            ddlTripType.DataValueField = "TripTypeID";
            ddlTripType.DataTextField = "TripTypeDescription";
            ddlTripType.DataBind();
            ddlTripType.Items.Insert(0, "--Select--");
            ddlTripType.Items[0].Value = "0";
            ddlTripType.SelectedIndex = 0;
            ddlTripType.Enabled = true;

        }

        private void FillVehicles()
        {
            int districtID = -1;

            if (Session["UserdistrictId"] != null)
            {
                districtID = Convert.ToInt32(Session["UserdistrictId"].ToString());
            }
            DataSet ds = fmsg.GetVehicleNumber();
            ddlAmbulanceID.DataSource = ds.Tables[0];
            ddlAmbulanceID.DataValueField = "VehicleID";
            ddlAmbulanceID.DataTextField = "VehicleNumber";
            ddlAmbulanceID.DataBind();
            ddlAmbulanceID.Items.Insert(0, "--Select--");
            ddlAmbulanceID.Items[0].Value = "0";
            ddlAmbulanceID.SelectedIndex = 0;
            ddlAmbulanceID.Enabled = true;
        }

        private void FillHoursandMins()
        {
            int i;
            for (i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    this.ddlHours.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                    this.ddlHours1.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.ddlHours.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    this.ddlHours1.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            for (i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    this.ddlMinutes.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                    this.ddlMinutes2.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.ddlMinutes.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    this.ddlMinutes2.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int res = 0;
            DateTime TripDate = Convert.ToDateTime(txtTripDate.Text);
            int Vehicle = Convert.ToInt32(ddlAmbulanceID.SelectedValue);
            int Trip = Convert.ToInt32(ddlTripType.SelectedValue);
            string Destination = Convert.ToString(txtDestinationLocation.Text);
            string StaTim = ddlHours.SelectedItem.Text + ":" + ddlMinutes.SelectedItem.Text;
            DateTime StartTime = Convert.ToDateTime(StaTim);
            int StartOdo = Convert.ToInt32(txtStartOdo.Text);
            string EndTim = ddlHours1.SelectedItem.Text + ":" + ddlMinutes2.SelectedItem.Text;
            DateTime EndTime = Convert.ToDateTime(EndTim);
            int EndOdo = Convert.ToInt32(txtEndOdo.Text);
            string Remarks = Convert.ToString(txtRemarks.Text);
            int CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());

            res = objFuelEntry.IInsertTrips(TripDate, Vehicle, Trip, Destination, StartTime, StartOdo, EndTime, EndOdo, Remarks, CreatedBy);

            if (res == 1)
            {
                Show("Trip Entry Added Successfully");

            }
            else
            {
                Show("Failure,Please Try After sometime");
            }

            ClearFields();



        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFields();

        }

        private void ClearFields()
        {
            ddlHours.SelectedIndex = -1;
            ddlAmbulanceID.SelectedIndex = -1;
            ddlHours1.SelectedIndex = -1;
            ddlMinutes.SelectedIndex = -1;
            ddlMinutes2.SelectedIndex = -1;
            ddlTripType.SelectedIndex = -1;
            txtDestinationLocation.Text = "";
            txtEndOdo.Text = "";
            txtRemarks.Text = "";
            txtStartOdo.Text = "";
            txtTripDate.Text = "";

        }


        protected void lbtnViewHistory_Click(object sender, EventArgs e)
        {

        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }
        protected void ddlAmbulanceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsOdo = new DataSet();
            dsOdo = objFuelEntry.IGetTripEntryOdo(Convert.ToInt32(ddlAmbulanceID.SelectedValue));
            if (dsOdo.Tables[0].Rows.Count != 0)
            {
                if (dsOdo.Tables[0].Rows[0]["ODO"].ToString() != string.Empty)
                {
                    maxOdo.Value = dsOdo.Tables[0].Rows[0]["ODO"].ToString();
                }
                else
                {
                    maxOdo.Value = "0";
                }
            }
            else
            {
                maxOdo.Value = "0";
            }
        }
    }
}