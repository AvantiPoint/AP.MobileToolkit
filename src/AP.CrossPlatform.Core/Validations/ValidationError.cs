using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AP.CrossPlatform.Validations
{
    public class ValidationError
    {
        public string Message { get; set; }

        public IEnumerable<string> MemberNames { get; set; }

        public static implicit operator string(ValidationError error) => error.Message;

        public static implicit operator ValidationError(ValidationResult result) =>
            new ValidationError()
            {
                MemberNames = result.MemberNames,
                Message = result.ErrorMessage
            };
    }
}