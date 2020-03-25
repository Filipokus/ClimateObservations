using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ClimateObservations.Repositories
{
    public static class GeolocationRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;

        #region CREATE
        public static int AddGeolocation(int area_id)
        {
            string statement = "insert into geolocation(area_id) values(@area_id) returning id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
                    command.Parameters.AddWithValue("area_id", area_id);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion
    }
}
