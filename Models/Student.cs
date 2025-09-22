using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public int Age { get; set; }
    }
}
