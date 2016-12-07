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
        List<Card> GetAllCardsManaCost(int manacost, int page, int pageSize);
        List<Card> GetUserCardsManaCost(int userId, int manacost, int page, int pageSize);
        List<Card> GetUserCardsMoreThenSevenManaCost(int userId, int page, int pageSize);
        int GetCardId(string cardImg);
        void InsertUserCard(int userId, int cardId);
        void RemoveUserCard(int userId, int cardId);
        int AmountOfCards();
        int AmountOfUserCards(int userId);
        int AmountOfAllCardsManaCost(int manacost);
        int AmountOfUserCardsManaCost(int userId, int manacost);
        int AmountOfUserCardsMoreThenSevenManaCost(int userId);
    }
}
