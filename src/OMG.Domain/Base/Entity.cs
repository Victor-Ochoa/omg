using OMG.Domain.Base.Contract;
using System.Text.Json.Serialization;

namespace OMG.Domain.Base;

public abstract class Entity: ISoftDeletable
{
    public virtual int Id { get; set; }
    [JsonIgnore]
    public bool IsDeleted { get; set; }
    [JsonIgnore]
    public DateTime? DeletedAt { get; set; }
}
