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

        public static IEnumerable<Category> GetAllCategories()
        {
            string stmt = "select id, name, basecategory_id from category";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Category category = null;
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
        public static IEnumerable<Category> GetParentCategories()
        {
            string stmt = "SELECT id, name, basecategory_id FROM category WHERE basecategory_id IS NOT NULL AND basecategory_id != 5";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Category category = null;
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
        public static IEnumerable<Category> GetChildCategories(int basecategory_id)
        {
            string stmt = "SELECT id, name, basecategory_id FROM category WHERE basecategory_id=@basecategory_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Category category = null;
                List<Category> categories = new List<Category>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("basecategory_id", basecategory_id);
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
