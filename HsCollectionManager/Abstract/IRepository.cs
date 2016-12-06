using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public interface IRepository
    {
        List<Card> GetAllCards();
        int GetUserId(string userName);
        //change to void
        bool InsertUser(string userName);

        int GetCardId(string cardImg);

        void InsertUserCard(int userId, int cardId);

        void RemoveUserCard(int userId, int cardId);

        List<Card> GetUserCards(int userId);
        List<Card> GetUserCardsManaCost(int userId, int manacost);
    }
}
