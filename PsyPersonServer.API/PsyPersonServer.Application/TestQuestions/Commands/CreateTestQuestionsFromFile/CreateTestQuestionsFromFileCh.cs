using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestionFromFile;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestionsFromFile
{
    public class CreateTestQuestionsFromFileCh : IRequestHandler<CreateTestQuestionsFromFileC, CreateTestQuestionsFromFileResponseDto<TestQuestionDto>>
    {
        public CreateTestQuestionsFromFileCh(IOptions<UploadTestQuestionsFromFileSettings> uploadTQFFSettings, ITestQuestionRepository repository, ILogger<CreateTestQuestionsFromFileCh> logger, IMapper mapper)
        {
            _uploadTQFFSettings = uploadTQFFSettings.Value;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        private readonly UploadTestQuestionsFromFileSettings _uploadTQFFSettings;
        private readonly ITestQuestionRepository _repository;
        private readonly ILogger<CreateTestQuestionsFromFileCh> _logger;
        private readonly IMapper _mapper;

        public async Task<CreateTestQuestionsFromFileResponseDto<TestQuestionDto>> Handle(CreateTestQuestionsFromFileC request, CancellationToken cancellationToken)
        {
            try
            {
                List<TestQuestion> lst = new List<TestQuestion>();

                XmlDocument xmlDoc = new XmlDocument();
                if (request.File.ContentType == "text/plain")
                {
                    using (var stream = request.File.OpenReadStream())
                    {
                        xmlDoc.Load(stream);
                    }
                }
                else if (request.File.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                    request.File.ContentType == "application/msword")
                {
                    using (var stream = request.File.OpenReadStream())
                    {
                        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(stream, false))
                        {
                            Body body = wordDoc.MainDocumentPart.Document.Body;
                            string c = body.InnerText;
                            byte[] byteArray = Encoding.UTF8.GetBytes(c);
                            MemoryStream stream1 = new MemoryStream(byteArray);
                            xmlDoc.Load(stream1);
                        }
                    }
                }

                XmlNodeList questionList = xmlDoc.GetElementsByTagName(_uploadTQFFSettings.Question);
                for (int i = 0; i < questionList.Count; i++)
                {
                    var question = new TestQuestion();
                    question.Answers = new List<TestQuestionAnswer>();

                    var b = questionList[i].SelectSingleNode(_uploadTQFFSettings.Name);
                    question.Name = b.InnerText.ToString();
                    question.QuestionType = TestQuestionTypeEnum.SimpleQuestion;
                    question.TestId = request.TestId;

                    for (int j = 0; j < questionList[i].ChildNodes.Count; j++)
                    {
                        var a = questionList[i].ChildNodes[j].Name;

                        if (a == _uploadTQFFSettings.AnswerRight)
                        {
                            var answer = new TestQuestionAnswer();
                            answer.Name = questionList[i].ChildNodes[j].InnerText;
                            answer.IsCorrect = true;
                            answer.IdForView = j;
                            question.Answers.Add(answer);
                        }
                        else if (a == _uploadTQFFSettings.Answer)
                        {
                            var answer = new TestQuestionAnswer();
                            answer.Name = questionList[i].ChildNodes[j].InnerText;
                            answer.IsCorrect = false;
                            answer.IdForView = j;
                            question.Answers.Add(answer);
                        }
                    }

                    lst.Add(question);
                }

                var resultList = new List<TestQuestion>();

                foreach (var i in lst)
                {
                    var res = await _repository.Create(i.Name, i.QuestionType, i.TestId, i.Answers);
                    resultList.Add(res);
                }

                return new CreateTestQuestionsFromFileResponseDto<TestQuestionDto> { List = resultList.Select(x => _mapper.Map<TestQuestionDto>(x)).ToList() };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create testQuestion trom file - {request.File.FileName} failed: {ex}", ex);
                throw new Exception($"Create testQuestion trom file failed {ex}", ex);
            }
        }
    }
}
