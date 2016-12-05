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

            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetUserCards(userId)
                    .OrderBy(x => x.ManaCost)
                    .ThenBy(x => x.Name)
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
            _repository.InsertUser(userModel.Name);
            ViewBag.Name = userModel.Name;
            SelectCards cards = new SelectCards
            {
                Cards = _repository.GetAllCards()
                .OrderBy(x => x.ManaCost)
                .ThenBy(x => x.Name)
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