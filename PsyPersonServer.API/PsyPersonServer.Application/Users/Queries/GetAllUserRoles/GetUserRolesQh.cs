using AutoMapper;
using MediatR;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Queries.GetAllUserRoles
{
    public class GetUserRolesQh : IRequestHandler<GetUserRolesQ, PagedResponse<RoleDto>>
    {
        public GetUserRolesQh(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public async Task<PagedResponse<RoleDto>> Handle(GetUserRolesQ request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetUserRoles(request.UserId);

            var total = roles.Count();

            if (request.Page > 0 && request.ItemPerPage > 0)
            {
                roles = roles.OrderByDescending(x => x.CreatedDate)
                    .Skip((request.Page - 1) * request.ItemPerPage)
                    .Take(request.ItemPerPage).ToList();
            }

            return new PagedResponse<RoleDto>(roles.Select(s => _mapper.Map<RoleDto>(s)), total);
        }
    }
}
