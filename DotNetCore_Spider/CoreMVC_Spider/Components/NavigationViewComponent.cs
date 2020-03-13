using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ZY.Application.MenuApp;
using ZY.Application.UserApp;

namespace CoreMVC_Spider.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IUserAppService _userAppService;
        public NavigationViewComponent(IMenuAppService menuAppService, IUserAppService userAppService)
        {
            _menuAppService = menuAppService;
            _userAppService = userAppService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session.GetString("CurrentUserId");
            var menus = _menuAppService.GetMenusByUser(Guid.Parse(userId));
            return View(menus);
        }
    }
}
