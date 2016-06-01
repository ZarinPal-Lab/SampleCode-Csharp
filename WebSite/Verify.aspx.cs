using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Verify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null) {
            if(Request.QueryString["Status"].ToString().Equals("OK")) {
                int Amount = 100;
                long RefID;
                System.Net.ServicePointManager.Expect100Continue = false;
                Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();

                int Status = zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE", Request.QueryString["Authority"].ToString(), Amount, out RefID);

                if(Status == 100) {
                    Response.Write("Success!! RefId: " + RefID);
                } else {
                    Response.Write("Error!! Status: " + Status);
                }

            } else {
                Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
            }
        } else {
            Response.Write("Invalid Input");
        }
    }
}