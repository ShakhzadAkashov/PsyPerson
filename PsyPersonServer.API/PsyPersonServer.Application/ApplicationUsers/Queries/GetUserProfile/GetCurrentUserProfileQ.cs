using MediatR;
using PsyPersonServer.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationUsers.Queries.GetUserProfile
{
    public class GetCurrentUserProfileQ : IRequest<UserDto>
    {
        public string UserId { get; set; }
    }
}
