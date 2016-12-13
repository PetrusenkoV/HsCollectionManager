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
            UserIdModel userIdModel = _userRepository.GetUserId(userModel.UserName);

            if (userIdModel.IsNull)
            {
                return View("NoUsersExistWithThisName", userModel);
            }

            userModel.UserId = userIdModel.UserId;

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
            try
            {
                _userRepository.InsertUser(userModel.UserName);
                UserIdModel userIdModel = _userRepository.GetUserId(userModel.UserName);

                userModel.UserId = userIdModel.UserId;

                userModel.IsEditable = true;

                return RedirectToAction("ShowCards", "Card", userModel);
            }
            catch
            {
                return View("ThisUserExists");
            }
        }
    }
}