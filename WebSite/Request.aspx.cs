using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRequest_Click(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.Expect100Continue = false;
        Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
        string Authority;

        int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", int.Parse(txtAmount.Text), txtDescription.Text.ToString(), "you@yoursite.com", "09123456789", "http://localhost/Verify.aspx", out Authority);

        if(Status == 100) {
            Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
        } else {
            Response.Write("error: " + Status);
        }
    }
}