using MediatR;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.UserTests.Commands.CancelTestForUser
{
    public class CancelTestForUserCh : IRequestHandler<CancelTestForUserC, bool>
    {
        public CancelTestForUserCh(IUserTestRepository userTestRepository)
        {
            _userTestRepository = userTestRepository;        
        }

        private readonly IUserTestRepository _userTestRepository;
        public async Task<bool> Handle(CancelTestForUserC request, CancellationToken cancellationToken)
        {
            var userTest = await _userTestRepository.GetById(request.UserTestId);
            if(userTest != null)
            {
                userTest.IsActive = false;
                var result = await _userTestRepository.Update(userTest.Id, userTest.IsActive, userTest.IsTested, userTest.AssignedDate);
                return result;
            }

            return false;
        }
    }
}
