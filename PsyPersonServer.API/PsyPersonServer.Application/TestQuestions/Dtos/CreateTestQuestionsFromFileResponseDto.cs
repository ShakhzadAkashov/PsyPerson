using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Models.Tests
{
    public class CreateTestQuestionsFromFileResponseDto<T>
    {
        public List<T> List { get; set; }
    }
}
