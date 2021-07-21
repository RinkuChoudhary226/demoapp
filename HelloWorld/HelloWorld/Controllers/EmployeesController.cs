using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int  id,string name)
        {
            return View(await _context.employees.Include(x => x.objempdept).ThenInclude(x => x.department).ToListAsync());
        }

        // GET: Employees/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees.Include(x => x.objempdept).ThenInclude(x => x.department).FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var employee = new Employee();
            var department = _context.departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            //department.Insert(0, new SelectListItem { Text = "Select", Value = "0", Selected = true });
            employee.departmentlist = department;
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in employee.DepartmentName)
                {
                    int id = 0;
                    int.TryParse(item, out id);
                    employee.objempdept.Add(new EmployeeDepartment
                    {
                        DepartmentId = id
                    }); ;
                }
                _context.employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.employees.Include(x => x.objempdept).Where(x => x.Id == id).FirstOrDefault();
            var selecteddepts = employee.objempdept.Select(x => x.DepartmentId).ToList();
            var department = _context.departments.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selecteddepts.Contains(x.Id)
            }).ToList();
            employee.departmentlist = department;
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingempdepts = _context.employees.Include(x => x.objempdept).FirstOrDefault(x => x.Id == employee.Id);
                    foreach (var empdepts in existingempdepts.objempdept.ToList())
                        _context.Remove(empdepts);
                    _context.SaveChanges();
                    _context.ChangeTracker.Clear(); 

                    foreach (var item in employee.DepartmentName)
                    {
                        int id = 0;
                        int.TryParse(item, out id);
                        employee.objempdept.Add(new EmployeeDepartment
                        {
                            DepartmentId = id
                        });
                    }
                    _context.employees.Update(employee);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            employee.objempdept.Add(new EmployeeDepartment
            {
                DepartmentId = 2
            });
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
