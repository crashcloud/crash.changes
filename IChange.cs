public interface IChange
{

    public DateTime Stamp { get; }

    public Guid Id { get; }

    public string Owner { get; }

    public bool Temporary { get; }

    public string? LockedBy { get; }

    public string? Payload { get; }

}