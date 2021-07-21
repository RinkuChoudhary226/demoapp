using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Controllers
{
    [Route("[controller]/[action]")]
    public class DepartmentController : Controller
    {
        private readonly DataContext _dataContext;
        public DepartmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [Route("~/department")]
        public IActionResult Index()
        {
            var department = _dataContext.departments.ToList();
                return View(department);
        }

        #region Add Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department objmodel)
        {
            _dataContext.Add(objmodel);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var department = _dataContext.departments.Where(x => x.Id == Id).FirstOrDefault();
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department objmodel)
        {
            _dataContext.Update(objmodel);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Details Department
        [Route("{id?}")]
        public IActionResult Details(int? Id)
        {
            var department = _dataContext.departments.Where(x => x.Id == Id).FirstOrDefault();
            if(department==null)
            {
                return View("Error",Id);
            }
            return View(department);
        }
        #endregion
        #region Deelte Department
        public IActionResult Delete(int Id)
        {
            var department = _dataContext.departments.Where(x => x.Id == Id).FirstOrDefault();
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(Department objmodel)
        {
            _dataContext.Remove(objmodel);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
