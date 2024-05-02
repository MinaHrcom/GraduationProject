namespace HRCom.Domain.BaseTypes;

public class HRCompanySettings
{
    public System System { get; set; }
    public GhostUser GhostUser { get; set; }
    public SmtpConfiguration SmtpConfiguration { get; set; }
    public string AWS_BUCKET { get; set; }
    public string AWS_FILE_PATH { get; set; }
    public string AWS_REGION { get; set; }
    public string AWS_SECRET_ACCESS_KEY { get; set; }
    public string AWS_ACCESS_KEY_ID { get; set; }
    public Fixed_Pages Fixed_Pages { get; set; }
    public Vonage Vonage { get; set; }
    public string DefaultPrimaryColor { get; set; }
    public string DefaultSecondaryColor { get; set; }
    public string DefaultPortalSloganEn { get; set; }
    public string DefaultPortalSloganAr { get; set; }
    public string DefaultJobBannerImage { get; set; }

    public string DefaultCandidateHomeImage { get; set; }
    public MicrosoftCalendarConfiguration MicrosoftCalendarConfiguration { get; set; }
    public FirebaseSetting FirebaseSetting { get; set; }
    public GoogleEmailSyncConfiguration GoogleEmailSyncConfiguration { get; set; }
    public MicrosoftEmailSyncConfiguration MicrosoftEmailSyncConfiguration { get; set; }
    public OracleStorage Oracle_Storage { get; set; }
    public Excel Excel { get; set; }
}

public class Excel
{
    public string Password { get; set; }
    public string Key { get; set; }
}

public class OracleStorage
{
    public string Tenancy { get; set; }
    public string User { get; set; }
    public string Fingerprint { get; set; }
    public string Region { get; set; }
    public string Key_File_Path { get; set; }
    public string Bucket_name { get; set; }
    public string Bucket_Namespace { get; set; }
    public string Bucket_Url { get; set; }
}

public class GoogleEmailSyncConfiguration
{
    public string Client_Id { get; set; }
    public string Client_Secret { get; set; }
    public string Redirect_URL { get; set; }
}

public class MicrosoftEmailSyncConfiguration
{
    public string Client_Id { get; set; }
    public string Client_Secret { get; set; }
    public string Redirect_URL { get; set; }
}

public class Vonage
{
    public int Api_Key { get; set; }
    public string Api_Secret { get; set; }
}

public class Fixed_Pages
{
    public string Approval_Requests_Page { get; set; }
    public string MyInterviews_Page { get; set; }
    public string Interview_Page { get; set; }
    public string CompanyLogin { get; set; }
    public string Candidate_Attachment_Page { get; set; }
    public string Candidate_Profile_Page { get; set; }
    public string Candidate_More_Info_Page { get; set; }
    public string Applicant_Profile_Page { get; set; }
    public string Subscription_Plans_Page { get; set; }
    public string Candidate_Offer_Page { get; set; }
    public string General_Candidate_Portal_Job_Page { get; set; }
    public string View_All_Requests { get; set; }
    public string Jobseeker_Job_Post_Page { get; set; }
    public string Candidate_Portal_Job_Post_Page { get; set; }
    public string Company_Redirect_After_Email_Sync_Page { get; set; }
    public string Candidate_Contractor_Page { get; set; }
    public string Manage_Candidates { get; set; }
    public string Candidates_Approval_Requests_Page { get; set; }
    public string Company_Portal_JobSummary { get; set; }
}

public class System
{
    public string App_Base_Url { get; set; }
    public string Receiver_Email { get; set; }
    public int Access_Token_Life_Time { get; set; }
    public string Company_Admin_URL { get; set; }
    public string Admin_Stage_Url { get; set; }
    public string Elastic_Search_Url { get; set; }
    public int Page_Size { get; set; }
    public string Candidate_Portal_Url { get; set; }
    public string General_Candidate_Portal_Url { get; set; }
    public string Candidate_Middleware_Url { get; set; }
    public string Default_Country { get; set; }
    public string[] System_Admin_Email { get; set; }
    public int Trial_Days { get; set; }
    public int Default_Video_Minutes { get; set; }
    public int Default_Job_Closing_Days { get; set; }
    public string FinancialTeamEmail { get; set; }
    public int Image_Max_Size { get; set; }
    public int File_Max_Size { get; set; }
    public string[] Image_Allowed_Formates { get; set; }
    public int Follow_Up_Verification_Hours { get; set; }
    public int Follow_Up_Registeration_Days { get; set; }
    public string UnifonicBaseUrl { get; set; }
    public string GooglrCalendarUrl { get; set; }
    public string MicrosoftCalendarUrl { get; set; }
    public string Request_Demo_Email { get; set; }
    public string Customer_Success_Email { get; set; }
    public string[] Valid_Agent_Email_Domains { get; set; }
}

public class SmtpConfiguration
{
    public string Host { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int Port { get; set; } = 587; // default smtp port
    public bool UseSSL { get; set; } = true;
    public string Follow_Up_Host { get; set; }
    public string Follow_Up_Login { get; set; }
    public string Follow_Up_Password { get; set; }
    //public string From_Email { get; set; }
    //public string Follow_Up_Email { get; set; }
}

public class GhostUser
{
    public string UserName { get; set; }
    public string Password { get; set; }

}

public class MicrosoftCalendarConfiguration
{
    public string AppId { get; set; }
    public string AppSecret { get; set; }
    public string MicrosoftCalendarUrl { get; set; }
    public string MicrosoftCalendarRedirectUrl { get; set; }
}

public class FirebaseSetting
{
    public string type { get; set; }
    public string project_id { get; set; }
    public string private_key_id { get; set; }
    public string private_key { get; set; }
    public string client_email { get; set; }
    public string client_id { get; set; }
    public string auth_uri { get; set; }
    public string token_uri { get; set; }
    public string auth_provider_x509_cert_url { get; set; }
    public string client_x509_cert_url { get; set; }
}
