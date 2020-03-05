using CoreMVC_Spider.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CoreMVC_Spider.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
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

        // POST: Account/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login([FromBody]LoginViewModel loginInfo)
        {
            try
            {
                // TODO: Add insert logic here
                if (loginInfo.UserName == "admin" && loginInfo.Password == "123456")
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "登录失败", loginInfo);
                return View();
            }
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
        [ValidateAntiForgeryToken]
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