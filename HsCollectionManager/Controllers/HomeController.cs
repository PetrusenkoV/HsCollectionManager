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
            int userId = _repository.GetUserId(userModel.Name);

            if (userId == -1)
            {
                return View("NoUsersExistWithThisName", userModel);
            }

            return ShowUserCards(userId, userModel.Name);
        }


        public ViewResult ShowUserCards(int userId, string userName, int page = 1)
        {
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetUserCards(userId)
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name)
                    .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetUserCards(userId).Count()
                },
                UserName = userName,
                UserId = userId
            };

            return View("ShowUserCards", cards);
        }

        public ViewResult ShowUserCardsManaCost(int userId, int manacost, int page = 1)
        {
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetUserCardsManaCost(userId, manacost)
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name)
                    .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetUserCards(userId).Count()
                },
                UserId = userId
            };

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
            if (_repository.InsertUser(userModel.Name))
            {
                return ShowAllCards(userModel.Name);
            }
            else
            {
                return View("ThisUserExists");
            }
        }

        public ViewResult ShowAllCards(string name, int page = 1)
        {
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetAllCards()
                        .OrderBy(x => x.ManaCost)
                        .ThenBy(x => x.Name)
                        .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetAllCards().Count()
                },
                UserName = name
            };

            return View("AllCards", cards);
        }

        [HttpPost]
        public void InsertUserCard(UserAddsCards model)
        {
            int cardId = _repository.GetCardId(model.CardImg);
            int userId = _repository.GetUserId(model.UserName);

            _repository.InsertUserCard(userId, cardId);
        }


        public void RemoveFromCollection(UserAddsCards model)
        {
            int cardId = _repository.GetCardId(model.CardImg);
            int userId = _repository.GetUserId(model.UserName);

            _repository.RemoveUserCard(userId, cardId);
        }
    }
}