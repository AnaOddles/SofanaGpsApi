using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SofanaGPSApi.AuthAttribute
{

    /// <summary>
    /// Registering the BasicAuth Attribute as a filter attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] // Can be used on class and method level
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Custom basic auth attribute for authentication
        /// </summary>
        public BasicAuthAttribute()
            : base(typeof(BasicAuthorizeFilter)) {}
    }
}