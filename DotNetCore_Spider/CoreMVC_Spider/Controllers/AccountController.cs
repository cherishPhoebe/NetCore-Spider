using CoreMVC_Spider.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using ZY.Application.UserApp;
using ZY.Utility.Convert;

namespace CoreMVC_Spider.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserAppService userAppService, ILogger<AccountController> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userAppService.CheckUser(loginInfo.UserName, loginInfo.Password);
                    if (user != null)
                    {
                        //记录Session
                        HttpContext.Session.SetString("CurrentUserId", user.Id.ToString());
                        HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户名或密码错误。");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "登录失败", loginInfo);
                    return View();
                }
            }
            ViewBag.ErrorInfo = ModelState.Values.First().Errors[0].ErrorMessage;
            return View(loginInfo);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}