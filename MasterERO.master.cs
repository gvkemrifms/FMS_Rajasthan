using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterERO : System.Web.UI.MasterPage
{
    // DataSet dsPerms;
    public string Username = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            GetUserName();
            Response.AppendHeader("REFRESH", "1200;URL=error.aspx");
            //dsPerms = (DataSet)Session["PermissionsDS"];
            //StringBuilder sb = new StringBuilder();
            //dsPerms.Tables[0].Columns["Parent"].ColumnName = "Level";
            //foreach (DataRow dr in dsPerms.Tables[0].Select("Level=0"))
            //{
            //    sb.Append(GetAllChildren(dr));
            //}        
        }

    }
    protected string GetUserName()
    {

        //Username = "Welcome : " + Session["User_Name"].ToString() + " | Role : " + Session["Role_Name"].ToString() + " | District : " + Session["District_Name"].ToString();
        Username = Session["User_Name"].ToString();

        return Username;
    }
}
