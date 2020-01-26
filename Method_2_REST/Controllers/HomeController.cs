using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NewZarinPal.Models;

namespace NewZarinPal.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            return View();
            //return RedirectToAction("RequestPayment", "Home");
        }
        public async Task<IActionResult> RequestPayment()
        {
            //SandBox Mode
            //var _url = "https://sandbox.zarinpal.com/pg/rest/WebGate/PaymentRequest.json";
            var _url = "https://www.zarinpal.com/pg/rest/WebGate/PaymentRequest.json";
            
            var _values = new Dictionary<string, string>
                {
                    { "MerchantID", "YOUR-ZARINPAL-MERCHANT-CODE" }, //Change This To work, some thing like this : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
                    { "Amount", "500" }, //Toman
                    { "CallbackURL", "http://localhost:5000/Home/VerifyPayment" },
                    { "Mobile", "CUSTOMER-MOBLIE-NUMBER" }, //Mobile number will be shown in the transactions list as a separate field.
                    { "Description", "This is a test payment" }
                };

            var _paymentRequestJsonValue = JsonConvert.SerializeObject(_values);
            var content = new StringContent(_paymentRequestJsonValue, Encoding.UTF8, "application/json");

            var _response = await client.PostAsync(_url, content);
            var _responseString = await _response.Content.ReadAsStringAsync();

            ViewBag.StatusCode = _response.StatusCode;
            ViewBag._responseString = _responseString;

            ZarinPalRequestResponseModel _zarinPalResponseModel =
             JsonConvert.DeserializeObject<ZarinPalRequestResponseModel>(_responseString);

            if (_response.StatusCode != System.Net.HttpStatusCode.OK) // Post Error
                return View();

            if (_zarinPalResponseModel.Status != 100) //Zarinpal Did not Accepted the payment
                return View();

            //SandBox Mode
            //return Redirect("https://sandbox.zarinpal.com/pg/StartPay/"+_zarinPalResponseModel.Authority/*+"/Sad"*/); 
            
            // [/ُSad] will redirect to the sadad gateway if you already have zarin gate enabled, let's read here
            // https://www.zarinpal.com/blog/زرین-گیت،-درگاهی-اختصاصی-به-نام-وبسایت/
            return Redirect("https://www.zarinpal.com/pg/StartPay/"+_zarinPalResponseModel.Authority/*+"/Sad"*/); 
        }
        public async Task<IActionResult> VerifyPayment(string Authority)
        {
            //SandBox Mode
            //var _url = "https://sandbox.zarinpal.com/pg/rest/WebGate/PaymentVerification.json";
            var _url = "https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json";
            
            var _values = new Dictionary<string, string>
                {
                    { "MerchantID", "YOUR-ZARINPAL-MERCHANT-CODE" }, //Change This To work, some thing like this : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
                    { "Authority", Authority },
                    { "Amount", "500" } //Toman
                };

            var _paymenResponsetJsonValue = JsonConvert.SerializeObject(_values);
            var content = new StringContent(_paymenResponsetJsonValue, Encoding.UTF8, "application/json");

            var _response = await client.PostAsync(_url, content);
            var _responseString = await _response.Content.ReadAsStringAsync();

            ViewBag.StatusCode = _response.StatusCode;
            ViewBag.responseString = _responseString;

            ZarinPalVerifyResponseModel _zarinPalResponseModel =
             JsonConvert.DeserializeObject<ZarinPalVerifyResponseModel>(_responseString);

            return View();
        }
    }
}
