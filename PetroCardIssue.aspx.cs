using GvkFMSAPP.BLL;
using ServiceReference2;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GvkFMSAPP.PL
{
    public partial class PetroCardIssue : System.Web.UI.Page
    {
        public IFuelManagement objFuelMan = new FuelManagement();
        GvkFMSAPP.BLL.FMSGeneral fmsg = new GvkFMSAPP.BLL.FMSGeneral();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User_Name"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["UserdistrictId"] != null)
                {
                    fmsg.UserDistrictId = Convert.ToInt32(Session["UserdistrictId"].ToString());
                }
                FillDistricts();
                FillAgency();
                FillCardType();
                FillGridPetroCard();
                FillFeUsers();
                btSave.Attributes.Add("onclick", "return isMandatory()");
                txtPetroCardNumber.Attributes.Add("onkeypress", "return isNumberKey(this,event)");

                DataSet dsPerms = (DataSet)Session["PermissionsDS"];
                dsPerms.Tables[0].DefaultView.RowFilter = "Url='" + Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1] + "'";
                PagePermissions p = new PagePermissions(dsPerms, dsPerms.Tables[0].DefaultView[0]["Url"].ToString(), dsPerms.Tables[0].DefaultView[0]["Title"].ToString());

                if (p.Add == true)
                {

                }
                if (p.Modify == true)
                {
                }
                if (p.View == true)
                {
                }
                if (p.Approve == true)
                {
                }
            }
        }

        protected void FillFeUsers()
        {
            ACLServiceClient client = new ACLServiceClient();
            DataSet ds = client.GetRoleBasedUsersList(int.Parse(ConfigurationManager.AppSettings["roleID"].ToString()));
            dd_listFe.DataSource = ds;
            dd_listFe.DataValueField = "PK_USER_ID";
            dd_listFe.DataTextField = "LOGIN_NAME";
            dd_listFe.DataBind();
            dd_listFe.Items.Insert(0, new ListItem("--Select--"));
        }

        private void FillDistricts()
        {
            DataSet ds = new DataSet();
            ds = objFuelMan.IFillDistricts();
            ddlFeuserDistrict.DataSource = ds.Tables[0];
            ddlFeuserDistrict.DataValueField = "ds_dsid";
            ddlFeuserDistrict.DataTextField = "ds_lname";
            ddlFeuserDistrict.DataBind();
            ddlFeuserDistrict.Items.Insert(0, "--Select--");
            //ddlDistricts.Items[0].Value = "0";
            ddlFeuserDistrict.SelectedIndex = 0;



        }





        protected void ddlDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {


        }





        private void FillCardType()
        {
            DataSet ds = new DataSet();
            ds = objFuelMan.IFillCardType();
            ddlCardType.DataSource = ds.Tables[0];
            ddlCardType.DataValueField = "CardTypeID";
            ddlCardType.DataTextField = "CardType";
            ddlCardType.DataBind();
            ddlCardType.Items.Insert(0, "--Select--");
            //ddlCardType.Items[0].Value = "0";
            ddlCardType.SelectedIndex = 0;

            ddlCardType.Enabled = true;

        }




        private void FillAgency()
        {
            int districtID = -1;

            if (Session["UserdistrictId"] != null)
            {
                districtID = Convert.ToInt32(Session["UserdistrictId"].ToString());
            }

            DataSet ds = new DataSet();
            ds = objFuelMan.IFillAgency(districtID);
            ddlAgency.DataSource = ds.Tables[0];
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, "--Select--");
            //ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;

            ddlAgency.Enabled = true;

        }





        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCardType.Enabled = true;
        }







        protected void btSave_Click(object sender, EventArgs e)
        {
            if (btSave.Text == "Save")
            {
               // Show(System.DateTime.Now.ToString());
                InsPetroCardIssueDetails(Convert.ToInt32(Session["UserdistrictId"].ToString()), Convert.ToInt64(txtPetroCardNumber.Text), Convert.ToInt32(ddlAgency.SelectedValue), Convert.ToInt32(ddlCardType.SelectedValue), Convert.ToDateTime(txtValidityEndDate.Text), Convert.ToInt32(dd_listFe.SelectedValue), Convert.ToDateTime(txtIssuedDate.Text), 0, 25, Convert.ToDateTime("05/24/2011"), 34, Convert.ToDateTime("05/25/2011"), Convert.ToInt32(ddlVehicles.SelectedValue), Convert.ToInt32(ddlFeuserDistrict.SelectedValue));
                FillGridPetroCard();
            }

            else
            {
                UpdPetroCardIssueDetails(Convert.ToInt32(txtEdit.Text), Convert.ToInt32(Session["UserdistrictId"].ToString()), Convert.ToInt64(txtPetroCardNumber.Text), Convert.ToInt32(ddlAgency.SelectedValue), Convert.ToInt32(ddlCardType.SelectedValue), Convert.ToDateTime(txtValidityEndDate.Text), Convert.ToInt32(dd_listFe.SelectedValue), Convert.ToDateTime(txtIssuedDate.Text), Convert.ToInt32(ddlVehicles.SelectedValue), Convert.ToInt32(ddlFeuserDistrict.SelectedValue));
                FillGridPetroCard();
            }
        }







        private void InsPetroCardIssueDetails(int DistrictID, long PetroCardNum, int AgencyID, int CardTypeID, DateTime ValidityEndDate, int IssuedToFE, DateTime PetroCardIssuedDate, int Status, int CreatedBy, DateTime CreatedDate, int UpdatedBy, DateTime UpdatedDate, int VehicleID, int UserDistrictID)
        {

            int res = objFuelMan.IInsPetroCardIssueDetails(DistrictID, PetroCardNum, AgencyID, CardTypeID, ValidityEndDate, IssuedToFE, PetroCardIssuedDate, Status, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, VehicleID, UserDistrictID);

            if (res == 1)
            {
                string strFmsScript = "Petro Card ISSUED";
                Show(strFmsScript);


            }
            else
            {
                string strFmsScript = "Petro Card Already Issued to a District";
                Show(strFmsScript);

            }
            FillGridPetroCard();
            Clearfields();

        }







        private void UpdPetroCardIssueDetails(int PetroCardIssueID, int DistrictID, long PetroCardNum, int AgencyID, int CardTypeID, DateTime ValidityEndDate, int IssuedToFE, DateTime PetroCardIssuedDate, int VehicleID, int UserDistrictID)
        {

            int res = objFuelMan.IUpdPetroCardIssueDetails(PetroCardIssueID, DistrictID, PetroCardNum, AgencyID, CardTypeID, ValidityEndDate, IssuedToFE, PetroCardIssuedDate, VehicleID, UserDistrictID);
            if (res == 1)
            {
                string strFmsScript = "Petro Card UPDATED";
                Show(strFmsScript);


            }
            else
            {
                string strFmsScript = "Failure";
                Show(strFmsScript);

            }
            FillGridPetroCard();
            Clearfields();

        }








        private void FillGridPetroCard()
        {
            int DistrictID = -1;

            if (Session["UserdistrictId"] != null)
            {
                DistrictID = Convert.ToInt32(Session["UserdistrictId"].ToString());
            }
            DataSet ds = new DataSet();
            ds = objFuelMan.IFillGridPetroCard(DistrictID);
            gvPetroCardIssue.DataSource = ds;
            gvPetroCardIssue.DataBind();
            foreach (GridViewRow item in gvPetroCardIssue.Rows)
            {
                if (item.Cells[2].Text != "&nbsp;")
                {
                    ((LinkButton)item.FindControl("lnkEdit")).Enabled = false;
                    ((LinkButton)item.FindControl("lnDeactivate")).Enabled = false;
                }
            }
        }







        protected void gvPetroCardIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPetroCardIssue.PageIndex = e.NewPageIndex;
            int DistrictID = -1;

            if (Session["UserdistrictId"] != null)
            {
                DistrictID = Convert.ToInt32(Session["UserdistrictId"].ToString());
            }
            DataSet ds = new DataSet();
            ds = objFuelMan.IFillGridPetroCard(DistrictID);

            gvPetroCardIssue.DataSource = ds;
            gvPetroCardIssue.DataBind();

            foreach (GridViewRow item in gvPetroCardIssue.Rows)
            {
                if (item.Cells[2].Text != null)
                {
                    ((LinkButton)item.FindControl("lnkEdit")).Enabled = false;
                    ((LinkButton)item.FindControl("lnDeactivate")).Enabled = false;
                }
            }
            FillGridPetroCard();
        }








        protected void gvPetroCardIssue_RowEditing(object sender, GridViewEditEventArgs e)
        {







        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        private void Clearfields()
        {

            txtPetroCardNumber.Text = "";
            ddlAgency.SelectedIndex = 0;
            ddlCardType.SelectedIndex = 0;
            txtValidityEndDate.Text = "";
            dd_listFe.SelectedIndex = 0;
            txtIssuedDate.Text = "";
            btSave.Text = "Save";
            ddlVehicles.SelectedIndex = 0;
            ddlFeuserDistrict.SelectedIndex = 0;
            ddlVehicles.Enabled = false;
        }

        protected void gvPetroCardIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }

        protected void gvPetroCardIssue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                FillDistricts();
                btSave.Text = "Update";


                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DataSet ds = new DataSet();
                ds = objFuelMan.IEditPetroCardDetails(id);

                txtEdit.Text = Convert.ToString(id);
                //ddlDistricts.SelectedValue = ds.Tables[0].Rows[0]["DistrictID"].ToString();
                //int did = Convert.ToInt16(ddlDistricts.SelectedValue);
                txtPetroCardNumber.Text = ds.Tables[0].Rows[0]["PetroCardNum"].ToString();
                FillAgency();
                ddlAgency.SelectedValue = ds.Tables[0].Rows[0]["AgencyID"].ToString();
                ddlCardType.SelectedValue = ds.Tables[0].Rows[0]["CardTypeID"].ToString();
                txtValidityEndDate.Text = ds.Tables[0].Rows[0]["ValidEndDate"].ToString();
                //txtIssuedToFE.Text = ds.Tables[0].Rows[0]["IssuedToFE"].ToString();
                dd_listFe.SelectedValue = ds.Tables[0].Rows[0]["IssuedToFE"].ToString();
                txtIssuedDate.Text = ds.Tables[0].Rows[0]["PetroCardIssDate"].ToString();
                FillVehicles();
                FillDistricts();
                ddlVehicles.SelectedValue = ds.Tables[0].Rows[0]["IssuedToVehicle"].ToString();
                ddlFeuserDistrict.SelectedValue = ds.Tables[0].Rows[0]["IssuedToDistrict"].ToString();


            }
            else if (e.CommandName == "Delete")
            {
                int result = 0;

                int id = Convert.ToInt32(e.CommandArgument.ToString());

                result = objFuelMan.IDeletePetroCardDetails(id);
                if (result == 1)
                {
                    string strFmsScript = "Petro Card Deactivated";
                    Show(strFmsScript);


                }
                else
                {
                    string strFmsScript = "Failure";
                    Show(strFmsScript);

                }
                FillGridPetroCard();
                Clearfields();
            }
            else
            {

            }
        }



        protected void dd_listFe_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillUserDistrictandVehicle();
        }

        private void FillUserDistrictandVehicle()
        {
            DataSet ds = new DataSet();
            ds = objFuelMan.IGetDistrictforUser(Convert.ToInt32(dd_listFe.SelectedValue));
            ddlFeuserDistrict.DataSource = ds.Tables[0];
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFeuserDistrict.DataValueField = "ds_dsid";
                ddlFeuserDistrict.DataTextField = "ds_lname";
                ddlFeuserDistrict.DataBind();


                ddlFeuserDistrict.SelectedIndex = 0;
            }
            else
            {
                Show("Please map the Fe to some District and then issue Petro Cards");
            }


            DataSet da = new DataSet();
            da = objFuelMan.IGetVehiclesforUser(Convert.ToInt32(dd_listFe.SelectedValue));
            ddlVehicles.DataSource = da.Tables[0];
            ddlVehicles.DataValueField = "VehicleID";
            ddlVehicles.DataTextField = "VehicleNumber";
            ddlVehicles.DataBind();
            ddlVehicles.Items.Insert(0, "--Select--");
            //ddlAgency.Items[0].Value = "0";
            ddlVehicles.SelectedIndex = 0;
            ddlVehicles.Enabled = true;


        }


        private void FillVehicles()
        {
            int districtID = -1;

            if (Session["UserdistrictId"] != null)
            {
                districtID = Convert.ToInt32(Session["UserdistrictId"].ToString());
            }
            fmsg.UserDistrictId = districtID;
            DataSet ds = fmsg.GetVehicleNumberPetroCardEdit();


            //ds = objFuelEntry.IFillVehicles(districtID);
            ddlVehicles.DataSource = ds.Tables[0];
            ddlVehicles.DataValueField = "VehicleID";
            ddlVehicles.DataTextField = "VehicleNumber";
            ddlVehicles.DataBind();
            ddlVehicles.Items.Insert(0, "--Select--");
            //ddlVehicles.Items[0].Value = "0";
            ddlVehicles.SelectedIndex = 0;
            ddlVehicles.Enabled = false;



        }

    }
}