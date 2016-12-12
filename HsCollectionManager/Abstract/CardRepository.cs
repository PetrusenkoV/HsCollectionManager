using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public class CardRepository : ICardRepository
    {
        readonly string _connectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;


        //public List<Card> GetAllCardsManaCost(int manacost, int page, int pageSize)
        //{
        //    var result = new List<Card>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("If (@manacost < 7) Begin " +
        //                                                   "If (@manacost = -1) Begin " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards " +
        //                                                   "Where manacost = @manacost " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end else begin " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards " +
        //                                                   "Where manacost >= 7 " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end", connection))
        //        {
        //            connection.Open();
        //            command.Parameters.AddWithValue("@manacost", manacost);
        //            command.Parameters.AddWithValue("@page", page);
        //            command.Parameters.AddWithValue("@pageSize", pageSize);

        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                result.Add(new Card
        //                {
        //                    Name = (string)reader["name"],
        //                    ManaCost = (int)reader["manacost"],
        //                    Category = (string)reader["category"],
        //                    Img = (string)reader["img"]
        //                });
        //            }
        //        }
        //    }
        //    return result;
        //}
        //public List<Card> GetUserCardsManaCost(int userId, int manacost, int page, int pageSize)
        //{
        //    List<Card> result = new List<Card>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("If (@manacost < 7) Begin " +
        //                                                   "If (@manacost = -1) begin " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards c Inner Join UserCards u " +
        //                                                   "on c.id = u.CardId " +
        //                                                   "Where u.UserId = @userId " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards c Inner Join UserCards u " +
        //                                                   "on c.id = u.CardId " +
        //                                                   "Where u.UserId = @userId and manacost = @manacost " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end else begin " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards c Inner Join UserCards u " +
        //                                                   "on c.id = u.CardId " +
        //                                                   "Where u.UserId = @userId and manacost >= 7 " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end", connection))
        //        {
        //            connection.Open();
        //            command.Parameters.AddWithValue("@userid", userId);
        //            command.Parameters.AddWithValue("@manacost", manacost);
        //            command.Parameters.AddWithValue("@page", page);
        //            command.Parameters.AddWithValue("@pageSize", pageSize);

        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                result.Add(new Card
        //                {
        //                    Name = (string)reader["name"],
        //                    ManaCost = (int)reader["manacost"],
        //                    Category = (string)reader["category"],
        //                    Img = (string)reader["img"]
        //                });
        //            }
        //        }
        //    }
        //    return result;
        //}

        //public List<Card> GetAllCardsCategoryManacost(string category, int manacost, int page, int pageSize)
        //{
        //    List<Card> result = new List<Card>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("If (@manacost < 7) Begin " +
        //                                                   "if (@manacost = -1) begin " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards " +
        //                                                   "Where category = @category " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards " +
        //                                                   "Where category = @category AND manacost = @manacost " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end else begin " +
        //                                                   "Select name, category, img, manacost " +
        //                                                   "From cards " +
        //                                                   "Where category = @category AND manacost >= 7 " +
        //                                                   "Order by manacost, name " +
        //                                                   "Offset (@page - 1) * @pageSize Rows Fetch next @pageSize Rows only " +
        //                                                   "end", connection))
        //        {
        //            connection.Open();
        //            command.Parameters.AddWithValue("@manacost", manacost);
        //            command.Parameters.AddWithValue("@page", page);
        //            command.Parameters.AddWithValue("@pageSize", pageSize);
        //            command.Parameters.AddWithValue("@category", category);

        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                result.Add(new Card
        //                {
        //                    Name = (string)reader["name"],
        //                    ManaCost = (int)reader["manacost"],
        //                    Category = (string)reader["category"],
        //                    Img = (string)reader["img"]
        //                });
        //            }
        //        }
        //    }
        //    return result;
        //}

        public List<Card> GetUserCardsCategoryManacost(int userId, string category, int manacost, bool isEditible, int page, int pageSize)
        {
            List<Card> result = new List<Card>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(QueryBuilderCards(userId, category, manacost, isEditible, page, pageSize), connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Card
                        {
                            Name = (string)reader["name"],
                            ManaCost = (int)reader["manacost"],
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

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Insert into UserCards (UserId, CardId) values " +
                                                           "(@userId, @cardId)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@cardId", cardId);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        //donothing
                    }
                }
            }
        }
        public void RemoveUserCard(int userId, int cardId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        //public int AmountOfAllCardsManaCost(int manacost)
        //{
        //    int result;

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("if(@manacost < 7) " +
        //                                                   "begin " +
        //                                                   "if(@manacost = -1) begin " +
        //                                                   "select Count(img) " +
        //                                                   "From cards " +
        //                                                   "end " +
        //                                                   "Select Count(img) " +
        //                                                   "From cards " +
        //                                                   "Where manacost = @manacost " +
        //                                                   "end " +
        //                                                   "else " +
        //                                                   "begin " +
        //                                                   "select Count(img) " +
        //                                                   "From cards " +
        //                                                   "where manacost >= 7 " +
        //                                                   "end", connection))
        //        {
        //            connection.Open();
        //            command.Parameters.AddWithValue("@manacost", manacost);

        //            result = (int)command.ExecuteScalar();
        //        }
        //    }
        //    return result;
        //}
        //public int AmountOfUserCardsManaCost(int userId, int manacost)
        //{
        //    int result;

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("if(@manacost < 7) " +
        //                                                   "begin " +
        //                                                   "if(@manacost = -1) " +
        //                                                   "begin " +
        //                                                   "Select Count(img) " +
        //                                                   "From cards c Inner Join UserCards u " +
        //                                                   "on c.id = u.CardId " +
        //                                                   "Where u.UserId = @userId " +
        //                                                   "end " +
        //                                                   "Select Count(img) " +
        //                                                   "From cards c Inner Join UserCards u " +
        //                                                   "on c.id = u.CardId " +
        //                                                   "Where u.UserId = @userId and manacost = @manacost " +
        //                                                   "end " +
        //                                                   "else " +
        //                                                   "begin " +
        //                                                   "Select Count(img) " +
        //                                                   "From cards c Inner Join UserCards u " +
        //                                                   "on c.id = u.CardId " +
        //                                                   "Where u.UserId = @userId and manacost >= 7 " +
        //                                                   "end ", connection))
        //        {
        //            connection.Open();
        //            command.Parameters.AddWithValue("@userid", userId);
        //            command.Parameters.AddWithValue("@manacost", manacost);

        //            result = (int)command.ExecuteScalar();
        //        }
        //    }
        //    return result;
        //}

        //public int AmountOfAllCardsCategoryManacost(string category, int manacost)
        //{
        //    int result;

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("if(@manacost < 7) " +
        //                                                   "begin " +
        //                                                   "if(@manacost = -1) " +
        //                                                   "begin " +
        //                                                   "Select count(img) " +
        //                                                   "From cards " +
        //                                                   "Where category = @category " +
        //                                                   "end " +
        //                                                   "Select Count(img) " +
        //                                                   "From cards " +
        //                                                   "Where category = @category AND manacost = @manacost " +
        //                                                   "end " +
        //                                                   "else " +
        //                                                   "begin " +
        //                                                   "select Count(img) " +
        //                                                   "From cards " +
        //                                                   "where category = @category AND manacost >= 7 " +
        //                                                   "end", connection))
        //        {
        //            connection.Open();
        //            command.Parameters.AddWithValue("@manacost", manacost);
        //            command.Parameters.AddWithValue("@category", category);

        //            result = (int)command.ExecuteScalar();
        //        }
        //    }
        //    return result;
        //}

        public int AmountOfUserCardsCategoryManacost(int userId, string className, bool isEditible, int manacost)
        {
            int result;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(QueryBuilderAmount(userId, className, manacost, isEditible), connection))
                {
                    connection.Open();

                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }

        private string QueryBuilderCards(int userId, string className, int manacost, bool isEditible, int page, int pageSize)
        {
            StringBuilder mainQuery =
                new StringBuilder("Select c.id, c.name, c.rarity_id, c.img, c.manacost from cards c ");

            mainQuery = AddUserJoinFilter(mainQuery, isEditible);

            mainQuery = AddUserClassFilter(mainQuery, className, userId, isEditible);

            mainQuery = AddManaCostFilter(mainQuery, manacost);

            mainQuery.Append("Order By manacost, c.name " +
                             $"Offset {(page - 1) * pageSize} Rows Fetch next {pageSize} Rows only ");

            return mainQuery.ToString();
        }

        private string QueryBuilderAmount(int userId, string className, int manacost, bool isEditible)
        {
            StringBuilder mainQuery = new StringBuilder("Select Count(img) from cards c ");

            mainQuery = AddUserJoinFilter(mainQuery, isEditible);

            mainQuery = AddUserClassFilter(mainQuery, className, userId, isEditible);

            mainQuery = AddManaCostFilter(mainQuery, manacost);

            return mainQuery.ToString();
        }

        private StringBuilder AddUserJoinFilter(StringBuilder str, bool isEditible)
        {
            str.Append(isEditible ? "" : "Inner join UserCards u on c.id = u.cardId ");

            return str;
        }
        private StringBuilder AddUserClassFilter(StringBuilder str, string className, int userId, bool isEditible)
        {

            string classString;
            string myString;
            string userFilter = isEditible ? "" : $"Where u.userId = {userId} ";

            if (className != "All")
            {
                str.Append("inner join Class cl on cl.id = c.class_id ");

                classString = "cl.name = '" + className + "' ";

                myString = AppendToStringConditionaly(userFilter, classString);
                str.Append(myString);
            }
            else
            {
                classString = "";
                myString = AppendToStringConditionaly(userFilter, classString);
                str.Append(myString);
            }

            return str;
        }

        private string AppendToStringConditionaly(string str, string appendString)
        {
            if (str.Contains("Where"))
            {
                str += appendString == "" ? "" : "and " + appendString + " ";
            }
            else
            {
                str += appendString == "" ? "" : "Where " + appendString + " ";
            }

            return str;
        }

        private StringBuilder AppendToStringBuilderConditionaly(StringBuilder str, string appendString)
        {
            string appStr;
            if (str.ToString().Contains("Where"))
            {
                appStr = appendString == "" ? "" : "and " + appendString + " ";
            }
            else
            {
                appStr = appendString == "" ? "" : "Where " + appendString + " ";
            }

            return str.Append(appStr);
        }

        private StringBuilder AddManaCostFilter(StringBuilder str, int manacost)
        {
            if (manacost == -1)
            {
                str.Append("");
            }
            else if (manacost < 7)
            {
                str = AppendToStringBuilderConditionaly(str, "manacost = " + manacost + " ");
            }
            else
            {
                str = AppendToStringBuilderConditionaly(str, "manacost >= 7");
            }

            return str;
        }
    }
}