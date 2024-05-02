namespace HRCom.Domain.BaseTypes
{
    public class UserEventCalendarResponse
    {
        public string Subject { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerEmail { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TimeZone { get; set; }
    }
}


