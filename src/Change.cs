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
			if (other is null) return false;

			if (!Id.Equals(other.Id)) return false;
			if (!Action.Equals(other.Action)) return false;
			if (!string.Equals(Type, other.Type, StringComparison.OrdinalIgnoreCase) != true) return false;
			if (!string.Equals(Owner, other.Owner, StringComparison.OrdinalIgnoreCase) != true) return false;
			if (!string.Equals(Payload, other.Payload, StringComparison.Ordinal) != true) return false;

			return true;
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
