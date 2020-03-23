using ClimateObservations.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ClimateObservations.Repositories
{
    class CategoryRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;

        public static IEnumerable<Category> GetCategories()
        {
            string stmt = "select id, name from category";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Category category;
                List<Category> categories = new List<Category>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            category = new Category
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                            };
                            categories.Add(category);
                        }
                    }
                }
                return categories;
            }
        }
    }
}
