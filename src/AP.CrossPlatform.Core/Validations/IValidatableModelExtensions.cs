using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using AP.CrossPlatform.Collections;

namespace AP.CrossPlatform.Validations
{
    public static class IValidatableModelExtensions
    {
        public static void CheckModelState(this IValidatableModel validatable)
        {
            try
            {
                if (validatable.Errors == null)
                    validatable.Errors = new ObservableErrorCollection();

                //new ValidationContext()
                var context = new ValidationContext(validatable);
                var results = new List<ValidationResult>();
                var valid = Validator.TryValidateObject(validatable, context, results, true);
                validatable.Errors.ReplaceRange(from r in results
                                                where !string.IsNullOrWhiteSpace(r.ErrorMessage)
                                                select (ValidationError)r);

                validatable.IsValid = !validatable.Errors.Any();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown in CheckModelState");
                Console.WriteLine(ex);
            }
        }

        public static ValidationError FirstErrorOnProperty(this IValidatableModel validatable, string propertyName)
        {
            try
            {
                var context = new ValidationContext(validatable)
                {
                    MemberName = propertyName
                };

                var propertyInfo = validatable.GetType().GetRuntimeProperty(propertyName);
                var value = propertyInfo.GetValue(validatable);
                var validationResults = new List<ValidationResult>();
                var isValid = Validator.TryValidateProperty(value, context, validationResults);

                return validationResults.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown in FirstErrorOnProperty");
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}