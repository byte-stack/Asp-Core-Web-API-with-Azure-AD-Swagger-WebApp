# Asp-Core-Web-API-with-Azure-AD-Swagger-WebApp
Azure Ad authentication with between Web api &amp; Web app build on Asp core.

Scenario
I have used following scenario while development the project.
EBS provide a service to the customer for which customer pay once in two months. Consider that EBS generate the bill and every bill has its cycle for the end user.  One user is connected to one billing details objects. 
As the business needs a web API which can be consumed on various platform such as mobile/desktop /web integration /web hook etc.
To full fill that need we need to have one web api in place which can deliver the billing details of the user [basic CRUD] operations. 
Important Links:
a.	Web API Swagger URL: https://webapiroot.azurewebsites.net/swagger/index.html
b.	Web app : https://webappaspcore.azurewebsites.net
c.	GitHub Repository[Public]: https://github.com/byte-stack/Asp-Core-Web-API-with-Azure-AD-Swagger-WebApp

A). Web API
This web api has the implementation of basic operations [GET/PUT/POST/DELETE].
a.	Framework: Asp core 2.2.
b.	Auth Type: Azure Active directory [ All Microsoft account/all organization].
c.	CORS enabled 
B). Swagger [For testing the Web Api]
 Swagger is enabled to web api to test the methods. Swagger also configured to use Azure Active directory to communicate to Web api for authorization. 

