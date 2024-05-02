namespace HRCom.Domain.BaseTypes
{
    public class HyperPayConfiguration
    {
        public string MadaEntityId { get; set; }
        public string EntityId { get; set; }
        public string AMEXEntityId { get; set; }
        public string STCEntityId { get; set; }
        public string AccessToken { get; set; }
        public string Currency { get; set; }
        public string BaseURL { get; set; }
        public string CheckoutURL { get; set; }
        public string RegistrationURL { get; set; }
        public string TokenizationURL { get; set; }
        public string Version { get; set; }
        public string TestMode { get; set; }
    }
}
