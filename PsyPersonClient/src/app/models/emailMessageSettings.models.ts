export class EmailMessageSettingDto{
    id: string = '';
    hostName: string = '';
    senderAddress: string = '';
    senderPswd: string = '';
    messageDisplayName: string = '';
}

export class SendEmailMessageC{
    receiverMailAddress: string = '';
    emailMessage: string = '';
    receiverFullName: string = '';
    letterHeader: string = '';
    isHTML: boolean = false;
}