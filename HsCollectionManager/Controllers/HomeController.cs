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
        
        public ViewResult ShowAllCards(UserModel userModel, int page = 1)
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
                UserName = userModel.UserName,
                UserId = userModel.UserId
            };

            return View("AllCards", cards);
        }
        public ViewResult ShowUserCards(UserModel userModel, int page = 1)
        {
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetUserCards(userModel.UserId)
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name)
                    .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetUserCards(userModel.UserId).Count()
                },
                UserName = userModel.UserName,
                UserId = userModel.UserId
            };

            return View("ShowUserCards", cards);
        }

        public ViewResult ShowUserCardsManaCost(UserModel userModel, int manacost, int page = 1)
        {
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetUserCardsManaCost(userModel.UserId, manacost)
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name)
                    .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetUserCards(userModel.UserId).Count()
                },
                UserId = userModel.UserId,
                UserName = userModel.UserName
            };

            return View("ShowUserCards", cards);
        }

        public ViewResult ShowUserCardsMoreThenSevenManaCost(UserModel userModel, int page = 1)
        {
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetUserCardsMoreThenSevenManaCost(userModel.UserId)
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name)
                    .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetUserCards(userModel.UserId).Count()
                },
                UserId = userModel.UserId,
                UserName = userModel.UserName
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