using AutoMapper;
using MediatR;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Commands.UpdateTest
{
    public class UpdateTestCh : IRequestHandler<UpdateTestC, bool>
    {
        public UpdateTestCh(ITestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ITestRepository _repository;
        private readonly IMapper _mapper;

        public Task<bool> Handle(UpdateTestC request, CancellationToken cancellationToken)
        {
            var testResultList = request.TestResultList.Select(x => _mapper.Map<TestResult>(x)).ToList();
            return _repository.Update(request.Id, request.Name, request.Description, request.ImgPath, testResultList);
        }
    }
}
