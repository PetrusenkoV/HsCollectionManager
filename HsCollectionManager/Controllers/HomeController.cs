using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HsCollectionManager.Abstract;
using HsCollectionManager.Models;

namespace HsCollectionManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly int _pageSize = 8;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(UserModel userModel)
        {
            int userId = _repository.GetUserId(userModel.UserName);
            userModel.UserId = userId;

            if (userId == -1)
            {
                return View("NoUsersExistWithThisName", userModel);
            }

            return ShowUserCards(userModel);
        }
        

        public SelectCards BuildSelectCardsModel(IEnumerable<Card> cards, UserModel userModel, int page)
        {
            SelectCards resultCards = new SelectCards
            {
                Cards = cards
                        .OrderBy(x => x.ManaCost)
                        .ThenBy(x => x.Name)
                        .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),

                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = cards.Count()
                },

                UserName = userModel.UserName,
                UserId = userModel.UserId
            };

            return resultCards;
        }
        public ViewResult ShowAllCards(UserModel userModel, int page = 1)
        {
            IEnumerable<Card> allCards = _repository.GetAllCards();

            var cards = BuildSelectCardsModel(allCards, userModel, page);

            return View("AllCards", cards);
        }
        public ViewResult ShowUserCards(UserModel userModel, int page = 1)
        {
            IEnumerable<Card> userCards = _repository.GetUserCards(userModel.UserId);

            var cards = BuildSelectCardsModel(userCards, userModel, page);

            return View("ShowUserCards", cards);
        }

        public ViewResult ShowUserCardsManaCost(UserModel userModel, int manacost, int page = 1)
        {
            IEnumerable<Card> cardsManaCost =
                _repository.GetUserCards(userModel.UserId)
                                    .Where(x => x.ManaCost == manacost);

            var cards = BuildSelectCardsModel(cardsManaCost, userModel, page);

            return View("ShowUserCards", cards);
        }

        public ViewResult ShowUserCardsMoreThenSevenManaCost(UserModel userModel, int page = 1)
        {
            IEnumerable<Card> cardsMoreThenSevenManaCost =
                _repository.GetUserCards(userModel.UserId)
                                    .Where(x => x.ManaCost >= 7);

            var cards = BuildSelectCardsModel(cardsMoreThenSevenManaCost, userModel, page);

            return View("ShowUserCards", cards);
        }

        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ViewResult SignUp(UserModel userModel)
        {
            if (_repository.InsertUser(userModel.UserName))
            {
                userModel.UserId = _repository.GetUserId(userModel.UserName);
                return ShowAllCards(userModel);
            }
            else
            {
                return View("ThisUserExists");
            }
        }


        [HttpPost]
        public void InsertUserCard(UserAddsCards model)
        {
            int cardId = _repository.GetCardId(model.CardImg);
            int userId = model.UserId;

            _repository.InsertUserCard(userId, cardId);
        }

        public void RemoveFromCollection(UserAddsCards model)
        {
            int cardId = _repository.GetCardId(model.CardImg);
            int userId = model.UserId;

            _repository.RemoveUserCard(userId, cardId);
        }
    }
}