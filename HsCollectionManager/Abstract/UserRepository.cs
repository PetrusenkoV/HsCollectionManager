using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public class UserRepository : IUserRepository
    {
        string connectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public int GetUserId(string userName)
        {
            int result = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select id from hsdb.dbo.users where name = @name", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@name", userName);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = (int)reader["id"];
                    }
                }
            }
            return result;
        }
        public bool InsertUser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Insert into users (name) values (@name)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@name", userName);
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }
            }
        }
    }
}