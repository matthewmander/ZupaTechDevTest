using System;
using System.Collections.Generic;
using System.Text;

namespace ZupaTechTest.Domain.Validators
{
    public class ValidationResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
