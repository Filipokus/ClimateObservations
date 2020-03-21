using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ClimateObservations.Repositories
{
    public class ObserverRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;
        #region CREATE
        public static int AddObserver(string firstname, string lastname)
        {
            string statement = "insert into observer(firstname, lastname) values(@firstname,@lastname) returning id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
                    command.Parameters.AddWithValue("firstname", firstname);
                    command.Parameters.AddWithValue("lastname", lastname);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
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
        public static IEnumerable<Observer> GetObservers()
        {
            string statement = "select id, firstname, lastname from observer";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Observer observer = null;
                List<Observer> observers = new List<Observer>();
                connection.Open();
                using (var command = new NpgsqlCommand(statement, connection))
                {
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
                            observers.Add(observer);
                        }
                    }
                }
                return observers.OrderBy(x => x.Lastname);
            }
        }
        #endregion
        #region UPDATE
        #endregion
        #region DELETE
        public static void DeleteObserver(int id)
        {
            string stmt = "DELETE FROM observer WHERE id = @id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteScalar();
                }
            }
        }

        public static void Delete(object poco)
        {

        }
        #endregion
    }
}
