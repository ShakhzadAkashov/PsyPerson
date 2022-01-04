using MediatR;
using Microsoft.AspNetCore.Identity;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.BlockAndUnBlockUser
{
    public class BlockAndUnBlockUserCh : IRequestHandler<BlockAndUnBlockUserC, BlockAndUnBlockUserResponseDto>
    {
        public BlockAndUnBlockUserCh(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private readonly UserManager<ApplicationUser> _userManager;

        public async Task<BlockAndUnBlockUserResponseDto> Handle(BlockAndUnBlockUserC request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user != null)
            {
                if (user.IsBlocked != null && user.IsBlocked == true)
                {
                    user.IsBlocked = false;
                    await _userManager.UpdateAsync(user);

                    return new BlockAndUnBlockUserResponseDto
                    {
                        Result = true,
                        IsBlocked = false
                    };
                }
                else
                {
                    user.IsBlocked = true;
                    await _userManager.UpdateAsync(user);

                    return new BlockAndUnBlockUserResponseDto
                    {
                        Result = true,
                        IsBlocked = true
                    };
                }
            }

            return new BlockAndUnBlockUserResponseDto
            {
                Result = false
            };
        }
    }
}
