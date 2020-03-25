﻿using ClimateObservations.Models;
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
        public static int AddObservation(DateTime date, int observer_id, int geolocation_id)
        {
            string statement = "insert into observation(date, observer_id, geolocation_id) values(@date,@observer_id,@geolocation_id) returning id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
                    int observationid = ObservationExists(date, geolocation_id, observer_id);
                    if (observationid != -1)
                    {
                        return observationid;
                    }
                    command.Parameters.AddWithValue("date", date);
                    command.Parameters.AddWithValue("observer_id", observer_id);
                    command.Parameters.AddWithValue("geolocation_id", geolocation_id);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion
        #region READ
        public static Observation GetObservation(int id)
        {
            string stmt = "select id, date from observation where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation observation = null;
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
                return observation;
            }
        }
        public static int ObservationExists(DateTime date, int geolocation_id, int observer_id)
        {
            string stmt = "select id from observation where date=@date AND geolocation_id=@geolocation_id AND observer_id=@observer_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation observation = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("date", date);
                    command.Parameters.AddWithValue("geolocation_id", geolocation_id);
                    command.Parameters.AddWithValue("observer_id", observer_id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            observation = new Observation
                            {
                                Id=(int)reader["id"],
                            };
                        }
                    }
                }
                if (observation != null)
                {
                    return observation.Id;
                }
                return -1;
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

        }
        #endregion
        #region DELETE
        public static void DeleteObservations(int observer_id)
        {
            string stmt = "DELETE FROM observations WHERE observer_id = @observer_id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, connection))
                        {
                            command.Parameters.AddWithValue("observer_id", observer_id);
                            command.ExecuteScalar();
                        }
                        transaction.Commit();
                    }
                    catch (PostgresException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion
    }
}
