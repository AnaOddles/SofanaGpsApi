using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SofanaGPSApi.Services;
using SofanaGPSApi.Models;
using System;
using System.Text;


namespace SofanaGPSApi.AuthAttribute
{
    /// <summary>
    /// BasicAuthFilter used for annotiating security on API Controller
    /// </summary>
    public class BasicAuthorizeFilter : IAuthorizationFilter
    {
        //Inject our user service 
        private readonly IUserService _userService;
        private readonly ILogger<BasicAuthAttribute> _logger;

        /// <summary>
        /// Constructor - passing in the UserService DI
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="logger"></param>
        public BasicAuthorizeFilter(IUserService userService, ILogger<BasicAuthAttribute> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        /// <summary>
        /// //Performs the actaul authentication for request
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Grab the auth header from the request
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            // If - valid auth header was passed in
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the encoded username and password 
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                // Decode from Base64 to string 
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                // Split username and password into own variables
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];

                // Check if login is valid 
                if (IsAuthorized(username, password))
                {
                    return;
                }
            }

            //If auth fails -

            // Return authentication type (causes browser to show login dialog)
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";

            // Return unauthorized
            context.Result = new UnauthorizedResult();

        }

        /// <summary>
        /// Helper method to check credentials for authentication
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool"</returns>
        protected bool IsAuthorized(string username, string password)
        {
            //Grab the user if exists with the password  
            User user = _userService.Get(username);

            //If passed in username does not exist or password is not correct
            if (user != null) {
                string encryptedPassword = user.password;
                if (BCrypt.Net.BCrypt.Verify(password, encryptedPassword))
                {
                    _logger.LogInformation("Authentication passed for {0}", username);
                    return true;
                }
            }

            _logger.LogInformation("Authentication failed for {0}", username);
            return false;
        }       
    }
}
