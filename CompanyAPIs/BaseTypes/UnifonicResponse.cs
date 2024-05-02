namespace HRCom.Domain.BaseTypes
{
    public class UnifonicResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string MessageID { get; set; }
        public string Status { get; set; }
        public string NumberOfUnits { get; set; }
        public string Cost { get; set; }
        public string CurrencyCode { get; set; }
        public string Balance { get; set; }
        public string Recipient { get; set; }
        public string TimeCreated { get; set; }
    }
}


