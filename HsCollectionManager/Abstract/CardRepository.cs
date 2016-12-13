﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public class CardRepository : ICardRepository
    {
        private readonly string _connectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public List<Card> GetUserCardsCategoryManacost(int userId, string category, int manacost, bool isEditable, int page, int pageSize)
        {
            var result = new List<Card>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(QueryBuilderCards(userId, category, manacost, isEditable, page, pageSize), connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Card
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            ManaCost = (int)reader["manacost"],
                            Img = (string)reader["img"]
                        });
                    }
                }
            }
            return result;
        }

        public int AmountOfUserCardsCategoryManacost(int userId, string className, bool isEditable, int manacost)
        {
            int result;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(QueryBuilderAmount(userId, className, manacost, isEditable), connection))
                {
                    connection.Open();

                    result = (int)command.ExecuteScalar();
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


        private string QueryBuilderCards(int userId, string className, int manacost, bool isEditable, int page, int pageSize)
        {
            StringBuilder mainQuery =
                new StringBuilder("Select c.id, c.name, c.rarity_id, c.img, c.manacost from cards c ");

            mainQuery = AddUserJoinFilter(mainQuery, isEditable);

            mainQuery = AddUserClassFilter(mainQuery, className, userId, isEditable);

            mainQuery = AddManaCostFilter(mainQuery, manacost);

            mainQuery.Append("Order By manacost, c.name " +
                             $"Offset {(page - 1) * pageSize} Rows Fetch next {pageSize} Rows only ");

            return mainQuery.ToString();
        }

        private string QueryBuilderAmount(int userId, string className, int manacost, bool isEditable)
        {
            StringBuilder mainQuery = new StringBuilder("Select Count(img) from cards c ");

            mainQuery = AddUserJoinFilter(mainQuery, isEditable);

            mainQuery = AddUserClassFilter(mainQuery, className, userId, isEditable);

            mainQuery = AddManaCostFilter(mainQuery, manacost);

            return mainQuery.ToString();
        }

        private StringBuilder AddUserJoinFilter(StringBuilder str, bool isEditable)
        {
            str.Append(isEditable ? "" : "Inner join UserCards u on c.id = u.cardId ");

            return str;
        }
        private StringBuilder AddUserClassFilter(StringBuilder str, string className, int userId, bool isEditable)
        {

            string classString = isEditable ? "" : $"u.userId = {userId} ";
           
            if (className != "All")
            {
                str.Append("inner join Class cl on cl.id = c.class_id ");

                classString += classString == "" ? "" : "and ";
                classString += "cl.name = '" + className + "' ";
            }
            else
            {
                classString += "";
            }

            return AppendToStringBuilderConditionaly(str, classString);
        }

        private StringBuilder AddManaCostFilter(StringBuilder str, int manacost)
        {
            string manacostString;

            if (manacost == -1)
            {
                manacostString = "";
            }
            else if (manacost < 7)
            {
                manacostString = "manacost = " + manacost + " ";
            }
            else
            {
                manacostString = "manacost >= 7";
            }

            return AppendToStringBuilderConditionaly(str, manacostString);
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
    }
}