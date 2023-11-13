namespace Crash.Changes.Extensions
{
	/// <summary>Utilities to Query Change Action</summary>
	public static class Utils
	{
		/// <summary>Tests a ChangeAction for being Temporary</summary>
		public static bool IsTemporary(this IChange change)
		{
			return change.HasFlag(ChangeAction.Temporary);
		}

		/// <summary>Tests a ChangeAction for being Locked</summary>
		public static bool IsLocked(this IChange change)
		{
			return change.HasFlag(ChangeAction.Locked);
		}

		/// <summary>Shorthand to Check a Change for the Flag</summary>
		public static bool HasFlag<T>(this IChange change, T flag) where T : Enum
		{
			return change.Action.HasFlag(flag);
		}
	}
}
