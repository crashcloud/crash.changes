public sealed class Change : IChange
{

    public DateTime Stamp { get; set; }

    public Guid Id { get; set; }

    public string Owner { get; set; }

    public bool Temporary { get; set; }

    public string? LockedBy { get; set; }

    public string? Payload { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Change() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Change(Guid id, string owner, string? payload)
    {
        Id = id;
        Owner = owner;
        Payload = payload;
        Stamp = DateTime.UtcNow;
    }

    public Change(IChange speck)
    {
        Stamp = speck.Stamp;
        Id = speck.Id;
        Owner = speck.Owner;
        Payload = speck.Payload;
        LockedBy = speck.Owner;
        Temporary = true;
    }

    public static Change CreateEmpty()
    {
        return new Change()
        {
            Id = Guid.NewGuid()
        };
    }

}