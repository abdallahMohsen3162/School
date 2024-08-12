using hendi.Models.Entities;
using School.Data;
using System.ComponentModel.DataAnnotations;
namespace School.Models.validation
{
    public class UniqueCourseNameAttribute: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var entity = context.Courses.SingleOrDefault(e => e.Name == value as string && e.Id != (validationContext.ObjectInstance as Course).Id);

            if (entity != null)
            {
                return new ValidationResult("Course name must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}
