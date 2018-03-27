using GvkFMSAPP.DLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace GvkFMSAPP.PL.VAS
{
    public partial class VehicleAllocation : System.Web.UI.Page
    {
        GvkFMSAPP.BLL.BaseVehicleDetails fmsobj = new GvkFMSAPP.BLL.BaseVehicleDetails();
        GvkFMSAPP.BLL.VAS_BLL.VASGeneral vehallobj = new GvkFMSAPP.BLL.VAS_BLL.VASGeneral();
        protected void Page_PreInit(Object sender, EventArgs e)
        {

            if (Session["Role_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
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
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                btnSubmit.Attributes.Add("onclick", "return validation()");

                GetDistrict();
                FillHoursandMins();
                //FillRequestedBy();
                txtReqBy.Text = Session["User_Name"].ToString();
                GetTime();
                btnSubmit.Enabled = true;
            }
        }

        public void GetDistrict()
        {
            ddlDistrict.DataSource = fmsobj.GetDistricts_new();
            ddlDistrict.DataTextField = "district_name";
            ddlDistrict.DataValueField = "district_id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        protected void GetTime()
        {
            // string[] timenow = DateTime.Now.ToString().Split(' ');
            string[] timenow = DateTime.Now.ToString("dd/MM/yyyy h:mm tt").Split(' ');

            txtUptimeDate.Text = timenow[0].ToString();
            int hour = Convert.ToInt32(timenow[1].Split(':')[0].ToString());
            int minute = Convert.ToInt32(timenow[1].Split(':')[1].ToString());

            if (timenow.Length > 2)
            {
                if (timenow[2] != "PM")
                {

                    if (hour < 10)
                    {
                        ddlUPHour.Items.FindByValue("0" + timenow[1].Split(':')[0].ToString()).Selected = true;
                    }
                    else
                    {
                        ddlUPHour.Items.FindByValue(timenow[1].Split(':')[0].ToString()).Selected = true;
                    }

                }
                else
                {
                    if (hour == 12)
                    {
                        ddlUPHour.Items.FindByValue(hour.ToString()).Selected = true;
                    }
                    else
                    {
                        hour = hour + 12;
                        ddlUPHour.Items.FindByValue(hour.ToString()).Selected = true;
                    }
                }
            }
            else
            {
                if (hour == 12)
                    hour = 0;
                else if (hour > 12)
                    hour = hour - 12;

                hour = hour + 12;
                ddlUPHour.Items.FindByValue(hour.ToString()).Selected = true;
            }


            if (minute < 10)
            {
                ddlUPMin.Items.FindByValue(timenow[1].Split(':')[1]).Selected = true;
            }
            else
            {
                ddlUPMin.Items.FindByValue(timenow[1].Split(':')[1]).Selected = true;
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

 public void insertAgent(string offroadid, string vehicleNo, string AgentID, string uptime, string OffTime, string PailotName, string PailotGid)
        {
            string ip = GetLocalIPAddress();
            SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Str"].ToString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into t_onroadAgent(onroadId ,vehicleNo, AgentID,AgentName,ip,OnRoadFromERC,OffTime,PailotName,PailotId) values ('" + offroadid + "', '" + vehicleNo + "', '" + AgentID + "', '" + Session["User_Name"].ToString() + "','" + ip + "', '"+ uptime + "','"+ OffTime + "','"+txtPailotName.Text+"','"+ txtPailotName.Text + "')";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            if(txtPailotName.Text == "" )
            {
                Show("Pailot name should not be left blank");
                return;
            }
            if ( txtpailotGid.Text == "")
            {
                Show("Pailot id should not be left blank");
                return;
            }


            string[] dttimedown = txtDownTime.Text.Split(' ');
            string[] DownTimestr = dttimedown[0].Split('/');
            //   Show(DownTimestr[2] + "-" + DownTimestr[0] + "-" + DownTimestr[0]);
            DateTime DownTime = Convert.ToDateTime(DownTimestr[2] + "-" + DownTimestr[0] + "-" + DownTimestr[1]);

            DateTime UptimeDate = DateTime.ParseExact(txtUptimeDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            // Show(DownTime.ToString() + "+++" + UptimeDate.ToString());
            //    string uptds = UptimeDate.ToString("yyyy-MM-dd");
            //    return;
            //  && (DownTime < UptimeDate) Removed downtime uptime validation
            if ((Convert.ToDecimal(ViewState["OdoReading"].ToString()) <= Convert.ToDecimal(txtOdo.Text)))
            {
                vehallobj.District = ddlDistrict.SelectedItem.Text;
                vehallobj.OffRoadVehicleNo = ddlVehicleNumber.SelectedItem.Text;
                vehallobj.ReasonForOffRoad = txtReasonforDown.Text;
                vehallobj.OffRoadDate = Convert.ToDateTime(txtDownTime.Text);
                vehallobj.Odometer = txtOdo.Text;
                vehallobj.RequestedBy = txtReqBy.Text;
                //vehallobj.RequestedBy = ddlistRequestedBy.SelectedItem.Text;
                string[] OfftimeDate = txtUptimeDate.Text.Split('/');

                vehallobj.UpTime = Convert.ToDateTime(OfftimeDate[2] + "-" + OfftimeDate[1] + "-" + OfftimeDate[0] + " " + ddlUPHour.SelectedValue + ":" + ddlUPMin.SelectedValue);
                vehallobj.BaseLocation = "0";
                vehallobj.NewSegFlag = "";
                string manids = "";
                vehallobj.NewSegMandalIds = "";
                vehallobj.Mandal = "";
                vehallobj.City = "";
                vehallobj.DistrictId = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
                vehallobj.MandalId = 0;
                vehallobj.CityId = 0;

                vehallobj.Flag = "Old";
                vehallobj.Latitude = "0.00";
                vehallobj.Longitude = "0.00";
                vehallobj.SegFlag = "Active";
                vehallobj.Segment = "";

                vehallobj.ContactNumber = txtContactNumber.Text;
                makeVehicleonRoad(ddlVehicleNumber.SelectedItem.Text, vehallobj.UpTime.ToString("yyyy-MM-dd HH:mm:ss"), Session["User_Name"].ToString(), txtOdo.Text, txtReqBy.Text, "NA", "NA");

                int insres = vehallobj.InsOffRoadVehAllocation();
                if (insres != 0)
                {
                    insertAgent(insres.ToString(), ddlVehicleNumber.SelectedItem.Text, Session["User_Id"].ToString(), vehallobj.UpTime.ToString("yyyy-MM-dd HH:mm:ss"), vehallobj.OffRoadDate.ToString("yyyy-MM-dd HH:mm:ss"), txtPailotName.Text, txtpailotGid.Text);
                    sendSms(ddlVehicleNumber.SelectedItem.Text, "", txtReasonforDown.ToString());
                   // InsertPailotDetails();
                    Show("Record Inserted Successfully!!");
                }
                else
                {
                    Show("Record not Inserted Successfully!!");
                }

                ClearAll();

            }
            else
            {
                if (Convert.ToDecimal(ViewState["OdoReading"].ToString()) > Convert.ToDecimal(txtOdo.Text))
                {
                    Show("Odometer reading should be greater than " + ViewState["OdoReading"].ToString());
                    btnSubmit.Enabled = true;
                }
                else
                {
                    Show("DownTime should be less than UpTime");
                    btnSubmit.Enabled = true;
                }

            }
        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   txtBaseLocation.Text = "";
            //ddlSegments.Items.Clear();
            //ddlSegments.Items.Insert(0, new ListItem("--Select--", "0"));
            ClearDropdown();
            //ddlSegments.Visible = true;
            //txtNewSegment.Visible = false;
            //lnkbtnNewSegment.Visible = true;
            //lnkbtnExtngSegment.Visible = false;
            // pnlmandals.Visible = false;
            //ddlBaseLocation.Visible = true;
            //txtBaseLocation.Visible = false;
            //lnkbtnExtngBaseLoc.Visible = false;
            //lnkbtnNewBaseLoc.Visible = true;
            //   chkbxinactivesegment.Checked = false;

            GetVehicles();
            //if (chkbxinactivesegment.Checked)
            //{
            //    GetInactiveSegments();
            //}
            //else
            //{
            //    GetSegments();
            //}
        }
        public void sendSms(string vehicleno, string breakdownid, string reason)
        {
            DataTable dtPenData = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Str"].ToString());
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from m_vehicle_supervisors where VehicleNo = '" + vehicleno + "'";
                cmd.Connection = conn;
                adp.Fill(dtPenData);
                conn.Close();
                if (dtPenData.Rows.Count > 0)
                {
                    string smsContent = "OnRoad-- Dear {name},\nVehicleNumber-" + vehicleno + " is made on road and base location:" + dtPenData.Rows[0]["BaseLocation"].ToString() + " by ERO:" + Session["User_Name"].ToString();

                    insertdata(smsContent, dtPenData.Rows[0]["Emename"].ToString(), dtPenData.Rows[0]["EmeContactNumber"].ToString());
                    insertdata(smsContent, dtPenData.Rows[0]["Pmname"].ToString(), dtPenData.Rows[0]["PmContactNumber"].ToString());
                    insertdata(smsContent, dtPenData.Rows[0]["Rmname"].ToString(), dtPenData.Rows[0]["RmContactNumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ErrorsEntry(ex.GetBaseException().ToString(), "class: FMSVehicleAllocation;Method: sendSms()", 0);
            }
        }
        public void insertdata(string smsContent, string name, string mobileno)
        {
            smsContent = smsContent.Replace("{name}", name);
            SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Str"].ToString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into t_SMS(smsContent ,mobileno) values ('" + smsContent + "', '" + mobileno + "')";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void GetVehicles()
        {
            ddlVehicleNumber.Items.Clear();
            vehallobj.District = ddlDistrict.SelectedItem.Text.ToString();
            ddlVehicleNumber.DataSource = vehallobj.GetVehicleNo();
            ddlVehicleNumber.DataTextField = "OffRoadVehicle_No";
            ddlVehicleNumber.DataValueField = "OffRoadVehicle_No";
            ddlVehicleNumber.DataBind();
            ddlVehicleNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillHoursandMins()
        {
            int i;
            for (i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    this.ddlUPHour.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.ddlUPHour.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            for (i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    this.ddlUPMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.ddlUPMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            ddlDistrict.SelectedIndex = 0;
            ddlVehicleNumber.Items.Clear();
            ddlVehicleNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            txtReasonforDown.Text = "";
            txtExpDateOfRec.Text = "";
            txtDownTime.Text = "";
            txtOdo.Text = "";
            // txtBaseLocation.Text = "";
            //ddlSegments.Items.Clear();
            //ddlSegments.Items.Insert(0, new ListItem("--Select--", "0"));
            ClearDropdown();
            //ddlSegments.Visible = true;
            //txtNewSegment.Visible = false;
            //lnkbtnNewSegment.Visible = true;
            //lnkbtnExtngSegment.Visible = false;
            // pnlmandals.Visible = false;
            //ddlBaseLocation.Visible = true;
            //txtBaseLocation.Visible = false;
            //lnkbtnExtngBaseLoc.Visible = false;
            //lnkbtnNewBaseLoc.Visible = true;
            //  chkbxinactivesegment.Checked = false;
            btnSubmit.Enabled = true;
            lblLatitude.Visible = false;
            lblLongitude.Visible = false;
            lblMandatory1.Visible = false;
            lblMandatory2.Visible = false;
            txtLatitude.Visible = false;
            txtLongitude.Visible = false;
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            lblpvODO.Text = "";
        }

        protected void ddlVehicleNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                vehallobj.OffRoadVehicleNo = ddlVehicleNumber.SelectedValue;
                DataSet dsoffrdvehdt = new DataSet();
                dsoffrdvehdt = vehallobj.GetOffRoadVehData();

                txtReasonforDown.Text = dsoffrdvehdt.Tables[0].Rows[0]["ReasonForOffRoad"].ToString();

                txtDownTime.Text = dsoffrdvehdt.Tables[0].Rows[0]["OffRoadDate"].ToString();

                txtExpDateOfRec.Text = dsoffrdvehdt.Tables[0].Rows[0]["ExpDateOfRecovery"].ToString();
                txtContactNumber.Text = dsoffrdvehdt.Tables[0].Rows[0]["vi_VehicleContact"].ToString();
                ViewState["OdoReading"] = dsoffrdvehdt.Tables[0].Rows[0]["Odometer"].ToString();

                lblpvODO.Text = ViewState["OdoReading"].ToString();
                DataSet oldmappingdetails = new DataSet();
                oldmappingdetails = vehallobj.oldmappingdetails();

                vehallobj.DistrictId = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
            }
            catch (Exception ex)
            {
                ErrorHandler.ErrorsEntry(ex.GetBaseException().ToString(), "class: FMSVehicleAllocation;Method: Vehicle Selected Index Changed()", 0);
            }

        }

        private int makeVehicleonRoad(string VehicleNumber, string onroaddate, string Status_change_by, string odo_meter, string informer_name, string piliot_name, string piliot_gid)
        {
            int i = 0;
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationSettings.AppSettings["MySqlConn"].ToString());
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateVehicleStatusOnroad";
                cmd.Parameters.AddWithValue("VehicleNumber", VehicleNumber);
                cmd.Parameters.AddWithValue("onroaddate", onroaddate);
                cmd.Parameters.AddWithValue("Status_change_by", Status_change_by);
                cmd.Parameters.AddWithValue("odo_meter", odo_meter);
                cmd.Parameters.AddWithValue("informer_name", informer_name);
                cmd.Parameters.AddWithValue("piliot_name", piliot_name);
                cmd.Parameters.AddWithValue("piliot_gid", piliot_gid);
                cmd.Connection = conn;
                DataSet ds = new DataSet();
                adp.Fill(ds);
                conn.Close();

            }
            catch (Exception ex)
            {
                ErrorHandler.ErrorsEntry(ex.ToString(), "class: FMSVehicleAllocation;Method: makeVehicleonRoad()", 0);
            }
            return i;
        }

        protected void chkbxinactivesegment_CheckedChanged(object sender, EventArgs e)
        {

            ClearDropdown();
            //ddlSegments.Visible = true;
            //txtNewSegment.Visible = false;
            //lnkbtnNewSegment.Visible = true;
            //lnkbtnExtngSegment.Visible = false;
            //ddlBaseLocation.Visible = true;
            //txtBaseLocation.Visible = false;
            //lnkbtnNewBaseLoc.Visible = true;
            //lnkbtnExtngBaseLoc.Visible = false;
            //   pnlmandals.Visible = false;
        }


        private void ClearDropdown()
        {
            //ddlMandal.Items.Clear();
            //ddlMandal.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlCity.Items.Clear();
            //ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlBaseLocation.Items.Clear();
            //ddlBaseLocation.Items.Insert(0, new ListItem("--Select--", "0"));
            txtContactNumber.Text = "";
            //   txtNewSegment.Text = "";
        }

    }
}