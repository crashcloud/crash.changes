public interface IChange
{

    public DateTime Stamp { get; }

    public Guid Id { get; }

    public string? Owner { get; }

    public string? Payload { get; }

    public int Action { get; set; }
}