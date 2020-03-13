using CoreMVC_Spider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ZY.Application.DepartmentApp;
using ZY.Application.DepartmentApp.Dtos;

namespace CoreMVC_Spider.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentAppService _service;
        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(IDepartmentAppService service, ILogger<DepartmentController> logger)
        {
            _logger = logger;
            _service = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTreeData()
        {
            var dtos = _service.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var dto in dtos)
            {
                treeModels.Add(new TreeModel() { Id = dto.Id.ToString(), Text = dto.Name, Parent = dto.ParentId == Guid.Empty ? "#" : dto.ParentId.ToString() });
            }
            return Json(treeModels);
        }
        /// <summary>
        /// 获取子级列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetChildrenByParent(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _service.GetChildrenByParent(parentId, startPage, pageSize, out rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result,
            });
        }
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult Edit(DepartmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ""// GetModelStateError()
                });
            }
            if (_service.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Faild" });
        }

        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }
                _service.DeleteBatch(delIds);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }
        public IActionResult Get(Guid id)
        {
            var dto = _service.Get(id);
            return Json(dto);
        }
    }
}