using AutoMapper;
using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Queries.GetTestingForCheck
{
    public class GetTestingForCheckQh : IRequestHandler<GetTestingForCheckQ, PagedResponse<UserTestingHistoryDto>>
    {
        public GetTestingForCheckQh(IUserTestingHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly IUserTestingHistoryRepository _repository;
        private readonly IMapper _mapper;

        public async Task<PagedResponse<UserTestingHistoryDto>> Handle(GetTestingForCheckQ request, CancellationToken cancellationToken)
        {
            var userTestingHistoryes = await _repository.GetUserTestingHistoryForCheck(request.Page, request.ItemPerPage, request.IsChecked);
            return new PagedResponse<UserTestingHistoryDto>(userTestingHistoryes.Data.Select(x => _mapper.Map<UserTestingHistoryDto>(x)), userTestingHistoryes.Total);
        }
    }
}
