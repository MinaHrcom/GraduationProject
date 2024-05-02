namespace HRCom.Domain.BaseTypes
{
    public interface IMetaDataBaseModel
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        Guid? CreatedBy { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
