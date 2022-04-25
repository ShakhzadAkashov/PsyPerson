using AutoMapper;
using MediatR;
using PsyPersonServer.Application.EmailMessage.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.EmailMessage.Queries.GetEmailMessageSetting
{
    public class GetEmailMessageSettingQh : IRequestHandler<GetEmailMessageSettingQ, EmailMessageSettingDto>
    {
        public GetEmailMessageSettingQh(IEmailMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly IEmailMessageRepository _repository;
        private readonly IMapper _mapper;

        public async Task<EmailMessageSettingDto> Handle(GetEmailMessageSettingQ request, CancellationToken cancellationToken)
        {
            var emailMessageSetting = await _repository.GetSetting();
            return _mapper.Map<EmailMessageSettingDto>(emailMessageSetting);
        }
    }
}
