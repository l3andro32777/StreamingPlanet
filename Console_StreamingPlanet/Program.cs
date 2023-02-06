// using SendGrid's C# Library - https://github.com/sendgrid/sendgrid-csharp
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using System.Numerics;
using System.Runtime.InteropServices;

string toEmail = "Insert an email to send";
string subject = "SendGrid Test";
string returnUrl = "https://test.com";
string message = "Please confirm your account by clicking <a href=\"" + returnUrl + "\">here</a>";

string sendGridApiKey = "";
if (string.IsNullOrEmpty(sendGridApiKey))
{
    throw new Exception("The 'SendGridApiKey' is not configured");
}

var client = new SendGridClient(sendGridApiKey);
var msg = new SendGridMessage()
{
    From = new EmailAddress("danielmestre@protonmail.com", "streamingplanet.com"),
    Subject = subject,
    PlainTextContent = message,
    HtmlContent = message
};
msg.AddTo(new EmailAddress(toEmail));

var response = await client.SendEmailAsync(msg);
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Email queued successfully");
}
else
{
    Console.WriteLine("Failed to send email");
    // Adding more information related to the failed email could be helpful in debugging failure,
    // but be careful about logging PII, as it increases the chance of leaking PII
}