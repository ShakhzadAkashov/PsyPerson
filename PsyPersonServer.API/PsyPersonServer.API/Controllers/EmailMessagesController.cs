using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.EmailMessage.Commands.CreateEmailMessageSetting;
using PsyPersonServer.Application.EmailMessage.Commands.RemoveEmailMessageSetting;
using PsyPersonServer.Application.EmailMessage.Commands.UpdateEmailMessageSetting;
using PsyPersonServer.Application.EmailMessage.Queries.GetEmailMessageSetting;
using PsyPersonServer.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailMessagesController : ControllerBase
    {
        public EmailMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize(Permissions.EmailMessageSetting_View)]
        [HttpGet]
        [Route("GetSetting")]
        //Get : /api/EmailMessageSettings/GetSetting
        public async Task<IActionResult> GetSetting()
        {
            return Ok(await _mediator.Send(new GetEmailMessageSettingQ()));
        }

        [Authorize(Permissions.EmailMessageSetting_Create)]
        [HttpPost]
        [Route("CreateSetting")]
        //POST : /api/EmailMessageSetting/CreateSetting
        public async Task<IActionResult> CreateSetting([FromBody] CreateEmailMessageSettingC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.EmailMessageSetting_Edit)]
        [HttpPut]
        [Route("UpdateSetting")]
        //PUT : /api/EmailMessageSetting/UpdateSetting
        public async Task<IActionResult> UpdateSetting([FromBody] UpdateEmailMessageSettingC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.EmailMessageSetting_Delete)]
        [HttpDelete]
        [Route("RemoveSetting")]
        //Delete : /api/EmailMessageSetting/RemoveSetting
        public async Task<IActionResult> RemoveSetting([FromBody] RemoveEmailMessageSettingC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
