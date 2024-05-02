using HRCom.Domain.BaseModels;

namespace HRCom.Domain.BaseTypes;

public abstract class MetaDataBaseModel<T> : Entity<T>, IMetaDataBaseModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
