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

        public CardController(IUserRepository userRepository, ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public ViewResult ShowCards(UserModel userModel, int page = 1)
        {
            if (userModel.IsEditable)
            {
                return ShowAllCards(userModel, page);
            }
            else
            {
                return ShowUserCards(userModel, page);
            }
        }

        public ViewResult ShowAllCards(UserModel userModel, int page = 1)
        {
            int totalRows;
            IEnumerable<Card> allCards;
            
            if (userModel.Category == "All")
            {
                allCards = _cardRepository.GetUserCardsCategoryManacost(userModel.UserId, userModel.Category,
                    userModel.Manacost, userModel.IsEditable, page, _pageSize);
                totalRows = _cardRepository.AmountOfUserCardsCategoryManacost(userModel.UserId, userModel.Category, userModel.IsEditable,
                    userModel.Manacost);
            }
            else
            {
                allCards = _cardRepository.GetUserCardsCategoryManacost(userModel.UserId, userModel.Category,
                    userModel.Manacost, userModel.IsEditable, page, _pageSize);
                totalRows = _cardRepository.AmountOfUserCardsCategoryManacost(userModel.UserId, userModel.Category, userModel.IsEditable,
                    userModel.Manacost);
            }
            
            var cards = BuildSelectCardsModel(allCards, userModel, totalRows, page);

            return View("AllCards", cards);
        }

        public ViewResult ShowUserCards(UserModel userModel, int page = 1)
        {
            int totalRows;
            IEnumerable<Card> userCards;

            if (userModel.Category == "All")
            {
                userCards = _cardRepository.GetUserCardsCategoryManacost(userModel.UserId, userModel.Category,
                    userModel.Manacost, userModel.IsEditable, page, _pageSize);
                totalRows = _cardRepository.AmountOfUserCardsCategoryManacost(userModel.UserId, userModel.Category, userModel.IsEditable,
                    userModel.Manacost);
            }
            else
            {
                userCards = _cardRepository.GetUserCardsCategoryManacost(userModel.UserId, userModel.Category,
                    userModel.Manacost, userModel.IsEditable, page, _pageSize);
                totalRows = _cardRepository.AmountOfUserCardsCategoryManacost(userModel.UserId, userModel.Category, userModel.IsEditable,
                    userModel.Manacost);
            }

            var cards = BuildSelectCardsModel(userCards, userModel, totalRows, page);

            return View("AllCards", cards);
        }

        public SelectCards BuildSelectCardsModel(IEnumerable<Card> cards, UserModel userModel, int amountOfCards, int page)
        {
            SelectCards resultCards = new SelectCards
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
        public void InsertUserCard(UserAddsCards model)
        {
            int cardId = _cardRepository.GetCardId(model.CardImg);
            int userId = model.UserId;

            _cardRepository.InsertUserCard(userId, cardId);
        }

        public void RemoveFromCollection(UserAddsCards model)
        {
            int cardId = _cardRepository.GetCardId(model.CardImg);
            int userId = model.UserId;

            _cardRepository.RemoveUserCard(userId, cardId);
        }
    }
}