using GvkFMSAPP.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GvkFMSAPP.PL
{
    public partial class FleetManufacturers : System.Web.UI.Page
    {
        public IFleetMaster ObjFMSMan = new FMSFleetMaster();

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] == null)
            {
                Response.Redirect("Error.aspx");
            }

            if (!IsPostBack)
            {
                grvManufacturerDetails.Columns[0].Visible = false;
                FillStates();
                FillDistricts(0);
                ddlFleetManufacturerDistrict.Enabled = false;
                //FillMandals(0);
                //ddlFleetManufacturerMandal.Enabled = false;
                //FillCities(0);
                //ddlFleetManufacturerCity.Enabled = false;
                FillGrid_FleetManufacturerDetails();

                btnManufacturerSave.Attributes.Add("onclick", "javascript:return validationFleetManufacturers()");
                //txtManufacturerName.Attributes.Add("onkeypress", "javascript:return OnlyAlphabets(this,event)");
                txtManufacturerModel.Attributes.Add("onkeypress", "javascript:return OnlyAlphaNumeric(this,event)");
                txtManufacturerAddress.Attributes.Add("onkeypress", "javascript:return remark(this,event)");
                txtManufacturerContactPerson.Attributes.Add("onkeypress", "javascript:return OnlyAlphabets(this,event)");
                txtManufacturerContactNumber.Attributes.Add("onkeypress", "javascript:return isNumberKey(this,event)");
                txtManufacturerTin.Attributes.Add("onkeypress", "javascript:return isNumberKey(this,event)");
                txtManufacturerErn.Attributes.Add("onkeypress", "javascript:return isNumberKey(this,event)");
            }


            //Permissions
            DataSet dsPerms = (DataSet)Session["PermissionsDS"];
            dsPerms.Tables[0].DefaultView.RowFilter = "Url='" + Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1] + "'";
            PagePermissions p = new PagePermissions(dsPerms, dsPerms.Tables[0].DefaultView[0]["Url"].ToString(), dsPerms.Tables[0].DefaultView[0]["Title"].ToString());

            pnlFleetManufacturers.Visible = false;
            grvManufacturerDetails.Visible = false;

            if (p.View == true)
            {
                grvManufacturerDetails.Visible = true;
                grvManufacturerDetails.Columns[5].Visible = false;

            }
            if (p.Add == true)
            {
                pnlFleetManufacturers.Visible = true;
                grvManufacturerDetails.Visible = true;
                grvManufacturerDetails.Columns[5].Visible = false;

            }
            if (p.Modify == true)
            {
                grvManufacturerDetails.Visible = true;
                grvManufacturerDetails.Columns[5].Visible = true;

            }
            if (p.Approve == true)
            {
            }
        }

        #endregion

        #region Reset Function

        private void FleetManufacturerDetailsReset()
        {
            txtManufacturerName.Text = "";
            txtManufacturerAddress.Text = "";
            txtManufacturerContactNumber.Text = "";
            txtManufacturerContactPerson.Text = "";
            txtManufacturerEmailId.Text = "";
            txtManufacturerErn.Text = "";
            txtManufacturerModel.Text = "";
            txtManufacturerTin.Text = "";
            //ddlFleetManufacturerCity.SelectedIndex = 0;
            ddlFleetManufacturerDistrict.SelectedIndex = 0;
            ddlFleetManufacturerDistrict.Enabled = false;
            //ddlFleetManufacturerMandal.SelectedIndex = 0;
            ddlManufacturerState.SelectedIndex = 0;
            ddlManufacturerType.SelectedIndex = 0;
            btnManufacturerSave.Text = "Save";
        }

        #endregion

        #region Fill States Function

        private void FillStates()
        {
            DataSet ds = new DataSet();
            ds = ObjFMSMan.IFillStates();
            ddlManufacturerState.DataSource = ds.Tables[0];
            ddlManufacturerState.DataValueField = "sc_scid";
            ddlManufacturerState.DataTextField = "sc_lname";
            ddlManufacturerState.DataBind();
            ddlManufacturerState.Items.Insert(0, "--Select--");
            ddlManufacturerState.Items[0].Value = "0";
            //ddlManufacturerState.SelectedIndex = 0;

            ddlFleetManufacturerDistrict.Enabled = true;

        }

        #endregion

        #region Fill Districts Function

        private void FillDistricts(int stateID)
        {
            DataSet ds = new DataSet();
            ds = ObjFMSMan.IFillDistricts(stateID);
            ddlFleetManufacturerDistrict.DataSource = ds.Tables[0];
            ddlFleetManufacturerDistrict.DataValueField = "DISTRICT_ID";
            ddlFleetManufacturerDistrict.DataTextField = "DISTRICT_NAME";
            ddlFleetManufacturerDistrict.DataBind();
            ddlFleetManufacturerDistrict.Items.Insert(0, "--Select--");
            ddlFleetManufacturerDistrict.Items[0].Value = "0";
            ddlFleetManufacturerDistrict.SelectedIndex = 0;

            ddlFleetManufacturerDistrict.Enabled = true;
        }

        #endregion

        #region Fill Mandals Function

        //private void FillMandals(int distId)
        //{
        //    DataSet ds = new DataSet();
        //    ds = ObjFMSMan.IFillMandals(distId);
        //    ddlFleetManufacturerMandal.DataSource = ds.Tables[0];
        //    ddlFleetManufacturerMandal.DataValueField = "md_mdid";
        //    ddlFleetManufacturerMandal.DataTextField = "md_lname";
        //    ddlFleetManufacturerMandal.DataBind();
        //    ddlFleetManufacturerMandal.Items.Insert(0, "Select");
        //    ddlFleetManufacturerMandal.Items[0].Value = "0";
        //    ddlFleetManufacturerMandal.SelectedIndex = 0;

        //    ddlFleetManufacturerMandal.Enabled = true;
        //}

        #endregion

        #region Fill Cities Function

        //private void FillCities(int cityId)
        //{
        //    DataSet ds = new DataSet();
        //    ds = ObjFMSMan.IFillCities(cityId);
        //    ddlFleetManufacturerCity.DataSource = ds.Tables[0];
        //    ddlFleetManufacturerCity.DataValueField = "ct_ctid";
        //    ddlFleetManufacturerCity.DataTextField = "ct_lname";
        //    ddlFleetManufacturerCity.DataBind();
        //    ddlFleetManufacturerCity.Items.Insert(0, "Select");
        //    ddlFleetManufacturerCity.Items[0].Value = "0";
        //    ddlFleetManufacturerCity.SelectedIndex = 0;
        //    ddlFleetManufacturerCity.Enabled = true;
        //}

        #endregion

        #region Selected Index change for State drop down list

        protected void ddlManufacturerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlManufacturerState.SelectedIndex == 0)
            {
                ddlFleetManufacturerDistrict.Enabled = false;
                //ddlFleetManufacturerMandal.Enabled = false;
                //ddlFleetManufacturerCity.Enabled = false;
            }
            else
            {
                ddlFleetManufacturerDistrict.Enabled = true;
                FillDistricts(Convert.ToInt32(ddlManufacturerState.SelectedValue));
                //ddlFleetManufacturerMandal.Enabled = false;
                //ddlFleetManufacturerCity.Enabled = false;
                //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlFleetManufacturerDistrict);
                // System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlScid);
            }
        }

        #endregion

        #region Selected Index Change for District drop down list

        protected void ddlFleetManufacturerDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFleetManufacturerDistrict.SelectedIndex == 0)
            {


                //ddlFleetManufacturerMandal.Enabled = true;
                //ddlFleetManufacturerCity.Enabled = false;
            }
            else
            {
                //ddlFleetManufacturerMandal.Enabled = true;
                //FillMandals(Convert.ToInt32(ddlFleetManufacturerDistrict.SelectedValue));
                //ddlFleetManufacturerCity.Enabled = false;
                //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlFleetManufacturerDistrict);
                // System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlScid);
            }
        }

        #endregion

        #region Selected Index Change for Mandal Drop Down List

        //protected void ddlFleetManufacturerMandal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlFleetManufacturerMandal.SelectedIndex == 0)
        //    {



        //        ddlFleetManufacturerCity.Enabled = true;
        //    }
        //    else
        //    {
        //        ddlFleetManufacturerCity.Enabled = true;
        //        FillCities(Convert.ToInt32(ddlFleetManufacturerMandal.SelectedValue));
        //        //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlFleetManufacturerDistrict);
        //        // System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlScid);
        //    }
        //}

        #endregion

        #region Save and Update Button

        protected void btnManufacturerSave_Click(object sender, EventArgs e)
        {
            if (btnManufacturerSave.Text == "Save")
            {
                SqlCommand cmd = new SqlCommand();

                DataSet ds = new DataSet();
                ds = ObjFMSMan.IFillGrid_FleetManufacturerDetails();
                if (ds.Tables[0].Select("FleetManufacturer_Name='" + txtManufacturerName.Text + "'").Length > 0)
                {
                    Show("Manufacturer Name Already Exists");
                }
                else
                {
                    string mfname = txtManufacturerName.Text;
                    int mftypid = Convert.ToInt32(ddlManufacturerType.SelectedValue);
                    string mfmodel = txtManufacturerModel.Text;
                    int mfstate = Convert.ToInt32(ddlManufacturerState.SelectedValue);
                    int mfdist = Convert.ToInt32(ddlFleetManufacturerDistrict.SelectedValue);
                    int mfmandal = 0;
                    int mfcity = 0;
                    //int mfmandal = Convert.ToInt32(ddlFleetManufacturerMandal.SelectedValue);
                    //int mfcity = Convert.ToInt32(ddlFleetManufacturerCity.SelectedValue);
                    string mfaddress = txtManufacturerAddress.Text;
                    long mfcontno = Convert.ToInt64(txtManufacturerContactNumber.Text);
                    string mfcontper = txtManufacturerContactPerson.Text;
                    string mfmail = txtManufacturerEmailId.Text;
                    long mftin = Convert.ToInt64(txtManufacturerTin.Text);
                    long mfern = Convert.ToInt64(txtManufacturerErn.Text);
                    int mfstatus = 1;
                    string mfinactby = Convert.ToString(Session["User_Id"]);
                    DateTime mfinactdate = DateTime.Today;
                    DateTime mfcreatedate = DateTime.Today;
                    string mfcreateby = Convert.ToString(Session["User_Id"]);
                    DateTime mfupdtdate = DateTime.Today;
                    string mfupdateby = Convert.ToString(Session["User_Id"]);
                    //InsertManufacturerDetails interface object
                    ds = ObjFMSMan.InsertManufacturerDetails(mfname, mftypid, mfmodel, mfstate, mfdist, mfmandal, mfcity,
                 mfaddress, mfcontno, mfcontper, mfmail, mftin, mfern,
                 mfstatus, mfinactby, mfinactdate, mfcreatedate, mfcreateby,
                 mfupdtdate, mfupdateby);
                    if (ds.Tables.Count == 0)
                    {
                        Show("Manufacturer Details added successfully");

                        FleetManufacturerDetailsReset();
                    }
                    else
                    {
                        Show("This Manufacturer details already exists");
                    }
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand();

                DataSet ds = new DataSet();
                ds = ObjFMSMan.IFillGrid_FleetManufacturerDetails();
                if (ds.Tables[0].Select("FleetManufacturer_Name='" + txtManufacturerName.Text + "' And FleetManufacturer_Id<>'" + hidManId.Value + "'").Length > 0)
                {
                    Show("Manufacturer Name Already Exists");
                }
                else
                {
                    int mfId = Convert.ToInt16(hidManId.Value);
                    string mfname = txtManufacturerName.Text;
                    int mftypid = Convert.ToInt32(ddlManufacturerType.SelectedValue);
                    string mfmodel = txtManufacturerModel.Text;
                    int mfstate = Convert.ToInt32(ddlManufacturerState.SelectedValue);
                    int mfdist = Convert.ToInt32(ddlFleetManufacturerDistrict.SelectedValue);
                    int mfmandal = 0;
                    int mfcity = 0;
                    //int mfmandal = Convert.ToInt32(ddlFleetManufacturerMandal.SelectedValue);
                    //int mfcity = Convert.ToInt32(ddlFleetManufacturerCity.SelectedValue);
                    string mfaddress = txtManufacturerAddress.Text;
                    long mfcontno = Convert.ToInt64(txtManufacturerContactNumber.Text);
                    string mfcontper = txtManufacturerContactPerson.Text;
                    string mfmail = txtManufacturerEmailId.Text;
                    long mftin = Convert.ToInt64(txtManufacturerTin.Text);
                    long mfern = Convert.ToInt64(txtManufacturerErn.Text);
                    //UpdateManufacturerDetails interface object
                    ds = ObjFMSMan.UpdateManufacturerDetails(mfId, mfname, mftypid, mfmodel, mfstate, mfdist, mfmandal, mfcity,
                 mfaddress, mfcontno, mfcontper, mfmail, mftin, mfern);
                    if (ds.Tables.Count == 0)
                    {
                        Show("Manufacturer Details Updated successfully");
                        FleetManufacturerDetailsReset();

                    }
                    else
                    {
                        Show("This Manufacturer details already exists ");

                    }
                }
            }
            FillGrid_FleetManufacturerDetails();
        }

        #endregion

        #region Reset Button

        protected void btnManufacturerReset_Click(object sender, EventArgs e)
        {
            FleetManufacturerDetailsReset();
        }

        #endregion

        #region Filling Gridview of Manufacturer Details

        public void FillGrid_FleetManufacturerDetails()
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            ds = ObjFMSMan.IFillGrid_FleetManufacturerDetails();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grvManufacturerDetails.DataSource = ds.Tables[0];
                    grvManufacturerDetails.DataBind();
                }
            }
            else
            {
                string strScript1 = "<script language=JavaScript>alert('" + "No record found" + "')</script>";
                Page.RegisterStartupScript("Success", strScript1);
            }
        }

        #endregion

        #region Row Editing of Manufacturer Details

        protected void grvManufacturerDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btnManufacturerSave.Text = "Update";
            int index = e.NewEditIndex;
            Label lblid = (Label)grvManufacturerDetails.Rows[index].FindControl("lblId");
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            hidManId.Value = lblid.Text;
            int mfId = Convert.ToInt16(hidManId.Value);
            ds = ObjFMSMan.IRowEditManufacturerDetails(mfId);
            Session["State_Id"] = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            Session["District_Id"] = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            Session["Mandal_Id"] = ds.Tables[0].Rows[0].ItemArray[5].ToString();
            Session["City_Id"] = ds.Tables[0].Rows[0].ItemArray[6].ToString();


            txtManufacturerName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_Name"].ToString());
            ddlManufacturerType.SelectedValue = ds.Tables[0].Rows[0]["FleetType_Id"].ToString();
            txtManufacturerModel.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fleet_Model"].ToString());
            ddlManufacturerState.SelectedValue = ds.Tables[0].Rows[0]["State_Id"].ToString();
            txtManufacturerAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_Address"].ToString());
            txtManufacturerContactNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_ContactNo"].ToString());
            txtManufacturerContactPerson.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_ContactPerson"].ToString());
            txtManufacturerEmailId.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_EmailId"].ToString());
            txtManufacturerTin.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_TIN"].ToString());
            txtManufacturerErn.Text = Convert.ToString(ds.Tables[0].Rows[0]["FleetManufacturer_ERN"].ToString());
            //FillStates();
            FillDistricts(Convert.ToInt32(Session["State_Id"].ToString()));
            ddlFleetManufacturerDistrict.SelectedValue = ds.Tables[0].Rows[0]["District_Id"].ToString();

            //FillMandals(Convert.ToInt32(Session["District_Id"].ToString()));
            //ddlFleetManufacturerMandal.SelectedValue = ds.Tables[0].Rows[0]["Mandal_Id"].ToString();

            //FillCities(Convert.ToInt32(Session["Mandal_Id"].ToString()));
            //ddlFleetManufacturerCity.ClearSelection();
            //ddlFleetManufacturerCity.Items.FindByValue(ds.Tables[0].Rows[0]["City_Id"].ToString()).Selected = true;
            //ddlFleetManufacturerCity.SelectedValue = ds.Tables[0].Rows[0]["City_Id"].ToString();
        }

        #endregion

        #region Page Index Changing of Manufacturer Details

        protected void grvManufacturerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvManufacturerDetails.PageIndex = e.NewPageIndex;
            FillGrid_FleetManufacturerDetails();
        }

        #endregion


        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }
    }
}