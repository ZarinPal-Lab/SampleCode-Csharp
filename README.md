# SampleCode-Csharp
Sample Code for C# .net And Asp Core

There are two samples for C# (REST and SOAP)

The __Rest__ sample is preferred by __Zarinpal__, by the way you are free to choose between REST and SOAP

### Prerequistics :page_with_curl:
To run sample of __REST__ you must have installed these prerequestics

* [.Net Core 3.1](https://dotnet.microsoft.com/download)

These Edittors are tested : 
* Visual Studio 2019
* VSCode

### Build Instructions :hammer:
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

### Contributing :two_men_holding_hands:

If you had any question or suggestion feel free to use __pull requests__ or __issues__, we are glad to be in touch with you.