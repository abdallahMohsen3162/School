using School.Data;
using System.ComponentModel.DataAnnotations;

namespace School.Models.validation
{
    public class UniqueCourseStateNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var entity = context.CourseStates.SingleOrDefault(e => e.Name == value as string);

            if (entity != null)
            {
                return new ValidationResult("CourseState name must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}
