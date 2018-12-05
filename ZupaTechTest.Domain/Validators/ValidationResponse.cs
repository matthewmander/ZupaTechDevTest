using System;
using System.Collections.Generic;
using System.Text;

namespace ZupaTechTest.Domain.Validators
{
    public class ValidationResponse
    {
        public ValidationResponse(bool success)
        {
            Success = success;
            ValidationErrors = new List<string>();
        }
        public ValidationResponse(bool success, IEnumerable<string> validationErrors)
        {
            Success = success;
            ValidationErrors = validationErrors;
        }
        public bool Success { get; private set; }
        public IEnumerable<string> ValidationErrors { get; private set; }
    }
}
