using HRCom.Domain.BaseModels;

namespace HRCom.Domain.BaseTypes;

public abstract class TenantWithMetaDataBaseModel<T> : Entity<T>, ITenantWithMetaDataBaseModel
{
    public Guid? TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
