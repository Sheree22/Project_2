using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShereeGreeffWebApp.Models
{
    public class NewEmpClass
    {
        [Key]
        public int Empid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int EmpNumber { get; set; }
    }
}
