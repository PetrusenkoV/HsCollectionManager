using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public interface ICardRepository
    {
        List<Card> GetAllCards(int page, int pageSize);
        List<Card> GetUserCards(int userId, int page, int pageSize);
        List<Card> GetAllCardsCategory(string category, int page, int pageSize);
        List<Card> GetUserCardsCategory(int userId, string category, int page, int pageSize);
        List<Card> GetAllCardsManaCost(int manacost, int page, int pageSize);
        List<Card> GetUserCardsManaCost(int userId, int manacost, int page, int pageSize);
        List<Card> GetAllCardsCategoryManacost(string category, int manacost, int page, int pageSize);
        List<Card> GetUserCardsCategoryManacost(int userId, string category, int manacost, int page, int pageSize);
        int GetCardId(string cardImg);
        void InsertUserCard(int userId, int cardId);
        void RemoveUserCard(int userId, int cardId);
        int AmountOfAllCards();
        int AmountOfAllCardsCategory(string category);
        int AmountOfUserCardsCategory(int userId, string category);
        int AmountOfUserCards(int userId);
        int AmountOfAllCardsManaCost(int manacost);
        int AmountOfUserCardsManaCost(int userId, int manacost);
        int AmountOfAllCardsCategoryManacost(string category, int manacost);
        int AmountOfUserCardsCategoryManacost(int userId, string category, int manacost);
    }
}
