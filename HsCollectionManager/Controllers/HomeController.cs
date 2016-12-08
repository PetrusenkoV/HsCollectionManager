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
        private readonly IUserRepository _userRepository;
        private readonly ICardRepository _cardRepository;
        private readonly int _pageSize = 8;

        public HomeController(IUserRepository userRepository, ICardRepository cardRepository)
        {
            _userRepository = userRepository;
            _cardRepository = cardRepository;
        }
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(UserModel userModel)
        {
            int userId = _userRepository.GetUserId(userModel.UserName);
            userModel.UserId = userId;
            userModel.IsEditable = false;

            if (userId == -1)
            {
                return View("NoUsersExistWithThisName", userModel);
            }

            return ShowCards(userModel);
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

        //public ViewResult ShowCardsManaCost(UserModel userModel, int page = 1)
        //{
        //    if (userModel.IsEditable)
        //    {
        //        return ShowAllCardsManaCost(userModel, page);
        //    }
        //    else
        //    {
        //        return ShowUserCardsManaCost(userModel, page);
        //    }
        //}

        //NAMING!!!
        public ViewResult ShowAllCards(UserModel userModel, int page = 1)
        {
            int totalRows;
            IEnumerable<Card> allCards;

            if (userModel.Manacost == -1)
            {
                allCards = _cardRepository.GetAllCards(page, _pageSize);
                totalRows = _cardRepository.AmountOfCards();
            }
            else
            {
                if (userModel.Manacost < 7)
                {
                    allCards = _cardRepository.GetAllCardsManaCost(userModel.Manacost, page, _pageSize);
                    totalRows = _cardRepository.AmountOfAllCardsManaCost(userModel.Manacost);
                }
                else
                {
                    allCards = _cardRepository.GetAllCardsMoreThenSixManaCost(page, _pageSize);
                    totalRows = _cardRepository.AmountOfAllCardsMoreThenSixManaCost();
                }
            }
            var cards = BuildSelectCardsModel(allCards, userModel, totalRows, page);

            return View("AllCards", cards);
        }
        public ViewResult ShowUserCards(UserModel userModel, int page = 1)
        {
            int totalRows;
            IEnumerable<Card> userCards;

            if (userModel.Manacost == -1)
            {
                userCards = _cardRepository.GetUserCards(userModel.UserId, page, _pageSize);
                totalRows = _cardRepository.AmountOfUserCards(userModel.UserId);
            }
            else
            {
                if (userModel.Manacost < 7)
                {
                    userCards = _cardRepository.GetUserCardsManaCost(userModel.UserId, userModel.Manacost, page,
                        _pageSize);
                    totalRows = _cardRepository.AmountOfUserCardsManaCost(userModel.UserId, userModel.Manacost);
                }
                else
                {
                    userCards = _cardRepository.GetUserCardsMoreThenSixManaCost(userModel.UserId, page, _pageSize);
                    totalRows = _cardRepository.AmountOfUserCardsMoreThenSixManaCost(userModel.UserId);
                }
            }

            var cards = BuildSelectCardsModel(userCards, userModel, totalRows, page);

            return View("AllCards", cards);
        }

        //public ViewResult ShowAllCardsManaCost(UserModel userModel, int page = 1)
        //{
        //    IEnumerable<Card> cardsManaCost =
        //        _cardRepository.GetAllCardsManaCost(userModel.Manacost, page, _pageSize);

        //    int totalRows = _cardRepository.AmountOfAllCardsManaCost(userModel.Manacost);

        //    var cards = BuildSelectCardsModel(cardsManaCost, userModel, totalRows, page);

        //    return View("ManaCostCards", cards);
        //}
        //public ViewResult ShowUserCardsManaCost(UserModel userModel, int page = 1)
        //{
        //    IEnumerable<Card> cardsManaCost =
        //        _cardRepository.GetUserCardsManaCost(userModel.UserId, userModel.Manacost, page, _pageSize);

        //    int totalRows = _cardRepository.AmountOfUserCardsManaCost(userModel.UserId, userModel.Manacost);

        //    var cards = BuildSelectCardsModel(cardsManaCost, userModel, totalRows, page);

        //    return View("ManaCostCards", cards);
        //}

        //public ViewResult ShowUserCardsMoreThenSevenManaCost(UserModel userModel, int page = 1)
        //{
        //    IEnumerable<Card> cardsMoreThenSevenManaCost =
        //        _cardRepository.GetUserCardsMoreThenSixManaCost(userModel.UserId, page, _pageSize);

        //    int totalRows = _cardRepository.AmountOfUserCardsMoreThenSixManaCost(userModel.UserId);

        //    var cards = BuildSelectCardsModel(cardsMoreThenSevenManaCost, userModel, totalRows, page);

        //    return View("ShowUserCards", cards);
        //}

        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ViewResult SignUp(UserModel userModel)
        {
            if (_userRepository.InsertUser(userModel.UserName))
            {
                userModel.UserId = _userRepository.GetUserId(userModel.UserName);
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