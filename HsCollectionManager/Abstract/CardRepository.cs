using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public class CardRepository : ICardRepository
    {
        string connectionString =
                "Data Source=VPETRUSENKO\\SQLEXPRESS;Initial Catalog=HsDb;Integrated Security=True";

        public List<Card> GetAllCards(int page, int pageSize)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost " +
                                                           "From cards " +
                                                           "Order By manacost, name " +
                                                           "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only", connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("page", page);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

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
        public List<Card> GetUserCards(int userId, int page, int pageSize)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost " +
                                                           "From cards c Inner Join UserCards u " +
                                                           "on c.id = u.CardId " +
                                                           "Where u.UserId = @userId " +
                                                           "Order by manacost, name " +
                                                           "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@page", page);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

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
        public List<Card> GetAllCardsManaCost(int manacost, int page, int pageSize)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost " +
                                                           "From cards " +
                                                           "Where manacost = @manacost " +
                                                           "Order by name " +
                                                           "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@manacost", manacost);
                    command.Parameters.AddWithValue("@page", page);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

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
        public List<Card> GetUserCardsManaCost(int userId, int manacost, int page, int pageSize)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost " +
                                                           "From cards c Inner Join UserCards u " +
                                                           "on c.id = u.CardId " +
                                                           "Where u.UserId = @userId and manacost = @manacost " +
                                                           "Order by name " +
                                                           "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@manacost", manacost);
                    command.Parameters.AddWithValue("@page", page);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

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
        public List<Card> GetUserCardsMoreThenSevenManaCost(int userId, int page, int pageSize)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select name, category, rarity, img, manacost " +
                                                           "From cards c Inner Join UserCards u " +
                                                           "on c.id = u.CardId " +
                                                           "Where u.UserId = @userId and manacost >= 7" +
                                                           "Order by manacost, name " +
                                                           "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@page", page);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

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
        public int GetCardId(string cardImg)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select id " +
                                                           "from cards " +
                                                           "where img = @img", connection))
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
                using (SqlCommand command = new SqlCommand("Insert into UserCards (UserId, CardId) values " +
                                                           "(@userId, @cardId)", connection))
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
                using (SqlCommand command = new SqlCommand("Delete from UserCards " +
                                                           "Where UserId = @userId AND CardId = @cardId", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@cardId", cardId);

                    command.ExecuteNonQuery();
                }
            }
        }
        public int AmountOfCards()
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select Count(img) " +
                                                           "From cards", connection))
                {
                    connection.Open();

                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }
        public int AmountOfUserCards(int userId)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select Count(img) " +
                                                           "From cards c Inner Join UserCards u " +
                                                           "on c.id = u.CardId" +
                                                           " Where u.UserId = @userId", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);

                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }

        public int AmountOfAllCardsManaCost(int manacost)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select Count(img) " +
                                                           "From cards c Inner Join UserCards u " +
                                                           "on c.id = u.CardId " +
                                                           "Where manacost = @manacost", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@manacost", manacost);

                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }
        public int AmountOfUserCardsManaCost(int userId, int manacost)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select Count(img) From cards c Inner Join UserCards u on c.id = u.CardId Where u.UserId = @userId and manacost = @manacost", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@manacost", manacost);

                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }
        public int AmountOfUserCardsMoreThenSevenManaCost(int userId)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select Count(img) From cards c Inner Join UserCards u on c.id = u.CardId Where u.UserId = @userId and manacost >= 7", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userid", userId);

                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }

    }
}