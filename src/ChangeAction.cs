namespace Crash.Changes
{
	/// <summary>The action of a <see cref="IChange" /></summary>
	[Flags]
	public enum ChangeAction
	{
		/// <summary>No Change</summary>
		None = 0,

		/// <summary>Add Change</summary>
		Add = 1 << 1,

		/// <summary>Remove Change</summary>
		Remove = 1 << 2,

		/// <summary>Misc. Update Change</summary>
		Update = 1 << 3,

		/// <summary>Transform Change</summary>
		Transform = 1 << 4,

		/// <summary>A Locking Change</summary>
		Locked = 1 << 5,

		/// <summary>An Unlocking Change</summary>
		Unlocked = 1 << 6,

		/// <summary>A Temporary Change</summary>
		Temporary = 1 << 7
	}
}
