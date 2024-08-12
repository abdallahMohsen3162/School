using School.Models;
using School.Models.validation;
using System.ComponentModel.DataAnnotations;

namespace hendi.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [UniqueCourseName]

        public string Name { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public CourseState ?State { get; set; }
        public ICollection<CourseStudent>? CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
