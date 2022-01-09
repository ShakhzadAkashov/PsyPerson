using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQh : IRequestHandler<GetAllUsersQ, PagedResponse<UserDto>>
    {
        public GetAllUsersQh(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public async Task<PagedResponse<UserDto>> Handle(GetAllUsersQ request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(request.UserName))
            {
                users = users.Where(x => x.UserName.Contains(request.UserName));
            }

            var total = await users.CountAsync(cancellationToken);

            if (request.Page > 0 && request.ItemPerPage > 0)
            {
                users = users.OrderByDescending(x => x.DateBirthday)
                    .Skip((request.Page - 1) * request.ItemPerPage)
                    .Take(request.ItemPerPage);
            }
            
            return new PagedResponse<UserDto>(users.Select(s => _mapper.Map<UserDto>(s)),total);
        }
    }
}
