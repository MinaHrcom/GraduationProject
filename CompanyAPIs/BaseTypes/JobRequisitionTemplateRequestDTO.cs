using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRCom.Domain.BaseTypes
{
    public class JobRequisitionTemplateRequestDTO
    {
        public string JobId { get; set; }
        public string TemplateNameAr { get; set; }
        public string TemplateNameEn { get; set; }
        [JsonPropertyName("with_rate")]
        public bool WithRate { get; set; }
        [JsonPropertyName("job_requisition_with_no_rate")]
        public JobRequisitionWithNoRateVM JobRequisitionWithNoRate { get; set; }
        public JobRequisitionTemplateVM JobRequisitionTemplate { get; set; }

        [JsonPropertyName("is_update")]
        public bool IsPublished { get; set; }

        [JsonPropertyName("template_id")]
        public Guid? TemplateId { get; set; }
    }

    public class JobRequisitionWithNoRateVM
    {
        [JsonPropertyName("with_descriptive_file")]
        public bool WithDescriptiveFile { get; set; }
        [JsonPropertyName("description_en")]
        public string DescriptionEn { get; set; }
        [JsonPropertyName("description_ar")]
        public string DescriptionAr { get; set; }

        [JsonPropertyName("company_brief_en")]
        [MaxLength(10000, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        public string CompanyBriefEn { get; set; }

        [JsonPropertyName("company_brief_ar")]
        [MaxLength(10000, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        public string CompanyBriefAr { get; set; }

        [JsonPropertyName("file")]
        public string File { get; set; }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        [JsonPropertyName("file_extention")]
        public string FileExtention { get; set; }

        [JsonPropertyName("is_file_updated")]
        public bool IsFileUpdated { get; set; }

        [JsonPropertyName("from_setup")]
        public bool FromSetup { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("image_extention")]
        public string ImageExtention { get; set; }
    }

    public class JobRequisitionTemplateVM
    {
        public CompanyBreifVM CompanyBreif { get; set; }
        public JobSpecificationVM JobSpecification { get; set; }
        public JobDescriptionVM JobDescription { get; set; }
        public WorkingExperienceVM WorkingExperience { get; set; }
        public EducationVM Education { get; set; }
        public CompetenciesVM Competencies { get; set; }
        public SkillsVM Skills { get; set; }
    }

    public class CompanyBreifVM
    {
        public int ItemId { get; set; }

        [MaxLength(10000, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        [Display(Name = "VALUEEN")]
        public string ValueEn { get; set; }
        [MaxLength(10000, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        [Display(Name = "VALUEAR")]
        public string ValueAr { get; set; }
        public bool ShowInAd { get; set; }
    }

    #region JobSpecificationVM Model

    public class JobSpecificationVM
    {
        public int SectionId { get; set; }

        [Range(0, 100, ErrorMessage = "INVALIDWEIGHT")]
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        [Display(Name = "JOBSPECIFICATION")]
        public int Weight { get; set; }
        public JobSepecificationItemVM JobSepecificationItem { get; set; }
    }
    public class JobSepecificationItemVM
    {
        public ResidencyVM Residency { get; set; }
        public AgeVM Age { get; set; }
        public GenderVM Gender { get; set; }
        public MaritalStatusVM MaritalStatus { get; set; }
        public NationalitiesVM Nationalities { get; set; }
        public CareerLevelVM CareerLevel { get; set; }
        public LanguageVM Language { get; set; }

    }

    public class ResidencyVM : Item
    {
        public List<ItemValue> ResidencyValue { get; set; }
    }
    public class NationalitiesVM : Item
    {
        public ItemValue NationalitiesValue { get; set; }
    }

    public class AgeVM : Item
    {
        public AgeValue AgeValue { get; set; }
    }

    public class AgeValue : ItemValue
    {
        [Range(0, 100, ErrorMessage = "Invalid Age")]
        public int? MinAge { get; set; }
        [Range(0, 100, ErrorMessage = "Invalid Age")]
        public int? MaxAge { get; set; }
        [Range(0, 100, ErrorMessage = "Invalid Age")]
        public int? Age { get; set; }
    }

    public class GenderVM : Item
    {
        public ItemValue GenderValue { get; set; }
    }

    public class MaritalStatusVM : Item
    {
        public ItemValue MaritalStatusValue { get; set; }
    }

    public class CareerLevelVM : Item
    {
        public ItemValue CareerLevelValue { get; set; }
    }

    public class LanguageVM : Item
    {
        public List<LanguageValue> LanguageValue { get; set; }
    }

    public class LanguageValue : ItemValue
    {
        public int ProficiencyLevelId { get; set; }
        public string ProficiencyLevelNameAr { get; set; }
        public string ProficiencyLevelNameEn { get; set; }
    }

    #endregion

    public class JobDescriptionVM
    {
        public int ItemId { get; set; }
        [MaxLength(10000, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        [Display(Name = "JOBDESCRIPTIONNAMEEN")]
        public string NameEn { get; set; }
        [MaxLength(10000, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        [Display(Name = "JOBDESCRIPTIONNAMEAR")]
        public string NameAr { get; set; }
        //public int? ClosingDays { get; set; }
        public List<ContractTypeValue> ContractTypeValue { get; set; }
        public bool ShowInAd { get; set; }
    }

    public class ContractTypeValue
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public class WorkingExperienceVM : Item
    {
        public List<WorkingExperienceValue> WorkingExperienceValue { get; set; }
    }

    public class WorkingExperienceValue
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [Range(0, 100, ErrorMessage = "INVALIDWEIGHT")]
        public int? Weight { get; set; }
        public int Years { get; set; }
    }

    public class EducationVM : Item
    {
        public List<EducationValue> EducationValue { get; set; }
    }

    public class EducationValue
    {
        // public int? CollegeId { get; set; }
        public string CollegeNameAr { get; set; }
        public string CollegeNameEn { get; set; }
        [MaxLength(200, ErrorMessage = "{0}_EXCEED_MAX_LENGTH")]
        [Display(Name = "StudyField")]
        public string StudyField { get; set; }
        public int? DegreeId { get; set; }
        public string DegreeNameAr { get; set; }
        public string DegreeNameEn { get; set; }
        public decimal? GPA { get; set; }
        public int? OutOf { get; set; }
        public int? Weight { get; set; }
    }

    public class CompetenciesVM
    {
        public int ItemId { get; set; }
        public bool ShowInAd { get; set; }
        public List<CompetenciesValue> CompetenciesValue { get; set; }
    }
    public class CompetenciesValue
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public class SkillsVM
    {
        public int ItemId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool ShowInAd { get; set; }
    }

    #region Generic 
    public class Item
    {
        public int ItemId { get; set; }
        public bool IsPreferred { get; set; }
        [Range(0, 100, ErrorMessage = "INVALIDWEIGHT")]
        public int? Weight { get; set; }
        public bool? ShowInAd { get; set; }

    }

    public class ItemValue
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [Range(0, 100, ErrorMessage = "INVALIDWEIGHT")]
        public int? Weight { get; set; }
    }

    #endregion
}