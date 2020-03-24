using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ClimateObservations.Repositories
{
    public static class ObservationRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;

        #region CREATE



        #endregion

        #region READ
        public static Observation GetObservation(int id)
        {
            string stmt = "select id, date from observation where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation observation;
                conn.Open();
                using(var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            observation = new Observation
                            {
                                Id = (int)reader["id"],
                                Date = (DateTime)reader["date"],
                            };
                        }
                    }
                }
                return null;
            }
        }
        public static IEnumerable<Observation> GetObservations(int id)
        {
            string stmt = "select id, date from observation where observer_id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation observation = null;
                List<Observation> observations = new List<Observation>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            observation = new Observation
                            {
                                Id = (int)reader["id"],
                                Date = (DateTime)reader["date"],
                            };
                            observations.Add(observation);
                        }
                    }
                }
                return observations;
            }
        }


        #endregion
        #region UPDATE
        public static void SaveObservation(Observation observation)
        {

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using(var command = new NpgsqlCommand())
                {
                    conn.Open();
                }

            }
        }






        #endregion
        #region DELETE
        #endregion
    }
}
