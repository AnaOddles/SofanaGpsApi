using System.Collections.Generic;
using SofanaGPSApi.Models;

namespace SofanaGPSApi_Testing
{
    public class LocationMockData
    {
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

        public static List<Location> GetEmptyLocations()
        {
            return new List<Location>();
        }

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