namespace OMG.Domain.Base;

public abstract class Event
{
    public virtual int Id { get; set; }

    public virtual DateTime DataCriacao { get; set; } = DateTime.Now;
}
