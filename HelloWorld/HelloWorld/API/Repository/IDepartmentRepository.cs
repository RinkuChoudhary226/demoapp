using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.API.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> Get();

        Department Get(int id);

        Department Create(Department Department);

        void Update(Department Department);

        void Delete(int id);
    }
}
