using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public class Repository : IRepository
    {
        string connectionString =
                "Data Source=VPETRUSENKO\\SQLEXPRESS;Initial Catalog=HsDb;Integrated Security=True";
        public List<Card> GetAllCards()
        {
            List<Card> result = new List<Card>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost From hsdb.dbo.cards", connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Card
                        {
                            Name = (string) reader["name"],
                            ManaCost = (int) reader["manacost"],
                            Category = (string) reader["category"],
                            Rarity = (string) reader["rarity"],
                            Img = (string) reader["img"]
                        });
                    }
                }
            }
            return result;
        }

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

        public int GetCardId(string cardImg)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select id from cards where img = @img", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@img", cardImg);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = (int)reader["id"];
                    }
                }
            }

            return result;
        }

        public void InsertUserCard(int userId, int cardId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Insert into UserCards (UserId, CardId) values (@userId, @cardId)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@cardId", cardId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveUserCard(int userId, int cardId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Delete from UserCards Where UserId = @userId AND CardId = @cardId", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@cardId", cardId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Card> GetUserCards(int userId)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost From cards c Inner Join UserCards u on c.id = u.CardId Where u.UserId = @userId", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Card
                        {
                            Name = (string)reader["name"],
                            ManaCost = (int)reader["manacost"],
                            Category = (string)reader["category"],
                            Rarity = (string)reader["rarity"],
                            Img = (string)reader["img"]
                        });
                    }
                }
            }
            return result;

        }

        public List<Card> GetUserCardsManaCost(int userId, int manacost)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost From cards c Inner Join UserCards u on c.id = u.CardId Where u.UserId = @userId and manacost = @manacost", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@manacost", manacost);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Card
                        {
                            Name = (string)reader["name"],
                            ManaCost = (int)reader["manacost"],
                            Category = (string)reader["category"],
                            Rarity = (string)reader["rarity"],
                            Img = (string)reader["img"]
                        });
                    }
                }
            }
            return result;
        }

        public List<Card> GetUserCardsMoreThenSevenManaCost(int userId)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost From cards c Inner Join UserCards u on c.id = u.CardId Where u.UserId = @userId and manacost >= 7", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Card
                        {
                            Name = (string)reader["name"],
                            ManaCost = (int)reader["manacost"],
                            Category = (string)reader["category"],
                            Rarity = (string)reader["rarity"],
                            Img = (string)reader["img"]
                        });
                    }
                }
            }
            return result;
        }
    }
}