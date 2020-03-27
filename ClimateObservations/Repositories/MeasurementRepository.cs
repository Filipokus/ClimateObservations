using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using static ClimateObservations.Repositories.ObservationRepository;

namespace ClimateObservations.Repositories
{
    public static class MeasurementRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;
        #region CREATE
        public static int AddMeasurement(int observer_id, int geolocation_id, double value, int category_id)
        {
            DateTime date = DateTime.Today;
            int observation_id = AddObservation(date, observer_id, geolocation_id);
            string statement = "insert into measurement(value, observation_id, category_id) values(@value,@observation_id,@category_id) returning id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
                    command.Parameters.AddWithValue("value", value);
                    command.Parameters.AddWithValue("observation_id", observation_id);
                    command.Parameters.AddWithValue("category_id", category_id);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion
    }
}
