using HelloWorld.API.Repository;
using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public IEnumerable<Department> GetDepartments()
        {
            return _departmentRepository.Get();
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public Department GetDepartmentById(int id)
        {
            return _departmentRepository.Get(id);
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public ActionResult<Department> CreateDepartment([FromBody] Department department)
        {
            var newdept = _departmentRepository.Create(department);
            return CreatedAtAction(nameof(GetDepartments), new { id = department.Id }, department);
        }

        [HttpPut]
        [Route("UpdateDepartment")]

        public ActionResult UpdateDepartment(int id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            _departmentRepository.Update(department);

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteDepartment")]

        public ActionResult Delete(int Id)
        {
            var depttodelete = _departmentRepository.Get(Id);
            if (depttodelete == null)
                return NotFound();

            _departmentRepository.Delete(depttodelete.Id);
            return NoContent();
        }
    }
}
