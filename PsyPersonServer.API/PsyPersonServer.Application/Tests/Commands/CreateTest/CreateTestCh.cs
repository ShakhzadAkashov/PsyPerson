using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Commands.CreateTest
{
    public class CreateTestCh : IRequestHandler<CreateTestC, TestDto>
    {
        public CreateTestCh(ITestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ITestRepository _repository;
        private readonly IMapper _mapper;

        public async Task<TestDto> Handle(CreateTestC request, CancellationToken cancellationToken)
        {
            var test = await _repository.Create(request.Name, request.Description,request.ImgPath);

            return _mapper.Map<TestDto>(test);
        }
    }
}
