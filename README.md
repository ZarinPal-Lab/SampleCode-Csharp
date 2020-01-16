# SampleCode-Csharp
Sample Code for C# .net And Asp Core

There are two samples for C# (REST and SOAP)

The __Rest__ sample is preferred by __Zarinpal__, by the way you are free to choose between REST and SOAP

### How It works simply :bicyclist:
1. Install Package

* __Newtonsoft.Json__

You can install __Newtonsoft.Json__ with this command 
for VSCode :
```
Dotnet Add Package Newtonsoft.Json
```
for Visual studio 2019 through the nuget or the command below :
```
Install-Package Newtonsoft.Json
```
2. NameSpaces
```
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NewZarinPal.Models;
```

3. PaymentRequest 
```
 public async Task<IActionResult> RequestPayment()
        {
            var _url = "https://www.zarinpal.com/pg/rest/WebGate/PaymentRequest.json";
            var _values = new Dictionary<string, string>
                {
                    { "MerchantID", "YOUR-ZARINPAL-MERCHANT-CODE" }, //Change This To work, some thing like this : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
                    { "Amount", "500" }, //Toman
                    { "CallbackURL", "http://localhost:5000/Home/VerifyPayment" },
                    { "Description", "خرید تست" }
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

            return Redirect("https://www.zarinpal.com/pg/StartPay/"+_zarinPalResponseModel.Authority+"/Sad");
            //return Redirect("https://www.zarinpal.com/pg/StartPay/" + _zarinPalResponseModel.Authority);
        }
```
4. VerifyPayment
```
public async Task<IActionResult> VerifyPayment(string Authority)
        {
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
```
5. Models
```
public class ZarinPalRequestResponseModel
     {
         public int Status { get; set; }
         public string Authority { get; set; }
     }
public class ZarinPalVerifyResponseModel
     {
         public int Status { get; set; }
         public string RefID { get; set; }
     }
```
6. View for both __PaymentRequest__ And __VerifyPayment__
```
<div class="text-center">
    <h6>@ViewBag.StatusCode</h6>
    <h6>@ViewBag.responseString</h6>
</div>
```
### Build Instructions for REST sample project :hammer:

#### Prerequistics :page_with_curl:
To run sample of __REST__ you must have installed these prerequestics

* [.Net Core 3.1](https://dotnet.microsoft.com/download)

These Edittors are tested : 
* Visual Studio 2019
* VSCode

If you desier to run The __Rest__ sample in __VSCode__ remember ro run this command in the terminal after openning the project
```
Dotnet Restore
```
This packages are Installed :
* Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
* Newtonsoft.Json

But only __Newtonsoft.Json__ is neccessary

The __Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation__ make the below line works is the Startup.cs file and is not neccessary in your project:
* services.AddControllersWithViews().AddRazorRuntimeCompilation();

For Visual Studio from the solution explorer simply right click on the project and click __build__

Do not Forget to set the __MerchantID__ in the __HomeController__ before start.

### Contributing and Questions :two_men_holding_hands:

If you had any question or suggestion feel free to use __pull requests__ or __issues__, we are glad to be in touch with you.
