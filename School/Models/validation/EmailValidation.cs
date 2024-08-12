using School.Data;
using System.ComponentModel.DataAnnotations;

namespace School.Models.validation
{
    public class UniqueEmailForCreate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                var dbContext = validationContext.GetService<ApplicationDbContext>();
                if (dbContext != null)
                {
                    var instance = validationContext.ObjectInstance as Student;
                    if (instance != null)
                    {
                        var existingGuest = dbContext.Students.Any(g => g.Email == email && g.Id != instance.Id);
                        if (existingGuest)
                        {
                            return new ValidationResult("Email already exists");
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
