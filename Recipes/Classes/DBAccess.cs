﻿using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Recipes.Classes
{
    /// <summary>
    /// Helper class for accessing the mySQL database, and performing various
    /// tasks with filtering the data, though this might be split into a new class later
    /// </summary>
    class DBAccess
    {
        /// <summary>
        /// Unsecurely storing the login info
        /// </summary>
        private static string connectionString = "Server=localhost;Database=food;Uid=root;Pwd=a;";

        /// <summary>
        /// For storing a local copy of the data if needed
        /// </summary>
        private DataTable myTable = new DataTable();

        /// <summary>
        /// Selects all ingredients and returns them as a List
        /// </summary>
        /// <returns>List of Ingredients</returns>
        public static List<Ingredient> GetIngredientList()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM ingredient", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ingredients.Add(new Ingredient((int)reader["ingredient_id"],
                                                           (string)reader["name"],
                                                           (string)reader["variant"],
                                                           (int)reader["size"],
                                                           (int)reader["qty"],
                                                           (float)reader["energy"],
                                                           (float)reader["fat"],
                                                           (float)reader["saturates"],
                                                           (float)reader["carbohydrates"],
                                                           (float)reader["sugars"],
                                                           (float)reader["fibre"],
                                                           (float)reader["protein"],
                                                           (float)reader["salt"],
                                                           (string)reader["type"],
                                                           (float)reader["price"]));
                        }
                    }
                }
                return ingredients;
            }
        }
        
        /// <summary>
        /// Supplies the original DataTable for use in the grid
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTable()
        {
            DataTable newTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM ingredient", connection);
                adapter.Fill(newTable);
            }
            return newTable;
        }

        public static DataView Filter(string column, string item)
        {
            DataTable newTable = new DataTable();
            string command = "SELECT * FROM ingredient WHERE " + column + "='" + item + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(newTable);
            }

            return newTable.AsDataView();
        }

        public static DataView SelectSingleColumn(string column, string condition1, string item)
        {
            DataTable newTable = new DataTable();
            string command = "SELECT " + column + " FROM ingredient WHERE " + column + "='" + item + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(newTable);
            }

            return newTable.AsDataView();
        }

        public static DataView QueryDataView(string q)
        {
            DataTable newTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(q, connection);
                adapter.Fill(newTable);
            }

            return newTable.AsDataView();
        }

        public static List<string> QueryList(string q, string columnName)
        {
            DataTable newTable = new DataTable();
            List<string> s = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(q, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s.Add((string)reader[columnName]);
                        }
                    }
                }
            }
            return s;
        }

        public static DataView FilterDV(DataView t1, string c, string i)
        {
            string q = c + " Like '%" + i + "%'";
            t1.RowFilter = q; // query example = "id = 10"

            return t1;
        }
    }
}
