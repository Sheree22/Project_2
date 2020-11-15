using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema; //include

namespace ShereeGreeffWebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ShereeGreeffWebAppUser class
    public class ShereeGreeffWebAppUser : IdentityUser
    {
        //properties 
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")] //datatype for column
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")] //datatype for column 
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "int")] //datatype for column 
        public int Age { get; set; }

        [PersonalData]
        [Column(TypeName = "int")] //datatype for column 
        public int Number { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(10)")] //datatype for column 
        public string Gender { get; set; }
    }
}
