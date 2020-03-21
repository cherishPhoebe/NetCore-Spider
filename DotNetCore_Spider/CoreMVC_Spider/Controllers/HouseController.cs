using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using ZY.Application.HouseApp;

namespace CoreMVC_Spider.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseAppService _houseAppService;
        private readonly ILogger<HouseController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public HouseController(IHouseAppService houseAppService, ILogger<HouseController> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _houseAppService = houseAppService;
            _logger = logger;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        // GET: House
        public async Task<ActionResult> Index()
        {
            var houseDtoList = await _houseAppService.GetHouseData();
            return View(houseDtoList);
        }

        // GET: House/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: House/Create
        public async Task<ActionResult> Create()
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: House/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: House/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: House/Edit/5
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

        // GET: House/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: House/Delete/5
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