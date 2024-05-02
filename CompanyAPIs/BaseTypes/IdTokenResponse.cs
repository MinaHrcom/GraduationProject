namespace HRCom.Domain.BaseTypes
{
    public class IdTokenResponse
    {
        public string iss { get; set; }
        public string azp { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string at_hash { get; set; }
        public string hd { get; set; }
        public string email { get; set; }
        public string email_verified { get; set; }
        public int iat { get; set; }
        public int exp { get; set; }
        public string nonce { get; set; }
    }
}
