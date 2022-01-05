using MediatR;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.UserTests.Commands.ReAssignTestForUser
{
    public class ReAssignTestForUserCh : IRequestHandler<ReAssignTestForUserC, bool>
    {
        public ReAssignTestForUserCh(IUserTestRepository userTestRepository)
        {
            _userTestRepository = userTestRepository;
        }

        private readonly IUserTestRepository _userTestRepository;

        public async Task<bool> Handle(ReAssignTestForUserC request, CancellationToken cancellationToken)
        {
            var userTest = await _userTestRepository.GetById(request.UserTestId);

            if (userTest != null)
            {
                userTest.AssignedDate = DateTime.Now;
                userTest.IsActive = true;
                userTest.IsTested = false;

                var result = await _userTestRepository.Update(userTest.Id, userTest.IsActive, userTest.IsTested, userTest.AssignedDate);
                return result;
            }

            return false;
        }
    }
}
