using HelloWorld.Data;
using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _dataContext;

        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Department Create(Department department)
        {
            _dataContext.departments.Add(department);
            _dataContext.SaveChanges();
            return department;
        }

        public void Delete(int id)
        {
            var itemtodelete = _dataContext.departments.Find(id);
            _dataContext.departments.Remove(itemtodelete);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Department> Get()
        {
            return _dataContext.departments.ToList();
        }

        public Department Get(int id)
        {
            return _dataContext.departments.Find(id);
        }

        public void Update(Department department)
        {
            _dataContext.departments.Update(department);
            _dataContext.SaveChanges();
        }
    }
}
