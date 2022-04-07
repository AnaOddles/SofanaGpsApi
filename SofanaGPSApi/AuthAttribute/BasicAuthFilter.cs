using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SofanaGPSApi.Services;
using System;
using System.Text;


namespace SofanaGPSApi.AuthAttribute
{
    public class BasicAuthorizeFilter : IAuthorizationFilter
    {
        //Inject our user service 
        private readonly UserService _userService;
        private readonly ILogger<BasicAuthAttribute> _logger;

        //Constructor - passing in the UserService DI
        public BasicAuthorizeFilter(UserService userService, ILogger<BasicAuthAttribute> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        //Performs the actaul authentication for request
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

        //Helper method to check credentials for authentication
        protected bool IsAuthorized(string username, string password)
        {   
            //Grab the user with the password  
            string encryptedPassword = _userService.Get(username).password;

            //If passed in username does not exist or password is not correct
            if (encryptedPassword != null && BCrypt.Net.BCrypt.Verify(password, encryptedPassword)) {
                _logger.LogInformation("Authentication passed for {0}", username);
                return true;
            }

            _logger.LogInformation("Authentication failed for {0}", username);
            return false;
        }       
    }
}
