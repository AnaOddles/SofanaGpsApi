using System.Collections.Generic;
using SofanaGPSApi.Models;

namespace SofanaGPSApi_Testing
{
    /// <summary>
    /// Mock Data for integration testing Location modules
    /// </summary>
    public class LocationMockData
    {
        /// <summary>
        /// Mock data for Get() endpoint
        /// </summary>
        /// <returns>List<Location></returns>
        public static List<Location> GetLocations()
        {
            return new List<Location> {
                new Location{
                    Id = "624f77b47f7eee1c98bee1cg",
                    lon = "-112.130393",
                    lat = "33.513111",
                    cartId = 0,
                    dateTime = "04/09/2022 01:08:59"
                },
                new Location{
                    Id = "624f77b47f7eee1c98bee1cg",
                    lon = "-112.1303678",
                    lat = "34.513178",
                    cartId = 1,
                    dateTime = "04/09/2022 01:10:03"
                },
            };
        }

        /// <summary>
        /// Mock data for Get() endpoint on empty response
        /// </summary>
        /// <returns>List<Location></returns>
        public static List<Location> GetEmptyLocations()
        {
            return new List<Location>();
        }

        /// <summary>
        /// Mock data for the GetAllWithCartId() endpoint
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>List<Location></returns>
        public static List<Location> GetAllWithCartId(int cartId)
        {
            return new List<Location> {
                new Location
                {
                    Id = "624f77b47f7eee1c98bee1cg",
                    lon = "-112.130393",
                    lat = "33.513111",
                    cartId = cartId,
                    dateTime = "04/09/2022 01:08:59"
                },
                new Location
                {
                    Id = "624f77b47f7eee1c98bee1cg",
                    lon = "-112.1303678",
                    lat = "34.513178",
                    cartId = cartId,
                    dateTime = "04/09/2022 01:10:03"
                },
            };

        }

        /// <summary>
        /// Mock data for the Get(string id) endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Location></returns>
        public static List<Location> Get(string id)
        {
            return new List<Location> {
                new Location
                {
                    Id = id,
                    lon = "-112.130393",
                    lat = "33.513111",
                    cartId = 0,
                    dateTime = "04/09/2022 01:08:59"
                }            
            };
        }

        /// <summary>
        /// Mock data for the Craete() endpoint
        /// </summary>
        /// <returns>Location</returns>
        public static Location NewLocation()
        {
            return new Location
            {
                Id = "624f77b47f7eee1c98bee1cg",
                lon = "-112.130393",
                lat = "33.513111",
                cartId = 0,
                dateTime = "04/09/2022 01:08:59"
            };
        }
    }
}