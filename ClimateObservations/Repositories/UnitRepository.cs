using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ClimateObservations.Repositories
{
    class UnitRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;

        public static IEnumerable<Unit> GetUnits()
        {
            string stmt = "select id, type, abbreviation from unit";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Unit unit = null;
                List<Unit> units = new List<Unit>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            unit = new Unit
                            {
                                Id = (int)reader["id"],
                                Type = (string)reader["type"],
                                Abbreviation = (string)reader["abbreviation"],
                            };
                            units.Add(unit);
                        }
                    }
                }
                return units;
            }
        }
    }
}
