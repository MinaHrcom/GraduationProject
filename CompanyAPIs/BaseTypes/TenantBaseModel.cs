using HRCom.Domain.BaseModels;

namespace HRCom.Domain.BaseTypes;

public abstract class TenantBaseModel<T> : Entity<T>, ITenantBaseModel
{
    public Guid? TenantId { get; set; }
}
