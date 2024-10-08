﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace CodeSparks.Temp
{
    public class InMemoryEmailSender : IEmailSender
    {
        private readonly List<EmailMessage> _emails = new List<EmailMessage>();

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Simulate email sending (logging, storing in memory)
            _emails.Add(new EmailMessage { Email = email, Subject = subject, HtmlMessage = htmlMessage });
            return Task.CompletedTask;
        }
    }

    public class EmailMessage
    {
        public required string Email { get; set; }
        public required string Subject { get; set; }
        public required string HtmlMessage { get; set; }
    }
}
