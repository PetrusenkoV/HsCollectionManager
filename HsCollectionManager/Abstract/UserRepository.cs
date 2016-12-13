using System.Configuration;
using System.Data.SqlClient;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public class UserRepository : IUserRepository
    {
        readonly string _connectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public UserIdModel GetUserId(string userName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select id from hsdb.dbo.users where name = @name", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@name", userName);

                    return new UserIdModel(command.ExecuteScalar());
                }
            }
        }
        public void InsertUser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Insert into users (name) values (@name)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@name", userName);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}