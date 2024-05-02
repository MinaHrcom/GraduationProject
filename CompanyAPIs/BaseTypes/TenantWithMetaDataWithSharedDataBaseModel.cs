using HRCom.Domain.BaseModels;

namespace HRCom.Domain.BaseTypes;

public abstract class TenantWithMetaDataWithSharedDataBaseModel<T> : Entity<T>, ITenantWithMetaDataWithSharedDataBaseModel
{
    public Guid? TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsSharedData { get; set; }
}