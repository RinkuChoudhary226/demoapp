using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }

        public ICollection<EmployeeDepartment> objempdept { get; set; } = new HashSet<EmployeeDepartment>();

        [NotMapped]
        public string[] DepartmentName { get; set; }
        [NotMapped]

        public List<SelectListItem> departmentlist { get; set; }    
    }
}
