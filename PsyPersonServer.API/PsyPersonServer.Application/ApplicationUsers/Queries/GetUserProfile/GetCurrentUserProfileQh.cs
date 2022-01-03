using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationUsers.Queries.GetUserProfile
{
    public class GetCurrentUserProfileQh : IRequestHandler<GetCurrentUserProfileQ, UserDto>
    {
        public GetCurrentUserProfileQh(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper; 

        public async Task<UserDto> Handle(GetCurrentUserProfileQ request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if(user != null)
                return _mapper.Map<UserDto>(user);

            return new UserDto();
        }
    }
}
