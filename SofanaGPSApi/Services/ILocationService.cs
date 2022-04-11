using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SofanaGPSApi.Models;

namespace SofanaGPSApi.Services
{
    /// <summary>
    /// Interface for the methods for LocationService
    /// </summary>
    public interface ILocationService
    {
        
        public Task<List<Location>> Get();
        public Task<List<Location>> Get(String id);
        public Task<List<Location>> GetAllWithCartId(int cartId);
        public Task<List<Location>> GetLastWithCartId(int cardId);
        public Task<List<Location>> GetLast();
        public Task<List<Location>> GetLastCoordinates();
        public Task<List<Location>> Create(Location location);
        public void Update(string id, Location locationIn);
        public void Remove(string id);
        void Remove(int id);
    }
}
