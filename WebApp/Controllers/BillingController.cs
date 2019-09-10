using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using WebAPI.Business.Models;
using WebApp.Core;


namespace WebApp.Controllers
{
    [Authorize]
    public class BillingController : Controller
    {
        /// <summary>
        /// Index method to return the default view for the controller 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            AuthenticationResult result = null;
            try
            {
                // Because we signed-in already in the WebApp, the userObjectId is know
                string userObjectID = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                // Using ADAL.Net, get a bearer token to access the TodoListService
                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userObjectID, HttpContext.Session));
                ClientCredential credential = new ClientCredential(AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.WebApiResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

            }
            catch (Exception ex)
            {
                //return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                return View(new Core.Models.APIExecutionResult { GetResult = ex.Message });
            }
            return View(new Core.Models.APIExecutionResult());
        }


        /// <summary>
        /// Get method to test the get method of Web api
        /// </summary>
        /// <returns>List of objects</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AuthenticationResult result = null;
            // List<TodoItem> itemList = new List<TodoItem>();

            try
            {
                // Because we signed-in already in the WebApp, the userObjectId is know
                string userObjectID = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                // Using ADAL.Net, get a bearer token to access the TodoListService
                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userObjectID, HttpContext.Session));
                ClientCredential credential = new ClientCredential(Microsoft.AspNetCore.Authentication.AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.WebApiResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

                // Retrieve the user's To Do List.
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, AzureAdOptions.Settings.WebApiBaseAddress + "/api/EBSBilling");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                HttpResponseMessage response = await client.SendAsync(request);

                // Return the To Do List in the view.
                if (response.IsSuccessStatusCode)
                {
                    List<Dictionary<string, string>> responseElements = new List<Dictionary<String, String>>();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    string responseString = await response.Content.ReadAsStringAsync();

                    return View("Index", new Core.Models.APIExecutionResult { GetResult = responseString });
                }

                //
                // If the call failed with access denied, then drop the current access token from the cache, 
                //     and show the user an error indicating they might need to sign-in again.
                //
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ProcessUnauthorized(authContext);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (HttpContext.Request.Query["reauth"] == "True")
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //
                    return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                }

                //
                // The user needs to re-authorize.  Show them a message to that effect.
                //

                //                newItem.Title = "(Sign-in required to view to do list.)";

                //ViewBag.ErrorMessage = "AuthorizationRequired";
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                //return View();
            }

            //
            // If the call failed for any other reason, show the user an error.
            //
            return View("Error");


        }


        public async Task<IActionResult> GetById(long id)
        {
            AuthenticationResult result = null;
            // List<TodoItem> itemList = new List<TodoItem>();

            try
            {
                // Because we signed-in already in the WebApp, the userObjectId is know
                string userObjectID = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                // Using ADAL.Net, get a bearer token to access the TodoListService
                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userObjectID, HttpContext.Session));
                ClientCredential credential = new ClientCredential(AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.WebApiResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

                // Retrieve the user's To Do List.
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, AzureAdOptions.Settings.WebApiBaseAddress + "/api/EBSBilling/" + id);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                HttpResponseMessage response = await client.SendAsync(request);

                // Return the To Do List in the view.
                if (response.IsSuccessStatusCode)
                {
                    List<Dictionary<string, string>> responseElements = new List<Dictionary<String, String>>();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    string responseString = await response.Content.ReadAsStringAsync();

                    return View("Index", new Core.Models.APIExecutionResult { GetByIdResult = responseString });
                }

                //
                // If the call failed with access denied, then drop the current access token from the cache, 
                //     and show the user an error indicating they might need to sign-in again.
                //
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ProcessUnauthorized(authContext);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (HttpContext.Request.Query["reauth"] == "True")
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //
                    return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                }

                //
                // The user needs to re-authorize.  Show them a message to that effect.
                //

                //                newItem.Title = "(Sign-in required to view to do list.)";

                //ViewBag.ErrorMessage = "AuthorizationRequired";
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                //return View();
            }

            //
            // If the call failed for any other reason, show the user an error.
            //
            return View("Error");
        }



        [HttpPost]
        public async Task<IActionResult> Post()
        {
            AuthenticationResult result = null;
            // List<TodoItem> itemList = new List<TodoItem>();
            var Amount = Request.Form["amount"];

            if (!decimal.TryParse(Amount, out _))
            {
                return BadRequest();
            }

            try
            {
                // Because we signed-in already in the WebApp, the userObjectId is know
                string userObjectID = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                // Using ADAL.Net, get a bearer token to access the TodoListService
                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userObjectID, HttpContext.Session));
                ClientCredential credential = new ClientCredential(AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.WebApiResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));


                HttpContent content = new StringContent(JsonConvert.SerializeObject(new BillingDetailModel
                {
                    Amount = Convert.ToDecimal(Amount),
                    BillingCycleId = 1,
                    UserId = 1,
                    CreatedDate = DateTime.Now

                }), System.Text.Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, AzureAdOptions.Settings.WebApiBaseAddress + "/api/EBSBilling");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                request.Content = content;
                HttpResponseMessage response = await client.SendAsync(request);


                // Return the To Do List in the view.
                if (response.IsSuccessStatusCode)
                {
                    List<Dictionary<string, string>> responseElements = new List<Dictionary<String, String>>();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    string responseString = await response.Content.ReadAsStringAsync();

                    return View("Index", new Core.Models.APIExecutionResult { PostResult = "200. Success" });
                }

                //
                // If the call failed with access denied, then drop the current access token from the cache, 
                //     and show the user an error indicating they might need to sign-in again.
                //
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ProcessUnauthorized(authContext);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (HttpContext.Request.Query["reauth"] == "True")
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //
                    return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                }

                ViewBag.ErrorMessage = "AuthorizationRequired";
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);

            }

            return View("Error");
        }


        public async Task<IActionResult> Put()
        {
            AuthenticationResult result = null;
            

            var Amount = Request.Query["putamount"];
            var BillingDetailsId = Request.Query["id"];
            if (!decimal.TryParse(Amount, out _))
            {
                return BadRequest();
            }

            if (!decimal.TryParse(BillingDetailsId, out _))
            {
                return BadRequest();
            }

            try
            {
                // Because we signed-in already in the WebApp, the userObjectId is know
                string userObjectID = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                // Using ADAL.Net, get a bearer token to access the TodoListService
                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userObjectID, HttpContext.Session));
                ClientCredential credential = new ClientCredential(AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.WebApiResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));


                HttpContent content = new StringContent(JsonConvert.SerializeObject(new BillingDetailModel
                {
                    Amount = 1,
                    BillingCycleId = 1,
                    UserId = 1,
                    CreatedDate = DateTime.Now.Date,
                    Id= Convert.ToInt64(1)

                }), System.Text.Encoding.UTF8, "application/json-patch+json");

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, AzureAdOptions.Settings.WebApiBaseAddress + "/api/EBSBilling/1");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                request.Content = content;
                
                HttpResponseMessage response = await client.SendAsync(request);


                // Return the To Do List in the view.
                if (response.IsSuccessStatusCode)
                {
                    List<Dictionary<string, string>> responseElements = new List<Dictionary<String, String>>();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    string responseString = await response.Content.ReadAsStringAsync();

                    return View("Index", new Core.Models.APIExecutionResult { PutResult = "200. Success" });
                }

                //
                // If the call failed with access denied, then drop the current access token from the cache, 
                //     and show the user an error indicating they might need to sign-in again.
                //
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ProcessUnauthorized(authContext);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                
                if (HttpContext.Request.Query["reauth"] == "True")
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //
                    return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                }

                ViewBag.ErrorMessage = "AuthorizationRequired";
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);

            }

            return View("Error");
        }

        
        public async Task<IActionResult> Delete()
        {
            AuthenticationResult result = null;

            var BillingDetailsId = Request.Query["id"];
          

            if (!decimal.TryParse(BillingDetailsId, out _))
            {
                return BadRequest();
            }

            try
            {
                // Because we signed-in already in the WebApp, the userObjectId is know
                string userObjectID = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                // Using ADAL.Net, get a bearer token to access the TodoListService
                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userObjectID, HttpContext.Session));
                ClientCredential credential = new ClientCredential(AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.WebApiResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, AzureAdOptions.Settings.WebApiBaseAddress + "/api/EBSBilling/"+ BillingDetailsId);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

                HttpResponseMessage response = await client.SendAsync(request);


                // Return the To Do List in the view.
                if (response.IsSuccessStatusCode)
                {
                    List<Dictionary<string, string>> responseElements = new List<Dictionary<String, String>>();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    string responseString = await response.Content.ReadAsStringAsync();

                    return View("Index", new Core.Models.APIExecutionResult { DeleteResult = "200. Success" });
                }

                //
                // If the call failed with access denied, then drop the current access token from the cache, 
                //     and show the user an error indicating they might need to sign-in again.
                //
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ProcessUnauthorized(authContext);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (HttpContext.Request.Query["reauth"] == "True")
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //
                    return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
                }

                ViewBag.ErrorMessage = "AuthorizationRequired";
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);

            }

            return View("Error");
        }

        private ActionResult ProcessUnauthorized(AuthenticationContext authContext)
        {

            var WebApiTokens = authContext.TokenCache.ReadItems().Where(a => a.Resource == AzureAdOptions.Settings.WebApiResourceId);
            foreach (TokenCacheItem tci in WebApiTokens)
                authContext.TokenCache.DeleteItem(tci);

            ViewBag.ErrorMessage = "UnexpectedError";

            //return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            return View("Error");
        }
    }
}
