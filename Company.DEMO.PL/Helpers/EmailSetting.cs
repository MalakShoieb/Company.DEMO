using System.Net;
using System.Net.Mail;
using NuGet.Packaging.Signing;

namespace Company.DEMO.PL.Helpers
{
    public static class EmailSetting
    {
        public static bool EmailSettings(Email email)
        {///send el email
            //mailserver => Gmail,outlook
            //PROTOCOL=>SMTP
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("shoiebmalak549@gmail.com", "vpemxdslinpyxsoi");
                //vpemxdslinpyxsoi
                client.Send("shoiebmalak549@gmail.com", email.To, email.Subject, email.Body);

                return true;



            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
