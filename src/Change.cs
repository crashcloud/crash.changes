namespace Crash.Changes
{
	/// <summary>Provides a reliable, reusable class for communication</summary>
	public sealed class Change : IChange, IEquatable<Change>
	{
		/// <summary>The time of creation</summary>
		public DateTime Stamp { get; set; }

		/// <summary>The Id		of the Change</summary>
		public Guid Id { get; set; }

		/// <summary>The originator of the Change</summary>
		public string? Owner { get; set; }

		/// <summary>Any related payload data</summary>
		public string? Payload { get; set; }

		/// <summary>The Payload Type</summary>
		public string Type { get; set; }

		/// <summary>The type of Change. See ChangeAction.</summary>
		public ChangeAction Action { get; set; }

		/// <summary>Tests for equality of two changes</summary>
		public bool Equals(Change? other)
		{
			return other?.GetHashCode() == GetHashCode();
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Owner, Action, Payload, Type);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return obj?.GetHashCode() == GetHashCode();
		}


		#region Constructors

		/// <summary>Empty Constructor</summary>
		public Change()
		{
			Id = Guid.NewGuid();
			Stamp = DateTime.UtcNow;
		}

		/// <summary>Creates a transmittable Change from an IChange</summary>
		public Change(IChange change)
		{
			Stamp = change.Stamp;
			Id = change.Id;
			Owner = change.Owner;
			Payload = change.Payload;
			Action = change.Action;
			Type = change.Type;
		}

		#endregion
	}
}
