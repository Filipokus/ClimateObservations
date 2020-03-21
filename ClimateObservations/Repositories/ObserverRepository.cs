using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ClimateObservations.Repositories
{
    class ObserverRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;
        #region CREATE

        #endregion
        #region READ
        public static Observer GetObserver(int id)
        {
            string statement = "select id, firstname, lastname from observer where id=@id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Observer observer = null;
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            observer = new Observer
                            {
                                Id = (int)reader["id"],
                                Firstname = (string)reader["firstname"],
                                Lastname = (string)reader["lastname"],
                            };
                        }
                    }
                }
                return observer;
            }
        }
        #endregion
        #region UPDATE

        #endregion
        #region DELETE

        #endregion
    }
}
