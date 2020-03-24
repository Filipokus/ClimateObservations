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
        public static IEnumerable<Observation> GetObservationsWithDetails(int id)
        {
            string stmt = "SELECT u.name AS categoryname, u.basecategory_id, u.unit_id, observation.date, observation.observer_id, measurement.id AS measurementid, measurement.value, measurement.observation_id, measurement.category_id, observation.id AS observationid, u.id AS categoryid, area.id AS areaid, area.name AS areaname FROM observer INNER JOIN observation ON observer.id = observation.observer_id INNER JOIN measurement ON observation.id = measurement.observation_id INNER JOIN category o ON measurement.category_id=o.id INNER JOIN category u ON u.basecategory_id=o.id INNER JOIN geolocation ON geolocation.id = observation.geolocation_id INNER JOIN area ON area.id = geolocation.area_id WHERE observer.id=@id";

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
                                Id = (int)reader["observationid"],
                                Date = (DateTime)reader["date"],
                                ObserverId = (int)reader["observer_id"],
                            };
                            observation.Measurements.Add(new Measurement
                            {
                                Id = (int)reader["measurementid"],
                                Value = (double)reader["value"],
                                ObservationId = (int)reader["observation_id"],
                                CategoryId = (int)reader["category_id"],
                            });
                            observation.Categories.Add(new Category
                            {
                                Id = (int)reader["categoryid"],
                                Name = (string)reader["categoryname"],
                                BaseCategoryId = (int)reader["basecategory_id"],
                                UnitId = (int)reader["unit_id"],
                            });
                            observation.Areas.Add(new Area
                            {
                                Id = (int)reader["areaid"],
                                Name = (string)reader["areaname"],
                            });
                            observations.Add(observation);
                        }
                    }
                }
                return observations;
            }
        }
        #endregion
        #region UPDATE
        public static void UpdateObservation(Observation observation)
        {
            string stmt = "select id, date from observation where observer_id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand())
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
