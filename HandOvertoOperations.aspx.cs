using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GvkFMSAPP.PL.Prior_MaintenanceStage
{
    public partial class HandOvertoOperations : System.Web.UI.Page
    {
        GvkFMSAPP.BLL.HandOvertoOperations handovertooperation = new GvkFMSAPP.BLL.HandOvertoOperations();
        GvkFMSAPP.BLL.FMSGeneral fmsGeneral = new GvkFMSAPP.BLL.FMSGeneral();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] == null)
            {
                Response.Redirect("Error.aspx");
            }

            if (!IsPostBack)
            {
                DataSet dsPerms = (DataSet)Session["PermissionsDS"];
                dsPerms.Tables[0].DefaultView.RowFilter = "Url='" + Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1] + "'";
                PagePermissions p = new PagePermissions(dsPerms, dsPerms.Tables[0].DefaultView[0]["Url"].ToString(), dsPerms.Tables[0].DefaultView[0]["Title"].ToString());

                btHandover.Attributes.Add("onclick", "return validation()");
                GetVehicleNumber();
                //GetHandoverTo();
                //GetHandoverBy();

                pnlHandOverToOperation.Visible = false;

                if (p.View == true)
                {
                }
                if (p.Add == true)
                {
                    pnlHandOverToOperation.Visible = true;
                }
                if (p.Modify == true)
                {
                }
                if (p.Approve == true)
                {
                }
            }
        }
        public void GetVehicleNumber()
        {
            DataSet ds = handovertooperation.GetVehicleNumber();//FMS.BLL.HandOvertoOperations.GetVehicleNumber();
            if (ds != null)
            {
                ddlVehicleNo.Items.Clear();
                ddlVehicleNo.DataSource = ds.Tables[0];
                ddlVehicleNo.DataValueField = "VehicleID";
                ddlVehicleNo.DataTextField = "VehicleNumber";
                ddlVehicleNo.DataBind();
                ddlVehicleNo.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }
        //public void GetHandoverTo()
        //{
        //    DataSet ds = handovertooperation.GetHandoverTo();//FMS.BLL.HandOvertoOperations.GetHandoverTo();
        //         if (ds != null)
        //        {

        //        ddlHandOverto.DataSource = ds.Tables[0];
        //        ddlHandOverto.DataValueField = "HandOverToOwnerID";
        //        ddlHandOverto.DataTextField = "HandOverTo";
        //        ddlHandOverto.DataBind();
        //        ddlHandOverto.Items.Insert(0, new ListItem("--Select--", "0"));
        //    }       
        //}
        //public void GetHandoverBy()
        //{
        //    DataSet ds = handovertooperation.GetHandoverBy();//FMS.BLL.HandOvertoOperations.GetHandoverBy();
        //      if (ds != null)
        //      {
        //        ddlHandOverBy.DataSource = ds.Tables[0];
        //        ddlHandOverBy.DataValueField = "HandOverToOwnerID";
        //        ddlHandOverBy.DataTextField = "HandoverBy";
        //        ddlHandOverBy.DataBind();
        //        ddlHandOverBy.Items.Insert(0, new ListItem("--Select--", "0"));
        //      }
        //}

        protected void btHandover_Click(object sender, EventArgs e)
        {
            handovertooperation.VehicleID = int.Parse(ddlVehicleNo.SelectedItem.Value);
            handovertooperation.HandoverTo = txtHandOverto.Text;
            handovertooperation.HandoverDate = DateTime.Parse(txtHandoverDate.Text);
            handovertooperation.HandoverBy = txtHandOverBy.Text;
            handovertooperation.QualityInspectionNo = txtQualityInspectionNo.Text;
            handovertooperation.HTOInspectionDate = DateTime.Parse(txtInspectionDate.Text);
            handovertooperation.HTOInspectionBy = txtInspectedBy.Text;
            handovertooperation.Remarks = txtRemarks.Text;
            int ret = handovertooperation.InsHandoverToOperations();
            
            if (ret == 1)
                Show("Vehicle Handover is Completed");
            else
                Show("Error");

            ClearControls();
            GetVehicleNumber();
        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }

        protected void ClearControls()
        {
            ddlVehicleNo.Items[0].Selected = true;
            //ddlHandOverto.SelectedIndex = 0;
            //ddlHandOverBy.SelectedIndex = 0;
            txtHandOverto.Text = "";
            txtHandOverBy.Text = "";
            txtHandoverDate.Text = "";
            txtInspectedBy.Text = "";
            txtInspectionDate.Text = "";
            txtQualityInspectionNo.Text = "";
            txtRemarks.Text = "";
        }

        protected void ddlVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlVehicleNo.SelectedIndex != 0)
            //{

            //    DataSet dates = fmsGeneral.GetRegistrationDate(int.Parse(ddlVehicleNo.SelectedItem.Value));
            //    DateTime dt = Convert.ToDateTime(dates.Tables[0].Rows[0]["RegDate"].ToString());
            //    vehicleRegistrationDate.Value = dt.ToString();

            //    DateTime Pdt = fmsGeneral.GetPurchaseDate(int.Parse(ddlVehicleNo.SelectedItem.Value));
            //    vehiclePurchaseDate.Value = Pdt.ToString();
            //}
        }
    }
}