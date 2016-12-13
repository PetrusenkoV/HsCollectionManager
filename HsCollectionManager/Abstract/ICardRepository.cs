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
        List<Card> GetUserCardsCategoryManacost(int userId, string category, int manacost, bool isEditable, int page, int pageSize);
        int AmountOfUserCardsCategoryManacost(int userId, string category, bool isEditable, int manacost);
        void InsertUserCard(int userId, int cardId);
        void RemoveUserCard(int userId, int cardId);
    }
}
