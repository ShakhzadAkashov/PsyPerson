using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public TestTypeEnum TestType { get; set; }
    }
}
