using Microsoft.EntityFrameworkCore;
using School.Models.validation;
using System.ComponentModel.DataAnnotations;
namespace hendi.Models.Entities
{
    public class CourseState
    {
        public int Id { get; set; }

        [UniqueCourseStateName]
        public string Name { get; set; }

        public ICollection<Course> ?Courses { get; set; }
    }

}
