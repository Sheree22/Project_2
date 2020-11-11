using System.ComponentModel.DataAnnotations; 
namespace Commander.Models
{
    public class Command
    {
        [Key] //annotation for primary key column
        public int Id { get; set; }

        [Required] //use data annotations to ensure column is not null
        public int Age { get; set; }

        //[MaxLenght(3)] //set lenght of a string
    }
}