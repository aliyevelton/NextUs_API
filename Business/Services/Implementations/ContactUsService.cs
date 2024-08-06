using AutoMapper;
using Business.DTOs.ContactUsDtos;
using Business.DTOs.MailDtos;
using Business.Exceptions.ContactUsExceptions;
using Business.HelperServices;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Business.Services.Implementations;

public class ContactUsService : IContactUsService
{
    private readonly IRepository<ContactUs> _repository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public ContactUsService(IRepository<ContactUs> repository, IMapper mapper, IConfiguration configuration)
    {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task AddAsync(ContactUsDto contactUsDto)
    {
        if (string.IsNullOrEmpty(contactUsDto.Name))
            throw new ContactUsNotNullException("Name cannot be null or empty.");
        if (string.IsNullOrEmpty(contactUsDto.Surname))
            throw new ContactUsNotNullException("Surname cannot be null or empty.");
        if (string.IsNullOrEmpty(contactUsDto.Email))
            throw new ContactUsNotNullException("Email cannot be null or empty.");
        if (string.IsNullOrEmpty(contactUsDto.Message))
            throw new ContactUsNotNullException("Message cannot be null or empty.");

        var contactUs = _mapper.Map<ContactUs>(contactUsDto);
        await _repository.AddAsync(contactUs);

        if (!IsValidEmail(contactUsDto.Email))
            throw new InvalidEmailException("The provided email address is not valid.");

        string body = $"Hello {contactUs.Name} {contactUs.Surname},<br/><br/>" +
                      "Thank you for contacting us. We will get back to you as soon as possible.<br/><br/>" +
                      "Best regards,<br/>" +
                      "NextUs <br/><br/>" +
                      "Please do not reply to this email.";
        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest { ToEmail = contactUs.Email, Subject = "NextUs Support", Body = body });

        string adminBody = $"Hello,<br/><br/>" +
                           $"You have a new message from {contactUs.Name} {contactUs.Surname}.<br/><br/>" +
                           $"Email: {contactUs.Email}<br/><br/>" +
                           $"Message: {contactUs.Message}<br/><br/>" +
                           $"Best regards,<br/><br/>" +
                           "NextUs <br/><br/>" +
                           "Please do not reply to this email.";
        await emailHelper.SendEmailAsync(new MailRequest { ToEmail = _configuration["MailSettings:AdminMail"], Subject = "New Support Message", Body = adminBody });

        await _repository.SaveAsync();
    }

    public async Task<List<ContactUsDto>> GetAllAsync()
    {
        var contactUs = await _repository.GetAllAsync();

        var contactUsDto = _mapper.Map<List<ContactUsDto>>(contactUs);
        return contactUsDto;
    }

    public async Task<ContactUsDto> GetByIdAsync(int id)
    {
        var contactUs = await _repository.GetSingleAsync(c => c.Id == id);
        if (contactUs == null)
            throw new ContactUsNotFoundException($"Contact not found by id: {id}");

        var contactUsDto = _mapper.Map<ContactUsDto>(contactUs);
        return contactUsDto;
    }

    public async Task DeleteAsync(int id)
    {
        var contactUs = await _repository.GetSingleAsync(c => c.Id == id);
        if (contactUs == null)
            throw new ContactUsNotFoundException($"Contact not found by id: {id}");

        contactUs.IsDeleted = true;
        await _repository.SaveAsync();
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
