namespace HRCom.Domain.BaseTypes;

public interface ITenantBaseModel
{
    Guid? TenantId { get; set; }
}
