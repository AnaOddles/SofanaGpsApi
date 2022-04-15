using System;
using System.Collections.Generic;
using SofanaGPSApi.Models;

namespace SofanaGPSApi.Services
{
    /// <summary>
    /// User Service Interface utilized for api authentication
    /// </summary>
    public interface IUserService
    {
        public long Get(string username, string password);
        public List<User> Get();
        public User Get(string username);
    }
}
