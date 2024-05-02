namespace HRCom.Domain.BaseTypes
{
    public class EmailFormatDTO
    {
        public string Subject { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string SubTitle { get; private set; }
        public UserCredentialsEmailFormatDTO UserCredentials { get; private set; }
        public CalendarEmailFormatDTO Calendar { get; private set; }
        public RedirectLinkEmailFormatDTO RedirectLink { get; private set; }
        public List<EmailAttachmentDTO> Files { get; private set; } = new List<EmailAttachmentDTO>();
        public CompanyInfoDTO CompanyInfo { get; private set; }

        public static EmailFormatDTO Template(string subject, string title, string body,
            string subTitle = null)
        => new()
        {
            Subject = subject,
            Title = title,
            Body = body,
            SubTitle = subTitle ?? string.Empty
        };

        public EmailFormatDTO WithLink(string buttonText, string redirectLink)
        {
            RedirectLink = new()
            {
                ButtonText = buttonText,
                RedirectLink = redirectLink
            };

            return this;
        }

        public EmailFormatDTO WithCalendar(string googleCalendar, string outlookCalendar)
        {
            Calendar = new CalendarEmailFormatDTO
            {
                GoogleCalendar = googleCalendar,
                OutlookCalendar = outlookCalendar
            };

            return this;
        }

        public EmailFormatDTO WithFile(string fileName, byte[] file)
        {
            Files.Add(new() { FileName = fileName, File = file });
            return this;
        }

        public EmailFormatDTO WithUserCredentials(string companyName, string username, string password, string portalLink)
        {
            UserCredentials = UserCredentialsEmailFormatDTO.Create(companyName, username, password, portalLink);

            return this;
        }

        public EmailFormatDTO WithCompanyInfo(string companyName, string backColor)
        {
            CompanyInfo = new(companyName, backColor);

            return this;
        }
    }

    public class RedirectLinkEmailFormatDTO
    {
        public string ButtonText { get; set; }
        public string RedirectLink { get; set; }
    }

    public class UserCredentialsEmailFormatDTO
    {
        public string CompanyName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string PortalLink { get; private set; }

        public static UserCredentialsEmailFormatDTO Create(string companyName, string username, string password, string portalLink)
        => new()
        {
            CompanyName = companyName,
            Username = username,
            Password = password,
            PortalLink = portalLink
        };
    }

    public class CalendarEmailFormatDTO
    {
        public string GoogleCalendar { get; set; }
        public string OutlookCalendar { get; set; }
    }

    public class EmailAttachmentDTO
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }


    public record CompanyInfoDTO(string Name, string BackColor);
}
