using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SofanaGPSApi.AuthAttribute
{
    //Registering the BasicAuth Attribute as a filter attribute
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] // Can be used on class and method level
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        public BasicAuthAttribute()
            : base(typeof(BasicAuthorizeFilter)) {}
    }
}
