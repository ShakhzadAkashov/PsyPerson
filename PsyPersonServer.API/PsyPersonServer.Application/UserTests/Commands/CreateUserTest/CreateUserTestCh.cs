using AutoMapper;
using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.UserTests.Commands.CreateUserTest
{
    public class CreateUserTestCh : IRequestHandler<CreateUserTestC, UserTestDto>
    {
        public CreateUserTestCh(IMapper mapper, IUserTestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private readonly IMapper _mapper;
        private readonly IUserTestRepository _repository;

        public async Task<UserTestDto> Handle(CreateUserTestC request, CancellationToken cancellationToken)
        {
            var userTest = await _repository.Create(request.UserId, request.TestId);
            return _mapper.Map<UserTestDto>(userTest);
        }
    }
}
