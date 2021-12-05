using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Dtos
{
    public class TestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public TestTypeEnum TestType { get; set; }
        public int AmountTestQuestions { get; set; }
    }
}
