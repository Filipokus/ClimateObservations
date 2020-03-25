using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ClimateObservations.Repositories
{
    public static class AreaRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;
        public static IEnumerable<Area> GetAreas()
        {
            string statement = "select id, name, country_id from area";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Area area = null;
                List<Area> areas = new List<Area>();
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            area = new Area
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                CountryId = (int)reader["country_id"],
                            };
                            areas.Add(area);
                        }
                    }
                }
                return areas.OrderBy(x => x.Name);
            }
        }
    }
}
