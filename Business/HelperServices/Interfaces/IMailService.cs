using Business.DTOs.MailDtos;

namespace Business.HelperServices.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
