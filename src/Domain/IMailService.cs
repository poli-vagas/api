namespace PoliVagas.Core.Domain;

public interface IMailService
{
    Task SendEmail(string subject, string to, string body);
}
