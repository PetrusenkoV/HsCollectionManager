using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HsCollectionManager.Abstract;
using HsCollectionManager.Models;

namespace HsCollectionManager.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardRepository _cardRepository;
        private readonly int _pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

        public CardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        
        public ViewResult ShowCards(UserModel userModel, int page = 1)
        {
            IEnumerable<Card> allCards = _cardRepository.GetUserCardsCategoryManacost(userModel.UserId, userModel.Category,
                userModel.Manacost, userModel.IsEditable, page, _pageSize);
            var totalRows = _cardRepository.AmountOfUserCardsCategoryManacost(userModel.UserId, userModel.Category, userModel.IsEditable,
                userModel.Manacost);
           
            var cards = BuildSelectCardsModel(allCards, userModel, totalRows, page);

            return View("AllCards", cards);
        }
        
        private SelectCards BuildSelectCardsModel(IEnumerable<Card> cards, UserModel userModel, int amountOfCards, int page)
        {
            var resultCards = new SelectCards
            {
                Cards = cards
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name),

                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = amountOfCards
                },

                UserModel = userModel
            };

            return resultCards;
        }

        [HttpPost]
        public void InsertUserCard(int cardId, int userId)
        {
            _cardRepository.InsertUserCard(userId, cardId);
        }

        [HttpPost]
        public void RemoveFromCollection(int cardId, int userId)
        {
            _cardRepository.RemoveUserCard(userId, cardId);
        }
    }
}