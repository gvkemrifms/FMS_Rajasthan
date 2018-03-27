using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoSoftGlobal;


namespace GvkFMSAPP.PL
{
    public partial class _Default : System.Web.UI.Page
    {
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
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                Button1_Click(sender, e);
                //TextBox txt1 = new TextBox();
                //AjaxControlToolkit.PopupControlExtender popUp = new AjaxControlToolkit.PopupControlExtender();
                //popUp.TargetControlID = "Panel1";
                //popUp.TargetControlID = "";
            }

            //div1.InnerHtml = CreateChart1();
            //div2.InnerHtml = CreateChart2();
        }
        public string CreateChart1()
        {
            string strXML = "";


            strXML += "<graph caption='Region-Wise Fuel Consumption' YAxisName='Fuel Consumption' numDivLines='6' formatNumberScale='0' decimalPrecision='0' anchorSides='10' anchorRadius='3' anchorBorderColor='009900' rotateNames='1' animation='1' yAxisMinValue='0'>";
            strXML += "<categories>";

            strXML += "<category name='Hyderabad' /> ";
            strXML += "<category name='Nizamabad' /> ";
            strXML += "<category name='Tirupati' /> ";
            strXML += "<category name='Vizag' /> ";
            strXML += "<category name='Vijayawada' /> ";
            strXML += "<category name='Warangal' /> ";

            strXML += "</categories>";


            strXML += "<dataset seriesName='Kilo Meters Traveled' color='8BBA00' showValues='1'>";
            strXML += " <set value='123' color='' link=''/>";
            strXML += " <set value='148' color='' link=''/>";
            strXML += " <set value='223' color='' link=''/>";
            strXML += " <set value='100' color='' link=''/>";
            strXML += " <set value='125' color='' link=''/>";
            strXML += " <set value='178' color='' link=''/>";
            strXML += " </dataset>";

            strXML += "<dataset seriesName='Qty in Lit' color='F6BD0F' showValues='1'>";
            strXML += " <set value='60' color='' link=''/>";
            strXML += " <set value='48' color='' link=''/>";
            strXML += " <set value='72' color='' link=''/>";
            strXML += " <set value='39' color='' link=''/>";
            strXML += " <set value='88' color='' link=''/>";
            strXML += " <set value='78' color='' link=''/>";
            strXML += " </dataset>";

            strXML += "<dataset seriesName='Total Amount' color='AFD8F8' showValues='1'>";
            strXML += " <set value='1980' color='' link=''/>";
            strXML += " <set value='1248' color='' link=''/>";
            strXML += " <set value='972' color='' link=''/>";
            strXML += " <set value='1239' color='' link=''/>";
            strXML += " <set value='1188' color='' link=''/>";
            strXML += " <set value='1578' color='' link=''/>";
            strXML += " </dataset>";

            strXML += "</graph>";

            return FusionCharts.RenderChartHTML("FusionCharts/FCF_MSColumn3D.swf", "", strXML, "myNext", "560", "400", false);
        }

        public string CreateChart2()
        {

            string strXML = "";
            strXML += "<graph  caption='Fuel Consumption by Vehicle Types' animation='1' isSliced='1' formatNumberScale='0' numberSufix='Lit' numberPrefix='' pieSliceDepth='30' decimalPrecision='0' shownames='1'>";


            strXML += "<set name='ALS' value='1000' color='AFD8F8' link=''/>";
            strXML += "<set name='BLS' value='660' color='F6BD0F' link='' isSliced='1' />";
            strXML += "<set name='RLS' value='980' color='D64646' link=''/>";



            strXML += "</graph>";





            //Create the chart - Column 3D Chart with data from strXML variable using dataXML method



            return FusionCharts.RenderChartHTML("FusionCharts/FCF_Pie3D.swf ", "", strXML, "myNext", "560", "400", false);
            //return FusionCharts.RenderChartHTML("../FusionCharts/FCF_Funnel.swf ", "", strXML, "myNext", "560", "400", false);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}