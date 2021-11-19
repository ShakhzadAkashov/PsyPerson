using MediatR;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Commands.UpdateTest
{
    public class UpdateTestCh : IRequestHandler<UpdateTestC, bool>
    {
        public UpdateTestCh(ITestRepository repository)
        {
            _repository = repository;
        }

        private ITestRepository _repository;

        public Task<bool> Handle(UpdateTestC request, CancellationToken cancellationToken)
        {
            return _repository.Update(request.Id, request.Name, request.Description, request.ImgPath);
        }
    }
}
