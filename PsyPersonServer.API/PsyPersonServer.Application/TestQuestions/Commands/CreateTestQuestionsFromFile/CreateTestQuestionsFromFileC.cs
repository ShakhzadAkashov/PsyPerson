using MediatR;
using Microsoft.AspNetCore.Http;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestionFromFile
{
    public class CreateTestQuestionsFromFileC : IRequest<CreateTestQuestionsFromFileResponseDto<TestQuestionDto>>
    {
        public IFormFile File { get; set; }
        public Guid TestId { get; set; }
        public TestTypeEnum TestType { get; set; }
    }
}
