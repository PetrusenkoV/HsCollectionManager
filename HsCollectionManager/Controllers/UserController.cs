using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HsCollectionManager.Abstract;
using HsCollectionManager.Models;

namespace HsCollectionManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel userModel)
        {
            int userId = _userRepository.GetUserId(userModel.UserName);
            userModel.UserId = userId;
            userModel.IsEditable = false;

            if (userId == -1)
            {
                return View("NoUsersExistWithThisName", userModel);
            }

            return RedirectToAction("ShowCards", "Card", userModel);
            
        }

        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserModel userModel)
        {
            if (_userRepository.InsertUser(userModel.UserName))
            {
                userModel.UserId = _userRepository.GetUserId(userModel.UserName);
                userModel.IsEditable = true;
                return RedirectToAction("ShowCards", "Card", userModel);
            }
            else
            {
                return View("ThisUserExists");
            }
        }
    }
}