using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationRoles.Queries.GetAllRoles
{
    public class GetAllRolesQh : IRequestHandler<GetAllRolesQ, PagedResponse<RoleDto>>
    {
        public GetAllRolesQh(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        public async Task<PagedResponse<RoleDto>> Handle(GetAllRolesQ request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.AsQueryable();

            var total = await roles.CountAsync(cancellationToken);

            if (request.Page > 0 && request.ItemPerPage > 0)
            { 
                roles = roles.OrderByDescending(x => x.CreatedDate)
                    .Skip((request.Page - 1) * request.ItemPerPage)
                    .Take(request.ItemPerPage);
            }

            return new PagedResponse<RoleDto>(roles.Select(x => _mapper.Map<RoleDto>(x)),total);
        }
    }
}
